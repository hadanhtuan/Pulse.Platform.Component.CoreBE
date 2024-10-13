using System;

namespace API.Shared.Core.Interfaces.Context.Enrichment
{
    /// <summary>
    /// Carries information from business rules about when in the future the entity
    /// should be recalculated and why it should be recalculated
    /// </summary>
    public interface IScheduledRecalculationTrigger
    {
        /// <summary>
        /// The future time that the entity should be triggered for recalculation
        /// </summary>
        public DateTime ScheduledTime { get; }

        /// <summary>
        /// The reason why the entity should be recalculated
        /// </summary>
        public string Reason { get; }
    }
}
