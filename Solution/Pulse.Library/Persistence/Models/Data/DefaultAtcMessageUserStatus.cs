using Library.Persistence.EntityTypes;
using System;

namespace Database.Models.Data
{
    
    /// Model for storing mappings between ATC messages and some default status. 
    
    public class DefaultAtcMessageUserStatus : AggregateRoot
    {
        
        /// Call sign following the format from atc messages
        
        public string CallSign { get; set; }

        
        /// The user status to propagate to a message
        
        public MessageUserStatus Status { get; set; }

        
        /// The operation time of the call sign
        
        public DateTime OperationTime { get; set; }

        
        /// Id of the user who created the entry
        
        public Guid? UserId { get; set; }

        
        /// The time of creation for the entry
        
        public DateTime Created { get; set; }
    }
}