using API.Shared.Core.Interfaces.Context.Matching;

namespace API.Shared.Core.Interfaces.Rules.Matching
{
    /// <summary>
    /// Registers adapter message types for consuming and processing by the AODB pipeline.
    /// </summary>
    public interface IAdapterRegistrant
    {
        /// <summary>
        /// Registers a consumer for the given adapter message type.
        /// The pipeline will subscribe to the topic for each adapter message registered
        /// with this method. Any adapter message type that needs to be processed must be
        /// registered using this method.
        /// </summary>
        /// <typeparam name="TMessage">type of adapter message to register</typeparam>
        void Register<TMessage>() where TMessage : IAdapterMessage;
    }
}