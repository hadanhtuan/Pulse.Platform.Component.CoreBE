using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    
    /// Exception thrown if a property of entity type is invalid.
    
    [Serializable]
    public class EntityTypeInvalidPropertyException : EntityTypeException
    {
        public EntityTypeInvalidPropertyException(string typeName, string property, string value, string explanation)
            : base("New Schema contains invalid characters in {Property} '{Value}' " +
                   "of EntityType '{EntityTypeName}'. {Explanation}.",
                new Dictionary<string, object>
                {
                    { "EntityTypeName", typeName },
                    { "Property", property },
                    { "Value", value },
                    { "Explanation", explanation }
                })
        {
        }

        protected EntityTypeInvalidPropertyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}