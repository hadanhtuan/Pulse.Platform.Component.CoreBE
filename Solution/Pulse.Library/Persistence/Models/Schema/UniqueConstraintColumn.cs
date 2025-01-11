using Library.Persistence.EntityTypes;

namespace Database.Models.Schema
{
    
    /// A UniqueConstraint is defined by a set of attributes (columns),
    /// or expressions on these (such as the date part of a DateTime), that must be unique.
    
    public class UniqueConstraintColumn : Entity
    {
        
        /// Primary key for related UniqueConstraint
        
        public virtual UniqueConstraint UniqueConstraint { get; set; } = null!;
        
        
        /// Typically just contains the InternalName of one of the attribute types
        /// related to the RootEntityType that the UniqueConstraint is related to.
        
        public string ColumnExpression { get; set; } = null!;
    }
}
