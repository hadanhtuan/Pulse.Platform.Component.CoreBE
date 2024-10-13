namespace API.Shared.Core.Interfaces.Permissions
{
    /// <summary>
    /// Provides <see cref="IPermissions"/> within a user context for running actions.
    /// </summary>
    public interface IPermissionsProvider
    {
        /// <summary>
        /// Returns <see cref="IUserInfo"/> for the given entity type.
        /// </summary>
        IUserInfo UserInfo { get; }
        /// <summary>
        /// Returns <see cref="IPermissions"/> for the given entity type.
        /// </summary>
        IPermissions GetPermissions(string internalEntityName);

        /// <summary>
        /// Returns <see cref="IPermissions"/> for the given entity.
        /// </summary>
        IPermissions GetPermissions<TEntity>(string internalEntityName, TEntity entity);
    }
}
