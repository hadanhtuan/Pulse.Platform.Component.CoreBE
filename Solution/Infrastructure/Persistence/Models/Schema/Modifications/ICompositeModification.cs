using System.Collections.Generic;

namespace Database.Models.Schema.Modifications
{
    /// <summary>
    /// A modification that is composed of other modifications.
    /// </summary>
    public interface ICompositeModification : IModification
    {
        /// <summary>
        /// The modifications that are part of this modification.
        /// </summary>
        IEnumerable<IModification> Modifications { get; }
    }
}