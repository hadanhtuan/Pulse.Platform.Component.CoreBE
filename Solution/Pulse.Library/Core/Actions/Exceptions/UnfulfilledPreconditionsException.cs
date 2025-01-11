using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace Pulse.Library.Core.Actions.Exceptions
{
    [Serializable]
    public class UnfulfilledPreconditonsException : FormattedMessageException
    {
        public UnfulfilledPreconditonsException(string action, Guid entityId, Exception? e = null) 
            : base(
                template: "Action: '{action}' for entity: '{entityId}' does not fulfill the preconditions.", 
                statusCode: HttpStatusCode.UnprocessableEntity, 
                properties: new Dictionary<string, object> 
                {
                    { "action", action },
                    { "entityId", entityId }
                },
                innerException: e) 
        { }

        public UnfulfilledPreconditonsException(string action, IEnumerable<Guid> entityIds, Exception? e = null)
            : base(
                template: "Action: '{action}' for following entities: '{entityIds}' does not fulfill the preconditions.",
                statusCode: HttpStatusCode.UnprocessableEntity,
                properties: new Dictionary<string, object>
                {
                    { "action", action },
                    { "entityIds", string.Join(", ", entityIds) }
                },
                innerException: e)
        { }

        protected UnfulfilledPreconditonsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
