using Database.Models.Data;
using System;
using System.Linq;

namespace Pipeline.Steps.MessageReceiver
{
    public static class InvokingReasonExtensions
    {
        public static bool IsRecalculation(this InvokingReason reason) => recalculationReasons.Contains(reason);

        private static readonly InvokingReason[] recalculationReasons =
        {
            InvokingReason.BusinessRuleTrigger, InvokingReason.ScheduledUpdate, InvokingReason.MasterDataUpdate,
            InvokingReason.BusinessRuleUpdate
        };
    }
}