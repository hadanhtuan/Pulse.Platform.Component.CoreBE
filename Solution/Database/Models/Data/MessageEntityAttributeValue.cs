using System;

namespace Database.Models.Data
{
    public class MessageEntityAttributeValue : EntityAttributeValue
    {
        public virtual RawMessage RawMessage { get; set; } = null!;

        /// <summary>
        /// The time when this message EAV was created , i.e. the time the 
        /// message eav was created by the MessageReceival pipeline step.
        /// </summary>
        public DateTime? Created { get; set; }
    }
}