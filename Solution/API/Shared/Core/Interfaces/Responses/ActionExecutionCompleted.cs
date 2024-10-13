namespace API.Shared.Core.Interfaces.Responses
{
    public class ActionExecutionCompleted : ActionExecutionResult
    {
        public ActionExecutionCompleted(INotification? notification = null) 
            : base(ActionStatus.Completed, notification)
        {
        }
    }
}
