using Library.Persistence.EntityTypes;
using System.Collections.Generic;

namespace Database.Models.Schema
{
    
    /// Definition of the abstract entity type in the 
    /// Encapsulates the shared properties of 
    /// <see cref="RootEntityType"/> and <see cref="ComplexAttributeEntityType"/>.
    
    public abstract class EntityType : Entity
    {
        
        /// Internal name used as a unique identifier for the entity type.
        /// Note the constraints on length and format.
        
        public string InternalName { get; set; } = null!;

        
        /// Internal name in plural of the entity type.
        
        public string PluralName { get; set; } = null!;

        
        /// Name of the entity type as displayed in a user interface.
        
        public string DisplayName { get; set; } = null!;

        
        /// Description of the entity type.
        
        public string? Description { get; set; }

        
        /// Link to external documentation for this entity type.
        /// This can for example be a definition of the entity in a dictionary
        /// or to a custom documentation describing the entity where
        /// it differs from the canonical entity definition.
        
        public string? ExternalDocumentationUrl { get; set; }

        
        /// Description of external documentation for this entity type, if any.
        /// This can explain for example that the documentation is a
        /// definition of the entity in a dictionary or custom definition if
        /// it differs from the canonical definition of the entity.
        
        public string? ExternalDocumentationDescription { get; set; }

        public Status Status { get; set; }

        
        /// A list of indexes, which are used for querying the EntityType.
        /// This is currently only supported for RootEntityType and not for ComplexAttributeEntityType.
        
        public virtual ICollection<Index> Indexes { get; set; } = new List<Index>();

        
        /// A RootEntityType can have a number of Unique Constraints,
        /// which each determine that certain attributes (or expressions on attributes) must be unique
        
        public virtual ICollection<UniqueConstraint> UniqueConstraints { get; set; } = new List<UniqueConstraint>();

        
        /// The schema version this entity was generated from.
        
        public virtual SchemaVersion SchemaVersion { get; set; } = null!;

        public virtual ICollection<AttributeType> AttributeTypes { get; set; } =
            new List<AttributeType>();

        
        /// The actions that can be executed on this entity type
        
        public virtual ICollection<Action> Actions { get; set; } =
            new List<Action>();
    }
}