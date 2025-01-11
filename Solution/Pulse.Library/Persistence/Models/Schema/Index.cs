using Library.Persistence.EntityTypes;

namespace Database.Models.Schema
{
    
    /// An index is defined by a list of columns, which is used for performing queries on the related EntityType.
    
    public class Index : Entity
    {
        
        /// Primary key for related EntityType
        
        public virtual EntityType EntityType { get; set; } = null!;

        
        /// Name of the index
        
        public string Name { get; set; } = null!;
        
        /// Columns are defined as a comma-separated list of internal name
        /// of an AttributeType for the EntityType, which are used for querying the EntityType.
        
        public string Columns { get; set; } = null!;
    }
}
