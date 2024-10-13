namespace API.Shared.Core.Interfaces.Context.Enrichment
{
    /// <summary>
    /// Provides access to an attribute on an entity and its values within enrichment rules,
    /// enabling rule authors to add values to the specific attribute.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPrioritizedAttributeWrapper<T> : IReadOnlyPrioritizedAttributeWrapper<T>
    {
        /// <summary>
        /// Sets the value (and optionally, the priority) of this attribute.
        /// </summary>
        /// <param name="value">Value to set on atttribute</param>
        /// <param name="priority">Priority or default 0</param>
        /// <param name="sourceName">Custom source name of the value</param>
        void SetValue(T value, int priority = 0, string? sourceName = null);

        /// <summary>
        /// Sets the value (and optionally, the priority) of this attribute with
        /// reference to a Master data entity.
        /// </summary>
        /// <param name="value">Value to set on atttribute</param>
        /// <param name="priority">Priority or default 0</param>
        /// <param name="masterDataReference">Referenced Master data entity</param>
        /// <param name="sourceName">Custom source name of the value</param>
        void SetValue(T value, MasterData.IEntity masterDataReference, int priority = 0, string? sourceName = null);

        /// <summary>
        /// Sets the value (and optionally, the priority) of this attribute
        /// to the value of another attribute.
        /// </summary>
        /// <param name="priority">Priority or default 0</param>
        /// <param name="aodbReference">Referenced attribute</param>
        /// <param name="sourceName">Custom source name of the value</param>
        void SetValue(IAttributeWrapper<T> aodbReference, int priority = 0, string? sourceName = null);

        /// <summary>
        /// Sets the value (and optionally, the priority) of this attribute with
        /// reference to another attribute.
        /// </summary>
        /// <param name="value">Value to set on atttribute</param>
        /// <param name="priority">Priority or default 0</param>
        /// <param name="aodbReference">Referenced attribute</param>
        /// <param name="sourceName">Custom source name of the value</param>
        void SetValue(T value, IAttributeWrapper<T> aodbReference, int priority = 0, string? sourceName = null);

        /// <summary>
        /// Sets the value (and optionally, the priority) of this attribute
        /// to the value of another attribute, with reference to a Master data entity.
        /// </summary>
        /// <param name="priority">Priority or default 0</param>
        /// <param name="aodbReference">Referenced attribute</param>
        /// <param name="masterDataReference">Referenced Master data entity</param>
        /// <param name="sourceName">Custom source name of the value</param>
        void SetValue(IAttributeWrapper<T> aodbReference, MasterData.IEntity masterDataReference, 
            int priority = 0, string? sourceName = null);

        /// <summary>
        /// Reset values of this attribute.
        /// </summary>
        void ResetValues();

        /// <summary>
        /// Reset values from a particular source on this attribute. Source is in this case 
        /// limited to system message source.
        /// </summary>
        /// <param name="source"></param>
        void ResetValuesFromSource(string source);
    }
}