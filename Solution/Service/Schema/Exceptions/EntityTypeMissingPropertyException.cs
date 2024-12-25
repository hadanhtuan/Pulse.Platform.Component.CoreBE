using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    /// <summary>
    /// Exception thrown if a required property of entity type is missing.
    /// </summary>
    [Serializable]
    public class EntityTypeMissingPropertyException : EntityTypeException
    {
        public EntityTypeMissingPropertyException(string typeName, string property)
            : base("New Schema contains EntityType '{EntityTypeName}', " +
                   "which is missing a {Property}.",
                new Dictionary<string, object> { { "EntityTypeName", typeName }, { "Property", property } })
        {
        }

        protected EntityTypeMissingPropertyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}