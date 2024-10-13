namespace API.Shared.Core.Interfaces.Context.Models
{
    public enum InvokingReason
    {
        RelinkInbound,
        RelinkOutbound,
        EntityValueUpdate,
        BusinessRuleUpdate,
        MasterDataUpdate,
        TimedUpdate,
        ManualMatch,
    }
}
