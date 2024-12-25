using System;

namespace Database.Models.Data
{
    /// <summary>
    /// Message from a user.
    /// </summary>
    public class ActionMessage : RawMessage
    {
        /// <summary>
        /// The internal name of the action that caused this message.
        /// </summary>
        public string ExecutingAction { get; set;} = null!;

        /// <summary>
        /// The internal name of the action that this message reverted.
        /// This is set by the executing action when triggered.
        /// </summary>
        public string? RevertAction { get; set; }

        /// <summary>
        /// Tracks whether the effects of this message has been Executed, or has been Reverted.
        /// </summary>
        public ActionStatus Status { get; set; }

        /// <summary>
        /// This holds the Guid for the UserId of the user which applied the Action
        /// The ActiveRole is received from the GlobalUserContext
        /// </summary>
        public Guid UserId { get; set; } = Guid.Empty;

        /// <summary>
        /// This holds the Guid for the ActiveRole of the user which applied the Action
        /// The ActiveRole is received from the GlobalUserContext
        /// </summary>
        public Guid ActiveRole { get; set; } = Guid.Empty;
    }
}