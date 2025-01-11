using Database.Models.Schema;

namespace Service.Schema.Service;


/// Interfaces for listeners of events in ISchemaService.

public interface ISchemaServiceListener
{
    
    /// Notifies the listener of a new schema version.
    
    /// <param name="currentVersion">current schema version</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdatedSchema(SchemaVersion currentVersion, CancellationToken cancellationToken = default);
}