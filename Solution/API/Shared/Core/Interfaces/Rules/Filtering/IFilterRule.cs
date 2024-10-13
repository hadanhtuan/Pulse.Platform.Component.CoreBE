
using API.Shared.Core.Interfaces.Context.Matching;

namespace API.Shared.Core.Interfaces.Rules.Filtering
{
    /// <summary>
    /// A business rule to determine if a given input message, in the form of an 
    /// IEntityInput, should be skipped by the processing pipeline.
    /// </summary>
    /// <typeparam name="TEntityInput"><see cref="IEntityInput"/> type of input message mapped 
    /// to entity type</typeparam>
    public interface IFilterRule<TEntityInput> where TEntityInput : IEntityInput
    {
        /// <summary>
        /// Perform the filter rule on the given request. 
        /// 
        /// Implementations should build and return a result using the 
        /// <see cref="IFilterRuleRequest{TEntityInput}.Result"/> builder.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IFilterRuleResult Filter(IFilterRuleRequest<TEntityInput> request);
    }
}