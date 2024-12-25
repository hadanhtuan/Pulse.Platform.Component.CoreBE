using System.Collections.Generic;

namespace Database.Models.Schema
{
    /// <summary>
    /// Definition of an AODB entity type used to model a child entity  attribute type, 
    /// i.e. a (list of) child-entity stored under a root entity (or under another child entity).
    /// </summary>
    /// <example>
    /// A sub structure attribute for Codeshare could be defined as a ChildEntityType
    /// that has the necessary attributes for modelling flight number, and this ChildEntityType can then
    /// be used to define a ChildEntityAttributeType on FlightLeg that has a list of Codeshares.
    /// </example>
    public class ChildEntityType : EntityType
    {

        /// <summary>
        /// Reference to all the <see cref="ComplexAttributeType"/>s that use this entity type as their ComplexDataType
        /// </summary>
        public virtual ICollection<ChildEntityAttributeType> ChildEntityAttributeTypes { get; private set; } = new List<ChildEntityAttributeType>();

    }
}
