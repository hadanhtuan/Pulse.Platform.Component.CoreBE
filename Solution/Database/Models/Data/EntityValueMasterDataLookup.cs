using System;
using Library.Persistence.EntityTypes;

namespace Database.Models.Data
{
    public class EntityValueMasterDataLookup : AggregateRoot
    {
        public virtual EntityValue EntityValue { get; set; } = null!;

        public Guid EntityValueId { get; set; }

        public DateTime Pointintime { get; set; }

        public ContextLookupSource LookupContextType { get; set; }
    }
}