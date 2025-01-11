namespace Database.Models.Schema.Modifications
{
    
    /// A modification to an <see cref="EntityType"/>.
    
    public interface IEntityModification : IModification
    {
        
        /// The modified EntityType.
        
        EntityType EntityType { get; }
    }
}