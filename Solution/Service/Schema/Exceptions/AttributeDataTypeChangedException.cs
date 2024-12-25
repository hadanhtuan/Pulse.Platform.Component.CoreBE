using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    /// <summary>
    /// Exception thrown if data type of attribute type has changed, which is
    /// not allowed.
    /// </summary>
    [Serializable]
    public class AttributeDataTypeChangedException : EntityTypeException
    {
        public AttributeDataTypeChangedException(string entityTypeName, string attributeTypeName)
            : base("The ValueDataType of ValueAttributeType '{AttributeTypeName}' " +
                   "of entity '{EntityTypeName}' was changed, which is not allowed.",
                new Dictionary<string, object>
                {
                    { "EntityTypeName", entityTypeName }, { "AttributeTypeName", attributeTypeName }
                })
        {
        }

        protected AttributeDataTypeChangedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}