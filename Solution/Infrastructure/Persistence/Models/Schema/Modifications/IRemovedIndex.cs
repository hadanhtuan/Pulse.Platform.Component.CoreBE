namespace Database.Models.Schema.Modifications
{
    /// <summary>
    /// A removed <see cref="Database.Models.Schema.Index"/> from an <see cref="EntityType"/>.
    /// </summary>
    public interface IRemovedIndex : IEntityModification
    {
        /// <summary>
        /// The removed <see cref="Database.Models.Schema.Index"/>.
        /// </summary>
        Index Index { get; }
    }
}