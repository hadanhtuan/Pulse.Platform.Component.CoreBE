
using API.Shared.Core.Interfaces.Responses;

namespace API.Shared.Core.Interfaces.Actions
{
    /// <summary>
    /// Defines an action that is called on a specific entity type.
    /// </summary>
    /// <typeparam name="TActionConfiguration">The configuration defined for this action.</typeparam>
    public interface IEntityTypeAction<TActionConfiguration> : IAction where TActionConfiguration : class
    {
        /// <summary>
        /// Instantiates a version of the configuration as defined for this action.
        /// Includes requirements, presentatation, layout, and default values.
        /// </summary>
        /// <returns>A configuration that defines the input configuration.</returns>
        public TActionConfiguration GetConfiguration();

        /// <summary>
        /// Executes the business logic defined for this action.
        /// </summary>
        /// <param name="input">The populated input configuration.</param>
        /// <param name="context">Context used to send an update to the AODB.</param>
        /// <returns>An <c>ActionResult</c> with details on the execution.</returns>
        public IActionExecutionResult Execute(TActionConfiguration input, IExecutionContext context);
    }
}
