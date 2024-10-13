namespace API.Shared.Core.Interfaces.Rules.VisitEnrichment
{
    /// <summary>
    /// Enriches a visit based on visit enrichment business rules
    /// and the input provided to the business rules.
    /// </summary>
    public interface IVisitEnricher
    {
        Task<IVisitEnrichmentResult> Enrich(IVisitEnricherInput input);
    }
}
