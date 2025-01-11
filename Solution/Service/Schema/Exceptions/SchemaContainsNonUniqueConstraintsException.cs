using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    
    /// Exception thrown if the Schema contains non-unique constraints.
    
    [Serializable]
    public class SchemaContainsNonUniqueConstraintsException : SchemaVersionException
    {
        public SchemaContainsNonUniqueConstraintsException(string typeName)
            : base("New Schema contains a non-unique constraints on EntityType(s) '{EntityTypeOne}'",
                new Dictionary<string, object> { { "EntityTypeOne", typeName } })
        {
        }

        protected SchemaContainsNonUniqueConstraintsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}