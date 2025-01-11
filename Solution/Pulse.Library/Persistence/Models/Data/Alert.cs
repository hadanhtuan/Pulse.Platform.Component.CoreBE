using System;
using Library.Persistence.EntityTypes;

namespace Database.Models.Data
{
    
    /// Alerts are created whenever the user should be made aware of some
    /// faulty state in a business perspective. Alerts can either be
    /// EntityValueAlerts (for example an alert raised on a FlightLeg) or
    /// RawMessageAlerts (for example an alert raised on an unmatched message). 
    
    public abstract class Alert : Entity
    {
        
        /// This is the message describing the reason for why the alert was raised.
        
        public string AlertMessage { get; set; } = null!;

        
        /// The status of the alert, see <see cref="AlertStatus"/> for possible values.
        
        public AlertStatus AlertStatus { get; set; }

        
        /// The severity level of the alert, see <see cref="AlertLevel"/> for possible values.
        
        public AlertLevel AlertLevel { get; set; }

        
        /// The type of alert, used to more easily identify the alert.
        
        public string AlertType { get; set; } = null!;

        
        /// The description of a specific alert type. For example what the "Cdm01" alert type constitutes.
        
        public string AlertTypeDescription { get; set; } = null!;

        
        /// The string here is the "RuleStackTrace".
        
        public string CausedBy { get; set; } = null!;

        
        /// This indicates the last changed the alert changed state, either from inactive to active, or vice versa.
        
        public DateTime LastChangedState { get; set; }

        
        /// This is used to indicate for which user roles the alert is relevant. 
        
        public string? UserRole { get; set; }

        
        /// This is optional and can be used to show where the business rules
        /// looked for master data, when the alert was raised.
        
        public string? MasterDataSourceTable { get; set; }

        
        /// This is optional and is to be used in conjunction with MasterDataSourceTable.
        /// It contains the value used in the look up from master data. 
        
        public string? LookUpValue { get; set; }

        
        /// This is optional and can be used to show where the business rules
        /// looked for master data, when the alert was raised.
        
        public Guid? MasterDataSourceId { get; set; }
    }
}