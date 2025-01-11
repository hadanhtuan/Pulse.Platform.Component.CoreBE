using System;

namespace Database.Models.Data
{
    
    /// Message from a user.
    
    public class ActionMessage : RawMessage
    {
        
        /// The internal name of the action that caused this message.
        
        public string ExecutingAction { get; set;} = null!;

        
        /// The internal name of the action that this message reverted.
        /// This is set by the executing action when triggered.
        
        public string? RevertAction { get; set; }

        
        /// Tracks whether the effects of this message has been Executed, or has been Reverted.
        
        public ActionStatus Status { get; set; }

        
        /// This holds the Guid for the UserId of the user which applied the Action
        /// The ActiveRole is received from the GlobalUserContext
        
        public Guid UserId { get; set; } = Guid.Empty;

        
        /// This holds the Guid for the ActiveRole of the user which applied the Action
        /// The ActiveRole is received from the GlobalUserContext
        
        public Guid ActiveRole { get; set; } = Guid.Empty;
    }
}