using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace Pulse.Library.Core.Actions.Exceptions
{
    [Serializable]
    public class ActionConfigurationException : FormattedMessageException
    {
        public ActionConfigurationException(string message, string action, string entityType, Exception? e = null) 
            : base(
                template: "Configuration for action: '{action}' for entity type: '{entityType}' " +
                  "could not be loaded due to {message}.",
                statusCode: HttpStatusCode.NotFound,
                properties: new Dictionary<string, object> 
                {
                    { "message", message },
                    { "action", action },
                    { "entityType", entityType }
                },
                innerException: e) 
        { }

        protected ActionConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
