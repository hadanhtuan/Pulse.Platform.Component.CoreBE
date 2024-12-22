using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Database.Models.Data
{
    /// <summary>
    /// Message from a system source.
    /// </summary>
    public class SystemMessage : RawMessage
    {
        /// <summary>
        /// The unique id of the raw adapter message which is set from
        /// the RawMessageId header of the message.
        /// Nullable for legacy data.
        /// </summary>
        public Guid? LogMessageId { get; set; } = null!;

        /// <summary>
        /// The internal type of this message; specific to the source.
        /// </summary>
        public string MessageType { get; set; } = null!;

        /// <summary>
        /// The source of this message.
        /// </summary>
        public string Source { get; set; } = null!;

        /// <summary>
        /// The type of this message for display.
        /// </summary>
        public string? DisplayedMessageType { get; set; } = null!;

        /// <summary>
        /// A status field for SystemMessages set by a user.
        /// </summary>
        public MessageUserStatus UserStatus { get; set; }

        /// <summary>
        /// A status field for SystemMessages set automatically by the system.
        /// </summary>
        public MessageSystemStatus SystemStatus { get; set; }

        /// <summary>
        /// The guid of the last user who matched or unmatched this message.
        /// </summary>
        public Guid? MatchedOrUnmatchedBy { get; set; }

        /// <summary>
        /// The guid of the last user who manually updated the UserStatus of this message.
        /// </summary>
        public Guid? UserStatusUpdatedBy { get; set; }

        /// <summary>
        /// The last time this message changed a status, covering both "matching" status and "user" status.
        /// </summary>
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// Dictionary of attribute-value pairs containing all the fields of the system message
        /// transformed to the common input format for the enrichment pipeline.
        /// </summary>
        public string AttributeValuePairs { get; set; } = null!;

        /// <summary>
        /// Holds the time for when a user last manually updated the message, e.g.
        /// through manually matching.
        /// </summary>
        public DateTime LastUserUpdateTime { get; set; }

        /// <summary>
        /// The identifying attributes belonging to a System Message that can be used in message matching.
        /// </summary>
        public virtual ICollection<MessageIdentifierAttribute> MessageIdentifierAttributes { get; set; } 
            = new Collection<MessageIdentifierAttribute>();
    }
}