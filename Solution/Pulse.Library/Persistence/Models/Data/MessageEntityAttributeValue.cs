using System;

namespace Database.Models.Data
{
    public class MessageEntityAttributeValue : EntityAttributeValue
    {
        public virtual RawMessage RawMessage { get; set; } = null!;

        
        /// The time when this message EAV was created , i.e. the time the 
        /// message eav was created by the MessageReceival pipeline step.
        
        public DateTime? Created { get; set; }
    }
}