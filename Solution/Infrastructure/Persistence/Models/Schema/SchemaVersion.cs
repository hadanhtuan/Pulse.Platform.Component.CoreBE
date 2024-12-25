using Library.Persistence.EntityTypes;
using System;
using System.Collections.Generic;

namespace Database.Models.Schema
{
    /// <summary>
    /// Version of the definition of the schema used in the 
    /// Contains definitions of all the entity types used in the 
    /// </summary>
    public class SchemaVersion : AggregateRoot
    {
        /// <summary>
        /// Notes on this version.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Version number assigned on insert in the AODB as the
        /// increment of the previous version.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Optional build number which may be correlated to the build
        /// number set on assemblies containing business rules.
        /// </summary>
        public string? BuildNumber { get; set; }

        /// <summary>
        /// The time when the schema became active in the 
        /// </summary>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// Definition of the entity types in the 
        /// </summary>
        public virtual ICollection<EntityType> EntityTypes { get; set; } = new List<EntityType>();
    }
}