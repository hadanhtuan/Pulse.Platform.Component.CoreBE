using DAP.Base.Logging;
using Database.Models.Schema.Modifications;
using Domain.AdapterFactory;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Database.Services.Schema.SqlBuilders;

internal class GenericTypeSqlUpdateBuilder : IGenericTypeSqlUpdateBuilder
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger logger;

    public GenericTypeSqlUpdateBuilder(IServiceProvider serviceProvider,
        ISystemLoggerFactory loggerFactory)
    {
        this.serviceProvider = serviceProvider;
        logger = loggerFactory.GetLogger<GenericTypeSqlUpdateBuilder>();
    }

    public IEnumerable<SqlUpdate> ToSqlUpdates(IModification modification)
    {
        // Use reflection to find builder for the type of modification
        object builder = GetInstanceOfSpecificType(typeof(ISqlUpdateBuilder<>), modification.GetType()) ??
                         throw new SqlUpdateBuilderTypeNotFoundException(
                             $"No implementation of ISqlUpdateBuilder<> found for {modification.GetType()}");

        MethodInfo method = builder.GetType().GetMethod("ToSqlUpdates")!;

        logger.Debug("Using {builder} for {modification}", builder, modification);

        return (IEnumerable<SqlUpdate>)method.Invoke(builder, new[] { modification })!;
    }

    private object? GetInstanceOfSpecificType(Type genericType, Type modificationType)
    {
        var instance = TryGetInstanceOfSpecificType(genericType, modificationType);

        if (instance == null)
        {
            // Try parent types of modificationType
            instance = modificationType.GetParentTypes()
                // Do not use generic composed modification builder
                .Where(t => !t.Equals(typeof(ICompositeModification)))
                .Select(t => TryGetInstanceOfSpecificType(genericType, t))
                .FirstOrDefault(t => t is not null);
        }

        return instance;
    }

    private object? TryGetInstanceOfSpecificType(Type genericType, Type argumentType)
    {
        var targetType = genericType.MakeGenericType(argumentType);

        return serviceProvider.GetService(targetType);
    }
}