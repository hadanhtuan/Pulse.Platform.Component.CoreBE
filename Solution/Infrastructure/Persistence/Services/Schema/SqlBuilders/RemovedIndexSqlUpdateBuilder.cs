using Database.Services.Schema.SqlBuilders.Helpers;
using Database.Models.Schema.Modifications;
using System;
using System.Collections.Generic;

namespace Database.Services.Schema.SqlBuilders;

internal class RemovedIndexSqlUpdateBuilder : ISqlUpdateBuilder<IRemovedIndex>
{
    private readonly string sqlTemplate = "DROP INDEX IF EXISTS |indexName|";

    private readonly IIndexNameHelper indexNameHelper;

    public RemovedIndexSqlUpdateBuilder(IIndexNameHelper indexNameHelper)
    {
        this.indexNameHelper = indexNameHelper;
    }

    public IEnumerable<SqlUpdate> ToSqlUpdates(IRemovedIndex modification)
    {
        var indexName = indexNameHelper.IndexNameFrom(
            modification.EntityType.InternalName, modification.Index.Columns);

        var sql = sqlTemplate.Replace("|indexName|", indexName);

        yield return new SqlUpdate(sql);
    }
}