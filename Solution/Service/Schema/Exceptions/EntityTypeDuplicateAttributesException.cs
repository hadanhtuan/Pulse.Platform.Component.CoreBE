using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    /// <summary>
    /// Exception thrown if an attribute type appears more than once in entity type.
    /// </summary>
    [Serializable]
    public class EntityTypeDuplicateAttributesException : EntityTypeException
    {
        public EntityTypeDuplicateAttributesException(string typeName)
            : base("EntityType with name '{EntityTypeName}' does not contain unique AttributeTypes.",
                new Dictionary<string, object> { { "EntityTypeName", typeName } })
        {
        }

        protected EntityTypeDuplicateAttributesException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}