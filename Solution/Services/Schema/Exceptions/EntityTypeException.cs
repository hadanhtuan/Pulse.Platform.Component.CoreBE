using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services.Schema.Exceptions
{
    /// <summary>
    /// Exception thrown because a new schema did not contain a valid definition
    /// of an entity type compared to the existing schema.
    /// </summary>
    [Serializable]
    public class EntityTypeException : SchemaValidationException
    {
        public EntityTypeException(string message)
            : base(message, new Dictionary<string, object>())
        {
        }

        public EntityTypeException(string messageTemplate, Dictionary<string, object> parameters)
            : base(messageTemplate, parameters)
        {
        }

        protected EntityTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}