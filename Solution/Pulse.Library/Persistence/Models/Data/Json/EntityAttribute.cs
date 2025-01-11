using Database.Models.Schema;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Library.Persistence.EntityTypes;

namespace Database.Models.Data.Json;

[DebuggerDisplay("Name = {AttributeTypeInternalName}")]
public class EntityAttribute
{
    internal EntityAttribute(AttributeType attributeType)
    {
        AttributeTypeInternalName = attributeType.InternalName;
        this.attributeType = attributeType;
    }

    
    /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
    /// in order to use <see cref="ReferenceHandler.Preserve"/>.
    
    [JsonConstructor]
    [Obsolete("This constructor is only intended for the System.Text.Json.JsonSerializer.")]
    public EntityAttribute()
    {
        AttributeTypeInternalName = string.Empty;
    }

    private AttributeType? attributeType;

    [JsonIgnore]
    public AttributeType AttributeType
    {
        get => attributeType!;
        internal set
        {
            attributeType = value;
            foreach (EntityAttributeValue attributeValue in sortedValues.Values)
            {
                attributeValue.AttributeType = value;
            }
        }
    }

    
    /// Internal name for the type of this attribute.
    /// Can be used to relate the attribute to the <see cref="AttributeType"/> in the Schema.
    
    [JsonInclude]
    public string AttributeTypeInternalName { get; private set; }

    // Internal values list is sorted by status, priority, credibility and finally created/received datetime.
    // SortedDictionary performs best for our use where we insert values one at a time
    private readonly SortedList<EntityAttributeValue, EntityAttributeValue> sortedValues
        = new(Comparer<EntityAttributeValue>.Create((v2, v1) =>
        {
            int result = -(v1.Status.CompareTo(v2.Status));
            if (result == 0)
            {
                result = (v1.Enrichment?.Priority ?? 0).CompareTo(v2.Enrichment?.Priority ?? 0);
            }

            if (result == 0)
            {
                // For messages, use Received for sorting, for Enrichments use Created
                result = (v1.Message?.RawMessage.Received ?? v1.Created)
                    .CompareTo(v2.Message?.RawMessage.Received ?? v2.Created);
                // In case two values have the same comparison value, always use Created
                // (as some RawMessages have the same Received field).
                if (result == 0)
                {
                    result = v1.Created.CompareTo(v2.Created);
                }
            }

            if (result == 0)
            {
                result = v1.Id.CompareTo(v2.Id);
            }

            return result;
        }));

    
    /// Provides access to previous values from <see cref="Values"/> 
    /// after calls to <see cref="ClearEnrichmentAttributeValues(Guid?)"/>.
    
    /// <remarks>
    /// This allows the pipeline to always access the <see cref="Values"/> that were present
    /// at the start of the pipeline run.
    /// </remarks>
    [JsonIgnore]
    public IEnumerable<EntityAttributeValue> PreviousValues { get; internal set; } =
        Enumerable.Empty<EntityAttributeValue>();

    [JsonIgnore]
    public IEnumerable<EntityAttributeValue> PreviousValuesWithoutOverwrittenMessageValues =>
        PreviousValues.Where(v => v.Message?.RawMessage == null || !PreviousValues.Any(
            valueWithReference => valueWithReference.Enrichment?.AttributeReferences.Contains(v) == true
                                  && valueWithReference.ValueEquals(v)));

    
    /// The individual values (typically from different data sources) for this attribute,
    /// returned in prioritized order (by priority, credibility and finally created/received time).
    
    [JsonInclude]
    public IEnumerable<EntityAttributeValue> Values
    {
        get => sortedValues.Keys;
        internal set
        {
            sortedValues.Clear();
            foreach (EntityAttributeValue a in value)
            {
                sortedValues.Add(a, a);
            }

            PreviousValues = sortedValues.Keys.Select(eav => eav.SnapshotStatus()).ToList();
        }
    }

