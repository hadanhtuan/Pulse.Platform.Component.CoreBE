
using API.Shared.Core.Interfaces.Context.Matching;

namespace API.Shared.Core.Interfaces.Rules.Filtering
{
    /// <summary>
    /// Allows calling one of a number of sub rules.
    /// </summary>
    /// <typeparam name="TEntityInput"></typeparam>
    public interface IFilterSubRuleRunner<TEntityInput> where TEntityInput : IEntityInput
    {
        /// <summary>
        /// Runs the given sub rule on the input. This can be chained.
        /// </summary>
        /// <typeparam name="TSubRule"></typeparam>
        /// <param name="source">Name of the message source</param>
        /// <param name="messageType">Message type</param>
        /// <returns></returns>
        public IFilterSubRuleRunner<TEntityInput> Run<TSubRule>(
            string source, string messageType)
            where TSubRule : IFilterSubRule<TEntityInput>;

        /// <summary>
        /// Returns the result after running the sub rules. If no sub rule applied 
        /// to the given message type, then this Result is null.
        /// </summary>
        public IFilterRuleResult? Result { get; }
    }
}