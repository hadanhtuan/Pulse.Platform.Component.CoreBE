namespace Pulse.Library.Core.Schema;


/// A version of the schema of the data model defining entity types in the AODB.
public class SchemaVersion
{
    public string? Note { get; set; }

    public int Version { get; set; }

    public string? BuildNumber { get; set; }

    public DateTime ValidFrom { get; set; }

    public virtual IEnumerable<EntityType> EntityTypes { get; set; }
        = new List<EntityType>();

    public virtual IEnumerable<EntityType> ComplexDataTypes { get; set; }
        = new List<EntityType>();
}