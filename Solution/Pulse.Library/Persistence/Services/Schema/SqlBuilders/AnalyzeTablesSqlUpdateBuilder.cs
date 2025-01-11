using System.Collections.Generic;
using System.Linq;

namespace Database.Services.Schema.SqlBuilders;


/// Creates an ANALYZE statement for the entity tables on which indexes
/// have been modified by any <see cref="SqlUpdateRequiringAnalyze"/>s.
/// This has shown to be necessary for new indexes to take effect, see #139219.

internal class AnalyzeTablesSqlUpdateBuilder : IDerivedSqlUpdateBuilder
{
    public IEnumerable<SqlUpdate> Create(IEnumerable<SqlUpdate> updates)
    {
        var tableNames = updates.OfType<SqlUpdateRequiringAnalyze>()
            .Select(x => x.TableName).Distinct()
            .OrderBy(x => x);

        if (tableNames.Any())
        {
            yield return new SqlUpdate("ANALYZE " + string.Join(", ", tableNames));
        }
    }
}