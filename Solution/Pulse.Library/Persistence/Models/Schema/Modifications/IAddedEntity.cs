namespace Database.Models.Schema.Modifications
{
    
    /// An <see cref="Database.Models.Schema.EntityType"/> added to the schema.
    
    public interface IAddedEntity : ICompositeModification
    {
        
        /// The added <see cref="Database.Models.Schema.EntityType"/>.
        
        EntityType EntityType { get; }
    }
}