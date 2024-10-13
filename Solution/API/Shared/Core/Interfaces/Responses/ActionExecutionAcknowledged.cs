namespace API.Shared.Core.Interfaces.Responses
{
    public class ActionExecutionAcknowledged : ActionExecutionResult
    {
        /// <summary>
        /// This constructor is used when the executed action is not resulting in a ParentOperationId.
        /// </summary>
        public ActionExecutionAcknowledged(INotification? notification = null) 
            : base(ActionStatus.Acknowledged, notification)
        {
        }

        /// <summary>
        /// This constructor is used when the executed action is resulting in a ParentOperationId that is returned to the UI.
        /// </summary>
        public ActionExecutionAcknowledged(string parentOperationId, INotification? notification = null)
            : base(ActionStatus.Acknowledged, parentOperationId, notification)
        {
        }
    }
}
