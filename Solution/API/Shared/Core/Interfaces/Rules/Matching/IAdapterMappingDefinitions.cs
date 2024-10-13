using System.Collections.Generic;

namespace API.Shared.Core.Interfaces.Context.Matching
{
    /// <summary>
    /// Configures how adapter message types are mapped to entity types 
    /// to be processed by the AODB pipeline.
    /// </summary>
    public interface IAdapterMappingDefinitions
    {
        /// <summary>
        /// Lists all of the mappings of adapter message types.
        /// </summary>
        IEnumerable<IAdapterMessageMapping> Mappings { get; }
    }
}
