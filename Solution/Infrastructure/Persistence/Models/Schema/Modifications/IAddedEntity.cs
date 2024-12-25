namespace Database.Models.Schema.Modifications
{
    /// <summary>
    /// An <see cref="Database.Models.Schema.EntityType"/> added to the schema.
    /// </summary>
    public interface IAddedEntity : ICompositeModification
    {
        /// <summary>
        /// The added <see cref="Database.Models.Schema.EntityType"/>.
        /// </summary>
        EntityType EntityType { get; }
    }
}