using System;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json
{
    
    /// Contains the extra data that a <see cref="RawMessage"/> has when it is an Action (UI) message
    /// (as opposed to a System (Adapter) message)
    
    public class RawActionMessage
    {
        
        /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
        /// in order to use <see cref="ReferenceHandler.Preserve"/>.
        
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

        
        /// The internal name of the action that caused this message.
        
        [JsonInclude]

        public string ExecutingAction { get; private set; }

        
        /// The internal name of the action that this message reverted.
        /// This is set by the executing action when triggered.
        
        [JsonInclude]

        public string? RevertAction { get; private set; }

        
        /// Tracks whether the effects of this message has been Executed, or has been Reverted.
        
        [JsonInclude]

        public ActionStatus Status { get; internal set; }

        
        /// This holds the Guid for the UserId of the user who applied the Action.
        /// The ActiveRole is received from the GlobalUserContext.
        
        [JsonInclude]

        public Guid UserId { get; private set; }

        
        /// This holds the Guid for the ActiveRole of the user who applied the Action.
        /// The ActiveRole is received from the GlobalUserContext.
        
        [JsonInclude]

        public Guid ActiveRole { get; private set; }

        
        /// The Global Identity of the latest participant in the request chain of the consumed message.
        /// Will be null if a request without the Global Identity was received.
        
        [JsonInclude]
        public Requester? Requester { get; set; }
    }
}
