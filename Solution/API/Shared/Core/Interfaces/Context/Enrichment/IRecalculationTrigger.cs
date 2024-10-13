namespace API.Shared.Core.Interfaces.Context.Enrichment
{
    /// <summary>
    /// Carries information from the business rules about an entity that should be triggered
    /// for recalculation - and why it should be recalculated - as soon as the pipeline run is complete. 
    /// </summary>
    public interface IRecalculationTrigger
    {
        public Guid Identifier { get; }

        public string Reason { get; }
    }
}
