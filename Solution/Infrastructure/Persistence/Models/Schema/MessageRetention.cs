using Library.Persistence.EntityTypes;
using Database.Models.Data;

namespace Database.Models.Schema
{
    /// <summary>
    /// Defines the message retention policy for the different message types received 
    /// for the <see cref="RootEntityType"/>.
    /// </summary>
    public class MessageRetention
    {
        /// <summary>
        /// The message type to which the retention policy applies.
        /// </summary>
        public string MessageType { get; set; } = null!;

        /// <summary>
        /// Set to true if previous messages that no longer reference any active <see cref="EntityValue"/>s should be discarded.
        /// </summary>
        public bool DiscardPreviousMessages { get; set; }
    }
}