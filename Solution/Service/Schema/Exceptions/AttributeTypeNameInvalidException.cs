using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    
    /// Exception thrown if name of attribute type is invalid.
    
    [Serializable]
    public class AttributeTypeNameInvalidException : EntityTypeException
    {
        public AttributeTypeNameInvalidException(string typeName, string attributeTypeName,
            string value, string explanation)
            : base("New Schema contains invalid characters in attribute InternalName '{Value}' " +
                   "of EntityType '{EntityTypeName}'. {Explanation}",
                new Dictionary<string, object>
                {
                    { "EntityTypeName", typeName },
                    { "AttributeTypeName", attributeTypeName },
                    { "Value", value },
                    { "Explanation", explanation }
                })
        {
        }

        protected AttributeTypeNameInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}