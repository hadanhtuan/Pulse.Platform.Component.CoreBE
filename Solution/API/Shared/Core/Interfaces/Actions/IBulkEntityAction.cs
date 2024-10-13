
using API.Shared.Core.Interfaces.Permissions;
using API.Shared.Core.Interfaces.Responses;

namespace API.Shared.Core.Interfaces.Actions
{
    /// <summary>
    /// Defines an action that is called on specified entities.
    /// </summary>
    /// <typeparam name="TEntity">The database representation of the entity the action is called on.</typeparam>
    /// <typeparam name="TActionConfiguration">The configuration defined for this action.</typeparam>
    public interface IBulkEntityAction<TEntity, TActionConfiguration> : IBulkAction where TActionConfiguration : class
    {
        /// <summary>
        /// Evaluates if the current request user has permission on each submitted entity. (scoped entity permissions)
        /// </summary>
        /// <param name="permissionEntities">Dictionary where the key is the entity and the value is its permission</param>
        /// <returns>
        /// Dictionary where the key is the entity identifier (`Guid`) and the value indicates whether 
        /// the role has permission to perform the action on that entity.
        /// </returns>
        public IDictionary<Guid, bool> HasEntityPermissions(IDictionary<TEntity, IPermissions> permissionEntities);

        /// <summary>
        /// Evaluates a set of preconditions on each entity against the state of the entity.
        /// </summary>
        /// <param name="entities">Collection of entities in their unchanged state.</param>
        /// <returns>Dictionary where the key is the entity identifier (`Guid`) and the value is an array of 
        /// strings describing validation errors, or an empty array if the entity meets all preconditions.</returns>
        public IDictionary<Guid, string[]> IsValidOn(IEnumerable<TEntity> entities);

        /// <summary>
        /// Instantiates a version of the configuration as defined for this action.
        /// </summary>
        /// <param name="entities">The list of entities in its unchanged state.</param>
        /// <returns>A configuration that defines the input configuration.</returns>
        public TActionConfiguration GetConfiguration(IEnumerable<TEntity> entities);

        /// <summary>
        /// Executes the business logic defined for this action.
        /// </summary>
        /// <param name="entities">The entity affected by the action.</param>
        /// <param name="input">The populated input configuration.</param>
        /// <param name="context">A new execution context to support send updated value of multiple entities to the processing pipeline.</param>
        /// <returns>An <c>ActionResult</c> with details on the execution.</returns>
        public IActionExecutionResult Execute(IEnumerable<TEntity> entities, TActionConfiguration input, IBulkExecutionContext context);
    }
}