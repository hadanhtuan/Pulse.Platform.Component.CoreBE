using Library.Persistence.EntityTypes;
using System;

namespace Database.Models.Data
{
    
    /// The processing state of an <see cref="EntityValue"/>, which is used to manage recalculation
    /// of the entity. If an <see cref="EntityValueProcessingState"/> is not created and persisted
    /// for an entity, the entity will not be recalculated.
    
    public class EntityValueProcessingState : AggregateRoot
    {
        
        /// The id of the EntityValue.
        
        /// <remarks>
        /// The relationship is deliberately not modeled so as to avoid any 
        /// unintentional references to instances of this state class.</remarks>
        public Guid EntityValueId { get; set; }

        
        /// The processing status of this EntityValue.
        
        public ProcessingStatus ProcessingStatus { get; set; } = ProcessingStatus.Unspecified;

        
        /// The time of the last update of the processing status. 
        
        /// <remarks>
        /// This will always be set when an EntityValue has been processed, but may be <see langword="null"/>
        /// for entities that have not been updated since the property was added.
        /// </remarks>
        public DateTime? ProcessingStatusLastUpdated { get; set; }

        
        /// The reasons for processing the EntityValue. 
        
        public ProcessingReasons ProcessingReasons { get; set; }

        
        /// The reasons provided from a rule triggering recalculation of this EntityValue.
        /// String containing possibly multiple reasons separated by "||".
        /// Also contains reasons from <see cref="ScheduledRecalculationReason"/> when the processing
        /// has been requested or is pending because of scheduled recalculation.
        
        public string? ProcessingRuleTriggerReasons { get; set; }

        
        /// The id of the latest updating transaction - PostgreSQL system column 
        /// used as ConcurrencyToken to prevent parallel updates of the same object.
        
        public uint xmin { get; set; }
    }
}