using Library.Persistence.EntityTypes;

namespace Database.Models.Schema
{
    /// <summary>
    /// Definition of an attribute in the 
    /// </summary>
    public abstract class AttributeType : Entity
    {
        /// <summary>
        /// Internal name used as a unique identifier for the attribute type.
        /// Note the constraints on the length and format of internal names.
        /// </summary>
        public string InternalName { get; set; } = null!;

        /// <summary>
        /// Short-form name of the attribute type as displayed in a user interface.
        /// </summary>
        public string? ShortDisplayName { get; set; }

        /// <summary>
        /// Long-form name of the attribute type as displayed in a user interface.
        /// </summary>
        public string? LongDisplayName { get; set; }

        /// <summary>
        /// Description of the attribute type.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Link to external documentation for this attribute type, if any.
        /// This can for example be a definition of the attribute in a dictionary
        /// or to a custom documentation describing the attribute where
        /// it differs from the canonical definition of the attribute or term.
        /// </summary>
        public string? ExternalDocumentationUrl { get; set; }

        /// <summary>
        /// Description of external documentation for this attribute type, if any.
        /// This can explain for example that the documentation is a
        /// definition of the attribute in a dictionary or custom definition if
        /// it differs from the canonical definition of the attribute or term.
        /// </summary>
        public string? ExternalDocumentationDescription { get; set; }

        /// <summary>
        /// Group that the attribute type belongs to;
        /// used to access the attribute in business rules.
        /// </summary>
        public string? Group { get; set; }

        /// <summary>
        /// The status of the attribute type.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// The entity type that this attribute type is part of.
        /// </summary>
        public virtual EntityType EntityType { get; set; } = null!;

        /// <summary>
        /// If the attributes has an update action name, the UI will use this to allow
        /// editing by triggering this action.        
        /// </summary>
        /// <remarks>
        /// If an attribute needs a modal action, you need to create a suitable action 
        /// that determines both the input model for UI and the business logic for how to update 
        /// the attribute.
        /// </remarks>
        public string? UpdateAction { get; set; }
    }
}