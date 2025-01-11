using System;
using Library.Persistence.EntityTypes;

namespace Database.Models.Data
{
    
    /// An event for processing in background services.
    
    public class BackgroundEvent : AggregateRoot
    {
        
        /// Type of background event. 
        
        public BackgroundEventType Type { get; set; }

        
        /// Type name of the serialized payload.
        
        public string PayloadTypeName { get; set; } = null!;

        
        /// Payload as a serialized object of the type specified in <see cref="PayloadTypeName"/>.
        
        public string Payload { get; set; } = null!;
    }
}