using Database.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Database.Data;

public class DesignTimeSchemaContext : IDesignTimeDbContextFactory<SchemaContext>
{
    public SchemaContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        string connectionString = configuration.GetConnectionStringWithApplicationName("PostgreSQL");
        int migrationTimeout = configuration.GetValue("SchemaContextFactoryOptions:MigrationTimeout", 600);

        var optionsBuilder = new DbContextOptionsBuilder<SchemaContext>();
        optionsBuilder.UseNpgsql(connectionString,
            opts => opts.CommandTimeout(migrationTimeout));

        return new SchemaContext(optionsBuilder.Options, null);
    }
}