using Database.Models.Schema;

namespace Service.Schema.Service;

/// <summary>
/// Interfaces for listeners of events in ISchemaService.
/// </summary>
public interface ISchemaServiceListener
{
    /// <summary>
    /// Notifies the listener of a new schema version.
    /// </summary>
    /// <param name="currentVersion">current schema version</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdatedSchema(SchemaVersion currentVersion, CancellationToken cancellationToken = default);
}