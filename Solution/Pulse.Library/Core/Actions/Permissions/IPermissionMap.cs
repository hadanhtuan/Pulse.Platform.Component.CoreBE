namespace Pulse.Library.Core.Actions.Permissions;


/// Map of permissions to access entities and their attributes.

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