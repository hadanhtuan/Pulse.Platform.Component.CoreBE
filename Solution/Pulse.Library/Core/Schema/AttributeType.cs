using System.ComponentModel.DataAnnotations;

namespace Pulse.Library.Core.Schema;

/// Attribute type in the AODB Schema.
public class AttributeType
{
    public string InternalName { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? Description { get; set; }

    public string? Group { get; set; }

    public bool IsRequired { get; set; }

    public Status Status { get; set; }

    public DataType DataType { get; set; }
}