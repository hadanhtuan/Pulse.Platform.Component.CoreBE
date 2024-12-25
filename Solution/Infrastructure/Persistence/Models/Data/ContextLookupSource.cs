namespace Database.Models.Data
{
    /// <summary>
    /// A ContextLookupSource distinguishes the step in the pipeline from
    /// which data have been looked up from the context (either in the AODB
    /// or in master data).
    /// </summary>
    public enum ContextLookupSource
    {
        EntityEnrichment,
        OutboundLinking,
        InboundLinking,
        OutboundVisitEnrichment,
        InboundVisitEnrichment,
        AlertEnrichment
    }
}