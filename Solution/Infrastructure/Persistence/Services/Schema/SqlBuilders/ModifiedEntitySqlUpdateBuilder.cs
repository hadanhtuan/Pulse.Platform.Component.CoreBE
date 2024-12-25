using Database.Models.Schema;
using Database.Models.Schema.Modifications;
using System;
using System.Collections.Generic;

namespace Database.Services.Schema.SqlBuilders;

internal class ModifiedEntitySqlUpdateBuilder : ISqlUpdateBuilder<IModifiedEntity>
{
    private readonly ISqlUpdateBuilder<ICompositeModification> composedModificationBuilder;

    public ModifiedEntitySqlUpdateBuilder(ISqlUpdateBuilder<ICompositeModification> composedModificationBuilder)
    {
        this.composedModificationBuilder = composedModificationBuilder;
    }

    public IEnumerable<SqlUpdate> ToSqlUpdates(IModifiedEntity modification) =>
        modification.EntityType is RootEntityType
            ? composedModificationBuilder.ToSqlUpdates(modification)
            : Array.Empty<SqlUpdate>();
}