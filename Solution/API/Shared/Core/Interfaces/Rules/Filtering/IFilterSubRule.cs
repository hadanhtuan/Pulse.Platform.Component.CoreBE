using API.Shared.Core.Interfaces.Context.Matching;

namespace API.Shared.Core.Interfaces.Rules.Filtering
{
    /// <summary>
    /// A sub rule to be called from <see cref="IFilterRule{TEntityInput}"/> to filter
    /// a specific type of input message.
    /// </summary>
    /// <typeparam name="TEntityInput"><see cref="IEntityInput"/> type of input message mapped 
    /// to entity type</typeparam>
    public interface IFilterSubRule<TEntityInput> where TEntityInput : IEntityInput
    {
        /// <summary>
        /// Performs the filter rule on the given request, building and returning
        /// a result with the outcome.
        /// 
        /// Implementations should use build and return a result using the method 
        /// <see cref="IFilterRuleRequest{TEntityInput}.BuildResult(bool)"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IFilterRuleResult Filter(IFilterRuleRequest<TEntityInput> request);
    }

}