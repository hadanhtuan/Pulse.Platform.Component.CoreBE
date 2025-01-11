using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Database.Models.Data
{
    
    /// Message from a system source.
    
    public class SystemMessage : RawMessage
    {
        
        /// The unique id of the raw adapter message which is set from
        /// the RawMessageId header of the message.
        /// Nullable for legacy data.
        
        public Guid? LogMessageId { get; set; } = null!;

        
        /// The internal type of this message; specific to the source.
        
        public string MessageType { get; set; } = null!;

        
        /// The source of this message.
        
        public string Source { get; set; } = null!;

        
        /// The type of this message for display.
        
        public string? DisplayedMessageType { get; set; } = null!;

        
        /// A status field for SystemMessages set by a user.
        
        public MessageUserStatus UserStatus { get; set; }

        
        /// A status field for SystemMessages set automatically by the system.
        
        public MessageSystemStatus SystemStatus { get; set; }

        
        /// The guid of the last user who matched or unmatched this message.
        
        public Guid? MatchedOrUnmatchedBy { get; set; }

        
        /// The guid of the last user who manually updated the UserStatus of this message.
        
        public Guid? UserStatusUpdatedBy { get; set; }

        
        /// The last time this message changed a status, covering both "matching" status and "user" status.
        
        public DateTime UpdatedTime { get; set; }

        
        /// Dictionary of attribute-value pairs containing all the fields of the system message
        /// transformed to the common input format for the enrichment pipeline.
        
        public string AttributeValuePairs { get; set; } = null!;

        
        /// Holds the time for when a user last manually updated the message, e.g.
        /// through manually matching.
        
        public DateTime LastUserUpdateTime { get; set; }

        
        /// The identifying attributes belonging to a System Message that can be used in message matching.
        
        public virtual ICollection<MessageIdentifierAttribute> MessageIdentifierAttributes { get; set; } 
            = new Collection<MessageIdentifierAttribute>();
    }
}