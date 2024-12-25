using Library.Persistence.EntityTypes;
using Database.Extensions;
using Database.Models.Schema;
using Newtonsoft.Json.Linq;
using System;

namespace Database.Models.Data
{
    public abstract class EntityAttributeValue : AggregateRoot
    {
        public string? Text { get; set; }

        public int? Integer { get; set; }

        public bool? Boolean { get; set; }

        public TimeSpan? Duration { get; set; }

        private DateTime? dateTime;

        public DateTime? DateTime
        {
            get
            {
                return dateTime;
            }
            set
            {
                dateTime = value.HasValue ? new DateTime(value.Value.Ticks, DateTimeKind.Utc) : null;
            }
        }

        public string? Dropdown { get; set; }

        public decimal? Decimal { get; set; }

        public Guid? Guid { get; set; }


        private string? json;

        /// <summary>
        /// Used to store the serialized complex value if the <see cref="AttributeType"/>
        /// is a <see cref="ComplexAttributeType"/>.
        /// </summary>
        public string? Json
        {
            get
            {
                return json?.AddZuluToNonZuluStrings();
            }
            set
            {
                json = value;
            }
        }

        public Guid EntityValueId { get; set; }

        public EntityAttributeStatus Status { get; set; }

        /// <summary>
        /// If the EAV was <see cref="AffectedBy"/> an <see cref="ActionMessage"/>
        /// this property tracks how it was affected, either added or deleted.
        /// </summary>
        public ActionEffectStatus? ActionEffect { get; set; }

        /// <summary>
        /// Reference to the <see cref="ActionMessage"/> that altered this EAV.
        /// </summary>
        public virtual ActionMessage? AffectedBy { get; set; }

        public virtual EntityValue EntityValue { get; set; } = null!;

        public virtual AttributeType AttributeType { get; set; } = null!;

        /// <summary>
        /// The time when the value of this EAV was different compared to the
        /// EAV that was previously set (prioritized) for the attribute type.
        /// 
        /// All EAVs should have this timestamp, but legacy data may not have
        /// this set. Code that assumes a value should use a default value
        /// (Datetime.MinValue).
        /// </summary>
        public DateTime? ValueLastModified { get; set; }

        public bool ValueIsEqualTo(EntityAttributeValue otherEav)
        {
            return (AttributeType.InternalName == otherEav.AttributeType.InternalName
                    && Guid == otherEav.Guid
                    && Decimal == otherEav.Decimal
                    && Dropdown == otherEav.Dropdown
                    && DateTime == otherEav.DateTime
                    && Duration == otherEav.Duration
                    && Boolean == otherEav.Boolean
                    && Integer == otherEav.Integer
                    && Text == otherEav.Text
                    && compareJson(otherEav.Json));
        }

        private bool compareJson(string? otherJson)
        {
            if (Json == otherJson)
            {
                return true;
            }

            if (Json == null || otherJson == null)
            {
                return false;
            }

            return JToken.DeepEquals(JObject.Parse(Json), JObject.Parse(otherJson));
        }

        /// <summary>
        /// Returns the value of this EntityAttributeValue as an unspecific object.
        /// </summary>
        /// <returns></returns>
        public object? ToObjectValue()
        {
            return AttributeType switch
            {
                ComplexAttributeType _ => Json,
                ValueAttributeType valueAttributeType => ValueToObject(valueAttributeType),
                _ => throw new ArgumentOutOfRangeException(message: "Unknown AttributeType class", null)
            };
        }

        private object? ValueToObject(ValueAttributeType valueAttributeType)
        {
            return valueAttributeType.ValueDataType switch
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
                _ => throw new ArgumentOutOfRangeException(nameof(valueAttributeType),
                    "Unknown AttributeType DataType")
            };
        }
    }
}