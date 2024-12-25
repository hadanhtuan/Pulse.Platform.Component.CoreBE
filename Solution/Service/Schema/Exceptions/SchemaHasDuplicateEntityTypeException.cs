using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    /// <summary>
    /// Exception thrown if schema contains two entity type with the same name.
    /// </summary>
    [Serializable]
    public class SchemaHasDuplicateEntityTypeException : SchemaVersionException
    {
        public SchemaHasDuplicateEntityTypeException(string duplicateKey)
            : base("New Schema contains duplicate EntityType '{EntityTypeName}'.",
                new Dictionary<string, object> { { "EntityTypeName", duplicateKey } })
        {
        }

        protected SchemaHasDuplicateEntityTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}