
using API.Shared.Core.Interfaces.Context.Matching;

namespace API.Shared.Core.Interfaces.Rules.Matching
{
    public interface IMatcherRule<in MessageType> where MessageType : IEntityInput
    {
        Task Match(MessageType inputMessage, IMatcherRuleResult outputMessage, IRuleContextFactory ruleContextFactory);
    }
}