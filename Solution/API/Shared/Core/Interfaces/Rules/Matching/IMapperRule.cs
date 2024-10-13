using API.Shared.Core.Interfaces.Context.Matching;

namespace API.Shared.Core.Interfaces.Rules.Matching
{
    /// <summary>
    /// Rule for mapping an <see cref="IAdapterMessage"/> to an <see cref="IEntityInput"/>.
    /// </summary>
    /// <typeparam name="TAdapterMessage"></typeparam>
    /// <typeparam name="TEntityInput"></typeparam>
    public interface IMapperRule<TAdapterMessage, TEntityInput> where TAdapterMessage : IAdapterMessage
        where TEntityInput : IEntityInput
    {
        /// <summary>
        /// Maps the input message onto the output message on the basis of rule context.
        /// </summary>
        /// <param name="inputMessage">The message to be mapped to the output message.</param>
        /// <param name="outputMessage">The result of the mapping.</param>
        /// <param name="ruleContextFactory">Factory to build desired ruleContext 
        /// containing extra information for the mapper to use.</param>
        Task Map(in TAdapterMessage inputMessage, ref TEntityInput outputMessage, 
            in IRuleContextFactory ruleContextFactory);
    }
}