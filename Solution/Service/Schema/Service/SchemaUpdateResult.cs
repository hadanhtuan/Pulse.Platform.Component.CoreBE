using Database.Models.Schema;

namespace Service.Schema.Service;


/// Result from calling <see cref="ISchemaService.UpdateSchema(SchemaVersion, bool, System.Threading.CancellationToken)"/>.

public class SchemaUpdateResult
{
    public SchemaUpdateResult(SchemaVersion schemaVersion, bool hasChanges, bool isDisruptive)
    {
        SchemaVersion = schemaVersion;
        HasChanges = hasChanges;
        HasDisruptiveChanges = isDisruptive;
    }

    
    /// The current schema after the update.
    
    public SchemaVersion SchemaVersion { get; }

    
    /// Returns <see langword="true"/> if the schema contained any changes compared to the 
    /// existing schema. 
    
    public bool HasChanges { get; }

    
    /// Returns <see langword="true"/> if the schema contained any changes compared to
    /// the existing schema that cause updates to the database that could possibly disrupt 
    /// operation of the component.
    
    public bool HasDisruptiveChanges { get; }
}