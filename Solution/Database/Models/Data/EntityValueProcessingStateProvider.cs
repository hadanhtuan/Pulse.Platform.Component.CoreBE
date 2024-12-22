using AdapterFactory;
using Database.Models.Schema;
using Library.Persistence.Repositories;
using System.Collections.Concurrent;

namespace Database.Models.Data
{
    /// <summary>
    /// Factory for adapting an <see cref="EntityValue"/> to an <see cref="EntityValueProcessingState"/>,
    /// looking it up in the database if not already cached and creating a new <see cref="EntityValueProcessingState"/>
    /// if none exists for the entity.
    /// 
    /// Note that for entities of a type listed in <see cref="entityTypesWithoutProcessingState"/>, which
    /// should never be recalculated, an non-persisted state of <see cref="ProcessingStatus.Processed"/> is returned.
    /// </summary>
    internal class EntityValueProcessingStateProvider :
        IAdapterFactory<EntityValue, EntityValueProcessingState>
    {
        private readonly IRepository<EntityValueProcessingState> processingStateRepository;
        private readonly ConcurrentDictionary<Guid, EntityValueProcessingState> adapters = new();

        internal IReadOnlyCollection<string> entityTypesWithoutProcessingState =
            new string[] { StandardElements.Visit };

        public EntityValueProcessingStateProvider(IRepository<EntityValueProcessingState> processingStateRepository)
        {
            this.processingStateRepository = processingStateRepository;
        }

        public EntityValueProcessingState? GetAdapter(EntityValue adaptee) =>
            adapters.GetOrAdd(adaptee.Id, _ => GetOrCreate(adaptee));

        private EntityValueProcessingState GetOrCreate(EntityValue entityValue)
        {
            return GetForEntityTypeWithoutProcessingStateOrNull(entityValue) ??
                   processingStateRepository
                       .SingleOrDefault(x => x.EntityValueId == entityValue.Id)
                   ?? Create(entityValue.Id);
        }

        // For entity types that should never be recalculated, create a blank state that is never persisted
        private EntityValueProcessingState? GetForEntityTypeWithoutProcessingStateOrNull(EntityValue entityValue) =>
            entityTypesWithoutProcessingState.Contains(entityValue.EntityType.InternalName)
                ? new() { EntityValueId = entityValue.Id, ProcessingStatus = ProcessingStatus.Processed }
                : null;

        private EntityValueProcessingState Create(Guid id)
        {
            EntityValueProcessingState state = new() { EntityValueId = id };
            processingStateRepository.Add(state);
            return state;
        }
    }
}