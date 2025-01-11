using Pulse.Library.Core.Actions.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace SA.Aodb.Actions.Framework.Exceptions
{
    [Serializable]
    public class ActionException : FormattedMessageException
    {
        public ActionException(string message, string? actionName = null, string? entityType = null, Guid? entityId = null,
            HttpStatusCode statusCode = HttpStatusCode.NotFound, Exception? innerException = null)
            : base(
                template: GetTemplate(message, actionName, entityType, entityId),
                statusCode: HttpStatusCode.NotFound,
                properties: GetParameters(actionName, entityType, entityId),
                innerException: innerException)
        {
        }

        private static Dictionary<string, object> GetParameters(string? action, string? entityType, Guid? entityId)
        {
            var ret = new Dictionary<string, object>();

            AddParameter(ret, nameof(action), action);
            AddParameter(ret, nameof(entityType), entityType);
            AddParameter(ret, nameof(entityId), entityId);

            return ret;
        }

        private static void AddParameter(Dictionary<string, object> dic, string name, object? value)
        {
            if (value != null)
            {
                dic.Add(name, value);
            }
        }

        private static string GetTemplate(string message, string? action, string? entityType, Guid? entityId)
        {
            var sb = new StringBuilder(message);

            AddMessageParameter(sb, nameof(action), action);
            AddMessageParameter(sb, nameof(entityType), entityType);
            AddMessageParameter(sb, nameof(entityId), entityId);

            return sb.ToString();
        }

        private static void AddMessageParameter(StringBuilder sb, string name, object? value)
        {
            if (value != null)
            {
                sb.Append($", {name}: '{{{name}}}'");
            }
        }

        protected ActionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
