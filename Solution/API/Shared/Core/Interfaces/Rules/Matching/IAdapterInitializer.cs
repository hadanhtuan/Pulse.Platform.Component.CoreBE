namespace API.Shared.Core.Interfaces.Rules.Matching
{
    /// <summary>
    /// Configures which adapter message types that should be processed by the AODB pipeline.
    /// </summary>
    public interface IAdapterInitializer
    {
        /// <summary>
        /// Initializes adapter message consumers.
        /// 
        /// Implementations of this method should call
        /// <see cref="IAdapterRegistrant.Register{TMessage}"/> for each
        /// type of adapter message that is to be processed. 
        /// </summary>
        /// <param name="adapterRegistrant"><see cref="IAdapterRegistrant"/> used for registering message types</param>
        public void Initialize(IAdapterRegistrant adapterRegistrant);
    }
}