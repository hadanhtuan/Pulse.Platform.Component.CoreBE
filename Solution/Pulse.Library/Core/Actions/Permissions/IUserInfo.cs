namespace Pulse.Library.Core.Actions.Permissions;

public interface IUserInfo
{
    Guid UserId { get; }

    Guid ActiveRole { get; }
}