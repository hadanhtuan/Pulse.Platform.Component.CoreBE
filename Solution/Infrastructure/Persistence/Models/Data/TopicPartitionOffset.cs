using Library.Persistence.EntityTypes;

namespace Database.Models.Data
{
    /// <summary>
    /// Model for storing the processed offsets for each topic partition.
    /// Used to avoid processing consumed messages that has already been processed.
    /// </summary>
    public class TopicPartitionOffset : AggregateRoot
    {
        /// <summary>
        /// Name of topic from which the message was consumed
        /// </summary>
        public string Topic { get; set; } = null!;

        /// <summary>
        /// Partition from which the message was consumed
        /// </summary>
        public long Partition { get; set; }

        /// <summary>
        /// Offset from which the message was consumed
        /// </summary>
        public long Offset { get; set; }
    }
}