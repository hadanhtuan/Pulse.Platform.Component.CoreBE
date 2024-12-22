namespace AdapterFactory;

/// <summary>
/// An adapter manager maintains a registry of adapter factories, which are
/// obtained from an <see cref="IServiceProvider"/> when needed.
/// 
/// The adapter manager looks up an IAdapterFactory for the given types for
/// adaptee and adapter, then delegates the GetAdapter invocation to the found
/// IAdapterFactory.getAdapter method.
/// </summary>
internal class AdapterManager : IAdapterManager
{
    private readonly IServiceProvider serviceProvider;

    public AdapterManager(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public TAdapter? GetAdapter<TAdapter>(object? adaptee) where TAdapter : class
    {
        if (adaptee == null)
        {
            return null;
        }

        var factory = LookupFactory<TAdapter>(adaptee);

        // No IAdapterFactory registered for the adaptation from adaptee type to TAdapter
        if (factory == null)
        {
            return null;
        }

        // Using reflection, build a generic method for
        // IAdapterFactory<TAdaptee,TAdapter>.GetAdapter(TAdaptee adaptee)
        // that can be used to invoke GetAdapter with specific type arguments
        var method = factory.GetType().GetMethod("GetAdapter")!;

        return (TAdapter?)method.Invoke(factory, new[] { adaptee });
    }

    private object? LookupFactory<TAdapter>(object adaptee) where TAdapter : class
    {
        var factory = TryLookupFactory<TAdapter>(adaptee.GetType());

        if (factory == null)
        {
            // Try parent types of adaptee
            factory = adaptee.GetType().GetParentTypes()
                .Select(t => TryLookupFactory<TAdapter>(t))
                .FirstOrDefault(factory => factory is not null);
        }

        return factory;
    }

    private object? TryLookupFactory<TAdapter>(Type adapteeType) where TAdapter : class
    {
        var factoryType = typeof(IAdapterFactory<,>)
            .MakeGenericType(adapteeType, typeof(TAdapter));

        return serviceProvider.GetService(factoryType);
    }
}