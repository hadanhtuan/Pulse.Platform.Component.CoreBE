using System;

namespace Database.Models.Data
{
    public class EnrichmentEntityAttributeValue : EntityAttributeValue
    {
        public string? MasterDataSourceTable { get; set; }

        public Guid? MasterDataSourceId { get; set; }

        public virtual EntityAttributeValue? AodbSource { get; set; }

        /// <summary>
        /// Contains a stack trace for the rule that caused the value to be set.
        /// </summary>
        public string CausedBy { get; set; } = null!;

        /// <summary>
        /// The time when this enrichment EAV was created. The semantics are that
        /// the value was received from a rule.
        /// </summary>
        public DateTime Received { get; set; }

        public int Priority { get; set; }

        public int Credibility { get; set; }

        public virtual EntityValue? EnrichmentContext { get; set; } = null!;

        public bool IsResetMark { get; set; } = false;

        public string? ResetMarkSpecificSource { get; set; } = null;

        public bool MetaDataAreEqualTo(EntityAttributeValue otherEav) =>
            otherEav is EnrichmentEntityAttributeValue otherEnrichmentEav &&
            Priority == otherEnrichmentEav.Priority &&
            Credibility == otherEnrichmentEav.Credibility &&
            EnrichmentContext == otherEnrichmentEav.EnrichmentContext &&
            CausedBy == otherEnrichmentEav.CausedBy &&
            MasterDataSourceId == otherEnrichmentEav.MasterDataSourceId &&
            MasterDataSourceTable == otherEnrichmentEav.MasterDataSourceTable &&
            ActionEffect == otherEnrichmentEav.ActionEffect &&
            AffectedBy == otherEnrichmentEav.AffectedBy &&
            IsResetMark == otherEnrichmentEav.IsResetMark &&
            ResetMarkSpecificSource == otherEnrichmentEav.ResetMarkSpecificSource &&
            AreSourcesEqualTo(otherEnrichmentEav.AodbSource);

        public bool AreSourcesEqualTo(EntityAttributeValue? other)
        {
            if (other == null || AodbSource == null)
            {
                return other == AodbSource;
            }
            if (other is EnrichmentEntityAttributeValue otherEnrichment)
            {
                return otherEnrichment.MetaDataAreEqualTo(AodbSource);
            }
            return other == AodbSource;
        }
    }
}