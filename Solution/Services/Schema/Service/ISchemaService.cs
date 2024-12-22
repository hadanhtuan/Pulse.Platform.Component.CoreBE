using Database.Models.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Schema.Service
{
    /// <summary>
    /// Provides support for reading and updating the schema.
    /// </summary>
    public interface ISchemaService
    {
        /// <summary>
        /// Fetches the current schema.
        /// </summary>
        /// <returns>The current schema</returns>
        SchemaVersion ReadSchema();

        /// <summary>
        /// Validates <paramref name="newSchema"/> then updates the current schema with changes made in
        /// <paramref name="newSchema"/>. Performs only validation and checks for changes
        /// if <paramref name="validateOnly"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="newSchema">The new schema</param>
        /// <param name="validateOnly">Whether to only validate the new schema and check if it has changed
        /// compared to the current schema; the schema is not actually changed.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Result with current <see cref="SchemaVersion"/> and whether the schema was changed,
        /// including whether any of the changes could case disruptions</returns>
        Task<SchemaUpdateResult> UpdateSchema(
            SchemaVersion newSchema, bool validateOnly, CancellationToken cancellationToken);

        /// <summary>
        /// Check if the entity type exists in schema.
        /// </summary>
        /// <param name="typeName">Internal type name</param>
        /// <returns>Does the type exist</returns>
        bool CheckIfTypeExists(string typeName);
    }
}