using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace Pulse.Library.Core.Actions.Exceptions
{
    [Serializable]
    public class NoPermissionException : FormattedMessageException
    {
        public NoPermissionException(string action, string entityType, Exception? e = null) 
            : base(
                template: "Requested action: '{action}' for entity type: '{entityType}' was unauthorized.", 
                statusCode: HttpStatusCode.Forbidden, 
                properties: new Dictionary<string, object> 
                {
                    { "action", action },
                    { "entityType", entityType }
                },
                innerException: e) 
        { }

        protected NoPermissionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
