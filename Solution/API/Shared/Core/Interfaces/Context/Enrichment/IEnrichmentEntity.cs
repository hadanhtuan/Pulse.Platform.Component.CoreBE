using System;
using System.Collections.Generic;

namespace API.Shared.Core.Interfaces.Context.Enrichment
{
    /// <summary>
    /// Interface for functionality only available for self-enrichment on all AODB entities.
    /// </summary>
    public interface IEnrichmentEntity
    {
        /// <summary>
        /// Returns a collection of <see cref="IScheduledRecalculationTrigger">s containing DateTimes that 
        /// this entity should be recalculated at, as well as the reasons for the scheduled recalculations.
        /// </summary>
        IReadOnlyCollection<IScheduledRecalculationTrigger> ScheduledRecalculations { get; }

        /// <summary>
        /// Schedule this entity to be recalculated in future due to a specified reason.
        /// </summary>
        /// <param name="timeInUtc">The time in UTC that the entity should be recalculated.</param>
        /// <param name="reason">The reason for being recalculated.</param>
        /// <returns>The scheduled trigger that was added</returns>
        IScheduledRecalculationTrigger AddScheduledRecalculation(DateTime timeInUtc, string reason);

        /// <summary>
        /// Adds a message with a key to a specified Kafka topic. If no topic name is provided, the message is sent to a defined channel of the message.
        /// </summary>
        /// <param name="message">The message that includes a key and needs to be sent to Kafka. This message must implement the <see cref="IMessageWithKey"/> interface.</param>
        /// <param name="topicName">The name of the Kafka topic where the message will be sent. If this parameter is null or empty, the message will be sent to a defined channel of the message.</param>
        void AddKafkaMessage(IMessageWithKey message, string? topicName = null);
    }
}
