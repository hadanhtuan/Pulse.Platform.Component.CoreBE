namespace API.Shared.Core.Interfaces.Rules.Filtering
{
    /// <summary>
    /// Utility to help build the result from a filter rule.
    /// </summary>
    public interface IFilterRuleResultBuilder
    {
        /// <summary>
        /// Returns a result to indicate that the input message should be processed.
        /// </summary>
        /// <returns><see cref="IFilterRuleResult"/> to return from rule</returns>
        IFilterRuleResult Process();

        /// <summary>
        /// Returns a result to indicate that the input message should be skipped.
        /// </summary>
        /// <param name="explanation">Message explaining why the input message is skipped</param>
        /// <returns><see cref="IFilterRuleResult"/> to return from rule</returns>
        IFilterRuleResult Skip(string explanation);
    }
}