using System.ComponentModel.DataAnnotations;

namespace Pulse.Library.Core.Schema;

/// Attribute type in the AODB Schema.
public class AttributeType
{
    /// Internal name used as a unique identifier for the attribute type.
    [MaxLength(63)]
    [RegularExpression(@"^[a-z_]{0,63}$")]
    public string InternalName { get; set; } = null!;

    /// Short-form name of the attribute type as displayed in a user interface.
    public string? ShortDisplayName { get; set; }

    /// Long-form name of the attribute type as displayed in a user interface.
    public string? LongDisplayName { get; set; }
        
    /// Description of the attribute type.
    public string? Description { get; set; }

    /// Group that the attribute type belongs to;
    /// used to access the attribute in business rules.
    public string? Group { get; set; }

    public Status Status { get; set; }
}