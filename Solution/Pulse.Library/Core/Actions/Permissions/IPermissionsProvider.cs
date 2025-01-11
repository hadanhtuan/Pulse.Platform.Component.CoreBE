namespace Pulse.Library.Core.Actions.Permissions;


/// Provides <see cref="IPermissions"/> within a user context for running actions.

public interface IPermissionsProvider
{
    
    /// Returns <see cref="IUserInfo"/> for the given entity type.
    
    IUserInfo UserInfo { get; }
    
    /// Returns <see cref="IPermissions"/> for the given entity type.
    
    IPermissions GetPermissions(string internalEntityName);

    
    /// Returns <see cref="IPermissions"/> for the given entity.
    
    IPermissions GetPermissions<TEntity>(string internalEntityName, TEntity entity);
}