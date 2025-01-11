using Pulse.Library.Core.Actions.Permissions;

namespace Pulse.Library.Action;


/// This class is an optional abstract class that actions can extend in order to access
/// the RuleContext and their IPermissions object, which would otherwise not be accessible.

public abstract class ActionBase
{
    private readonly IPermissionsProvider permissionsProvider;

    protected RuleContext RuleContext { get; }
    protected IAodbContextAccess AodbContextAccess { get; }

    protected ActionBase(RuleContext ruleContext, IActionContext actionContext)
    {
        RuleContext = ruleContext;
        AodbContextAccess = new AodbContextAccess(ruleContext.Aodb);
        this.permissionsProvider = actionContext.PermissionsProvider;
        this.UserInfo = actionContext.PermissionsProvider.UserInfo;
    }

    protected IUserInfo UserInfo { get; }
    protected IPermissions GetPermissions<TEntity>(string internalEntityName, TEntity entity) => 
        permissionsProvider.GetPermissions(internalEntityName, entity);
}