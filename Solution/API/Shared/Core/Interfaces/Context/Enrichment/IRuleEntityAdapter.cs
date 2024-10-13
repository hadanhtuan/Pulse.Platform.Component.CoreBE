using API.Shared.Core.Interfaces.Context.Enrichment;
using API.Shared.Core.Interfaces.Context.Models;
using System;
using System.Collections.Generic;

namespace API.Shared.Core.Interfaces.Context.Enrichment
{
    /// <summary>
    /// Adapter for an entity used in business rules; responsible for
    /// adapting the entity model in the database to the entity model in
    /// the rules context.
    /// </summary>
    /// <remarks>This type is used in the rules context framework and is not
    /// intended to be exposed to rules code.</remarks>
    public interface IRuleEntityAdapter
    {
        /// <summary>
        /// Internal name of the EntityType of this entity.
        /// </summary>
        string EntityTypeName { get; }

        /// <summary>
        /// Returns the id of this entity.
        /// </summary>
        Guid Id { get; }

        ICollection<IEntityValueAlert> Alerts { get; }
        IAlert AddAlert(AlertLevel level, Enum type);
        IAlert AddAlert(AlertLevel level, Enum type, string message);
        IAlert AddAlert(AlertLevel level, Enum type, string masterdataTable, string? lookUpValue);

        /// <summary>
        /// Adds a message with a key to a specified Kafka topic. If no topic name is provided, the message is sent to a defined channel of the message.
        /// </summary>
        /// <param name="message">The message that includes a key and needs to be sent to Kafka. This message must implement the <see cref="IMessageWithKey"/> interface.</param>
        /// <param name="topicName">The name of the Kafka topic where the message will be sent. If this parameter is null or empty, the message will be sent to a defined channel of the message.</param>
        void AddKafkaMessage(IMessageWithKey message, string? topicName = null);

        IPrioritizedAttributeWrapper<T>
            FetchPrioritizedAttribute<T>(string attributeInternalName);
        IPrioritizedComplexAttributeWrapper<TReadonlyInterface, TInterface>
            FetchPrioritizedComplexAttribute<TReadonlyInterface, TInterface, TImplementation>(string attributeInternalName)
            where TImplementation : TInterface, new()
            where TInterface : TReadonlyInterface;

        IReadOnlyPrioritizedAttributeWrapper<T>
            FetchReadOnlyPrioritizedAttribute<T>(string attributeInternalName);
        IReadOnlyPrioritizedAttributeWrapper<TInterface>
            FetchReadOnlyPrioritizedComplexAttribute<TInterface, TImplementation>(string attributeInternalName)
            where TImplementation : TInterface;

        List<IRecalculationTrigger> Recalculations { get; }
        IRecalculationTrigger AddRecalculation(Guid identifier, string reason);
        IEnumerable<IRecalculationTrigger> AddRecalculation(IEnumerable<Guid> identifiers, string reason);

        List<IScheduledRecalculationTrigger> ScheduledRecalculations { get; }
        IScheduledRecalculationTrigger AddScheduledRecalculation(DateTime timeInUtc, string reason);

        IRawMessage? LastModifiedBy { get; }

        /// <summary>
        /// Returns an <see cref="IEnrichedRuleEntity"/> for this entity that exposes the results from enrichment.
        /// </summary>
        /// <returns></returns>
        IEnrichedRuleEntity ToResult();
    }
}