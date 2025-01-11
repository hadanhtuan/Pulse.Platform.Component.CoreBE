using Library.Persistence.EntityTypes;

namespace Database.Models.Schema
{
    
    /// Definition of an entity action in the 
    
    public class Action : Entity
    {
        
        /// Internal name used as a unique identifier for the action.
        
        public string InternalName { get; set; } = null!;

        
        /// Short-form name of the action as displayed in a user interface.
        
        public string? ShortDisplayName { get; set; }

        
        /// Long-form name of the action as displayed in a user interface.
        
        public string? LongDisplayName { get; set; }

        
        /// Description of the action.
        
        public string? Description { get; set; }

        
        /// The status of the attribute type.
        
        public Status Status { get; set; }
    }
}