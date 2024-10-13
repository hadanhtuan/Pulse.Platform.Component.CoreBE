
using API.Shared.Core.Interfaces.Context.Models;

namespace API.Shared.Core.Interfaces.Context.Matching
{
    /// <summary>
    /// When attritubes on mapped entities implement this interface, the AODB pipeline knows that
    /// we are dealing with a complex attribute that should be serialized to JSON.
    /// </summary>
    /// <typeparam name="T">
    /// The specific (auto-generated) type used to model the complex attribute type.
    /// </typeparam>
    public interface IComplexAttributeInput<out T> where T : IComplexAttribute
    {
        /// <summary>
        /// Determines whether an empty complex attribute should be serialized as null or as a 
        /// JSON with empty values.
        /// </summary>
        /// <remarks>
        /// Default value should be true, as we prefer null values if the mapper has not assigned a specific value at 
        /// least one property on the complex attribute.
        /// </remarks>
        public bool IgnoreWhenEmpty { get; set; }

        /// <summary>
        /// The actual strongly typed (auto-generated) value to serialize to JSON
        /// </summary>
        public T Item { get; }

    }
}
