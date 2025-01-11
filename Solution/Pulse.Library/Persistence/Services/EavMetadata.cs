using Database.Models.Data;
using Database.Models.Schema;
using System;
using System.Linq;

namespace Database.Services;

internal class EavMetadata
{
    public DateTime Received { get; set; }
    public int Priority { get; set; }
    public bool VisitEnrichmentAsInbound { get; set; }
    public bool VisitEnrichmentAsOutbound { get; set; }

    internal static EavMetadata ForRemovedAttribute()
    {
        return new EavMetadata
        {
            Received = DateTime.UtcNow,
            Priority = 0,
            VisitEnrichmentAsInbound = false,
            VisitEnrichmentAsOutbound = false
        };
    }

    public static EavMetadata FromEav(EntityValue ev, Database.Models.Data.Json.EntityAttribute eav) =>
        Builder.Build(ev, eav);

    private static class Builder
    {
        public static EavMetadata Build(EntityValue ev, Database.Models.Data.Json.EntityAttribute attribute)
        {
            var eav = attribute.Best;
            var emd = new EavMetadata
            {
                Received = attribute.Best?.GetReceived() ?? DateTime.UtcNow,
                Priority = eav?.Enrichment?.Priority ?? 0
            };
            if (eav is not null && eav.Status != EntityAttributeStatus.Archived)
            {
                DetermineVisitEnrichmentDirection(ev, eav, emd);
            }
            else
            {
                var resetMarker = attribute.ResetMarkers.OrderByDescending(m => m.Created).FirstOrDefault();
                if (resetMarker is not null)
                {
                    DetermineVisitEnrichmentDirection(ev, resetMarker, emd);
                }
                else
                {
                    return EavMetadata.ForRemovedAttribute();
                }
            }

            return emd;
        }

        private static void DetermineVisitEnrichmentDirection(
            EntityValue ev, Database.Models.Data.Json.EntityAttributeValue eav, EavMetadata edm)
        {
            // Specifically for flightlegs, determine if attribute was created in context of either inbound or outbound visit.
            FlightLeg? evAsFlightLeg = ev as FlightLeg;
            edm.VisitEnrichmentAsInbound = (eav.Enrichment != null &&
                                            eav.Enrichment.EnrichmentContextId == evAsFlightLeg?.VisitAsInbound?.Id);
            edm.VisitEnrichmentAsOutbound = (eav.Enrichment != null &&
                                             eav.Enrichment.EnrichmentContextId == evAsFlightLeg?.VisitAsOutbound?.Id);
        }

        private static void DetermineVisitEnrichmentDirection(
            EntityValue ev, Database.Models.Data.Json.EntityAttributeResetMarker resetMarker, EavMetadata edm)
        {
            // Specifically for flightlegs, determine if attribute was created in context of either inbound or outbound visit.
            FlightLeg? evAsFlightLeg = ev as FlightLeg;
            edm.VisitEnrichmentAsInbound = (resetMarker.EnrichmentContextId == evAsFlightLeg?.VisitAsInbound?.Id);
            edm.VisitEnrichmentAsOutbound = (resetMarker.EnrichmentContextId == evAsFlightLeg?.VisitAsOutbound?.Id);
        }
    }
}