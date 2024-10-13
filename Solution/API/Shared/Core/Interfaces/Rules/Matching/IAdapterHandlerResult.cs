using API.Shared.Core.Interfaces.Context.Matching;
using API.Shared.Core.Interfaces.Rules.Filtering;

namespace API.Shared.Core.Interfaces.Rules.Matching
{
    /// <summary>
    /// Contains the result of running mapping, matching, i.e. an <see cref="IAdapterHandlerEntityResult"/>
    /// as well as a <see cref="MatchError"/> that specifies whether to create or not if no match was found.
    /// </summary>
    public interface IAdapterHandlerResult : IAdapterHandlerEntityResult
    {
        /// <summary>
        /// Is used to indiciate whether to Create if no match was found
        /// (as well as signalling the error if more than one match was found)
        /// </summary>
        MatchError? MatchError { get; }

        /// <summary>
        /// The source of the message as determined by the mapping rule.
        /// </summary>
        string MessageSource { get; }

        /// <summary>
        /// The type of the message as determined by the mapping rule.
        /// </summary>
        string MessageType { get; }
        
        /// <summary>
        /// Specify if the enrichment pipeline should be invoked synchronously
        /// or the output put on an intermediate enrichment topic.
        /// </summary>
        bool RunSynchronously { get; }

        /// <summary>
        /// The result of performing the filter rule on the adapter message if it were matched.
        /// No result is set if the adapter message was not matched.
        /// </summary>
        IFilterRuleResult? FilterRuleResult { get; }
    }
}
