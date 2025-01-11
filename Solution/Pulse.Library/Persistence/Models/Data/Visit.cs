using Database.Data;
using Database.Models.Schema;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Database.Models.Data
{
    
    /// Aircraft visit consisting of an inbound flight, an outbound flight,
    /// and a number of ground legs for movements on the ground.
    
    public class Visit : EntityValue
    {
        public Visit(SchemaContext context) : base(context) { }
        public Visit() { }

        public int? Priority { get; set; }

        public string? LinkedDueTo { get; set; }

        
        /// The flight leg that is the arriving flight leg.
        
        public virtual FlightLeg? InboundFlight { get; private set; }
        public Guid? InboundFlightId;

        
        /// The flight leg that is the departing flight leg. 
        
        public virtual FlightLeg? OutboundFlight { get; private set; }
        public Guid? OutboundFlightId;

        
        /// Sets the arrival flight leg of this visit.
        
        /// <param name="flightLeg"></param>
        public void SetInbound(FlightLeg? flightLeg)
        {
            InboundFlight = flightLeg;
            if (flightLeg != null)
            {
                flightLeg.VisitAsInbound = this;
            }
        }

        
        /// Sets the departure flight leg of this visit.
        
        /// <param name="flightLeg"></param>
        public void SetOutbound(FlightLeg? flightLeg)
        {
            OutboundFlight = flightLeg;
            if (flightLeg != null)
            {
                flightLeg.VisitAsOutbound = this;
            }
        }

        
        /// Groundlegs belonging to this visit.
        
        public virtual ICollection<GroundLeg> GroundLegs { get; } = new Collection<GroundLeg>();

        
        /// Create attribute values for the fixed priority and linked_due_to if they are also defined
        /// as attributes on the visit in the schema.
        
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