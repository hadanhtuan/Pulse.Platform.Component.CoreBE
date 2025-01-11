using Microsoft.Extensions.Configuration;
using Npgsql;
using System;

namespace Database.Extensions;

public static class ConfigurationExtensions
{
    public static NpgsqlConnectionStringBuilder GetConnectionStringBuilder(this IConfiguration configuration,
        string name)
    {
        return new NpgsqlConnectionStringBuilder(configuration.GetConnectionString(name));
    }

    public static string GetConnectionStringWithApplicationName(this IConfiguration configuration,
        string connectionStringName, string applicationName)
    {
        var builder = configuration.GetConnectionStringBuilder(connectionStringName);
        builder.ApplicationName = applicationName;
        return builder.ConnectionString;
    }

    public static string GetConnectionStringWithApplicationName(this IConfiguration configuration,
        string connectionStringName)
    {
        return configuration.GetConnectionStringWithApplicationName(connectionStringName, Environment.MachineName);
    }
}