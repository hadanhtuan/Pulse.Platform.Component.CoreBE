
using API.Shared.Core.Interfaces.Permissions;

namespace API.Shared.Core.Interfaces.Actions
{
    /// <summary>
    /// Root interface for both <see cref="IEntityAction{TEntity, TActionConfiguration}"/> 
    /// and <see cref="IEntityTypeAction{TActionConfiguration}"/>.
    /// for the common permission functionality.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Evalutes the permissions of the access token in the request.
        /// </summary>
        /// <param name="permissions">Interface for evaluating permissions.</param>
        /// <returns>True if the role has access to this action, otherwise false.</returns>
        public bool HasPermission(IPermissions permissions);
    }
}