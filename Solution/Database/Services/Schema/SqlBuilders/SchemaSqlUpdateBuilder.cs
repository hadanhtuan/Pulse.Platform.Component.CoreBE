using Database.Models.Schema.Modifications;
using System.Collections.Generic;
using System.Linq;

namespace Database.Services.Schema.SqlBuilders;

/// <summary>
/// Builds <see cref="SqlUpdate"/>s for a schema modification using a delegate
/// builder and adds derived updates built using registered <see cref="IDerivedSqlUpdateBuilder"/>s.
/// </summary>
internal class SchemaSqlUpdateBuilder : ISchemaSqlUpdateBuilder
{
    private readonly ISqlUpdateBuilder<ICompositeModification> updateBuilder;
    private readonly IEnumerable<IDerivedSqlUpdateBuilder> derivedUpdateBuilders;

    public SchemaSqlUpdateBuilder(
        ISqlUpdateBuilder<ICompositeModification> updateBuilder,
        IEnumerable<IDerivedSqlUpdateBuilder> derivedUpdateBuilders)
    {
        this.updateBuilder = updateBuilder;
        this.derivedUpdateBuilders = derivedUpdateBuilders;
    }

    public IEnumerable<SqlUpdate> ToSqlUpdates(ICompositeModification modification)
    {
        List<SqlUpdate> updates = updateBuilder.ToSqlUpdates(modification).ToList();
        updates.AddRange(CreateDerivedUpdates(updates));
        return updates;
    }

    private IEnumerable<SqlUpdate> CreateDerivedUpdates(List<SqlUpdate> updates) =>
        derivedUpdateBuilders.SelectMany(builder => builder.Create(updates));
}