using Database.Models.Schema;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json
{
    [System.Diagnostics.DebuggerDisplay("Value={GetValue()} Created={Created} Enr={Enrichment} Msg={Message}")]
    public class EntityAttributeValue
    {
        
        /// Constructor for creating an EntityAttributeValue that comes from a Message.
        
        public EntityAttributeValue(AttributeType attributeType, Guid id, object? value, EntityAttributeStatus status,
            EntityAttributeMessageValue entityAttributeMessageValue)
            : this(attributeType, id, value, System.DateTime.UtcNow, status)
        {
            Message = entityAttributeMessageValue;
        }

        
        /// Constructor for creating an EntityAttributeValue that comes from an Enrichment.
        
        public EntityAttributeValue(AttributeType attributeType, Guid id, object? value,
            DateTime created, EntityAttributeStatus status, EntityAttributeEnrichmentValue entityAttributeEnrichmentValue)
            : this(attributeType, id, value, created, status)
        {
            Enrichment = entityAttributeEnrichmentValue;
        }

        private EntityAttributeValue(AttributeType attributeType, Guid id, object? value, DateTime created, EntityAttributeStatus status)
        {
            AttributeType = attributeType;
            Id = id;
            Created = created;
            ValueOrMetadataLastChanged = created;
            Status = status;
            SetValue(value);
        }

        
        /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
        /// in order to use <see cref="ReferenceHandler.Preserve"/>.
        
        [JsonConstructor]
        [Obsolete("This constructor is only intended for the System.Text.Json.JsonSerializer.")]
        public EntityAttributeValue() { }

        [JsonIgnore]
        public AttributeType AttributeType { get; internal set; } = null!;

        
        /// Unique identifier of the attribute.
        
        [JsonInclude]
        public Guid Id { get; internal set; } = System.Guid.NewGuid();

        
        /// When the attribute value was created.
        
        [JsonInclude]
        public DateTime Created { get; private set; }

        
        /// When the value or metadata (priority, source, etc.) of the attribute was last modified.
        /// This timestamp is transferred from a previous "equivalent" EntityAttributeValue (that is,
        /// with the same value and message or enrichment metadata).
        
        [JsonInclude]
        public DateTime ValueOrMetadataLastChanged { get; internal set; }

        
        /// Status of the attribute (Active/Deleted).
        
        /// <remarks>
        /// Note, that archived attributes are typically deleted when saving an EntityValue.
        /// </remarks>
        [JsonInclude]
        public EntityAttributeStatus Status { get; private set; } = EntityAttributeStatus.Active;

        
        /// Contains the previous status when explicitly set, otherwise null
        
        /// <remarks>
        /// PreviousStatus should store the value of <see cref="Status"/> at the time PreviousStatus was set.
        /// It is meant to be used to store the Status of this <see cref="EntityAttributeValue"/> 
        /// at the time it was stored in <see cref="EntityAttribute.PreviousValues"/>
        /// </remarks>
        [JsonIgnore]
        internal EntityAttributeStatus? PreviousStatus { get; private set; }

        
        /// If this value was assigned based on a raw message (system/adapter or Action/UI) then this contains the message related information.
        
        [JsonInclude]
        public EntityAttributeMessageValue? Message { get; private set; }

        
        /// Returns true if this attribute value was created by an Action (UI) message
        
        [JsonIgnore]
        public bool IsFromActionMessage => Message?.RawMessage?.ActionMessage != null;

        
        /// Returns true if this attribute is an enrichment that references (prioritizes) 
        /// an attribute value created by an Action (UI) message
        
        [JsonIgnore]
        public bool HasReferenceToEntityAttributeValueFromActionMessage
            => Enrichment?.AttributeReferences.Any(a => a.IsFromActionMessage) == true;

        
        /// If this value was assigned based on an enrichment, then this contains the enrichment related information.
        
        [JsonInclude]
        public EntityAttributeEnrichmentValue? Enrichment { get; internal set; }

        
        /// Used to store the value if the owning <see cref="AttributeType"/> is <see cref="Schema.AttributeType.Text"/>.
        
        [JsonInclude]
        public string? Text { get; private set; }

        
        /// Used to store the value if the owning <see cref="AttributeType"/> is <see cref="Schema.AttributeType.Integer"/>.
        
        [JsonInclude]
        public int? Integer { get; private set; }

        
        /// Used to store the value if the owning <see cref="AttributeType"/> is <see cref="Schema.AttributeType.Boolean"/>.
        
        [JsonInclude]
        public bool? Boolean { get; private set; }

        
        /// Used to store the value if the owning <see cref="AttributeType"/> is <see cref="Schema.AttributeType.Duration"/>.
        
        [JsonInclude]
        public TimeSpan? Duration { get; private set; }


        private DateTime? dateTime;
        
        /// Used to store the value if the owning <see cref="AttributeType"/> is <see cref="Schema.AttributeType.DateTime"/>.
        
        [JsonInclude]
        public DateTime? DateTime {
            get
            {
                return dateTime;
            }
            private set
            {
                dateTime = value.HasValue ? new DateTime(value.Value.Ticks, DateTimeKind.Utc) : null;
            }
        }

        
        /// Used to store the value if the owning <see cref="AttributeType"/> is <see cref="Schema.AttributeType.Dropdown"/>.
        
        [JsonInclude]
        public string? Dropdown { get; private set; }

        
        /// Used to store the value if the owning <see cref="AttributeType"/> is <see cref="Schema.AttributeType.Decimal"/>.
        
        [JsonInclude]
        public decimal? Decimal { get; private set; }

        
        /// Used to store the value if the owning <see cref="AttributeType"/> is <see cref="Schema.AttributeType.Guid"/>.
        
        [JsonInclude]
        public Guid? Guid { get; private set; }

        
        /// Used to store the value if the owning <see cref="AttributeType"/> is <see cref="ComplexAttributeType"/>.
        
        [JsonInclude]
        public string? Json { get; internal set; }

        [JsonInclude]
        public bool IsAbsent { get; internal set; }

        public object? GetValue()
        {
            return AttributeType switch
            {
                ComplexAttributeType _ => Json,
                ValueAttributeType valueAttributeType => valueAttributeType.ValueDataType switch
                {
                    DataType.Text => Text,
                    DataType.Integer => Integer,
                    DataType.Boolean => Boolean,
                    DataType.Duration => Duration,
                    DataType.DateTime => DateTime,
                    DataType.Date => DateTime,
                    DataType.Dropdown => Dropdown,
                    DataType.Decimal => Decimal,
                    DataType.TextArea => Text,
                    DataType.Guid => Guid,
                    _ => throw new System.NotSupportedException($"The DataType '{valueAttributeType.ValueDataType}' is not supported.")
                },
                _ => throw new System.NotSupportedException($"The AttributeType '{AttributeType.GetType()}' is not supported.")
            };
        }

        
        /// Compares the <see cref="GetValue"/> og this object with the other - taking into account that if
        /// one of the objects is not null, the <see cref="Object.Equals(object?)"/> has to be used for correct equality testing.
        
        /// <param name="other"></param>
        /// <returns></returns>
        public bool ValueEquals(EntityAttributeValue? other)
        {
            if (other == null)
            {
                return false;
            }
            object? myValue = GetValue();
            object? otherValue = other.GetValue();
            return (myValue == otherValue || myValue != null && myValue.Equals(otherValue));
        }

        public DateTime GetReceived()
        {
            var initialEav = Enrichment?.GetFirstLeafAttributeReference() ?? this;

            if (initialEav.Enrichment != null)
            {
                return initialEav.ValueOrMetadataLastChanged;
            }
            if (initialEav.Message?.RawMessage != null)
            {
                return initialEav.Message.RawMessage.Received;
            }

            return initialEav.Created;
        }

        private void SetValue(object? value)
        {
            switch (AttributeType)
            {
                case ComplexAttributeType _:
                    // NewtonSoft is used as MS .Net library cannot handle the generic polymorphic type hierarchy
                    Json = (value == null) ? null : (value as string) ?? Newtonsoft.Json.JsonConvert.SerializeObject(value);
                    return;
                case ValueAttributeType valueAttributeType:
                    switch (valueAttributeType.ValueDataType) {
                        case DataType.Text:
                        case DataType.TextArea:
                            Text = (string?)value;
                            return;
                        case DataType.Integer:
                            Integer = (int?)value;
                            return;
                        case DataType.Boolean:
                            Boolean = (bool?)value;
                            return;
                        case DataType.Duration:
                            Duration = (TimeSpan?)value;
                            return;
                        case DataType.DateTime:
                            DateTime = (DateTime?)value;
                            return;
                        case DataType.Date:
                            DateTime = (DateTime?)value;
                            if (DateTime.HasValue)
                            {
                                DateTime = DateTime.Value.Date;
                            }
                            return;
                        case DataType.Dropdown:
                            Dropdown = (string?)value;
                            return;
                        case DataType.Decimal:
                            Decimal = (decimal?)value;
                            return;
                        case DataType.Guid:
                            Guid = (Guid?)value;
                            return;
                        default:
                            throw new NotSupportedException($"The DataType '{valueAttributeType.ValueDataType}' is not supported.");
                    }
                default:
                    throw new NotSupportedException($"The AttributeType '{AttributeType.GetType()}' is not supported.");
            }
        }

        internal void Revert(Guid actionMsgIdOfRevertAction)
        {
            switch (Message!.ActionEffect)
            {
                case ActionEffectStatus.Added:
                    Status = EntityAttributeStatus.Deleted;
                    Message.ActionEffect = ActionEffectStatus.Deleted;
                    Message.AffectedByRawMsgId = actionMsgIdOfRevertAction;
                    ValueOrMetadataLastChanged = System.DateTime.UtcNow;
                    break;

                case ActionEffectStatus.Deleted:
                    Status = EntityAttributeStatus.Active;
                    Message.ActionEffect = ActionEffectStatus.Added;
                    Message.AffectedByRawMsgId = actionMsgIdOfRevertAction;
                    ValueOrMetadataLastChanged = System.DateTime.UtcNow;
                    break;
            }
        }

        
        /// Deletes this value as an effect of the given message.
        
        /// <param name="message"></param>
        internal EntityAttributeValue Delete(Data.RawMessage message)
        {
            // Make a shallow copy as we need to distinguish the Status field between
            // the previous EAV and this now deleted EAV. Otherwise, the previous value would
            // be the exact same reference as this and have its status updated as well.
            var deletedEav = (EntityAttributeValue)this.MemberwiseClone();
            deletedEav.Status = EntityAttributeStatus.Deleted;
            deletedEav.ValueOrMetadataLastChanged = System.DateTime.UtcNow;

            if (message is ActionMessage)
            {
                deletedEav.Message!.ActionEffect = ActionEffectStatus.Deleted;
                deletedEav.Message.AffectedByRawMsgId = message.Id;
            }

            return deletedEav;
        }

        
        /// Snapshots value of <see cref="Status"/> and stores it in <see cref="PreviousStatus"/>
        
        /// <returns>The <see cref="EntityAttributeValue"/> on which the method was invoked </returns>
        internal EntityAttributeValue SnapshotStatus()
        {
            PreviousStatus = Status;
            return this;
        }

        internal void Archive()
        {
            Status = EntityAttributeStatus.Archived;
        }

        
        /// Compares the value, attribute type, enrichment, and message of this to that
        /// of the <c>other</c> instance. 
        /// The <see cref="Created"/> and <see cref="Status"/> properties are not compared
        /// (except for <see cref="EntityAttributeStatus.Deleted"/>).
        
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsEquivalentTo(EntityAttributeValue other)
        {
            if (this == other)
            {
                return true;
            }

            if (AttributeType.InternalName != other.AttributeType.InternalName)
            {
                return false;
            }
            if (!ValueEquals(other))
            {
                return false;
            }

            if ((Status == EntityAttributeStatus.Deleted && other.Status != EntityAttributeStatus.Deleted) ||
                (Status != EntityAttributeStatus.Deleted && other.Status == EntityAttributeStatus.Deleted))
            {
                return false;
            }

            if (Message != null)
            {
                return Message.IsEquivalentTo(other.Message);
            }
            if (Enrichment != null)
            {
                return Enrichment.IsEquivalentTo(other.Enrichment);
            }
            throw new NotSupportedException("Either Message or Enrichment is expected to be not null.");
        }

        
        /// Name of the source of this attribute value, which depends on whether the value is
        /// from a message or an enrichment:
        /// <para>
        /// If this value is from a system message, then <see cref="SourceName"/> is <see cref="SystemSource"/>.
        /// </para>
        /// <para>
        /// If this value is from an action message, then <see cref="SourceName"/> is "UiMessage".
        /// </para>
        /// <para>
        /// If this value is from an enrichment, then the <see cref="SourceName"/> is the
        /// custom source name if it were provided in the call to SetValue on 
        /// <see cref="IPrioritizedAttributeWrapper{T}"/> in a rule; 
        /// the source of the system message if the (possibly indirectly) referenced value is from a system message;
        /// the short display name of the referenced value if that value is from another attribute type;
        /// "Masterdata" if a master data reference was provided in the call to SetValue on
        /// <see cref="IPrioritizedAttributeWrapper{T}"/> in a rule;
        /// or "AODB" otherwise.
        /// </para>
        
        [JsonIgnore]
        public string SourceName
        {
            get
            {
                if (Message != null)
                {
                    return ToSourceName(Message);
                }
                if (Enrichment != null)
                {
                    return Enrichment.CustomSourceName ?? GetSourceNameOfEnrichment();
                }
                throw new NotSupportedException("Either Message or Enrichment is expected to be not null.");
            }
        }

        private string GetSourceNameOfEnrichment()
        {
            var aodbRef = Enrichment!.GetFirstLeafAttributeReference();
            if (aodbRef != null)
            {
                return aodbRef.Message != null
                    ? ToSourceName(aodbRef.Message)
                    // Enrichment with another enrichment of same attribute
                    : "AODB";
            }
            return Enrichment.MasterDataReferences.Any() ? "Masterdata" : "AODB";
        }

        private static string ToSourceName(EntityAttributeMessageValue messageValue) =>
            messageValue.RawMessage.SystemMessage != null
                ? messageValue.RawMessage.SystemMessage.Source
                : "UiMessage";
    }
}
