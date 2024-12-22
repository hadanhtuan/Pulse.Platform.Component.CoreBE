using Database.Models.Schema.Modifications;
using System;
using System.Collections.Generic;

namespace Database.Services.Schema.SqlBuilders;

internal class RemovedUniqueConstraintSqlUpdateBuilder : ISqlUpdateBuilder<IRemovedUniqueConstraint>
{
    private readonly string sqlTemplate = "DROP INDEX IF EXISTS |constraintName|";

    public IEnumerable<SqlUpdate> ToSqlUpdates(IRemovedUniqueConstraint modification)
    {
        var sql = sqlTemplate
            .Replace("|constraintName|", $"{modification.UniqueConstraint.InternalName}_u");

        yield return new SqlUpdate(sql);
    }
}