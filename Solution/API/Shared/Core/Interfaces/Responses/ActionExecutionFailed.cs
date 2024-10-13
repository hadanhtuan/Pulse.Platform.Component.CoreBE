namespace API.Shared.Core.Interfaces.Responses
{
    public class ActionExecutionFailed : ActionExecutionResult
    {
        public ActionExecutionFailed(string errorMessage, INotification? notification = null) 
            : base(ActionStatus.Failed,  notification)
        {
            ErrorMessage = errorMessage;
        }
    }
}
