namespace Database.Models.Schema.Modifications
{
    /// <summary>
    /// A removed <see cref="Database.Models.Schema.UniqueConstraint"/> from an <see cref="EntityType"/>.
    /// </summary>
    public interface IRemovedUniqueConstraint : IEntityModification
    {
        /// <summary>
        /// The removed <see cref="Database.Models.Schema.UniqueConstraint"/>.
        /// </summary>
        UniqueConstraint UniqueConstraint { get; }
    }
}