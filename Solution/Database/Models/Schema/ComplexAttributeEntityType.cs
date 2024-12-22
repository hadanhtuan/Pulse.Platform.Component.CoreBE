using System.Collections.Generic;

namespace Database.Models.Schema
{
    /// <summary>
    /// Definition of an AODB entity type used to model a complex attribute type, 
    /// i.e. a sub-entity stored under a root entity as a JSON string/object.
    /// </summary>
    /// <example>
    /// A complex attribute for CodeshareEnriched could be defined as a ComplexAttributeEntityType
    /// that has attributes for flight number and either external data source or master data reference to 
    /// codeshare agreement used to assign the flight number.
    /// </example>
    public class ComplexAttributeEntityType : EntityType
    {

        /// <summary>
        /// Reference to all the <see cref="ComplexAttributeType"/>s that use this entity type as their ComplexDataType
        /// </summary>
        public virtual ICollection<ComplexAttributeType> ComplexAttributeTypes { get; private set; } = new List<ComplexAttributeType>();

    }
}