    [JsonIgnore]
    public IEnumerable<EntityAttributeValue> ValuesWithoutOverwrittenMessageValues =>
        Values.Where(v => v.Message?.RawMessage == null || !Values.Any(
            valueWithReference => valueWithReference.Enrichment?.AttributeReferences.Contains(v) == true
                                  && valueWithReference.ValueEquals(v)));

    
    /// Removes all <see cref="EntityAttributeEnrichmentValue"/>s that fulfill a given
    /// enrichmentContextFilter - if called with null, all <see cref="EntityAttributeEnrichmentValue"/>s are removed.
    
    /// <remarks>
    /// Used in the pipeline to clear out previous enrichments before re-enriching.
    /// The entity matched by a message should be cleared completely, while entities being enriched in context of
    /// another entity should only have the enrichments from that context removed.
    /// </remarks>
    internal void ClearEnrichmentAttributeValues(Func<Guid, bool>? enrichmentContextFilter = null)
    {
        IEnumerable<EntityAttributeValue> enrichmentValuesToRemove = Values.Where(v => v.Enrichment != null);
        if (enrichmentContextFilter != null)
        {
            enrichmentValuesToRemove = enrichmentValuesToRemove
                .Where(v => v.Enrichment != null && enrichmentContextFilter(v.Enrichment.EnrichmentContextId));
        }

        // Mark as Archived in case removed attributes are referenced from other (active) attributes
        enrichmentValuesToRemove.ToList().ForEach(v =>
        {
            Remove(v);
        });
    }

    
    /// Adds a new <see cref="EntityAttributeValue"/> with the value and rawMessage reference.
    
    /// <remarks>
    /// For raw messages that represent system messages, only the most recent value per 
    /// <see cref="RawSystemMessage.Source"/> and <see cref="RawSystemMessage.MessageType"/> is kept in the
    /// <see cref="Values"/> collection.
    /// </remarks>
    /// <param name="value"></param>
    /// <param name="rawMessage"></param>
    internal EntityAttributeValue AddMessageValue(
        Guid id,
        object? value,
        RawMessage rawMessage,
        EntityAttributeStatus status = EntityAttributeStatus.Active)
    {
        EntityAttributeValue eav = new(AttributeType, id, value, status, new EntityAttributeMessageValue(rawMessage));

        // For system messages, we only store the most recently received value of each attribute per source/message type
        if (rawMessage.SystemMessage != null)
        {
            EntityAttributeValue? existingEav
                = sortedValues.Keys.SingleOrDefault(
                    eav => eav.Message?.RawMessage.SystemMessage?.Source == rawMessage.SystemMessage.Source &&
                           eav.Message?.RawMessage.SystemMessage?.MessageType == rawMessage.SystemMessage.MessageType);

            if (existingEav?.Message?.RawMessage?.Received > rawMessage.Received)
            {
                return existingEav; // Do not add older values than we already have
            }

            if (existingEav?.Message?.RawMessage?.Received <= rawMessage.Received)
            {
                // Keep ValueOrMetadataLastChanged from existing if we receive the same value
                if (eav.ValueEquals(existingEav))
                {
                    eav.ValueOrMetadataLastChanged = existingEav.ValueOrMetadataLastChanged;
                }

                Remove(existingEav); // Remove the older value
            }
        }

        AddAttributeValue(eav);

        return eav;
    }

    
    /// Adds a new <see cref="EntityAttributeValue"/> with enrichment details.
    
    /// <param name="value"></param>
#pragma warning disable S107 // Methods should not have too many parameters
    public EntityAttributeValue AddEnrichmentValue(
        Guid id,
        object? value,
        DateTime created,
        string causedBy,
        Guid enrichmentContextId,
        int priority = 0,
        string? sourceName = null,
        IEnumerable<MasterDataReference>? masterDataReferences = null,
        IEnumerable<EntityAttributeValue>? attributeReferences = null,
        EntityAttributeStatus status = EntityAttributeStatus.Active,
        bool isAbsent = false)
#pragma warning restore S107 // Methods should not have too many parameters
    {
        // Recurse through archived references to get a possible active reference at the bottom.
        // This behaviour is inherited from relational model.
        // Note that this is Lazily invoked. In other words, accessing attributeReferences
        // on the created enrichment will dynamically update if any references are modified.
        attributeReferences = attributeReferences?
            .Select(RecurseThroughArchivedEnrichments)
            .OfType<EntityAttributeValue>(); // removes null values

        EntityAttributeValue eav = new(AttributeType, id, value, created, status,
            new EntityAttributeEnrichmentValue(
                causedBy,
                enrichmentContextId,
                priority,
                sourceName,
                masterDataReferences,
                attributeReferences));

        eav.IsAbsent = isAbsent;

        AddAttributeValue(eav);

        return eav;
    }

