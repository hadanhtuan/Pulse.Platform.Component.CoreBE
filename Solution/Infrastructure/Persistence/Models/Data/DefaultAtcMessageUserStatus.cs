using Library.Persistence.EntityTypes;
using System;

namespace Database.Models.Data
{
    /// <summary>
    /// Model for storing mappings between ATC messages and some default status. 
    /// </summary>
    public class DefaultAtcMessageUserStatus : AggregateRoot
    {
        /// <summary>
        /// Call sign following the format from atc messages
        /// </summary>
        public string CallSign { get; set; }

        /// <summary>
        /// The user status to propagate to a message
        /// </summary>
        public MessageUserStatus Status { get; set; }

        /// <summary>
        /// The operation time of the call sign
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// Id of the user who created the entry
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// The time of creation for the entry
        /// </summary>
        public DateTime Created { get; set; }
    }
}