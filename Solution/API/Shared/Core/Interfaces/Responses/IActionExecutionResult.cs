
using API.Shared.Core.Interfaces.Responses.Report;

namespace API.Shared.Core.Interfaces.Responses
{
    /// <summary>
    /// Result of the <c>Execution</c> of an <c>EntityAction</c> or <c>EntityTypeAction</c>
    /// </summary>
    public interface IActionExecutionResult
    {
        /// <summary>
        /// Contains a number of errors grouped on the internal name of the attribute.
        /// </summary>
        public IDictionary<string, List<string>> ValidationError { get; }

        /// <summary>
        /// An human readable error message to present the result.
        /// </summary>
        public string? ErrorMessage { get; }

        /// <summary>
        /// Contains the status of the execution.
        /// See <c>ActionStatus</c>
        /// </summary>
        public ActionStatus Status { get; }

        /// <summary>
        /// Each action decides whether it should issue a notification. 
        /// If it should, the content of the notification is provided here.
        /// </summary>
        public INotification? Notification { get; }

        /// <summary>
        /// Additional configuration provided with <see cref="ActionStatus.InsufficientInput"/> status.
        /// </summary>
        public object? Configuration { get; set; }

        /// <summary>
        /// Name of the insufficient Input action if an external action is needed
        /// </summary>
        public string? NewInsufficientInputAction { get; set; }

        /// <summary>
        /// The ParentOperationId of the task-tree triggered by this action
        /// </summary>
        public string? ParentOperationId { get; set; }

        /// <summary>
        /// Returns the new Action Model in the case of ActionStatus.InsufficientInput
        /// </summary>
        public List<string>? InsufficientInputReasons { get; set; }

        /// <summary>
        /// Metadata report containing info about the action executed.
        /// </summary>
        public IMetadataReport? MetadataReport { get; set; }
    }
}
