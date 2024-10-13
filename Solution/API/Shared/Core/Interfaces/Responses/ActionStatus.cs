namespace API.Shared.Core.Interfaces.Responses
{
    /// <summary>
    /// Status of invoking an action.
    /// </summary>
    public enum ActionStatus
    {
        /// <summary>
        /// Acknowledged - Action finished as expected; further processing required.
        /// </summary>
        Acknowledged,
        /// <summary>
        /// NotAcknowledged - Action failed due to business logic error - see error meesage and ValidationError.
        /// </summary>
        NotAcknowledged,
        /// <summary>
        /// InsufficientInput - Action cannot be completed due to the insufficient input, client need to request updated configuration.
        /// </summary>
        InsufficientInput,
        /// <summary>
        /// Completed - Action finished as expected and all internal processing is done.
        /// </summary>
        Completed,
        /// <summary>
        /// Failure - Technical failure occured - see error message.
        /// </summary>
        Failed
    }
}
