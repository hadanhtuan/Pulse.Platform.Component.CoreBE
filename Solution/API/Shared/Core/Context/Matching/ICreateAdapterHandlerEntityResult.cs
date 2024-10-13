using AODB.PipelineRulesInterfaces.Matching;

namespace API.Shared.Core.Context.Matching
{
    /// <summary>
    /// Interface used to signal to the <see cref="AdapterHandlerEntityResult"/> that a given class
    /// represents a child entity and that it knows how to create a <see cref="IAdapterHandlerEntityResult"/>
    /// based on it's values.
    /// </summary>
    internal interface ICreateAdapterHandlerEntityResult
    {
        IAdapterHandlerEntityResult? CreateAdapterHandlerEntityResult();
    }
}
