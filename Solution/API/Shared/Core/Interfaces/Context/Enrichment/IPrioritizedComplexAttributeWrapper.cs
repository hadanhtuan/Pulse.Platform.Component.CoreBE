namespace API.Shared.Core.Interfaces.Context.Enrichment
{
    /// <summary>
    /// Wraps an attribute on an entity in enrichment rules,
    /// enabling rule authors to add values to the specific attribute.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPrioritizedComplexAttributeWrapper<TReadonlyInterface, TInterface>
        : IPrioritizedAttributeWrapper<TReadonlyInterface> 
        where TInterface : TReadonlyInterface
        
    {
        TInterface CreateNewValue();
    }
}
