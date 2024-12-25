namespace Database.Models.Schema.Modifications
{
    /// <summary>
    /// A <see cref="Database.Models.Schema.UniqueConstraint"/> added to an <see cref="EntityType"/>.
    /// </summary>
    public interface IAddedUniqueConstraint : IEntityModification
    {
        /// <summary>
        /// The added <see cref="Database.Models.Schema.UniqueConstraint"/>.
        /// </summary>
        UniqueConstraint UniqueConstraint { get; }
    }
}