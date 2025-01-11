using Library.Persistence.EntityTypes;
using System;

namespace Database.Models.Data
{
    
    /// An attribute used in message matching on a System Message.
    
    public class MessageIdentifierAttribute : Entity
    {
        
        /// Internal name of the Attribute Type.
        
        public string InternalName { get; set; } = null!;

        
        /// The value of the attribute.
        
        public string Value { get; set; } = null!;

        
        /// The System Message to which the Attribute value belongs.
        
        public virtual SystemMessage SystemMessage { get; set; } = null!;
    }
}