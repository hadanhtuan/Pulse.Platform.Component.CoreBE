using AODB.Rules.Context.Matching;
using API.Shared.Core.Interfaces.Responses;

namespace API.Shared.Core.Interfaces.Actions
{
    /// <summary>
    ///  Defines methods for executing bulk operations on entities.
    /// </summary>
    public interface IBulkExecutionContext
    {
        /// <summary>
        /// Updates a collection of entities and publishes messages to the Kafka action ui topic. This method serializes each entity to JSON and creates a message to be published to the outbox component. If a notification is provided, it is updated with the task count and acknowledged. If an error occurs during the update, an error notification is published.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities being updated.</typeparam>
        /// <param name="updateEntities">A dictionary where the key is the unique identifier of the entity and the value is the entity to update.</param>
        /// <param name="notification">A notification object to track the progress of the update. This parameter is optional.</param>
        /// <param name="customAttributes">A dictionary of custom attributes to add to each entity. This parameter is optional.</param>
        /// <param name="removedAttributes">A list of attributes to be removed from each entity. This is the same purpose as the Delete method in the ExcecutionContext. This parameter is also optional.</param>
        /// <returns>The Id of the parent operation if the update is successful; otherwise, null.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the action internal name is not set.</exception>
        /// <remarks>
        /// This method serializes each entity to JSON and creates a message to be published to the delivery service. 
        /// If a notification is provided, it is updated with the task count and acknowledged. 
        /// If an error occurs during the update, an error notification is published.
        /// </remarks>
        public string? Update<TEntity>(Dictionary<Guid, TEntity> updateEntities,
            INotification? notification = null,
            IDictionary<string, object>? customAttributes = null,
            List<string>? removedAttributes = null) where TEntity : BaseEntityInput;

        /// <summary>
        /// Sets the internal name of the action to be executed.
        /// </summary>
        /// <param name="actionInternalName">The internal name of the action.</param>
        void SetActionName(string actionInternalName);

        /// <summary>
        /// Sets the label of the action to be executed.
        /// </summary>
        /// <param name="actionLabel">The label of the action.</param>
        void SetActionLabel(string actionLabel);
    }
}