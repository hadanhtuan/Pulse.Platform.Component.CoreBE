using Pulse.Library.Core.Actions.Responses.Report;
using System.Collections.Generic;

namespace Pulse.Library.Core.Actions.Responses
{
    
    /// Result of the <c>Execution</c> of an <c>EntityAction</c> or <c>EntityTypeAction</c>
    
    public interface IActionExecutionResult
    {
        
        /// Contains a number of errors grouped on the internal name of the attribute.
        
        public IDictionary<string, List<string>> ValidationError { get; }

        
        /// An human readable error message to present the result.
        
        public string? ErrorMessage { get; }

        
        /// Contains the status of the execution.
        /// See <c>ActionStatus</c>
        
        public ActionStatus Status { get; }

        
        /// Each action decides whether it should issue a notification. 
        /// If it should, the content of the notification is provided here.
        
        public INotification? Notification { get; }

        
        /// Additional configuration provided with <see cref="ActionStatus.InsufficientInput"/> status.
        
        public object? Configuration { get; set; }

        
        /// Name of the insufficient Input action if an external action is needed
        
        public string? NewInsufficientInputAction { get; set; }

        
        /// The ParentOperationId of the task-tree triggered by this action
        
        public string? ParentOperationId { get; set; }

        
        /// Returns the new Action Model in the case of ActionStatus.InsufficientInput
        
        public List<string>? InsufficientInputReasons { get; set; }

        
        /// Metadata report containing info about the action executed.
        
        public IMetadataReport? MetadataReport { get; set; }
    }
}
