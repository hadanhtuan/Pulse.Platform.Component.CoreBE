using Library.Persistence.EntityTypes;

namespace Database.Models.Schema
{
    /// <summary>
    /// A UniqueConstraint is defined by a set of attributes (columns),
    /// or expressions on these (such as the date part of a DateTime), that must be unique.
    /// </summary>
    public class UniqueConstraintColumn : Entity
    {
        /// <summary>
        /// Primary key for related UniqueConstraint
        /// </summary>
        public virtual UniqueConstraint UniqueConstraint { get; set; } = null!;
        
        /// <summary>
        /// Typically just contains the InternalName of one of the attribute types
        /// related to the RootEntityType that the UniqueConstraint is related to.
        /// </summary>
        public string ColumnExpression { get; set; } = null!;
    }
}
