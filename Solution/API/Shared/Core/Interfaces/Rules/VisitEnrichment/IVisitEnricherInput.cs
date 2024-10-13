using API.Shared.Core.Interfaces.Context.Enrichment;

namespace API.Shared.Core.Interfaces.Rules.VisitEnrichment
{
    /// <summary>
    /// The message that is sent to the IVisitEnricher to trigger the Visit enrichment business rules.
    /// </summary>
    public interface IVisitEnricherInput
    {
        public IRuleEntityAdapter VisitAdapter { get; }
        public IRuleEntityAdapter? InboundFlightAdapter { get; }
        public IRuleEntityAdapter? OutboundFlightAdapter { get; }
        public IEnumerable<IRuleEntityAdapter> GroundLegAdapters { get; }

        public int? Priority { get; }
        public string? Reason { get; }
    }
}