using Library.Persistence.EntityTypes;
using Database.Models.Data;

namespace Database.Models.Schema
{
    
    /// Defines the message retention policy for the different message types received 
    /// for the <see cref="RootEntityType"/>.
    
    public class MessageRetention
    {
        
        /// The message type to which the retention policy applies.
        
        public string MessageType { get; set; } = null!;

        
        /// Set to true if previous messages that no longer reference any active <see cref="EntityValue"/>s should be discarded.
        
        public bool DiscardPreviousMessages { get; set; }
    }
}