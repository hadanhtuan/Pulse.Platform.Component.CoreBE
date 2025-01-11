using Database.Models.Schema;
using Database.Models.Schema.Modifications;
using System.Collections.Generic;

namespace Database.Services.Schema.SqlBuilders;


/// Builds <see cref="SqlUpdate"/>s for an added <see cref="RootEntityType"/>. If the <see cref="IAddedEntity"/>
/// is not a RootEntityType, no updates are made.

internal class AddedEntitySqlUpdateBuilder : ISqlUpdateBuilder<IAddedEntity>
{
    private readonly string sqlTemplate =
        @"CREATE TABLE IF NOT EXISTS public.entity_|entityName| 
            (
	            entity_id UUID PRIMARY KEY,
	            last_modified TIMESTAMP
            )";

    private readonly ISqlUpdateBuilder<ICompositeModification> composedModificationBuilder;

    public AddedEntitySqlUpdateBuilder(ISqlUpdateBuilder<ICompositeModification> composedModificationBuilder)
    {
        this.composedModificationBuilder = composedModificationBuilder;
    }

    public IEnumerable<SqlUpdate> ToSqlUpdates(IAddedEntity modification)
    {
        List<SqlUpdate> updates = new();
        if (modification.EntityType is RootEntityType)
        {
            updates.Add(GetCreateTableSql(modification));
            updates.AddRange(composedModificationBuilder.ToSqlUpdates(modification));
        }

        return updates;
    }

    private SqlUpdate GetCreateTableSql(IAddedEntity modification)
    {
        var sql = sqlTemplate.Replace("|entityName|", modification.EntityType.InternalName);

        return new SqlUpdate(sql);
    }
}