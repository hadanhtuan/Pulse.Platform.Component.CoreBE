using API.Shared.Core.Interfaces.Context.Enrichment;
using System;
using System.Collections.Generic;

namespace API.Shared.Core.Context.Enrichment
{
    /// <inheritdoc/>
    public abstract class EnrichmentBaseRuleEntity : BaseRuleEntity, IEnrichmentEntity
    {
        private readonly IRuleEntityAdapter adapter;

        /// <inheritdoc/>
        protected EnrichmentBaseRuleEntity(IRuleEntityAdapter ruleEntityAdapter) : base(ruleEntityAdapter)
        {
            adapter = ruleEntityAdapter;
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<IScheduledRecalculationTrigger> ScheduledRecalculations =>
            adapter.ScheduledRecalculations.AsReadOnly();

        /// <inheritdoc/>
        public IScheduledRecalculationTrigger AddScheduledRecalculation(DateTime timeInUtc, string reason) =>
            adapter.AddScheduledRecalculation(timeInUtc, reason);

        /// <inheritdoc/>
        public void AddKafkaMessage(IMessageWithKey message, string? topicName = null) =>
            adapter.AddKafkaMessage(message, topicName);
    }
}