namespace Database.Models.Schema.Modifications
{
    
    /// An added <see cref="Database.Models.Schema.AttributeType"/> to an <see cref="EntityType"/>.
    
    public interface IAddedAttribute : IEntityModification
    {
        
        /// The added <see cref="Database.Models.Schema.AttributeType"/>.
        
        AttributeType AttributeType { get; }
    }
}