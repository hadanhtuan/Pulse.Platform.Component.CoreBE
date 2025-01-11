
using Database.Data;

namespace Database.Models.Data
{
    
    /// A ground leg is part of a visit.
    
    public class GroundLeg : EntityValue
    {
        public GroundLeg(SchemaContext context) : base(context) { }
        public GroundLeg() { }

        public virtual Visit? Visit { get; set; }

    }
}