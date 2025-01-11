using Pulse.Library.Persistence.Models.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pulse.Library.CLI.Generator.Models;

/// Attribute type in the AODB Schema.
public class AttributeTypeView
{
    /// Internal name used as a unique identifier for the attribute type.
    [MaxLength(63)]
    [RegularExpression(@"^[a-z_]{0,63}$")]
    public string InternalName { get; set; } = null!;

    /// Internal name in plural of the attribute type.
    public string? PluralName { get; set; }

    /// Short-form name of the attribute type as displayed in a user interface.
    public string? ShortDisplayName { get; set; }

    /// Long-form name of the attribute type as displayed in a user interface.
    public string? LongDisplayName { get; set; }
        
    /// Description of the attribute type.
    public string? Description { get; set; }

    /// Group that the attribute type belongs to;
    /// used to access the attribute in business rules.
    public string? Group { get; set; }

    /// The status of the attribute type.
    public Status Status { get; set; }
}