using System;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json
{
    /// <summary>
    /// Abstract base classe that contains the shared properties of enrichments such as
    /// <see cref="EntityValueAlert"/> and <see cref="EntityAttributeEnrichmentValue"/>.
    /// </summary>
    public abstract class EnrichmentValueBase
    {
        protected EnrichmentValueBase(string causedBy, Guid enrichmentContextId)
        {
            CausedBy = causedBy;
            EnrichmentContextId = enrichmentContextId;
        }

        /// <summary>
        /// The time when this Enrichment was created. 
        /// </summary>
        [JsonInclude]
        public DateTime Created { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Contains a stack trace for the rule that caused the value to be set.
        /// </summary>
        [JsonInclude]
        public string CausedBy { get; internal set; }

        /// <summary>
        /// This property contains the Entity Id of the root entity that is causing the business rules enrichment.
        /// Typically, this corresponds to the Id of the EntityValue that the enrichment is saved on, but some root 
        /// entities are allowed to also assign enriched values on related entities (e.g. Visits that enrich Flight Legs)
        /// </summary>
        /// <remarks>
        /// This property allows the pipeline to consider which enrichments came from which step/context, which is necessary
        /// when determining which enrichment values to reset and which to keep.
        /// </remarks>
        [JsonInclude]
        public Guid EnrichmentContextId { get; private set; }

    }
}
