using Library.Persistence.EntityTypes;
using System;

namespace Database.Models.Data
{
    
    /// Hash values generated for a message describing an entity. 
    
    public class EntityValueHash : AggregateRoot
    {
        
        /// Unique identifier of the entity that the message is about.
        
        public Guid EntityValueId { get; set; }

        
        /// Entity that the message is about.
        
        public virtual EntityValue EntityValue { get; set; } = null!;

        
        /// Hash value of the message describing an entity. Messages are 
        /// determined as containing changes to the entity if their hash values differ.
        
        public byte[] HashValue { get; set; } = null!;

        
        /// Date when this hash was updated. 
        
        public DateTime Updated { get; set; }
    }
}