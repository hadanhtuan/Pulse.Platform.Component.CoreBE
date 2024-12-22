using Rules.ContextInterfaces.Matching;
using System;

namespace API.Shared.Core.Context.Matching
{
    public class AdapterMessageMapping : IAdapterMessageMapping
    {
        public Type AdapterMessageType { get; private set; } = null!;
        public Type EntityInputInterfaceType { get; private set; } = null!;
        public Type EntityInputImplementationType { get; private set; } = null!;

        public static AdapterMessageMapping Create<TAdapterMessage, TEntityInput, TEntityInputImpl>()
            where TAdapterMessage : IAdapterMessage
            where TEntityInput : IEntityInput
            where TEntityInputImpl : BaseEntityInput, TEntityInput, new() => new AdapterMessageMapping
        {
            AdapterMessageType = typeof(TAdapterMessage),
            EntityInputInterfaceType = typeof(TEntityInput),
            EntityInputImplementationType = typeof(TEntityInputImpl)
        };
    }
}