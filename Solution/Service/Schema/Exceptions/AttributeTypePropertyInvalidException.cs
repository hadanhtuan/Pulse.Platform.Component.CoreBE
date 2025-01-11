using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    
    /// Exception thrown if a property of attribute type is invalid.
    
    [Serializable]
    public class AttributeTypePropertyInvalidException : EntityTypeException
    {
        public AttributeTypePropertyInvalidException(string typeName, string attributeTypeName, string property,
            string value, string explanation)
            : base("AttributeType '{AttributeTypeName}' of EntityType '{EntityTypeName}' " +
                   "has invalid characters in {Property} '{Value}'. {Explanation}",
                new Dictionary<string, object>
                {
                    { "EntityTypeName", typeName },
                    { "AttributeTypeName", attributeTypeName },
                    { "Property", property },
                    { "Value", value },
                    { "Explanation", explanation }
                })
        {
        }

        protected AttributeTypePropertyInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}