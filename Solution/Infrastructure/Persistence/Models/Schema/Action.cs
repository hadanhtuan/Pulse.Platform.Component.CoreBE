using Library.Persistence.EntityTypes;

namespace Database.Models.Schema
{
    /// <summary>
    /// Definition of an entity action in the 
    /// </summary>
    public class Action : Entity
    {
        /// <summary>
        /// Internal name used as a unique identifier for the action.
        /// </summary>
        public string InternalName { get; set; } = null!;

        /// <summary>
        /// Short-form name of the action as displayed in a user interface.
        /// </summary>
        public string? ShortDisplayName { get; set; }

        /// <summary>
        /// Long-form name of the action as displayed in a user interface.
        /// </summary>
        public string? LongDisplayName { get; set; }

        /// <summary>
        /// Description of the action.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The status of the attribute type.
        /// </summary>
        public Status Status { get; set; }
    }
}