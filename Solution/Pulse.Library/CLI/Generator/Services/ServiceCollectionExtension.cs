using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pulse.Library.CLI.Generator.Configurations;
using Pulse.Library.CLI.Generator.Services.Aodb;
using Pulse.Library.CLI.Generator.Services.FileHelper;

namespace Pulse.Library.CLI.Generator.Services;

public static class ServiceCollectionExtension
{
    public static ServiceCollection AddGeneratorServices(this ServiceCollection services, IConfiguration config)
    {
        services.Configure<SchemaProviderOption>(config.GetSection(SchemaProviderOption.Section));
        services.PostConfigure<SchemaProviderOption>(o => { });
        services.AddScoped<GeneratorRunner>();
        services.AddSingleton<AodbContextGenerator>();
        services.AddSingleton<AodbCodeGenerator>();
        services.AddSingleton<FileWriter>();

        return services;
    }
}
