namespace Database.Models.Schema.Modifications
{
    /// <summary>
    /// An added <see cref="Database.Models.Schema.AttributeType"/> to an <see cref="EntityType"/>.
    /// </summary>
    public interface IAddedAttribute : IEntityModification
    {
        /// <summary>
        /// The added <see cref="Database.Models.Schema.AttributeType"/>.
        /// </summary>
        AttributeType AttributeType { get; }
    }
}