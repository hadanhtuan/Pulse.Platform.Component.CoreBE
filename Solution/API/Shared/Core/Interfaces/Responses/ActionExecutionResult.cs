using API.Shared.Core.Interfaces.Responses.Report;

namespace API.Shared.Core.Interfaces.Responses
{
    public abstract class ActionExecutionResult : IActionExecutionResult
    {
        public IDictionary<string, List<string>> ValidationError { get; protected set; }

        public string? ErrorMessage { get; protected set; }

        public ActionStatus Status { get; protected set; }

        /// <summary>
        /// Returns a notification model to the Action Framework to publish
        /// </summary>
        public INotification? Notification { get; protected set; }

        /// <summary>
        /// Returns the new Action Configuration in the case of ActionStatus.InsufficientInput
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
        /// Returns the list of the insufficient input rules
        /// </summary>
        public List<string>? InsufficientInputReasons { get; set; }

        /// <summary>
        /// Returns an object containing varying information about the action executed.
        /// </summary>
        public IMetadataReport? MetadataReport { get; set; }

        private ActionExecutionResult(INotification? notification = null)
        {
            ValidationError = new Dictionary<string, List<string>>();
            Notification = notification;
        }

        protected ActionExecutionResult(ActionStatus status, INotification? notification = null)
            : this(notification)
        {
            Status = status;
        }

        protected ActionExecutionResult(ActionStatus status, string? parentOperationId, INotification? notification = null)
            : this(status, notification)
        {
            ParentOperationId = parentOperationId;
        }
    }
}
