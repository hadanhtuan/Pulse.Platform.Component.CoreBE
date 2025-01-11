namespace Database.Models.Schema.Modifications
{
    
    /// An <see cref="Schema.Index"/> added to an <see cref="EntityType"/>.
    
    public interface IAddedIndex : IEntityModification
    {
        
        /// The added <see cref="Schema.Index"/>.
        
        Index Index { get; }
    }
}