namespace API.Shared.Core.Interfaces.Responses
{
    /// <summary>
    /// Provides the content of the notification to the Action Framework so that a 
    /// notification can be issued for the initialised action.
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// The name of action. I.e., Label/Title.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The short explanatory message content of the notification.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// The number of subtasks created as a result of the executed action - known before subtasks are initiated
        /// </summary>
        public short Tasks { get; set; }
    }
}
