using Database.Models.Schema;

namespace Database.Repositories
{
    /// <summary>
    /// Provides a SchemaVersion.
    /// </summary>
    public interface ISchemaProvider
    {
        /// <summary>
        /// Returns a SchemaVersion or null if none exists.
        /// </summary>
        public SchemaVersion? SchemaVersion { get; }
    }
}