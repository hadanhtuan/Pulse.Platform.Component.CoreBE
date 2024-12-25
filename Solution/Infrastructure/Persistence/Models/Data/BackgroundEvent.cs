using System;
using Library.Persistence.EntityTypes;

namespace Database.Models.Data
{
    /// <summary>
    /// An event for processing in background services.
    /// </summary>
    public class BackgroundEvent : AggregateRoot
    {
        /// <summary>
        /// Type of background event. 
        /// </summary>
        public BackgroundEventType Type { get; set; }

        /// <summary>
        /// Type name of the serialized payload.
        /// </summary>
        public string PayloadTypeName { get; set; } = null!;

        /// <summary>
        /// Payload as a serialized object of the type specified in <see cref="PayloadTypeName"/>.
        /// </summary>
        public string Payload { get; set; } = null!;
    }
}