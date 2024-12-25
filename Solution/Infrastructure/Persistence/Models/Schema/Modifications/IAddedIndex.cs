namespace Database.Models.Schema.Modifications
{
    /// <summary>
    /// An <see cref="Schema.Index"/> added to an <see cref="EntityType"/>.
    /// </summary>
    public interface IAddedIndex : IEntityModification
    {
        /// <summary>
        /// The added <see cref="Schema.Index"/>.
        /// </summary>
        Index Index { get; }
    }
}