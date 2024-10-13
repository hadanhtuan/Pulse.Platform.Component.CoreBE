namespace API.Shared.Core.Interfaces.Rules.VisitEnrichment
{
    public interface IVisitEnricherProvider
    {
        /// <summary>
        /// Attempts to load a VisitEnricher from the Rules.Context assembly if a unique implementation exists.
        /// The VisitEnricher controls the business rules invoke flow
        /// </summary>
        /// <returns>A VisitEnricher or throws an error</returns>
        IVisitEnricher GetVisitEnricher();
    }
}