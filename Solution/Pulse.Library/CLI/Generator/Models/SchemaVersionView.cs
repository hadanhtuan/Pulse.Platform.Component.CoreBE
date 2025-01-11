namespace Pulse.Library.CLI.Generator.Models;


/// A version of the schema of the data model defining entity types in the AODB.
public class SchemaView
{
    public string? Note { get; set; }

    public int Version { get; set; }

    public string? BuildNumber { get; set; }
    
    public DateTime ValidFrom { get; set; }
    
    public virtual IEnumerable<EntityTypeView> EntityTypes { get; set; } 
        = new List<EntityTypeView>();
}