using Library.Persistence.EntityTypes;

namespace Database.Models.Data
{
    
    /// Model for storing the processed offsets for each topic partition.
    /// Used to avoid processing consumed messages that has already been processed.
    
    public class TopicPartitionOffset : AggregateRoot
    {
        
        /// Name of topic from which the message was consumed
        
        public string Topic { get; set; } = null!;

        
        /// Partition from which the message was consumed
        
        public long Partition { get; set; }

        
        /// Offset from which the message was consumed
        
        public long Offset { get; set; }
    }
}