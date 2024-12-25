using System;
using System.Text.Json.Serialization;

namespace Database.Models.Data
{
    /// <summary>
    /// The EntityValueAlert are alerts raised for EntityValues, which can for example be FlightLegs or Visits. These alerts have a reference to the EntityValue for
    /// which they were raised, as well as a reference to the context they were raised in, see EnrichmentContext. 
    /// </summary>
    public class EntityValueAlert : Alert
    {
        /// <summary>
        /// A reference to the EntityValue that this alert was raised for.
        /// </summary>
        [JsonIgnore]
        public virtual EntityValue? EntityValueWithAlert { get; set; }

        /// <summary>
        /// This indicates in which context the alert was set, for example a FlightLeg- or Visit context. This can be different from the EntityValueWithAlert.
        /// </summary>
        [JsonIgnore]
        public virtual EntityValue EnrichmentContext { get; set; } = null!;

        /// <summary>
        /// The Id of the EntityValue that EnrichmentContext refers to.
        /// </summary>
        public Guid EnrichmentContextId { get; set; }

    }
}