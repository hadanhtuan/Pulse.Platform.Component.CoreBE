using System;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json
{
    /// <summary>
    /// Stores information about when an enrichment rule has performed an attribute value reset.
    /// Provides background information to allow investigatation into why message values have disappeared.
    /// </summary>
    /// <remarks>
    /// When enrichment rules reset attribute values, all message values for the attribute are removed 
    /// (possibly filtered by a message source). Storing these markers makes it possible to investigate when
    /// and why such removals have occurred.
    /// </remarks>
    public class EntityAttributeResetMarker : EnrichmentValueBase
    {
        public EntityAttributeResetMarker(string causedBy, Guid enrichmentContextId) : base(causedBy, enrichmentContextId) { }

        /// <summary>
        /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
        /// in order to use <see cref="ReferenceHandler.Preserve"/>.
        /// </summary>
        [JsonConstructor]
        [Obsolete("This constructor is only intended for the System.Text.Json.JsonSerializer.")]
        public EntityAttributeResetMarker() : this(string.Empty, Guid.Empty) { }

        /// <summary>
        /// Specifies for which message source previous attribute values have been removed.
        /// If null, the previous values have been removed across all message sources.
        /// </summary>
        [JsonInclude]
        public string? Source { get; init; }

    }
}
