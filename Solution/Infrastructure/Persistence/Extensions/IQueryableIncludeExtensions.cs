using Database.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Extensions;

public static class IQueryableIncludeExtensions
{
    [Obsolete(
        "Included to be able to read old rows from the database. For all new queries, the new JsonState storage model should be used instead.")]
    public static IQueryable<TEntityValue> EagerLoadEntityValueDataForMigrationToJsonModel<TEntityValue>(
        this IQueryable<TEntityValue> entityValues) where TEntityValue : EntityValue
    {
        return entityValues
            .Include(ev => ev.ModifiedBy)
            .Include(ev => ev.EntityAttributeValues).ThenInclude(eav => eav.AttributeType)
            .Include(ev => ev.EntityAttributeValues)
            .ThenInclude(eav => ((EnrichmentEntityAttributeValue)eav).AffectedBy)
            .Include(ev => ev.EntityAttributeValues)
            .ThenInclude(eav => ((EnrichmentEntityAttributeValue)eav).AodbSource)
            .AsSplitQuery();
    }

    [Obsolete(
        "Included to be able to read old rows from the database. For all new queries, the new JsonState storage model should be used instead.")]
    internal static IQueryable<TEntity> IncludeWithEagerLoadForMigrationToJsonModel<TEntity, TProperty>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, TProperty?>> navigationPropertyPath)
        where TEntity : class
        where TProperty : EntityValue
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return source
            .Include(navigationPropertyPath).ThenInclude(ev => ev.ModifiedBy)
            .Include(navigationPropertyPath).ThenInclude(ev => ev.EntityAttributeValues)
            .ThenInclude(eav => eav.AttributeType)
            .Include(navigationPropertyPath).ThenInclude(ev => ev.EntityAttributeValues)
            .ThenInclude(eav => ((EnrichmentEntityAttributeValue)eav).AffectedBy)
            .Include(navigationPropertyPath).ThenInclude(ev => ev.EntityAttributeValues)
            .ThenInclude(eav => ((EnrichmentEntityAttributeValue)eav).AodbSource)
            .AsSplitQuery();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }


    [Obsolete(
        "Included to be able to read old rows from the database. For all new queries, the new JsonState storage model should be used instead.")]
    internal static IQueryable<TEntity> IncludeWithEagerLoadForMigrationToJsonModel<TEntity, TProperty>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, IEnumerable<TProperty>>> navigationPropertyPath)
        where TEntity : class
        where TProperty : EntityValue
    {
        return source
            .Include(navigationPropertyPath).ThenInclude(ev => ev.ModifiedBy)
            .Include(navigationPropertyPath).ThenInclude(ev => ev.EntityAttributeValues)
            .ThenInclude(eav => eav.AttributeType)
            .Include(navigationPropertyPath).ThenInclude(ev => ev.EntityAttributeValues)
            .ThenInclude(eav => ((EnrichmentEntityAttributeValue)eav).AffectedBy)
            .Include(navigationPropertyPath).ThenInclude(ev => ev.EntityAttributeValues)
            .ThenInclude(eav => ((EnrichmentEntityAttributeValue)eav).AodbSource)
            .AsSplitQuery();
    }
}