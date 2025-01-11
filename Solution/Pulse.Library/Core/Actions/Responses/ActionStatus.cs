namespace Pulse.Library.Core.Actions.Responses
{
    
    /// Status of invoking an action.
    
    public enum ActionStatus
    {
        
        /// Acknowledged - Action finished as expected; further processing required.
        
        Acknowledged,
        
        /// NotAcknowledged - Action failed due to business logic error - see error meesage and ValidationError.
        
        NotAcknowledged,
        
        /// InsufficientInput - Action cannot be completed due to the insufficient input, client need to request updated configuration.
        
        InsufficientInput,
        
        /// Completed - Action finished as expected and all internal processing is done.
        
        Completed,
        
        /// Failure - Technical failure occured - see error message.
        
        Failed
    }
}
