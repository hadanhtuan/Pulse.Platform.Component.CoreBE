using System;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json
{
    
    /// Contains the extra data that a <see cref="RawMessage"/> has when it is an System (Adapter) message
    /// (as opposed to a Action (UI) message)
    
    public class RawSystemMessage
    {
        public RawSystemMessage(string source, string messageType, string? displayedMessageType)
        {
            Source = source;
            MessageType = messageType;
            DisplayedMessageType = displayedMessageType;
        }

        
        /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
        /// in order to use <see cref="System.Text.Json.Serialization.ReferenceHandler.Preserve"/>.
        
        [JsonConstructor]
        [Obsolete("This constructor is only intended for the System.Text.Json.JsonSerializer.")]
        public RawSystemMessage() : this(string.Empty, string.Empty, null)
        {
        }


        
        /// The source of this message.
        
        [JsonInclude]
        public string Source { get; private set; }

        
        /// The internal type of this message; specific to the source.
        
        [JsonInclude]
        public string MessageType { get; private set; }

        
        /// The type of this message for display.
        
        [JsonInclude]
        public string? DisplayedMessageType { get; private set; }

    }
}
