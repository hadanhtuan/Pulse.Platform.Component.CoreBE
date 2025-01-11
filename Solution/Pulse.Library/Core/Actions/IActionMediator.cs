using Newtonsoft.Json.Linq;
using Pulse.Library.Core.Actions;
using Pulse.Library.Core.Actions.Actions;
using Pulse.Library.Core.Actions.Responses;
using System;
using System.Collections.Generic;

namespace Pulse.Library.Core.Actions.Framework
{
    
    /// Mediates the communication between the AODB component and the actions.
    /// Responsible for instantiating the action rules and fetching the correct entries from the
    /// aodb database.
    
    public interface IActionMediator
    {
        
        /// Instantiates the corresponding Entity type action and evaluates whether the caller is
        /// permitted to run the action.
        
        /// <param name="actionType">The action that permission should be evaluated against</param>
        /// <param name="entityType">The internal name of the entity type</param>
        /// <returns>true if access is allowed.</returns>
        public bool HasEntityTypePermission<TActionConfiguration>(Type actionType, string entityType)
            where TActionConfiguration : class;

        
        /// Instantiates the corresponding entity action and evaluates whether the caller is
        /// permitted to run the action.
        
        /// <param name="actionType">The action that permission should be evaluated against</param>
        /// <param name="entityType">The internal name of the entity type</param>
        /// <param name="entityId">The internal unique identifier of the entity</param>
        /// <param name="entity">An optional param that can be provided in order to avoid a db call. Useful for when
        /// multiple actions are evaluated on the same entity</param>
        /// <returns>true if access is allowed.</returns>
        public bool HasEntityPermission<TEntity, TActionConfiguration>(Type actionType, string entityType, Guid entityId, TEntity? entity = null)
            where TActionConfiguration : class
            where TEntity : class;

        
        /// Loads the database entry and evaluates the precondition logic against it.
        
        /// <param name="actionType">The IEntityAction containing the logic.</param>
        /// <param name="entityType">The type of entity the action is designed for</param>
        /// <param name="entityId">The internal unique identifier of the entity</param>
        /// <param name="entity">An optional param that can be provided in order to avoid a db call. Useful for when
        /// multiple actions are evaluated on the same entity</param>
        /// <returns>True if preconditions are fulfilled</returns>
        public bool IsValidOn<TEntity, TActionConfiguration>(Type actionType, string entityType, Guid entityId, TEntity? entity = null)
            where TActionConfiguration : class
            where TEntity : class;

        
        /// Returns the action configuration model if the caller has permission to fetch it.
        
        /// <param name="actionType">The IEntityAction type</param>
        /// <param name="entityType">Internal name of the entity type</param>
        /// <param name="entityId">Unique identifier of the entity</param>
        /// <param name="optionalParameters">Optional parameters for getting the configuration.</param>
        /// <returns>The action configuration describing the input data.</returns>
        public TActionConfiguration GetEntityActionConfiguration<TEntity, TActionConfiguration>(Type actionType,
            string entityType, Guid entityId, IDictionary<string, string>? optionalParameters = null)
            where TActionConfiguration : class;

        
        /// Executes the action using the input model if the caller has permission and the preconditions are fulfilled.
        
        /// <param name="actionType">The IEntityAction type</param>
        /// <param name="entityType">Internal name of the entity type</param>
        /// <param name="entityId">Unique identifier of the entity</param>
        /// <param name="input">The model the action has been configured for</param>
        /// <param name="context">Execution context</param>
        /// <returns>The result of the execution</returns>
        public IActionExecutionResult ExecuteEntityAction<TEntity, TActionConfiguration>(Type actionType,
            string entityType, Guid entityId, JObject input, IExecutionContext context)
            where TActionConfiguration : class;

        
        /// Returns the action configuration model if the caller has permission to fetch it.
        
        /// <param name="actionType">The IEntityTypeAction type</param>
        /// <param name="entityType">The internal name of the entity type</param>
        /// <returns>The action configuration describing the input data.</returns>
        public TActionConfiguration GetEntityTypeActionConfiguration<TActionConfiguration>(Type actionType,
            string entityType)
            where TActionConfiguration : class;

        
        /// Executes the action using the input model if the caller has permission and the preconditions are fulfilled.
        
        /// <param name="actionType">The IEntityTypeAction type</param>
        /// <param name="entityType">The internal name of the entity type</param>
        /// <param name="input">The model the action has been configured for</param>
        /// <param name="context">Execution context</param>
        /// <returns>The result of the execution</returns>
        public IActionExecutionResult ExecuteEntityTypeAction<TActionConfiguration>(Type actionType, string entityType,
            JObject input, IExecutionContext context)
            where TActionConfiguration : class;

        
        /// Loads an entity from the query database
        
        /// <typeparam name="TEntity">The entity type to load.</typeparam>
        /// <param name="entityType">The internal name of the entity type</param>
        /// <param name="entityId">The unique identifier of the entity</param>
        /// <returns>An aodb query entity</returns>
        public TEntity GetEntity<TEntity>(string entityType, Guid entityId);
    }
}
