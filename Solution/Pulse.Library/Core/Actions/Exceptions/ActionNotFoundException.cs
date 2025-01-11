using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace Pulse.Library.Core.Actions.Exceptions
{
    [Serializable]
    public class ActionNotFoundException : FormattedMessageException
    {
        public ActionNotFoundException(string action, string entityType, Exception? e = null) 
            : base(
                template: "Could not find action: '{action}' for entity type: '{entityType}'.", 
                statusCode: HttpStatusCode.NotFound, 
                properties: new Dictionary<string, object> 
                {
                    { "action", action },
                    { "entityType", entityType }
                },
                innerException: e) 
        { }

        protected ActionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
