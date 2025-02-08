namespace Pulse.Library.Core.Schema;

public class UniqueConstraint
{
    public string DisplayName { get; set; } = null!;

    public string? Description { get; set; }

    public string InternalName { get; set; } = null!;

    public string? RowFilterExpression { get; set; }

    public List<string> ColumnExpressions { get; set; } = [];
}