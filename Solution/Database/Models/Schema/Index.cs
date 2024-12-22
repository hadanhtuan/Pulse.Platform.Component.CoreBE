using Library.Persistence.EntityTypes;

namespace Database.Models.Schema
{
    /// <summary>
    /// An index is defined by a list of columns, which is used for performing queries on the related EntityType.
    /// </summary>
    public class Index : Entity
    {
        /// <summary>
        /// Primary key for related EntityType
        /// </summary>
        public virtual EntityType EntityType { get; set; } = null!;

        /// <summary>
        /// Name of the index
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Columns are defined as a comma-separated list of internal name
        /// of an AttributeType for the EntityType, which are used for querying the EntityType.
        /// </summary>
        public string Columns { get; set; } = null!;
    }
}