    private EntityAttributeValue? RecurseThroughArchivedEnrichments(EntityAttributeValue? entityAttributeValue)
    {
        if (entityAttributeValue == null)
        {
            return null;
        }

        if (entityAttributeValue.Status == EntityAttributeStatus.Archived)
        {
            return RecurseThroughArchivedEnrichments(
                entityAttributeValue.Enrichment?.AttributeReferences.FirstOrDefault());
        }
        else
        {
            return entityAttributeValue;
        }
    }

    internal void RemoveRawMessageAttributes(RawMessage rawMessage)
    {
        foreach (EntityAttributeValue entityAttributeValue in GetEntityAttributeValuesByRawMessage(rawMessage))
        {
            sortedValues.Remove(entityAttributeValue);
        }
    }

    
    /// Deletes message values from this attribute as an effect of the given <see cref="Data.RawMessage"/>. 
    /// Note that a message may indicate removal of an attribute that the message also contains a value
    /// for, in which case the new attribute value is kept as added.
    /// If the message is a SystemMessage then only values from the specified source are deleted.
    
    /// <param name="message"></param>
    public void DeleteMessageValues(Data.RawMessage message)
    {
        var eavs = sortedValues.Keys.Where(eav =>
            eav.Message != null &&
            eav.Message.RawMessage.Id != message.Id &&
            eav.Status == EntityAttributeStatus.Active);

        if (message is SystemMessage sm)
        {
            eavs = eavs.Where(eav => eav.Message!.RawMessage.SystemMessage != null &&
                                     eav.Message!.RawMessage.SystemMessage.Source == sm.Source);
        }

        foreach (var eav in eavs.ToList())
        {
            sortedValues.Remove(eav);
            // The returned eav is a copy to avoid updating previous values.
            var deletedEav = eav.Delete(message);
            sortedValues.Add(deletedEav, deletedEav);
        }
    }

    internal IList<EntityAttributeValue> GetEntityAttributeValuesByRawMessage(RawMessage rawMessage)
    {
        return sortedValues.Keys.Where(eav => eav.Message?.RawMessage == rawMessage).ToList();
    }

    internal void AddAttributeValue(EntityAttributeValue entityAttributeValue)
    {
        RemoveEarlierEquivalentValues(entityAttributeValue);

        AssignValueOrMetadataLastModifiedFromPreviousEquivalentValue(entityAttributeValue);

        sortedValues.Add(entityAttributeValue, entityAttributeValue);
    }

    private void AssignValueOrMetadataLastModifiedFromPreviousEquivalentValue(EntityAttributeValue entityAttributeValue)
    {
        var previousEav = PreviousValues.FirstOrDefault(previous => previous.IsEquivalentTo(entityAttributeValue));
        if (previousEav != null)
        {
            entityAttributeValue.ValueOrMetadataLastChanged = previousEav.ValueOrMetadataLastChanged;
        }
    }

    private void RemoveEarlierEquivalentValues(EntityAttributeValue entityAttributeValue)
    {
        List<EntityAttributeValue> earlierEquivalentValues =
            Values.Where(v => v.IsEquivalentTo(entityAttributeValue)).ToList();

        earlierEquivalentValues.ForEach(earlierEquivalentValue => Remove(earlierEquivalentValue));
    }

    
    /// Removes the entityAttributeValue from the sorted list of <see cref="Values"/> and also
    /// marks it as Archived.
    
    /// <param name="entityAttributeValue"></param>
    internal void Remove(EntityAttributeValue entityAttributeValue)
    {
        sortedValues.Remove(entityAttributeValue);
        entityAttributeValue.Archive();
    }

    
    /// List of the most recent attribute value resets performed by enrichment rules.
    /// Provides background information to allow investigatation into why message values have disappeared from the EntityValue.
    
