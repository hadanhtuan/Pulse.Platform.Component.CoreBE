using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pulse.Library.CLI.Generator.Services;

namespace Pulse.Library.CLI.Generator;
public static class CLI
{
    public static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .AddCommandLine(args)
            .AddJsonFile($"appsettings.json", true, true);
        var config = builder.Build();

        var services = new ServiceCollection()
            .AddGeneratorServices(config)
            .BuildServiceProvider();

        services.GetService<GeneratorRunner>().Generate();
    }
}