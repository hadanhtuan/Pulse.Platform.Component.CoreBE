using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Service.Schema.Exceptions
{
    
    /// Base class for exception thrown if a new schema does not validate,
    /// providing properties for a template-based message using parameters.
    
    [Serializable]
    public abstract class SchemaValidationException : Exception
    {
        public string MessageTemplate { get; private set; } = null!;
        public Dictionary<string, object> Parameters { get; private set; } = null!;

        protected SchemaValidationException(string messageTemplate, Dictionary<string, object>? parameters)
            : base(FormatTemplateFromPropertiesDict(messageTemplate, parameters ?? new Dictionary<string, object>()))
        {
            MessageTemplate = messageTemplate;
            Parameters = parameters ?? new Dictionary<string, object>();
        }

        // This protected constructor is used for deserialization.
        protected SchemaValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        // GetObjectData performs a custom serialization.
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info,
            StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        private static string FormatTemplateFromPropertiesDict(
            string? template, Dictionary<string, object> parameters)
        {
            var message = template ?? "";
            foreach (var (key, value) in parameters)
            {
                message = message.Replace($"{{{key}}}", value.ToString());
            }

            return message;
        }
    }
}