    /// <remarks>
    /// Only the most recent resets are stored, so if an attribute is being reset multiple times, 
    /// it is only the latest reset that will be stored.
    /// </remarks>
    [JsonInclude]
    public ICollection<EntityAttributeResetMarker> ResetMarkers { get; internal set; } =
        new Collection<EntityAttributeResetMarker>();

    public void Reset(string causedBy, Guid enrichmentContextId, string? source = null)
    {
        RemoveResetAttributes(source);
        RemoveResetMarkers(source);
        ResetMarkers.Add(new EntityAttributeResetMarker(causedBy, enrichmentContextId) { Source = source });
    }

    private void RemoveResetMarkers(string? source = null)
    {
        if (source != null)
        {
            var marker = ResetMarkers.FirstOrDefault(m => m.Source == source);
            if (marker != null)
            {
                ResetMarkers.Remove(marker);
            }
        }
        else
        {
            ResetMarkers.Clear();
        }
    }

    private void RemoveResetAttributes(string? source = null)
    {
        var valuesToRemove = sortedValues.Keys
            .Where(eav => source == null || eav.Message?.RawMessage.SystemMessage?.Source == source)
            .ToList();

        valuesToRemove.ForEach(entityAttributeValue => Remove(entityAttributeValue));
    }

    
    /// Reverts the <see cref="ActionMessage"/> with the actionMsgIdToRevert and assigns
    /// the actionMsgIdOfRevertAction as <see cref="EntityAttributeMessageValue.AffectedByRawMsgId"/> on
    /// the attributes that get reverted.
    
    internal void RevertActionMessage(Guid actionMsgIdToRevert, Guid actionMsgIdOfRevertAction)
    {
        var eavsToRevert = Values
            .Where(eav => eav.Message?.AffectedByRawMsgId == actionMsgIdToRevert).ToList();

        foreach (var eav in eavsToRevert)
        {
            sortedValues.Remove(eav);
            eav.Revert(actionMsgIdOfRevertAction);
            sortedValues.Add(eav, eav);
        }
    }

    
    /// The "Best" value from <see cref="Values"/>.
    
    public EntityAttributeValue? Best { get => ValuesWithoutOverwrittenMessageValues.FirstOrDefault(); }

    
    /// The "Best" value from <see cref="PreviousValues"/>.
    
    public EntityAttributeValue? PreviousBest { get => PreviousValuesWithoutOverwrittenMessageValues.FirstOrDefault(); }

    
    /// Indicator for whether the Best value for this Attribute has ever had a non-empty, non-null value stored
    
    [JsonInclude]
    public bool BestHasHadPersistedNonNullValue { get; internal set; }

    
    /// The time the Best value last changed - if the attribute is enriched with the same value repeatedly,
    /// this DateTime will remain the same even though the enrichment value will have 
    /// a new <see cref="EntityAttributeValue.Created"/> value on it.
    
    /// <remarks>
    /// This field is maintained by comparing new enriched values with previously enriched values from last pipeline run.
    /// </remarks>
    [JsonInclude]
    public DateTime BestValueLastChanged
    {
        get => BestValueIsDifferentFromPreviousBest ? now : previousBestValueLastChanged;
        internal set { previousBestValueLastChanged = value; }
    }

    // Will be UtcNow first time an EntityAttribute is created, but always be deserialized from Json afterwards.
    private DateTime previousBestValueLastChanged = DateTime.UtcNow;

    // Used to return the same UtcNow in case there are several calls to BestValueLastChanged
    private readonly DateTime now = DateTime.UtcNow;

    
    /// Determines if the current Best Value is different from the previous one.
    
    [JsonIgnore]
    public bool BestValueIsDifferentFromPreviousBest => !HaveEqualValues(Best, PreviousBest);

    private static bool HaveEqualValues(EntityAttributeValue? currentValue, EntityAttributeValue? previousValue)
    {
        return (currentValue?.Status == previousValue?.PreviousStatus)
               && (currentValue == previousValue || currentValue != null && currentValue.ValueEquals(previousValue));
    }
}