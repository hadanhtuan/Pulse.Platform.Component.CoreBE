using System.Collections.Generic;

namespace Database.Models.Schema.Modifications
{
    
    /// A modification that is composed of other modifications.
    
    public interface ICompositeModification : IModification
    {
        
        /// The modifications that are part of this modification.
        
        IEnumerable<IModification> Modifications { get; }
    }
}