using System;
using System.Text.Json.Serialization;

namespace Database.Models.Data
{
    
    /// The EntityValueAlert are alerts raised for EntityValues, which can for example be FlightLegs or Visits. These alerts have a reference to the EntityValue for
    /// which they were raised, as well as a reference to the context they were raised in, see EnrichmentContext. 
    
    public class EntityValueAlert : Alert
    {
        
        /// A reference to the EntityValue that this alert was raised for.
        
        [JsonIgnore]
        public virtual EntityValue? EntityValueWithAlert { get; set; }

        
        /// This indicates in which context the alert was set, for example a FlightLeg- or Visit context. This can be different from the EntityValueWithAlert.
        
        [JsonIgnore]
        public virtual EntityValue EnrichmentContext { get; set; } = null!;

        
        /// The Id of the EntityValue that EnrichmentContext refers to.
        
        public Guid EnrichmentContextId { get; set; }

    }
}