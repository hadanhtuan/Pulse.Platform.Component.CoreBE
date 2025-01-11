namespace Pulse.Library.Core.Actions.Responses
{
    public class ActionExecutionAcknowledged : ActionExecutionResult
    {
        
        /// This constructor is used when the executed action is not resulting in a ParentOperationId.
        
        public ActionExecutionAcknowledged(INotification? notification = null) 
            : base(ActionStatus.Acknowledged, notification)
        {
        }

        
        /// This constructor is used when the executed action is resulting in a ParentOperationId that is returned to the UI.
        
        public ActionExecutionAcknowledged(string parentOperationId, INotification? notification = null)
            : base(ActionStatus.Acknowledged, parentOperationId, notification)
        {
        }
    }
}
