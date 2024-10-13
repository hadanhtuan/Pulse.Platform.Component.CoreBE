
using API.Shared.Core.Interfaces.Context.Matching;
using API.Shared.Core.Interfaces.Rules.Matching;

namespace API.Shared.Core.Context.Matching
{
    /// <summary>
    /// Generic wrapper that can be used when an EntityInput has attributes that point to
    /// child EntityInput structures and you need the child Entities to be matched and 
    /// assigned either an Identitifer or mark to be created.
    /// Examples are Flight Legs and Ground Legs on Visits.
    /// </summary>
    /// <typeparam name="TEntityInputInterface">The specific EntityInput interface that should be 
    /// wrapped with matching information.</typeparam>
    /// <typeparam name="TEntityInputImplementation">The specific EntityInput implementation that should be 
    /// wrapped with matching information.</typeparam>
    public class MatchableEntityInput<TEntityInputInterface, TEntityInputImplementation> : 
        ICreateAdapterHandlerEntityResult,
        IMatchableEntityInput<TEntityInputInterface>
        where TEntityInputImplementation : BaseEntityInput, TEntityInputInterface
        where TEntityInputInterface : IEntityInput

    {
        public MatchableEntityInput(TEntityInputImplementation entity)
        {
            this.entity = entity;
        }

        private Guid? identifier;
        /// <inheritdoc/>
        public Guid? Identifier
        {
            get => identifier;
            set
            {
                identifier = value;
                if (identifier != null)
                {
                    shouldBeCreated = false;
                }
            }
        }

        bool shouldBeCreated;
        /// <inheritdoc/>
        public bool ShouldBeCreated
        {
            get => shouldBeCreated;
            set
            {
                shouldBeCreated = value;
                if (shouldBeCreated)
                {
                    identifier = null;
                }
            }
        }

        private readonly TEntityInputImplementation entity;
        /// <inheritdoc/>
        public TEntityInputInterface Entity => entity;

        /// <summary>
        /// Creates a <see cref="IAdapterHandlerEntityResult"/> that represents this MatchableEntityInput
        /// as a sub entity returned from mapping and matching.
        /// </summary>
        /// <returns>Null if no Identifier was assigned and ShouldBeCreated is false, otherwise a 
        /// <see cref="IAdapterHandlerEntityResult"/> representation of the MatchableEntityInput</returns>
        public IAdapterHandlerEntityResult? CreateAdapterHandlerEntityResult()
        {
            if (Identifier == null && !ShouldBeCreated)
            {
                return null;
            }
            return AdapterHandlerEntityResult.Create<TEntityInputInterface>(Identifier, entity);
        }

    }
}
