using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AdapterFactory;

public static class ServiceExtensions
{
    /// <summary>
    /// Registers a scoped <see cref="IAdapterManager"/>, which can be injected into classes
    /// that need to use extended behavior of existing objects by adapting them to different types.
    ///
    /// It is safe to invoke this method multiple times.
    /// </summary>
    /// <param name="services"></param>
    public static void AddAdapterManager(this IServiceCollection services)
    {
        services.TryAddScoped<IAdapterManager, AdapterManager>();
    }
}