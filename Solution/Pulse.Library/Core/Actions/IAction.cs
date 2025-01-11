using Pulse.Library.Core.Actions.Actions;
using Pulse.Library.Core.Actions.Permissions;

namespace Pulse.Library.Core.Actions;


/// Root interface for both <see cref="IEntityAction{TEntity,TActionConfiguration}"/> 
/// and <see cref="IEntityTypeAction{TActionConfiguration}"/>.
/// for the common permission functionality.

public interface IAction
{
    
    /// Evalutes the permissions of the access token in the request.
    
    /// <param name="permissions">Interface for evaluating permissions.</param>
    /// <returns>True if the role has access to this action, otherwise false.</returns>
    public bool HasPermission(IPermissions permissions);
}