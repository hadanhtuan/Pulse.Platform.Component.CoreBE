using Database.Data;
using Database.Models.Schema;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Database.Models.Data
{
    /// <summary>
    /// Aircraft visit consisting of an inbound flight, an outbound flight,
    /// and a number of ground legs for movements on the ground.
    /// </summary>
    public class Visit : EntityValue
    {
        public Visit(SchemaContext context) : base(context) { }
        public Visit() { }

        public int? Priority { get; set; }

        public string? LinkedDueTo { get; set; }

        /// <summary>
        /// The flight leg that is the arriving flight leg.
        /// </summary>
        public virtual FlightLeg? InboundFlight { get; private set; }
        public Guid? InboundFlightId;

        /// <summary>
        /// The flight leg that is the departing flight leg. 
        /// </summary>
        public virtual FlightLeg? OutboundFlight { get; private set; }
        public Guid? OutboundFlightId;

        /// <summary>
        /// Sets the arrival flight leg of this visit.
        /// </summary>
        /// <param name="flightLeg"></param>
        public void SetInbound(FlightLeg? flightLeg)
        {
            InboundFlight = flightLeg;
            if (flightLeg != null)
            {
                flightLeg.VisitAsInbound = this;
            }
        }

        /// <summary>
        /// Sets the departure flight leg of this visit.
        /// </summary>
        /// <param name="flightLeg"></param>
        public void SetOutbound(FlightLeg? flightLeg)
        {
            OutboundFlight = flightLeg;
            if (flightLeg != null)
            {
                flightLeg.VisitAsOutbound = this;
            }
        }

        /// <summary>
        /// Groundlegs belonging to this visit.
        /// </summary>
        public virtual ICollection<GroundLeg> GroundLegs { get; } = new Collection<GroundLeg>();

        /// <summary>
        /// Create attribute values for the fixed priority and linked_due_to if they are also defined
        /// as attributes on the visit in the schema.
        /// </summary>
        protected override void OnUpdateState()
        {
            if (JsonState.EntityType.AttributeTypes.Any(a => a.InternalName == StandardElements.Priority) &&
                Priority != null)
            {
                JsonState.AddAttributeEnrichmentValue(
                    StandardElements.Priority, Priority, null!, Id, 0, null, null, null);
            }
            if (JsonState.EntityType.AttributeTypes.Any(a => a.InternalName == StandardElements.LinkedDueTo) &&
                LinkedDueTo != null)
            {
                JsonState.AddAttributeEnrichmentValue(
                    StandardElements.LinkedDueTo, LinkedDueTo, null!, Id, 0, null, null, null);
            }
        }
    }
}