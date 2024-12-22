namespace Database.Models.Schema.Modifications
{
    /// <summary>
    /// A modification to an <see cref="EntityType"/>.
    /// </summary>
    public interface IEntityModification : IModification
    {
        /// <summary>
        /// The modified EntityType.
        /// </summary>
        EntityType EntityType { get; }
    }
}