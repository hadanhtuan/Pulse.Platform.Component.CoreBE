using Database.Models.Schema.Modifications;
using Database.Services.Schema.SqlBuilders.Helpers;

namespace Database.Services.Schema.SqlBuilders;

internal class AddedIndexSqlUpdateBuilder : ISqlUpdateBuilder<IAddedIndex>
{
    private readonly IIndexNameHelper indexNameHelper;

    private readonly string sqlTemplate =
        "CREATE INDEX IF NOT EXISTS |indexName| ON public.entity_|entityName| (|indexColumns|)";

    public AddedIndexSqlUpdateBuilder(IIndexNameHelper indexNameHelper)
    {
        this.indexNameHelper = indexNameHelper;
    }

    public IEnumerable<SqlUpdate> ToSqlUpdates(IAddedIndex modification)
    {
        var quotedIndexColumns = modification.Index.Columns.Split(",").Select(x => $"\"{x.Trim()}\"");

        var indexName = indexNameHelper.IndexNameFrom(
            modification.EntityType.InternalName, modification.Index.Columns);

        var sql = sqlTemplate
            .Replace("|entityName|", modification.EntityType.InternalName)
            .Replace("|indexColumns|", $"{string.Join(",", quotedIndexColumns)}")
            .Replace("|indexName|", indexName);

        yield return new SqlUpdateRequiringAnalyze(sql, modification.EntityType.InternalName);
    }
}