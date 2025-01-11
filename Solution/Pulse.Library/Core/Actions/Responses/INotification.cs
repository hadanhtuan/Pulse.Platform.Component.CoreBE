namespace Pulse.Library.Core.Actions.Responses
{
    
    /// Provides the content of the notification to the Action Framework so that a 
    /// notification can be issued for the initialised action.
    
    public interface INotification
    {
        
        /// The name of action. I.e., Label/Title.
        
        string Name { get; }

        
        /// The short explanatory message content of the notification.
        
        string Message { get; }

        
        /// The number of subtasks created as a result of the executed action - known before subtasks are initiated
        
        public short Tasks { get; set; }
    }
}
