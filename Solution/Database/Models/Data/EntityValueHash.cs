using Library.Persistence.EntityTypes;
using System;

namespace Database.Models.Data
{
    /// <summary>
    /// Hash values generated for a message describing an entity. 
    /// </summary>
    public class EntityValueHash : AggregateRoot
    {
        /// <summary>
        /// Unique identifier of the entity that the message is about.
        /// </summary>
        public Guid EntityValueId { get; set; }

        /// <summary>
        /// Entity that the message is about.
        /// </summary>
        public virtual EntityValue EntityValue { get; set; } = null!;

        /// <summary>
        /// Hash value of the message describing an entity. Messages are 
        /// determined as containing changes to the entity if their hash values differ.
        /// </summary>
        public byte[] HashValue { get; set; } = null!;

        /// <summary>
        /// Date when this hash was updated. 
        /// </summary>
        public DateTime Updated { get; set; }
    }
}