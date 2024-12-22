using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services.Schema.Exceptions
{
    /// <summary>
    /// Exception thrown if a property of attribute type is missing.
    /// </summary>
    [Serializable]
    public class AttributeTypePropertyMissingException : EntityTypeException
    {
        public AttributeTypePropertyMissingException(string typeName, string attributeTypeName, string property)
            : base("AttributeType '{AttributeTypeName}' of EntityType " +
                   "'{EntityTypeName}' is missing {Property}.",
                new Dictionary<string, object>
                {
                    { "EntityTypeName", typeName },
                    { "AttributeTypeName", attributeTypeName },
                    { "Property", property }
                })
        {
        }

        protected AttributeTypePropertyMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}