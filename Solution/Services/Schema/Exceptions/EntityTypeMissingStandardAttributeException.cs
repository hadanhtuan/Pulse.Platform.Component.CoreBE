using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services.Schema.Exceptions
{
    /// <summary>
    /// Exception thrown if a standard attribute of an entity type is missing.
    /// </summary>
    [Serializable]
    public class EntityTypeMissingStandardAttributeException : EntityTypeException
    {
        public EntityTypeMissingStandardAttributeException(string entityTypeName, string attributeTypeName)
            : base(
                "EntityType '{EntityTypeName}' does not contain active standard attribute type {AttributeTypeName}.",
                new Dictionary<string, object>
                {
                    { "EntityTypeName", entityTypeName }, { "AttributeTypeName", attributeTypeName }
                })
        {
        }

        protected EntityTypeMissingStandardAttributeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}