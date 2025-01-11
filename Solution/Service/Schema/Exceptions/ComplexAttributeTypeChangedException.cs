using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    
    /// Exception thrown if IsCollection or ComplexDataType on ComplexAttributeType has changed, which is
    /// not allowed.
    
    [Serializable]
    public class ComplexAttributeTypeChangedException : EntityTypeException
    {
        public ComplexAttributeTypeChangedException(string entityTypeName, string attributeTypeName)
            : base("The IsCollection or the ComplexDataType of ComplexAttributeType '{AttributeTypeName}' " +
                   "of entity '{EntityTypeName}' was changed, which is not allowed.",
                new Dictionary<string, object>
                {
                    { "EntityTypeName", entityTypeName }, { "AttributeTypeName", attributeTypeName }
                })
        {
        }

        protected ComplexAttributeTypeChangedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}