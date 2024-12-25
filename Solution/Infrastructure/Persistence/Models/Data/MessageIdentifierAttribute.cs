using Library.Persistence.EntityTypes;
using System;

namespace Database.Models.Data
{
    /// <summary>
    /// An attribute used in message matching on a System Message.
    /// </summary>
    public class MessageIdentifierAttribute : Entity
    {
        /// <summary>
        /// Internal name of the Attribute Type.
        /// </summary>
        public string InternalName { get; set; } = null!;

        /// <summary>
        /// The value of the attribute.
        /// </summary>
        public string Value { get; set; } = null!;

        /// <summary>
        /// The System Message to which the Attribute value belongs.
        /// </summary>
        public virtual SystemMessage SystemMessage { get; set; } = null!;
    }
}