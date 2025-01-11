using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    
    /// Exception thrown if a standard entity type does not exist in state Active.
    
    [Serializable]
    public class SchemaMissingStandardEntityTypeException : SchemaVersionException
    {
        public SchemaMissingStandardEntityTypeException(string typeName)
            : base("Standard EntityType '{EntityTypeName}' does not exist in state Active in the schema.",
                new Dictionary<string, object> { { "EntityTypeName", typeName } })
        {
        }

        protected SchemaMissingStandardEntityTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}