using System;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json
{
    /// <summary>
    /// Contains the extra data that a <see cref="RawMessage"/> has when it is an Action (UI) message
    /// (as opposed to a System (Adapter) message)
    /// </summary>
    public class RawActionMessage
    {
        /// <summary>
        /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
        /// in order to use <see cref="ReferenceHandler.Preserve"/>.
        /// </summary>
        [JsonConstructor]
        [Obsolete("This constructor is only intended for the System.Text.Json.JsonSerializer.")]
        public RawActionMessage() : this(ActionStatus.Undefined, string.Empty, Guid.Empty, Guid.Empty)
        {
        }

        public RawActionMessage(ActionStatus status, string executingAction, Guid userId, Guid activeRole, 
            string? revertAction = null, Requester? requester = null)
        {
            Status = status;
            ExecutingAction = executingAction;
            UserId = userId;
            ActiveRole = activeRole;
            RevertAction = revertAction;
            Requester = requester;
        }

        /// <summary>
        /// The internal name of the action that caused this message.
        /// </summary>
        [JsonInclude]

        public string ExecutingAction { get; private set; }

        /// <summary>
        /// The internal name of the action that this message reverted.
        /// This is set by the executing action when triggered.
        /// </summary>
        [JsonInclude]

        public string? RevertAction { get; private set; }

        /// <summary>
        /// Tracks whether the effects of this message has been Executed, or has been Reverted.
        /// </summary>
        [JsonInclude]

        public ActionStatus Status { get; internal set; }

        /// <summary>
        /// This holds the Guid for the UserId of the user who applied the Action.
        /// The ActiveRole is received from the GlobalUserContext.
        /// </summary>
        [JsonInclude]

        public Guid UserId { get; private set; }

        /// <summary>
        /// This holds the Guid for the ActiveRole of the user who applied the Action.
        /// The ActiveRole is received from the GlobalUserContext.
        /// </summary>
        [JsonInclude]

        public Guid ActiveRole { get; private set; }

        /// <summary>
        /// The Global Identity of the latest participant in the request chain of the consumed message.
        /// Will be null if a request without the Global Identity was received.
        /// </summary>
        [JsonInclude]
        public Requester? Requester { get; set; }
    }
}
