﻿namespace Database.Models.Schema
{
    
    /// Definition of a type of attribute that is defined by a <see cref="ChildEntityType"/>,
    /// i.e. a (list of) child-entity stored under a root entity (or under another child entity).
    
    /// <example>
    /// A sub structure attribute for Codeshare could be defined as a ChildEntityType
    /// that has the necessary attributes for modelling flight number, and this ChildEntityType can then
    /// be used to define a ChildEntityAttributeType on FlightLeg that has a list of Codeshares.
    /// </example>
    public class ChildEntityAttributeType : AttributeType
    {
        public ChildEntityAttributeType(ChildEntityType childEntityType, ChildEntityAttributeDataStructure dataStructure)
        {
            ChildEntityType = childEntityType;
            DataStructure = dataStructure;
        }

        
        /// Parameter-less constructor for Entity Framework
        
        protected ChildEntityAttributeType()
        {
        }

        
        /// The <see cref="ChildEntityType"/> used to define this attribute type.
        /// Defines which sub-attributes can be placed on entity values of this child entity attribute type.
        
        /// <remarks>
        /// Private setter is for Entity Framework (which guarentees not null through required foreign key)
        /// </remarks>
        public virtual ChildEntityType ChildEntityType { get; private set; } = null!;

        
        /// Determines whether this <see cref="ChildEntityAttributeType"/> should contain single entities or lists.
        
        public ChildEntityAttributeDataStructure DataStructure { get; private set; }

    }
}
