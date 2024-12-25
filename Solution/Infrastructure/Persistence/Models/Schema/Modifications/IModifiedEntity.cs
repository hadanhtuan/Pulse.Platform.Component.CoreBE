namespace Database.Models.Schema.Modifications
{
    /// <summary>
    /// A modification to an <see cref="Database.Models.Schema.EntityType"/>.
    /// </summary>
    public interface IModifiedEntity : ICompositeModification
    {
        /// <summary>
        /// The modified <see cref="Database.Models.Schema.EntityType"/>.
        /// </summary>
        EntityType EntityType { get; }
    }
}