using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Database.Models.Schema
{
    
    /// Defines the retention policy for the <see cref="RootEntityType"/>.
    
    public class Retention
    {
        
        /// Defines the message retention policy for the different message types received 
        /// for the <see cref="RootEntityType"/>.
        
        public virtual ICollection<MessageRetention> MessageRetention { get; set; } = new Collection<MessageRetention>();
    }
}