using Library.Persistence.EntityTypes;
using System.Collections.Generic;
using System.Linq;

namespace Database.Models.Schema
{
    /// <summary>
    /// A RootEntityType can have a number of Unique Constraints,
    /// which each determine that certain attributes (or expressions on attributes) must be unique
    /// </summary>
    public class UniqueConstraint : Entity
    {
        /// <summary>
        /// Primary key for related EntityType
        /// </summary>
        public virtual EntityType EntityType { get; set; } = null!;

        /// <summary>
        /// The name of the UniqueConstraint displayed in the UI as part of the error message if an update fails due to this Unique Constraint.
        /// </summary>
        public string DisplayName { get; set; } = null!;

        /// <summary>
        /// A description of the specific Unique Constraint. Used for further information
        /// as part of the error message if an update fails due to this Unique Constraint.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// A unique identifier, used internally by the system. Only lowercase English letters (a-z) are accepted,
        /// as well as the special character _.
        /// Attempting to use spaces, capital letters, other special characters or numbers will give an error.
        /// </summary>
        public string InternalName { get; set; } = null!;

        /// <summary>
        /// If a UniqueFilter should only be valid for a subset of the rows,
        /// this field can be used to express a row filter in Postgresql syntax (e.g. expressed as a where clause).
        /// </summary>
        public string? RowFilterExpression { get; set; }

        /// <summary>
        /// Typically just contains the InternalName of one of the attribute types
        /// related to the RootEntityType that the UniqueConstraint is related to.
        /// </summary>
        public virtual ICollection<UniqueConstraintColumn> ColumnExpressions { get; set; } = new List<UniqueConstraintColumn>();

        /// <summary>
        /// Returns whether this is equivalent to an <paramref name="other"/> <see cref="UniqueConstraint"/>
        /// in terms of <see cref="InternalName"/>, <see cref="RowFilterExpression"/> and <see cref="ColumnExpressions"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns><see langword="true"/> if this is interchangeable with other</returns>
        public bool IsEquivalentTo(UniqueConstraint other)
        {
            return InternalName == other.InternalName &&
                RowFilterExpression == other.RowFilterExpression &&
                Enumerable.SequenceEqual(
                    ColumnExpressions.OrderBy(c => c.ColumnExpression).Select(c => c.ColumnExpression),
                    other.ColumnExpressions.OrderBy(c => c.ColumnExpression).Select(c => c.ColumnExpression));
        }
    }
}