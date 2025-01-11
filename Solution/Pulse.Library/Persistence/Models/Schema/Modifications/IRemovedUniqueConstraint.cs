namespace Database.Models.Schema.Modifications
{
    
    /// A removed <see cref="Database.Models.Schema.UniqueConstraint"/> from an <see cref="EntityType"/>.
    
    public interface IRemovedUniqueConstraint : IEntityModification
    {
        
        /// The removed <see cref="Database.Models.Schema.UniqueConstraint"/>.
        
        UniqueConstraint UniqueConstraint { get; }
    }
}