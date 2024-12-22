using Database.Models.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json;

[DebuggerDisplay("Pri={Priority} CausedBy={CausedBy} EnrContextId={EnrichmentContextId}")]
public class EntityAttributeEnrichmentValue : EnrichmentValueBase
{
    public EntityAttributeEnrichmentValue(
        string causedBy,
        Guid enrichmentContextId,
        int priority = 0,
        string? sourceName = null,
        IEnumerable<MasterDataReference>? masterDataReferences = null,
        IEnumerable<EntityAttributeValue>? attributeReferences = null
    )
        : base(causedBy, enrichmentContextId)
    {
        Priority = priority;
        CustomSourceName = sourceName;
        MasterDataReferences = masterDataReferences ?? Array.Empty<MasterDataReference>();
        AttributeReferences = attributeReferences ?? Array.Empty<EntityAttributeValue>();
    }

    /// <summary>
    /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
    /// in order to use <see cref="ReferenceHandler.Preserve"/>.
    /// </summary>
    [JsonConstructor]
    [Obsolete("This constructor is only intended for the System.Text.Json.JsonSerializer.")]
    public EntityAttributeEnrichmentValue() : this(string.Empty, Guid.Empty) { }

    /// <summary>
    /// Represents how important an enriched value is, e.g. that enriched value based on one source is more important than another.
    /// </summary>
    /// <remarks>
    /// Default priority is 0, in which case the most recent value will be chosen as most important value.
    /// </remarks>
    [JsonInclude]
    public int Priority { get; private set; }

    /// <summary>
    /// Source name provided by rules when creating an enrichment. 
    /// </summary>
    [JsonInclude]
    public string? CustomSourceName { get; private set; }

    /// <summary>
    /// Optional array of references to Master Data which the enriched value is based on.
    /// </summary>
    [JsonInclude]
    public IEnumerable<MasterDataReference> MasterDataReferences { get; private set; }

    private IEnumerable<EntityAttributeValue> attributeReferences = Array.Empty<EntityAttributeValue>();

    /// <summary>
    /// Optional array of references to other entity attribute values which the enriched value is based on.
    /// </summary>
    [JsonInclude]
    public IEnumerable<EntityAttributeValue> AttributeReferences
    {
        // Never return archiced references as these are deleted from attribute value collections and not used once they are archived.
        // This behaviour is inherited from relational model.
        // Also, do not return attributes without AttributeType as these are not attributes inside the current EntityValue.
        get => attributeReferences.Where(attr =>
            attr.Status != EntityAttributeStatus.Archived && attr.AttributeType != null);
        private set
        {
            attributeReferences = value;
        }
    }

    public bool IsEquivalentTo(EntityAttributeEnrichmentValue? other) =>
        other != null &&
        EnrichmentContextId == other.EnrichmentContextId &&
        CausedBy == other.CausedBy &&
        Priority == other.Priority &&
        CustomSourceName == other.CustomSourceName &&
        (MasterDataReferences.Count() == other.MasterDataReferences.Count() &&
         MasterDataReferences.Zip(other.MasterDataReferences).All(t => t.First.IsEquivalentTo(t.Second))) &&
        (attributeReferences.Count() == other.attributeReferences.Count() &&
         attributeReferences.Zip(other.attributeReferences).All(t => t.First.IsEquivalentTo(t.Second)));

    public EntityAttributeValue? GetFirstLeafAttributeReference()
    {
        var aodbRef = AttributeReferences.FirstOrDefault();

        // As we only output one AODB reference (not nested), we recursively dig down to the lowest
        // attribute reference to get to a possible system message (through possible prioritizations).
        while (aodbRef?.Enrichment?.AttributeReferences.Any() == true)
        {
            aodbRef = aodbRef.Enrichment.AttributeReferences.First();
        }

        return aodbRef;
    }
}