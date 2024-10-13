using API.Shared.Core.Interfaces.Context.Enrichment;

namespace API.Shared.Core.Interfaces.Rules.VisitEnrichment
{
    /// <summary>
    /// The result returned from the IVisitEnricher after visit enrichment has been performed.
    /// </summary>
    public interface IVisitEnrichmentResult
    {
        /// <summary>
        /// The enriched entity, which contains the EEAVs, alerts, and recalculations that
        /// have been added during enrichment.
        /// </summary>
        public IEnrichedRuleEntity EnrichedVisit { get; }

        /// <summary>
        /// The enriched inbound flight leg, which contains the EEAVs, alerts, and recalculations that
        /// have been added during enrichment.
        /// </summary>
        public IEnrichedRuleEntity? EnrichedInboundFlight { get; }

        /// <summary>
        /// The enriched outbound flight leg, which contains the EEAVs, alerts, and recalculations that
        /// have been added during enrichment.
        /// </summary>
        public IEnrichedRuleEntity? EnrichedOutboundFlight { get; }

        /// <summary>
        /// A list with the enriched ground legs,  which contains the EEAVs, alerts, and recalculations that
        /// have been added during enrichment.
        /// </summary>
        public IList<IEnrichedRuleEntity> EnrichedGroundLegs { get; }
    }
}