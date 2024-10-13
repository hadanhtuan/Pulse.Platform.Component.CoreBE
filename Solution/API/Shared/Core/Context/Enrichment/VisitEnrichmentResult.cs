
using API.Shared.Core.Interfaces.Context.Enrichment;
using API.Shared.Core.Interfaces.Rules.VisitEnrichment;

namespace API.Shared.Core.Context.Enrichment
{
    /// <inheritdoc/>
    public class VisitEnrichmentResult : IVisitEnrichmentResult
    {
        /// <inheritdoc/>
        public IEnrichedRuleEntity EnrichedVisit { get; set; } = null!;
        /// <inheritdoc/>
        public IEnrichedRuleEntity? EnrichedInboundFlight { get; set; } = null!;
        /// <inheritdoc/>
        public IEnrichedRuleEntity? EnrichedOutboundFlight { get; set; } = null!;
        /// <inheritdoc/>
        public IList<IEnrichedRuleEntity> EnrichedGroundLegs { get; set; } = null!;
    }
}