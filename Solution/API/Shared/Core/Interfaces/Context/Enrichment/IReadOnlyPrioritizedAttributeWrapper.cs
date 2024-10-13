namespace API.Shared.Core.Interfaces.Context.Enrichment
{
    /// <summary>
    /// Exposes attribute with values as read-only.
    /// </summary>
    /// <typeparam name="T">the type of the values</typeparam>
    public interface IReadOnlyPrioritizedAttributeWrapper<out T>
    {
        /// <summary>
        /// Returns the best value of this attribute according to the priority of the values.
        /// For values of equal priority, values that are received (or created) the most
        /// recently are prioritized over earlier values.
        /// </summary>
        public IAttributeWrapper<T>? Best { get; }

        /// <summary>
        /// Returns all value of this attribute, ordered according to the priority of the values.
        /// For values of equal priority, values that are received (or created) the most
        /// recently are prioritized over earlier values.
        /// </summary>
        public IAttributeWrapper<T>[] All { get; }

        /// <summary>
        /// Returns the previous best value of this attribute.
        /// </summary>
        public IAttributeWrapper<T>? Previous { get; }
    }
}