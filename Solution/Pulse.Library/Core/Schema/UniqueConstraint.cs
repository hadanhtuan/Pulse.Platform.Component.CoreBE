namespace Pulse.Library.Core.Schema;

public class UniqueConstraint
{
    /// The name of the UniqueConstraint displayed in the UI as part of the error message if an update fails due to this Unique Constraint.
    public string DisplayName { get; set; } = null!;

    public string? Description { get; set; }

    /// Attempting to use spaces, capital letters, other special characters or numbers will give an error.
    public string InternalName { get; set; } = null!;

    /// If a UniqueFilter should only be valid for a subset of the rows,
    /// this field can be used to express a row filter in Postgresql syntax (e.g. expressed as a where clause).
    public string? RowFilterExpression { get; set; }

    /// Typically just contains the InternalName of one of the attribute types
    public List<string> ColumnExpressions { get; set; } = [];
}