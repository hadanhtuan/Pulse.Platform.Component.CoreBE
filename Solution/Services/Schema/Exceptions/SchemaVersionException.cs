using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services.Schema.Exceptions
{
    /// <summary>
    /// Exception thrown because a new schema was not a valid update to an existing schema.
    /// </summary>
    [Serializable]
    public class SchemaVersionException : SchemaValidationException
    {
        public SchemaVersionException(string message)
            : base(message, new Dictionary<string, object>())
        {
        }

        public SchemaVersionException(string template, Dictionary<string, object> parameters)
            : base(template, parameters)
        {
        }

        protected SchemaVersionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}