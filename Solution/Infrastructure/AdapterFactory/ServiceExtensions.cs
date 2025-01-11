using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Domain.AdapterFactory;

public static class ServiceExtensions
{
    
    /// Registers a scoped <see cref="IAdapterManager"/>, which can be injected into classes
    /// that need to use extended behavior of existing objects by adapting them to different types.
    ///
    /// It is safe to invoke this method multiple times.
    
    /// <param name="services"></param>
    public static void AddAdapterManager(this IServiceCollection services)
    {
        services.TryAddScoped<IAdapterManager, AdapterManager>();
    }
}