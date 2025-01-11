using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    
    /// Exception thrown if an existing attribute of an entity type is missing.
    
    [Serializable]
    public class EntityTypeMissingAttributeException : EntityTypeException
    {
        public EntityTypeMissingAttributeException(string typeName)
            : base("New EntityType '{EntityTypeName}' does not contain all old AttributeTypes.",
                new Dictionary<string, object> { { "EntityTypeName", typeName } })
        {
        }

        protected EntityTypeMissingAttributeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}