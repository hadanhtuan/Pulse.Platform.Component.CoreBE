using System;
using System.Collections.Generic;

namespace API.Shared.Core.Interfaces.Responses
{
    public class ActionExecutionNotAcknowledged : ActionExecutionResult
    {
        public ActionExecutionNotAcknowledged(string errorMessage, INotification notification, 
            Func<ValidationErrorBuilder, IDictionary<string, List<string>>> validationErrors) 
            : base(ActionStatus.NotAcknowledged, notification)
        {
            ErrorMessage = errorMessage;
            ValidationError = validationErrors.Invoke(new ValidationErrorBuilder());
        }

        public ActionExecutionNotAcknowledged(string errorMessage, 
            Func<ValidationErrorBuilder, IDictionary<string, List<string>>> validationErrors) 
            : base(ActionStatus.NotAcknowledged, null)
        {
            ErrorMessage = errorMessage;
            ValidationError = validationErrors.Invoke(new ValidationErrorBuilder());
        }

        public ActionExecutionNotAcknowledged(string errorMessage, ValidationErrorBuilder errorBuilder, 
            INotification? notification = null) 
            : base(ActionStatus.NotAcknowledged, notification)
        {
            ErrorMessage = errorMessage;
            ValidationError = errorBuilder.Build();
        }

        public ActionExecutionNotAcknowledged(string errorMessage, IDictionary<string, List<string>> validationErrors, 
            INotification? notification = null) 
            : base(ActionStatus.NotAcknowledged, notification)
        {
            ErrorMessage = errorMessage;
            ValidationError = validationErrors;
        }
    }
}
