
using Database.Data;

namespace Database.Models.Data
{
    /// <summary>
    /// A ground leg is part of a visit.
    /// </summary>
    public class GroundLeg : EntityValue
    {
        public GroundLeg(SchemaContext context) : base(context) { }
        public GroundLeg() { }

        public virtual Visit? Visit { get; set; }

    }
}