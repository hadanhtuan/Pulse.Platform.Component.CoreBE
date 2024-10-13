using API.Shared.Core.Interfaces.Responses;

namespace API.Shared.Core.Interfaces.Actions
{
    /// <summary>
    /// Defines an action that is called on a specific entity.
    /// </summary>
    /// <typeparam name="TEntity">The database representation of the entity the action is called on.</typeparam>
    /// <typeparam name="TActionConfiguration">The configuration defined for this action.</typeparam>
    public interface IEntityAction<in TEntity, TActionConfiguration> : IAction where TActionConfiguration : class
    {
        /// <summary>
        /// Evaluates a set of preconditions against the state of the entity.
        /// </summary>
        /// <param name="entity">The entity in its unchanged state.</param>
        /// <returns>True if the preconditions are fulfilled.</returns>
        public bool IsValidOn(TEntity entity);

        /// <summary>
        /// Instantiates a version of the configuration as defined for this action.
        /// Includes requirements, presentatation, layout, and default values.
        /// </summary>
        /// <param name="entity">The entity in its unchanged state.</param>
        /// <returns>A configuration that defines the input configuration.</returns>
        public TActionConfiguration GetConfiguration(TEntity entity);

        /// <summary>
        /// Instantiates a version of the configuration as defined for this action.
        /// Includes requirements, presentatation, layout, and default values.
        /// </summary>
        /// <param name="entity">The entity in its unchanged state.</param>
        /// <param name="optionalParameters">Optional parameters for getting the configuration.</param>
        /// <returns>A configuration that defines the input configuration.</returns>
        public TActionConfiguration GetConfiguration(TEntity entity, IDictionary<string, string>? optionalParameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the business logic defined for this action.
        /// </summary>
        /// <param name="entity">The entity affected by the action.</param>
        /// <param name="input">The populatedinput configuration.</param>
        /// <param name="context">Context used to send an update to the AODB.</param>
        /// <returns>An <c>ActionResult</c> with details on the execution.</returns>
        public IActionExecutionResult Execute(TEntity entity, TActionConfiguration input, IExecutionContext context);
    }
}
