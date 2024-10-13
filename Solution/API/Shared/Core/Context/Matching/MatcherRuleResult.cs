
using API.Shared.Core.Interfaces.Context.Matching;

namespace API.Shared.Core.Context.Matching
{
    public class MatcherRuleResult : IMatcherRuleResult
    {
        public Guid? Identifier { get; set; }
        public MatchError? MatchError { get; set; }
        public bool RunSynchronously { get; set; }

    }
}
