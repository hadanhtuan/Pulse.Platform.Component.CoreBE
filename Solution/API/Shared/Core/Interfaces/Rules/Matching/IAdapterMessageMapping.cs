using System;

namespace API.Shared.Core.Interfaces.Context.Matching
{
    /// <summary>
    /// Defines mapping of a adapter message type and its entity input type, which is used 
    /// for mapping, matching and filtering rules.
    /// </summary>
    public interface IAdapterMessageMapping
    {
        /// <summary>
        /// The type of the adapter message.
        /// </summary>
        public Type AdapterMessageType { get; }

        /// <summary>
        /// The interface type of the entity input sent into rules, 
        /// which should extend <c>IEntityInput</c>.
        /// </summary>
        public Type EntityInputInterfaceType { get; }

        /// <summary>
        /// The implementation type of the entity input sent into rules, 
        /// which should extend <c>BaseEntityInput</c>.
        /// </summary>
        public Type EntityInputImplementationType { get; }
    }
}
