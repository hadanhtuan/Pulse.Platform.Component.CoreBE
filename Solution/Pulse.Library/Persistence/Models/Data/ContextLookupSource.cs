namespace Database.Models.Data
{
    
    /// A ContextLookupSource distinguishes the step in the pipeline from
    /// which data have been looked up from the context (either in the AODB
    /// or in master data).
    
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