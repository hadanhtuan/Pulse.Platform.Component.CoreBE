using System;
using System.Collections.Generic;

namespace API.Shared.Core.Interfaces.Responses
{
    public class ActionExecutionInsufficientInput<TActionConfiguration> : ActionExecutionResult
    {
        public ActionExecutionInsufficientInput(
            object? configuration,
            string errorMessage, 
            Func<ValidationErrorBuilder, 
            IDictionary<string, List<string>>> validationErrors,
            List<string>? insufficientInpuntReasons,
            INotification? notification = null)
            : base(ActionStatus.InsufficientInput, notification)
        {
            Configuration = configuration;
            ErrorMessage = errorMessage;
            ValidationError = validationErrors.Invoke(new ValidationErrorBuilder());
            InsufficientInputReasons = insufficientInpuntReasons;
        }

        public ActionExecutionInsufficientInput(
            List<string>? insufficientInpuntReasons, 
            object? configuration = null,
            INotification? notification = null)
            : base(ActionStatus.InsufficientInput, notification)
        {
            InsufficientInputReasons = insufficientInpuntReasons;
            Configuration = configuration;
        }
    }
}
