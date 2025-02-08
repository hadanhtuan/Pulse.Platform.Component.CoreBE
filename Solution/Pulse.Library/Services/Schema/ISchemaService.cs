using Pulse.Library.Core.Schema;

namespace Pulse.Library.Services.Schema;

/// Provides support for reading and updating the schema.
public interface ISchemaService
{
        
    /// Fetches the current schema.
    /// <returns>The current schema</returns>
    SchemaVersion ReadSchema();

        
    /// Validates newSchema then updates the current schema with changes made in
    /// newSchema. Performs only validation and checks for changes
    /// if validateOnly is true.
    Task<SchemaUpdateResult> UpdateSchema(
        SchemaVersion newSchema, bool validateOnly, CancellationToken cancellationToken);

        
    /// Check if the entity type exists in schema.
    bool CheckIfEntityExists(string entityInternalName);
}