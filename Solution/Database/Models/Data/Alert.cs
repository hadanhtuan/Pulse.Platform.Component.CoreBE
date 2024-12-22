using System;
using Library.Persistence.EntityTypes;

namespace Database.Models.Data
{
    /// <summary>
    /// Alerts are created whenever the user should be made aware of some
    /// faulty state in a business perspective. Alerts can either be
    /// EntityValueAlerts (for example an alert raised on a FlightLeg) or
    /// RawMessageAlerts (for example an alert raised on an unmatched message). 
    /// </summary>
    public abstract class Alert : Entity
    {
        /// <summary>
        /// This is the message describing the reason for why the alert was raised.
        /// </summary>
        public string AlertMessage { get; set; } = null!;

        /// <summary>
        /// The status of the alert, see <see cref="AlertStatus"/> for possible values.
        /// </summary>
        public AlertStatus AlertStatus { get; set; }

        /// <summary>
        /// The severity level of the alert, see <see cref="AlertLevel"/> for possible values.
        /// </summary>
        public AlertLevel AlertLevel { get; set; }

        /// <summary>
        /// The type of alert, used to more easily identify the alert.
        /// </summary>
        public string AlertType { get; set; } = null!;

        /// <summary>
        /// The description of a specific alert type. For example what the "Cdm01" alert type constitutes.
        /// </summary>
        public string AlertTypeDescription { get; set; } = null!;

        /// <summary>
        /// The string here is the "RuleStackTrace".
        /// </summary>
        public string CausedBy { get; set; } = null!;

        /// <summary>
        /// This indicates the last changed the alert changed state, either from inactive to active, or vice versa.
        /// </summary>
        public DateTime LastChangedState { get; set; }

        /// <summary>
        /// This is used to indicate for which user roles the alert is relevant. 
        /// </summary>
        public string? UserRole { get; set; }

        /// <summary>
        /// This is optional and can be used to show where the business rules
        /// looked for master data, when the alert was raised.
        /// </summary>
        public string? MasterDataSourceTable { get; set; }

        /// <summary>
        /// This is optional and is to be used in conjunction with MasterDataSourceTable.
        /// It contains the value used in the look up from master data. 
        /// </summary>
        public string? LookUpValue { get; set; }

        /// <summary>
        /// This is optional and can be used to show where the business rules
        /// looked for master data, when the alert was raised.
        /// </summary>
        public Guid? MasterDataSourceId { get; set; }
    }
}