using AODB.PipelineRulesInterfaces.Filtering;
using AODB.PipelineRulesInterfaces.Matching;
using AODB.Rules.ContextInterfaces.Matching;
using System;

namespace API.Shared.Core.Context.Matching
{
    public class AdapterHandlerResult : AdapterHandlerEntityResult, IAdapterHandlerResult
    {
        public MatchError? MatchError { get; }
        public string MessageSource { get; }
        public string MessageType { get; }
        public bool RunSynchronously { get; }
        public IFilterRuleResult? FilterRuleResult { get; }

        public static AdapterHandlerResult Create<TEntityInputInterface>(
            MatchError? matchError,
            Guid? identifier,
            BaseEntityInput entityInput,
            bool runSynchronously,
            IFilterRuleResult? filterResult
            ) where TEntityInputInterface : IEntityInput
        {
            return new AdapterHandlerResult(matchError, identifier, entityInput, runSynchronously, 
                typeof(TEntityInputInterface), filterResult);
        }

        protected AdapterHandlerResult(
            MatchError? matchError,
            Guid? identifier,
            BaseEntityInput entityInput,
            bool runSynchronously,
            Type entityInputInterfaceType,
            IFilterRuleResult? filterResult
            ) : base(identifier, entityInput, entityInputInterfaceType)
        {
            MatchError = matchError;
            RunSynchronously = runSynchronously;
            MessageSource = entityInput.MessageSource;
            MessageType = entityInput.MessageType;
            FilterRuleResult = filterResult;
        }
    }
}