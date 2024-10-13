namespace API.Shared.Core.Interfaces.Permissions
{
    /// <summary>
    /// Map of permissions to access entities and their attributes.
    /// </summary>
    public interface IPermissionMap
    {
        bool CanReadAll { get; }

        bool CanWriteAll { get; }

        bool CanExecuteAll { get; }

        bool CanCreate { get; }

        IEnumerable<string> AllowedActions { get; }

        bool CanRead(string attributeInternalName);

        bool CanWrite(string attributeInternalName);

        bool CanExecute(string actionInternalName);
    }
}
