using System;
using System.Collections.Generic;
using Library.Persistence.EntityTypes;
using Database.Models.Schema;
using Library.Persistence.EntityTypes;

namespace Database.Models.Data
{
    
    /// Base class for a message from an external input source.
    /// This is actually not considered a "raw" message,
    /// but rather a "cooked" message.
    
    public abstract class RawMessage : AggregateRoot
    {
        
        /// This is the actual content of the messages received.
        
        public string Payload { get; set; } = null!;

        
        /// The trace-id received from the message metadata headers.
        
        [Obsolete("Will be removed when no longer required as fallback")]
        public string? TraceId { get; set; } = null!;

        
        /// The parent-id received from the message metadata headers.
        
        [Obsolete("Will be removed when no longer required as fallback")]
        public string? ParentId { get; set; } = null!;

        
        /// The time when this message was received.
        
        /// <remarks>
        /// Value is normally set from metadata in the incoming kafka message.
        /// This means the received time can be considerably earlier than the
        /// time when the message was consumed by the 
        /// </remarks>
        public DateTime Received { get; set; }

        public InvokingReason Reason { get; set; }

        public Guid? EntityValueModifiedId { get; set; }

        
        /// The entity that this message is matched to.
        
        public virtual EntityValue? EntityValue { get; set; }

        
        /// The type of entity this message can match to.
        
        public virtual RootEntityType? EntityType { get; set; }

        
        /// Entity attributes values contained in this message.
        
        [Obsolete("This foreign key relation has been replaced by the JsonState model on " +
                  "EntityValue and is only used for the gradual migration from Relational model to Json model")]
        public virtual ICollection<MessageEntityAttributeValue> MessageEntityAttributeValues { get; set; }
            = new List<MessageEntityAttributeValue>();

        
        /// Alerts relating to matching this message to an entity.
        
        public virtual ICollection<RawMessageAlert> Alerts { get; set; } = new List<RawMessageAlert>();

        
        /// The Global Identity of the latest participant in the request chain of the consumed message.
        /// Will be null if a request without the Global Identity was received.
        
        public virtual Requester? Requester { get; set; }

        
        /// The Global Identity of all previous participants in the request chain of the consumed message. 
        /// Will be null if a request without the Global Identity was received. 
        
        public virtual List<Requester>? PreviousRequesters { get; set; }

        
        /// For messages that map to multiple contained entities, a <see cref="RawMessage"/>
        /// is created for each mapped entity. Those RawMessages will refer to their 
        /// parent message, which is the message containing the payload.
        /// If this property is not set, then this is a "root" message corresponding to
        /// an input system (adapter) message or an action message.
        
        /// <remarks>
        /// This is not persisted.
        /// </remarks>
        public RawMessage? ParentMessage { get; set; }
    }
}