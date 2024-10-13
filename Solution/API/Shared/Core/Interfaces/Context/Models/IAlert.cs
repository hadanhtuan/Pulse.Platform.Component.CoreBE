using System;

namespace API.Shared.Core.Interfaces.Context.Models
{
    /// <summary>
    /// An alert that can be raised from business rules.
    /// </summary>
    public interface IAlert
    {
        /// <summary>
        /// This is the message describing the reason for why the alert was raised.
        /// </summary>
        public string AlertMessage { get; }

        /// <summary>
        /// The status of the alert, which is either Active or Inactive.
        /// </summary>
        public AlertStatus AlertStatus { get; }

        /// <summary>
        /// The severity level of the alert. Can be Info, Low, Medium or High.
        /// </summary>
        public AlertLevel AlertLevel { get; }

        /// <summary>
        /// The type of alert, used to more easily identify the alert.
        /// </summary>
        public Enum AlertType { get; }

        /// <summary>
        /// The description of a specific alert type. For example what the "Cdm01" alert type constitutes.
        /// </summary>
        public string AlertTypeDescription { get; }

        /// <summary>
        /// The string here is the "RuleStackTrace".
        /// </summary>
        public string CausedBy { get; }

        /// <summary>
        /// This indicates the last changed the alert changed state, either from inactive to active, or vice versa.
        /// </summary>
        public DateTime LastChangedState { get; }

        /// <summary>
        /// This is used to indicate for which user roles the alert is relevant. 
        /// </summary>
        public string? UserRole { get; }

        /// <summary>
        /// This is optional and can be used to show where the business rules looked for master data, 
        /// when the alert was raised.
        /// </summary>
        public string? MasterdataSourceTable { get; }

        /// <summary>
        /// This is optional and can be used to show where the business rules looked for master data, 
        /// when the alert was raised.
        /// </summary>
        public Guid? MasterdataSourceId { get; }

        /// <summary>
        /// This is optional and can be used to show what value was used
        /// looking for master data, when the alert was raised.
        /// </summary>
        public string? LookUpValue { get; }
    }
}