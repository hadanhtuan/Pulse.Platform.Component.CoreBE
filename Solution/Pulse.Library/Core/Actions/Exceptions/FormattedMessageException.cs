using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace Pulse.Library.Core.Actions.Exceptions
{
    
    /// Base class for exception providing properties for a template-based message using parameters.
    
    [Serializable]
    public abstract class FormattedMessageException : Exception
    {
        public string Template { get; private set; } = null!;
        public Dictionary<string, object> Properties { get; private set; } = null!;
        public HttpStatusCode StatusCode { get; private set; }

        protected FormattedMessageException(
            string template,
            HttpStatusCode statusCode,
            Dictionary<string, object>? properties, 
            Exception? innerException)
            : base(FormatTemplateFromPropertiesDict(template, properties ?? new Dictionary<string, object>()), 
                  innerException)
        {
            Template = template;
            Properties = properties ?? new Dictionary<string, object>();
            StatusCode = statusCode;
        }

        // This protected constructor is used for deserialization.
        protected FormattedMessageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        // GetObjectData performs a custom serialization.
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
