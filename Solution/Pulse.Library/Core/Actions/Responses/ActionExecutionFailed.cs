namespace Pulse.Library.Core.Actions.Responses
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
