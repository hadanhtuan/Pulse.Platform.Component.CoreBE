using PipelineRulesInterfaces.Matching;
using Rules.ContextInterfaces.Matching;

namespace API.Shared.Core.Context.Matching
{
    public static class EntityInputExtensions
    {
        /// <summary>
        /// Builds an <see cref="IAdapterHandlerEntityResult"/> from this
        /// <see cref="IEntityInput"/> object.
        /// </summary>
        /// <typeparam name="TEntityInput">Type of <see cref="IEntityInput"/></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IAdapterHandlerEntityResult ToAdapterHandlerEntityResult<TEntityInput>(
            this TEntityInput input)
            where TEntityInput : IEntityInput =>
            AdapterHandlerEntityResult.Create<TEntityInput>(null, (input as BaseEntityInput)!);
    }
}