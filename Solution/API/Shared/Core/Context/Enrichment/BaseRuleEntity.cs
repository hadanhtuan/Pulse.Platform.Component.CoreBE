using API.Shared.Core.Interfaces.Context.Enrichment;
using API.Shared.Core.Interfaces.Context.Linking;
using API.Shared.Core.Interfaces.Context.Models;
using AlertLevel = API.Shared.Core.Interfaces.Context.Models.AlertLevel;

namespace API.Shared.Core.Context.Enrichment
{
    /// <summary>
    /// Abstract base class for specific entity classes to extend from.
    ///
    /// The implementation of exposed properties and methods are delegated to an IRuleEntity.
    /// </summary>
    public abstract class BaseRuleEntity : ILinkEntity
    {
        private readonly IRuleEntityAdapter adapter;

        /// <inheritdoc/>
        public Guid Id
        {
            get { return adapter.Id; }
        }

        /// <inheritdoc/>
        public ICollection<IEntityValueAlert> Alerts => adapter.Alerts;

        /// <inheritdoc/>
        public IRawMessage? LastModifiedBy => adapter.LastModifiedBy;

        /// <inheritdoc/>
        protected BaseRuleEntity(IRuleEntityAdapter adapter)
        {
            this.adapter = adapter;
        }

        /// <inheritdoc/>
        public IPrioritizedAttributeWrapper<T> FetchPrioritizedAttribute<T>(
            string attributeInternalName) =>
            adapter.FetchPrioritizedAttribute<T>(attributeInternalName);

        /// <inheritdoc/>
        public IPrioritizedComplexAttributeWrapper<TReadonlyInterface, TInterface>
            FetchPrioritizedAttribute<TReadonlyInterface, TInterface, TImplementation>(string attributeInternalName)
            where TInterface : TReadonlyInterface
            where TImplementation : TInterface, new()
            => adapter.FetchPrioritizedComplexAttribute<TReadonlyInterface, TInterface, TImplementation>(attributeInternalName);

        /// <inheritdoc/>
        public IReadOnlyPrioritizedAttributeWrapper<T> FetchReadOnlyPrioritizedAttribute<T>(
            string attributeInternalName) =>
            adapter.FetchReadOnlyPrioritizedAttribute<T>(attributeInternalName);

        /// <inheritdoc/>
        public IReadOnlyPrioritizedAttributeWrapper<TInterface>
            FetchReadOnlyPrioritizedAttribute<TInterface, TImplementation>(string attributeInternalName)
            where TImplementation : TInterface, new()
            => adapter.FetchReadOnlyPrioritizedComplexAttribute<TInterface, TImplementation>(attributeInternalName);

        /// <inheritdoc/>
        public IAlert AddAlert(AlertLevel level,
            Enum type, string masterdataTable, string? lookUpValue) =>
            adapter.AddAlert(level, type, masterdataTable, lookUpValue);

        /// <inheritdoc/>
        public IAlert AddAlert(AlertLevel level, Enum type) =>
            adapter.AddAlert(level, type);

        /// <inheritdoc/>
        public IAlert AddAlert(AlertLevel level, Enum type, string message) =>
            adapter.AddAlert(level, type, message);

        /// <inheritdoc/>
        public IReadOnlyCollection<IRecalculationTrigger> Recalculations => adapter.Recalculations.AsReadOnly();

        /// <inheritdoc/>
        public IRecalculationTrigger AddRecalculation(Guid identifier, string reason) =>
            adapter.AddRecalculation(identifier, reason);

        /// <inheritdoc/>
        public IEnumerable<IRecalculationTrigger> AddRecalculation(IEnumerable<Guid> identifiers, string reason) =>
            adapter.AddRecalculation(identifiers, reason);
    }
}