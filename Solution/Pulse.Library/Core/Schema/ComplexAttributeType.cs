namespace Pulse.Library.Core.Schema;

public class ComplexAttributeType : AttributeType
{

    public string ComplexDataTypeInternalName { get; set; } = null!;

    public bool IsCollection { get; set; }

    public string? AdvancedFilteringDefinition { get; set; }
}