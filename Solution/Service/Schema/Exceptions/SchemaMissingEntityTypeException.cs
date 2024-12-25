using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    /// <summary>
    /// Exception thrown if an existing entity type is missing.
    /// </summary>
    [Serializable]
    public class SchemaMissingEntityTypeException : SchemaVersionException
    {
        public SchemaMissingEntityTypeException(string typeName)
            : base("EntityType '{EntityTypeName}' is part of the current Schema and has been removed in new Schema.",
                new Dictionary<string, object> { { "EntityTypeName", typeName } })
        {
        }

        protected SchemaMissingEntityTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}