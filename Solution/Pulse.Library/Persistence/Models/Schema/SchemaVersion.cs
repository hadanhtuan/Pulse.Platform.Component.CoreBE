using Library.Persistence.EntityTypes;
using System;
using System.Collections.Generic;

namespace Database.Models.Schema
{
    
    /// Version of the definition of the schema used in the 
    /// Contains definitions of all the entity types used in the 
    
    public class SchemaVersion : AggregateRoot
    {
        
        /// Notes on this version.
        
        public string? Note { get; set; }

        
        /// Version number assigned on insert in the AODB as the
        /// increment of the previous version.
        
        public int Version { get; set; }

        
        /// Optional build number which may be correlated to the build
        /// number set on assemblies containing business rules.
        
        public string? BuildNumber { get; set; }

        
        /// The time when the schema became active in the 
        
        public DateTime ValidFrom { get; set; }

        
        /// Definition of the entity types in the 
        
        public virtual ICollection<EntityType> EntityTypes { get; set; } = new List<EntityType>();
    }
}