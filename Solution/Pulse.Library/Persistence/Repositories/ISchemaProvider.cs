using Database.Models.Schema;

namespace Database.Repositories
{
    
    /// Provides a SchemaVersion.
    
    public interface ISchemaProvider
    {
        
        /// Returns a SchemaVersion or null if none exists.
        
        public SchemaVersion? SchemaVersion { get; }
    }
}