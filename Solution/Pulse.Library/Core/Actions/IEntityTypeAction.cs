using Pulse.Library.Core.Actions;
using Pulse.Library.Core.Actions.Responses;

namespace Pulse.Library.Core.Actions.Actions
{
    
    /// Defines an action that is called on a specific entity type.
    
    /// <typeparam name="TActionConfiguration">The configuration defined for this action.</typeparam>
    public interface IEntityTypeAction<TActionConfiguration> : IAction where TActionConfiguration : class
    {
        
        /// Instantiates a version of the configuration as defined for this action.
        /// Includes requirements, presentatation, layout, and default values.
        
        /// <returns>A configuration that defines the input configuration.</returns>
        public TActionConfiguration GetConfiguration();

        
        /// Executes the business logic defined for this action.
        
        /// <param name="input">The populated input configuration.</param>
        /// <param name="context">Context used to send an update to the AODB.</param>
        /// <returns>An <c>ActionResult</c> with details on the execution.</returns>
        public IActionExecutionResult Execute(TActionConfiguration input, IExecutionContext context);
    }
}
