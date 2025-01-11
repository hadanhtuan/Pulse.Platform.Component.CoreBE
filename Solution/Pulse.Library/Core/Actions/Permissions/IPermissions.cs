namespace Pulse.Library.Core.Actions.Permissions;


/// Interface for evaluating whether the role has access to perform the action.

public interface IPermissions
{
    
    /// Interface to evaluate permissions against the ACL model where
    /// the scopes have already been populated based on the current entity.
    
    public IPermissionMap PermissionMap { get; }

    
    /// Evalutes whether the role has access to an entity-level permission action.
    
    /// <param name="action">The permission action to validate.</param>
    /// <returns>True if the role has permission, otherwise false.</returns>
    public bool HasEntityAction(string action);

    
    /// Evaluates whether the role has write access to the attribute on this entity.
    
    /// <param name="attribute">The internal name of the attribute.</param>
    /// <returns>True if the role has write access, otherwise false.</returns>
    public bool HasAttributeWriteAccess(string attribute);
}