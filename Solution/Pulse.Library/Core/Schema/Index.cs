namespace Pulse.Library.Core.Schema;

/// An index is defined by a list of columns, which is used for performing queries on the related EntityType.
public class Index
{
    public string Name { get; set; } = null!;

    public string Columns { get; set; } = null!;
}