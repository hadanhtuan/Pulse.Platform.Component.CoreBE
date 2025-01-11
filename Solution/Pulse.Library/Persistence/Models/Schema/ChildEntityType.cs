using System.Collections.Generic;

namespace Database.Models.Schema
{
    
    /// Definition of an AODB entity type used to model a child entity  attribute type, 
    /// i.e. a (list of) child-entity stored under a root entity (or under another child entity).
    
    /// <example>
    /// A sub structure attribute for Codeshare could be defined as a ChildEntityType
    /// that has the necessary attributes for modelling flight number, and this ChildEntityType can then
    /// be used to define a ChildEntityAttributeType on FlightLeg that has a list of Codeshares.
    /// </example>
    public class ChildEntityType : EntityType
    {

        
        /// Reference to all the <see cref="ComplexAttributeType"/>s that use this entity type as their ComplexDataType
        
        public virtual ICollection<ChildEntityAttributeType> ChildEntityAttributeTypes { get; private set; } = new List<ChildEntityAttributeType>();

    }
}
