﻿using System;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json
{
    /// <summary>
    /// Alerts can be placed on <see cref="EntityValue"/> by business rules to indicate 
    /// that something unexpected has happened and (may) need manual correction or attention.
    /// </summary>
    public class EntityValueAlert : EnrichmentValueBase
    {
        /// <summary>
        /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
        /// in order to use <see cref="System.Text.Json.Serialization.ReferenceHandler.Preserve"/>.
        /// </summary>
        [JsonConstructor]
        [Obsolete("This constructor is only intended for the System.Text.Json.JsonSerializer.")]
        public EntityValueAlert() : base(string.Empty, Guid.Empty)
        {
            AlertType = string.Empty;
            AlertTypeDescription = string.Empty;
            AlertMessage = string.Empty;
        }

        public EntityValueAlert(
            string alertType, 
            string alertTypeDescription, 
            string alertMessage, 
            string causedBy, 
            Guid enrichmentContextId,
            bool isExistingAlert = false)
            : base(causedBy, enrichmentContextId)
        {
            AlertType = alertType;
            AlertTypeDescription = alertTypeDescription;
            AlertMessage = alertMessage;
                        
            if (!isExistingAlert)
            {
                // For new alerts, this marks them with AlertStatusHasChanged
                // (unless they are inactivated before saving them)
                previousLastChangedState = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// The type of alert, used to more easily identify the alert.
        /// </summary>
        [JsonInclude]
        public string AlertType { get; private set; }

        /// <summary>
        /// The description of a specific alert type. For example what the "Cdm01" alert type constitutes.
        /// </summary>
        public string AlertTypeDescription { get; set; }

        /// <summary>
        /// This is the message describing the reason for why the alert was raised.
        /// </summary>
        public string AlertMessage { get; set; }

        /// <summary>
        /// The severity level of the alert, see <see cref="AlertLevel"/> for possible values.
        /// </summary>
        public AlertLevel AlertLevel { get; set; } = AlertLevel.Info;

        /// <summary>
        /// The status of the alert, see <see cref="AlertStatus"/> for possible values.
        /// </summary>
        /// <remarks>
        /// Alerts can only be created as active and then be inactivated/reactived by the pipeline logic.
        /// </remarks>
        [JsonInclude]
        public AlertStatus AlertStatus { get; internal set; } = AlertStatus.Active;

        /// <summary>
        /// Allows an active alert to be hidden.
        /// </summary>
        public bool Hidden { get; set; } = false;

        /// <summary>
        /// This indicates the last time the alert changed state, either from inactive to active, or vice versa.
        /// </summary>
        [JsonInclude]
        public DateTime LastChangedState { get; internal set; } = DateTime.UtcNow;

        /// <summary>
        /// Returns true if this alert has changed <see cref="AlertStatus"/> since it was loaded/created.
        /// </summary>
        /// <remarks>
        /// Will return false if the <see cref="AlertStatus"/> has been toggled back and forth and returned to the value the 
        /// Alert was created/loaded with.
        /// </remarks>
        public bool AlertStatusHasChanged { get { return previousLastChangedState.HasValue; } }
                
        /// <summary>
        /// Used to store the previous <see cref="LastChangedState"/> when <see cref="AlertStatus"/> changes value,
        /// in case we need to return to the LastChangedState if the status is toggled back.
        /// </summary>
        /// <remarks>
        /// Will be null when the <see cref="AlertStatus"/> has the same value as it was created/loaded with.
        /// </remarks>
        private DateTime? previousLastChangedState;

        /// <summary>
        /// Updates the <see cref="AlertStatus"/> to the new value and also updates <see cref="LastChangedState"/> if the
        /// status value has changed.
        /// </summary>
        /// <remarks>
        /// Uses the private <see cref="previousLastChangedState"/> to keep the value for <see cref="LastChangedState"/> if
        /// the status is toggled back and forth between Active/Inactive and ends in the initial state it had.
        /// </remarks>
        public void SetAlertStatus(AlertStatus newStatus)
        {
            if (AlertStatus != newStatus)
            {
                AlertStatus = newStatus;
                if (previousLastChangedState.HasValue)
                {
                    LastChangedState = previousLastChangedState.Value;
                    previousLastChangedState = null;
                }
                else
                {
                    previousLastChangedState = LastChangedState;
                    LastChangedState = DateTime.UtcNow;
                }
            }
        }

        /// <summary>
        /// This is optional and can be used to indicate for which user roles the alert is relevant. 
        /// </summary>
        public string? UserRole { get; set; }

        /// <summary>
        /// This is optional and can be used to show where the business rules
        /// looked for master data, when the alert was raised.
        /// </summary>
        public string? MasterDataSourceTable { get; set; }

        /// <summary>
        /// This is optional and is to be used in conjunction with <see cref="MasterDataSourceTable"/>.
        /// It contains the value used in the lookup from master data. 
        /// </summary>
        public string? MasterDataLookUpValue { get; set; }

    }
}
