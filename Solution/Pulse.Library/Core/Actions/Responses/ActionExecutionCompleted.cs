namespace Pulse.Library.Core.Actions.Responses
{
    public class ActionExecutionCompleted : ActionExecutionResult
    {
        public ActionExecutionCompleted(INotification? notification = null) 
            : base(ActionStatus.Completed, notification)
        {
        }
    }
}
