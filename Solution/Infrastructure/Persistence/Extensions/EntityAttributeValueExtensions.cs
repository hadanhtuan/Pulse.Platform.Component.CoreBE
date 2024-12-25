using Database.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace Database.Extensions
{
    public static class EntityAttributeValueExtensions
    {
        public static void RemoveSourceSpecificResetEavs(this List<EntityAttributeValue> eavList)
        {
            var latestSourceSpecificResetMarksForEachSource =
                eavList.OfType<EnrichmentEntityAttributeValue>()
                    .Where(eEav => eEav.ResetMarkSpecificSource != null)
                    .GroupBy(eEav => eEav.ResetMarkSpecificSource!)
                    .Select(grouping => grouping.OrderByDescending(eEav => eEav.Received).First());

            foreach (var rEav in latestSourceSpecificResetMarksForEachSource)
            {
                eavList.RemoveAll(eav =>
                    eav is MessageEntityAttributeValue { RawMessage: SystemMessage sMEav } &&
                    sMEav.Source == rEav.ResetMarkSpecificSource &&
                    eav.GetBestCreatedAtValue() < rEav.Received);
            }
        }

        public static void RemoveAllEavsReceivedBeforeLatestResetMarkFilter(this List<EntityAttributeValue> eavList)
        {
            var firstResetMark = eavList.OfType<EnrichmentEntityAttributeValue>()
                .OrderByDescending(eEav => eEav.Received)
                .FirstOrDefault(eEav => eEav.IsResetMark);

            if (firstResetMark != null)
            {
                eavList.RemoveAll(eav =>
                eav.GetBestCreatedAtValue() < firstResetMark.Received);
            }
        }

        public static System.DateTime GetBestCreatedAtValue(this EntityAttributeValue eav)
        {
            return eav switch
            {
                MessageEntityAttributeValue mEav => mEav.Created ?? mEav.RawMessage.Received,
                EnrichmentEntityAttributeValue eEav => eEav.Received,
                _ => throw new System.NotSupportedException($"The type {eav.GetType()} is not supported.")
            };
        }
    }
}
