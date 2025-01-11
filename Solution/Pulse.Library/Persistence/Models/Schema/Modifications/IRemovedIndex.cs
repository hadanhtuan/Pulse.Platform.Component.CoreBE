namespace Database.Models.Schema.Modifications
{
    
    /// A removed <see cref="Database.Models.Schema.Index"/> from an <see cref="EntityType"/>.
    
    public interface IRemovedIndex : IEntityModification
    {
        
        /// The removed <see cref="Database.Models.Schema.Index"/>.
        
        Index Index { get; }
    }
}