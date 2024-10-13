namespace API.Shared.Core.Interfaces.Actions
{
    /// <summary>
    /// Interface intentionally left empty. The purpose is to let ActionProvider
    /// in component MEE can detect this action without implement the IAction
    /// which contains HasPermission method is not used for IBulkEntityAction.
    /// </summary>
    public interface IBulkAction
    {
    }
}
