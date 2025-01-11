namespace Database.Models.Schema.Modifications
{
    
    /// A modification to an <see cref="Database.Models.Schema.EntityType"/>.
    
    public interface IModifiedEntity : ICompositeModification
    {
        
        /// The modified <see cref="Database.Models.Schema.EntityType"/>.
        
        EntityType EntityType { get; }
    }
}