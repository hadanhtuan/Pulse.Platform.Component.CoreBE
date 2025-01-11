using Library.Persistence.EntityTypes;

namespace Database.Models.Schema
{
    
    /// Definition of an attribute in the 
    
    public abstract class AttributeType : Entity
    {
        
        /// Internal name used as a unique identifier for the attribute type.
        /// Note the constraints on the length and format of internal names.
        
        public string InternalName { get; set; } = null!;

        
        /// Short-form name of the attribute type as displayed in a user interface.
        
        public string? ShortDisplayName { get; set; }

        
        /// Long-form name of the attribute type as displayed in a user interface.
        
        public string? LongDisplayName { get; set; }

        
        /// Description of the attribute type.
        
        public string? Description { get; set; }

        
        /// Link to external documentation for this attribute type, if any.
        /// This can for example be a definition of the attribute in a dictionary
        /// or to a custom documentation describing the attribute where
        /// it differs from the canonical definition of the attribute or term.
        
        public string? ExternalDocumentationUrl { get; set; }

        
        /// Description of external documentation for this attribute type, if any.
        /// This can explain for example that the documentation is a
        /// definition of the attribute in a dictionary or custom definition if
        /// it differs from the canonical definition of the attribute or term.
        
        public string? ExternalDocumentationDescription { get; set; }

        
        /// Group that the attribute type belongs to;
        /// used to access the attribute in business rules.
        
        public string? Group { get; set; }

        
        /// The status of the attribute type.
        
        public Status Status { get; set; }

        
        /// The entity type that this attribute type is part of.
        
        public virtual EntityType EntityType { get; set; } = null!;

        
        /// If the attributes has an update action name, the UI will use this to allow
        /// editing by triggering this action.        
        
        /// <remarks>
        /// If an attribute needs a modal action, you need to create a suitable action 
        /// that determines both the input model for UI and the business logic for how to update 
        /// the attribute.
        /// </remarks>
        public string? UpdateAction { get; set; }
    }
}