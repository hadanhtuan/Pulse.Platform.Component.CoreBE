using Database.Data;

namespace Database.Models.Data
{
    
    /// A flight leg that can be linked to other flight legs to form visits.
    
    public class FlightLeg : EntityValue
    {
        public FlightLeg(SchemaContext context) : base(context) { }
        public FlightLeg() { }

        public virtual Visit? VisitAsOutbound { get; internal set; } = null;
        public virtual Visit? VisitAsInbound { get; internal set; } = null;
    }
}