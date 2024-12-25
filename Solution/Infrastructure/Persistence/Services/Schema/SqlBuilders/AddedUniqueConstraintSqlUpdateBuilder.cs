using Database.Models.Schema.Modifications;
using System.Collections.Generic;
using System.Linq;

namespace Database.Services.Schema.SqlBuilders;

internal class AddedUniqueConstraintSqlUpdateBuilder : ISqlUpdateBuilder<IAddedUniqueConstraint>
{
    private readonly string sqlTemplate =
        @"CREATE UNIQUE INDEX IF NOT EXISTS |constraintName| 
            ON public.entity_|entityName| (|constraintColumns|) |rowFilter|;";

    public IEnumerable<SqlUpdate> ToSqlUpdates(IAddedUniqueConstraint modification)
    {
        var rowFilter = modification.UniqueConstraint.RowFilterExpression != null
            ? "WHERE " + modification.UniqueConstraint.RowFilterExpression
            : string.Empty;

        List<string> columnExpressions = modification.UniqueConstraint.ColumnExpressions
            .Select(column => column.ColumnExpression).ToList();

        var sql = sqlTemplate
            .Replace("|entityName|", modification.EntityType.InternalName)
            .Replace("|constraintColumns|", $"{string.Join(", ", columnExpressions)}")
            .Replace("|constraintName|", $"{modification.UniqueConstraint.InternalName}_u")
            .Replace("|rowFilter|", rowFilter);

        yield return new SqlUpdate(sql);
    }
}