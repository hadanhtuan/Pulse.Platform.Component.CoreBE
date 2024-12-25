using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Database.Models.Schema
{
    /// <summary>
    /// Defines the retention policy for the <see cref="RootEntityType"/>.
    /// </summary>
    public class Retention
    {
        /// <summary>
        /// Defines the message retention policy for the different message types received 
        /// for the <see cref="RootEntityType"/>.
        /// </summary>
        public virtual ICollection<MessageRetention> MessageRetention { get; set; } = new Collection<MessageRetention>();
    }
}