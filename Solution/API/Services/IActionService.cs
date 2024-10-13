using Newtonsoft.Json.Linq;
using SA.Aodb.Actions.Framework.IO.Responses;
using SA.Aodb.Actions.Framework.Providers.Cache;
using SA.Aodb.Actions.Interfaces.Responses;
using System;
using System.Collections.Generic;

namespace API.Services
{
    public interface IActionService
    {
        /// <summary>
        /// Returns all actions.
        /// </summary>
        /// <returns>A list of all low level actions.</returns>
        IEnumerable<ActionInfo> GetAllActions();

        /// <summary>
        /// Fetches all actions for the specified entity type including information on
        /// whether permissions were fulfilled. Will always fulfill preconditions.
        /// </summary>
        /// <param name="entityName">The internal name of the entity type</param>
        /// <param name="includeHidden">Option to include hidden actions</param>
        /// <returns>A list of all actions with limited information</returns>
        IEnumerable<MinimalActionView> GetEntityActions(string entityName, bool includeHidden = false);

        /// <summary>
        /// Fetches all actions for the specified entity type including information on
        /// whether permissions and preconditions are fulfilled.
        ///
        /// Ignores actions that were set to hidden.
        /// </summary>
        /// <param name="entityTypeName">The internal name of the entity type</param>
        /// <param name="entityId">The unique identifier of the entity</param>
        /// <param name="includeHidden">Option to include hidden actions</param>
        /// <returns>A list of all actions</returns>
        IEnumerable<MinimalActionView> GetEntityActions(string entityTypeName, Guid entityId, bool includeHidden = false);

        /// <summary>
        /// Returns detailed info about the action configuration
        /// </summary>
        /// <param name="actionName">The internal name of the action</param>
        /// <param name="entityTypeName">The internal name of the entity type</param>
        /// <returns>A detailed action configuration</returns>
        DetailedActionView GetEntityTypeActionConfiguration(string actionName, string entityTypeName);

        /// <summary>
        /// Returns detailed info about the action configuration
        /// </summary>
        /// <param name="actionName">The internal name of the action</param>
        /// <param name="entityTypeName">The internal name of the entity type</param>
        /// <param name="entityId">The unique identifier of the entity instance</param>
        /// <param name="optionalParameters">Optional parameters for getting the configuration.</param>
        /// <returns>A detailed action configuration</returns>
        DetailedActionView GetEntityActionConfiguration(
            string actionName,
            string entityTypeName,
            Guid entityId,
            IDictionary<string, string>? optionalParameters);

        /// <summary>
        /// Executes an action that targets an entity_type instead of an entity instance.
        /// </summary>
        /// <param name="actionName">Internal name of the action</param>
        /// <param name="entityTypeName">Internal name of the entity type</param>
        /// <param name="input">Input data configuration</param>
        /// <returns>A detailed result of the execution</returns>
        IActionExecutionResult ExecuteEntityTypeAction(string actionName, string entityTypeName,
            JObject input);

        /// <summary>
        /// Executes an action identified with action on the entity instance with entity id of type entity_type.
        /// Returns error when action is not allowed for current user role.
        /// </summary>
        /// <param name="actionName">Internal name of the action</param>
        /// <param name="entityTypeName">Internal name of the entity type</param>
        /// <param name="entityId">The unique identifier of the enityt instance</param>
        /// <param name="input">Input data configuration</param>
        /// <returns>A detailed result of the execution</returns>
        IActionExecutionResult ExecuteEntityAction(string actionName, string entityTypeName, Guid entityId,
            JObject input);
    }
}