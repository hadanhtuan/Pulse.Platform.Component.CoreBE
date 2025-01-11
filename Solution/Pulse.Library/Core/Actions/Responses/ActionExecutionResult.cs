using Pulse.Library.Core.Actions.Responses.Report;
using System.Collections.Generic;

namespace Pulse.Library.Core.Actions.Responses
{
    public abstract class ActionExecutionResult : IActionExecutionResult
    {
        public IDictionary<string, List<string>> ValidationError { get; protected set; }

        public string? ErrorMessage { get; protected set; }

        public ActionStatus Status { get; protected set; }

        
        /// Returns a notification model to the Action Framework to publish
        
        public INotification? Notification { get; protected set; }

        
        /// Returns the new Action Configuration in the case of ActionStatus.InsufficientInput
        
        public object? Configuration { get; set; }

        
        /// Name of the insufficient Input action if an external action is needed
        
        public string? NewInsufficientInputAction { get; set; }

        
        /// The ParentOperationId of the task-tree triggered by this action
        
        public string? ParentOperationId { get; set; }

        
        /// Returns the list of the insufficient input rules
        
        public List<string>? InsufficientInputReasons { get; set; }

        
        /// Returns an object containing varying information about the action executed.
        
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
