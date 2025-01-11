using Database.Models.Outbox;
using System;

namespace Database.Models.Data
{
    public class PrioritizedEntity
    {
        public Guid Id { get; set; }

        public MessagePriority Priority { get; set; }
    }
}
