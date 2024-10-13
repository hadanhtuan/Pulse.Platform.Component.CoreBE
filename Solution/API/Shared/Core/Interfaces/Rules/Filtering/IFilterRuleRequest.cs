using API.Shared.Core.Interfaces.Context.Matching;

namespace API.Shared.Core.Interfaces.Rules.Filtering
{
    /// <summary>
    /// Request object that is input to a filter rule. This represents an incoming input message
    /// of a given source and type with the previous message of that source and type if it exists
    /// on the entity that the message has been mapped to.
    /// </summary>
    /// <typeparam name="TEntityInput"></typeparam>
    public interface IFilterRuleRequest<TEntityInput> where TEntityInput : IEntityInput
    {
        /// <summary>
        /// The incoming message mapped to the entity that should be evaluated to be
        /// possibly skipped by the processing pipeline.
        /// </summary>
        TEntityInput Incoming { get; }

        /// <summary>
        /// The previous message mapped to the entity that the incoming message can be
        /// evaluated against in order to determine if the incoming message should be skipped.
        /// </summary>
        TEntityInput? Previous { get; }

        /// <summary>
        /// Starts building a <see cref="IFilterRuleResult"/> with information about the outcome 
        /// of the rule to be returned in the 
        /// <see cref="IFilterRule{TEntityInput}.Filter(IFilterRuleRequest{TEntityInput})"/>
        /// method.
        /// </summary>
        /// <returns><see cref="IFilterRuleResultBuilder"/> to use for building a result</returns>
        IFilterRuleResultBuilder Result { get; }

        /// <summary>
        /// Interface for defining sub rules to be run. Sub rules are defined as a chain that
        /// ends by calling <see cref="IFilterSubRuleRunner{TEntityInput}.Result"/>, which will be
        /// <c>null</c> if none of the rules match the given message.
        /// </summary>
        IFilterSubRuleRunner<TEntityInput> SubRules { get; }

        /// <summary>
        /// Returns <c>true</c> if the values of all attributes in the Incoming and Previous messages are equal,
        /// while excluding the named attributes from the comparison.
        /// </summary>
        /// <param name="excludeAttributeNames">names of attributes to not include in comparison</param>
        /// <returns>whether the two messages contain the same attribute values</returns>
        bool AttributesEqualExcept(params string[] excludeAttributeNames);

        /// <summary>
        /// Returns the time since the previous message of the given source and type was received.
        /// </summary>
        TimeSpan TimeSincePreviousReceived { get; }
    }
}