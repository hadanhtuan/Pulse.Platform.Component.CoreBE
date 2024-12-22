using Rules.Context.Matching;
using API.Shared.Core.Interfaces.Responses;
using System.Linq.Expressions;

namespace API.Shared.Core.Interfaces.Actions
{
    /// <summary>
    /// A context which enables the actions to send updates to the AODB from the executing logic.
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// Transforms the entity into an update for the AODB, and runs any custom validation/execution
        /// defined for the entity type before publishing the update to the AODB Pipeline.
        /// </summary>
        /// <typeparam name="TEntity">The entity the update should map to.</typeparam>
        /// <param name="entity"></param>
        /// <param name="customAttributes">Custom attributes which can extend the entity model</param>
        void Update<TEntity>(TEntity entity, IDictionary<string, object>? customAttributes = null)
            where TEntity : BaseEntityInput;

        /// <summary>
        /// Marks the property to be deleted when it is picked up by the  If a value is supplied to entity as well, 
        /// all receieved values will be deleted, and the new value applied after.
        /// </summary>
        /// <typeparam name="TEntity">The type of EntityInput to delete a property for.</typeparam>
        /// <typeparam name="TProperty">The property/attribute selector to delete values for.</typeparam>
        /// <param name="entity">The entity that the property/attribute should be deleted for.</param>
        /// <param name="property">The property/attribute that all received values should be deleted for.</param>
        void Delete<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> property)
            where TEntity : BaseEntityInput;

        /// <summary>
        /// Sends a message to the AODB to revert a previously executed action for the entity that the action was called on.
        /// Will identify the last update message from the action with the specified internal name and revert it.
        /// 
        /// Note: If multiple updates are called by the action to revert, multiple <see cref="RevertAction{TRevertable}()"></see>
        /// needs to be called as well. 
        /// </summary>
        void RevertAction<TRevertable>() where TRevertable : IAction, IRevertable;

        /// <summary>
        /// Sends a message to the AODB to revert a previously executed action on the specified entity.
        /// Will identify the last update message from the action with the specified internal name and revert it.
        /// 
        /// Note: If multiple updates are called by the action to revert, multiple <see cref="RevertAction{TRevertable, TEntity}(TEntity)"></see>
        /// needs to be called as well. 
        /// </summary>
        /// <param name="onEntity">The entity that the action should be reverted on.</param>
        void RevertAction<TRevertable, TEntity>(TEntity onEntity)
            where TRevertable : IAction, IRevertable
            where TEntity : BaseEntityInput;

        /// <summary>
        /// Creates a child IExecutionContext. Optionally takes an Id, if updates will
        /// be performed on an existing entity. If null, all updates will be treated as if
        /// they are being performed on nonexistent entities.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="AlternativeEntityType">If the entity type of the sub execution context differes from the execution context</param>
        /// <returns></returns>
        IExecutionContext CreateSubExecutionContext(Guid? entityId = null, string? AlternativeEntityType = null);

        /// <summary>
        /// Creates a main task in the notification component through the notification library. 
        /// Important: Call this method NotifyUser before Update to assure the main task exist before the child tasks.
        /// </summary>
        /// <see cref="Update{TEntity}(TEntity, IDictionary{string, object}?)"/>
        /// <param name="notification"></param> The notification to be displayed in the UI
        /// <param name="actionStatus"></param> The action status to be displayed in the notification toaster
        void NotifyUser(INotification notification, ActionStatus actionStatus);

        /// <summary>
        /// Gets the ParentOperationId of the task-tree triggered by this action
        /// </summary>
        string GetParentOperationId();

        /// <summary>
        /// Returns a service of the given <typeparamref name="ServiceType"/>. This is an extension point
        /// for adding custom services to be registered with the component without making changes to this 
        /// interface.
        /// </summary>
        /// <typeparam name="ServiceType">Type of service</typeparam>
        /// <returns>Instance of the <typeparamref name="ServiceType"/></returns>
        /// <exception cref="NotSupportedException">If the service is not registered.</exception>
        ServiceType GetService<ServiceType>() where ServiceType : class;

        /// <summary>
        /// Sets a custom Action Label to display a desired "Source" in the UI when displaying the changelog. 
        /// </summary>
        /// <param name="customActionLabel">Sets a custom Label.</param>
        public void SetCustomActionLabel(string? customActionLabel);
    }
}