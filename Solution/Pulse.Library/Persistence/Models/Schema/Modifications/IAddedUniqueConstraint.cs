namespace Database.Models.Schema.Modifications
{
    
    /// A <see cref="Database.Models.Schema.UniqueConstraint"/> added to an <see cref="EntityType"/>.
    
    public interface IAddedUniqueConstraint : IEntityModification
    {
        
        /// The added <see cref="Database.Models.Schema.UniqueConstraint"/>.
        
        UniqueConstraint UniqueConstraint { get; }
    }
}