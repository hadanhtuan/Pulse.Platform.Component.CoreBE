namespace Database.Models.Data
{
    public enum InvokingReason
    {
        RelinkInbound,
        RelinkOutbound,
        EntityValueUpdate,
        BusinessRuleUpdate,
        MasterDataUpdate,
        ScheduledUpdate,
        BusinessRuleTrigger,
        ManualMatch,
        ManualUnMatch,
        RequestedViaApi
    }
}