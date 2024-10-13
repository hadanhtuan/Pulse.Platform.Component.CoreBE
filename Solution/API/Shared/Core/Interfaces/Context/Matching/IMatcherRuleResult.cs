using System;

namespace API.Shared.Core.Interfaces.Context.Matching
{
    public interface IMatcherRuleResult
    {
        /// <summary>
        /// Id of a matched EntityValue if found, otherwise null.
        /// </summary>
        public Guid? Identifier { get; set; }

        /// <summary>
        /// Is used to indiciate whether to Create if no match was found
        /// (as well as signalling the error if more than one match was found)
        /// </summary>
        public MatchError? MatchError { get; set; }

        /// <summary>
        /// Specify if the enrichment pipeline should be invoked synchronously
        /// or the output put on an intermediate enrichment topic.
        /// </summary>
        bool RunSynchronously { get; set; }
    }
}

