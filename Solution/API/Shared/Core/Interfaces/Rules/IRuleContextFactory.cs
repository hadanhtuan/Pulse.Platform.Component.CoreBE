using System.Diagnostics;

namespace API.Shared.Core.Interfaces.Rules
{
    /// <summary>
    /// Factory for building a rule context, which is only accessible in rules code
    /// and therefore not exposed on this interface. The rule context is provided
    /// by an extension in the rules solution in the configuration repository.
    /// </summary>
    public interface IRuleContextFactory : IPointInTimes
    {
        /// <summary>
        /// Returns a <see cref="CancellationToken"/> to check if the pipeline
        /// has been cancelled.
        /// </summary>
        CancellationToken CancellationToken { get; }

        /// <summary>
        /// Starts an activity to be used for logging. This can be called for a 
        /// sub rule sequence and for individual sub rules to track their progress.
        /// </summary>
        /// <param name="name">Name of activity</param>
        /// <returns><see cref="Activity"/> that should be disposed after activity has completed
        /// or null if activity tracing is not enabled</returns>
        Activity? StartActivity(string name);
    }
}