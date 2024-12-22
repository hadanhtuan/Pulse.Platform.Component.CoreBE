using System;
using System.Collections.Generic;
using Library.Persistence.EntityTypes;
using Database.Models.Schema;
using Library.Persistence.EntityTypes;

namespace Database.Models.Data
{
    /// <summary>
    /// Base class for a message from an external input source.
    /// This is actually not considered a "raw" message,
    /// but rather a "cooked" message.
    /// </summary>
    public abstract class RawMessage : AggregateRoot
    {
        /// <summary>
        /// This is the actual content of the messages received.
        /// </summary>
        public string Payload { get; set; } = null!;

        /// <summary>
        /// The trace-id received from the message metadata headers.
        /// </summary>
        [Obsolete("Will be removed when no longer required as fallback")]
        public string? TraceId { get; set; } = null!;

        /// <summary>
        /// The parent-id received from the message metadata headers.
        /// </summary>
        [Obsolete("Will be removed when no longer required as fallback")]
        public string? ParentId { get; set; } = null!;

        /// <summary>
        /// The time when this message was received.
        /// </summary>
        /// <remarks>
        /// Value is normally set from metadata in the incoming kafka message.
        /// This means the received time can be considerably earlier than the
        /// time when the message was consumed by the 
        /// </remarks>
        public DateTime Received { get; set; }

        public InvokingReason Reason { get; set; }

        public Guid? EntityValueModifiedId { get; set; }

        /// <summary>
        /// The entity that this message is matched to.
        /// </summary>
        public virtual EntityValue? EntityValue { get; set; }

        /// <summary>
        /// The type of entity this message can match to.
        /// </summary>
        public virtual RootEntityType? EntityType { get; set; }

        /// <summary>
        /// Entity attributes values contained in this message.
        /// </summary>
        [Obsolete("This foreign key relation has been replaced by the JsonState model on " +
                  "EntityValue and is only used for the gradual migration from Relational model to Json model")]
        public virtual ICollection<MessageEntityAttributeValue> MessageEntityAttributeValues { get; set; }
            = new List<MessageEntityAttributeValue>();

        /// <summary>
        /// Alerts relating to matching this message to an entity.
        /// </summary>
        public virtual ICollection<RawMessageAlert> Alerts { get; set; } = new List<RawMessageAlert>();

        /// <summary>
        /// The Global Identity of the latest participant in the request chain of the consumed message.
        /// Will be null if a request without the Global Identity was received.
        /// </summary>
        public virtual Requester? Requester { get; set; }

        /// <summary>
        /// The Global Identity of all previous participants in the request chain of the consumed message. 
        /// Will be null if a request without the Global Identity was received. 
        /// </summary>
        public virtual List<Requester>? PreviousRequesters { get; set; }

        /// <summary>
        /// For messages that map to multiple contained entities, a <see cref="RawMessage"/>
        /// is created for each mapped entity. Those RawMessages will refer to their 
        /// parent message, which is the message containing the payload.
        /// If this property is not set, then this is a "root" message corresponding to
        /// an input system (adapter) message or an action message.
        /// </summary>
        /// <remarks>
        /// This is not persisted.
        /// </remarks>
        public RawMessage? ParentMessage { get; set; }
    }
}