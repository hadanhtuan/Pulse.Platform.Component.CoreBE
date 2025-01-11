using Database.Models.Schema.Modifications;
using System.Collections.Generic;
using System.Linq;

namespace Database.Services.Schema.SqlBuilders;


/// Turns a <see cref="ICompositeModification"/> into SQL update statements.

internal class CompositeModificationSqlUpdateBuilder : ISqlUpdateBuilder<ICompositeModification>
{
    private readonly IGenericTypeSqlUpdateBuilder delegateBuilder;

    public CompositeModificationSqlUpdateBuilder(IGenericTypeSqlUpdateBuilder delegateBuilder)
    {
        this.delegateBuilder = delegateBuilder;
    }

    public IEnumerable<SqlUpdate> ToSqlUpdates(ICompositeModification entity) =>
        entity.Modifications.SelectMany(GetSqlUpdates).ToList();

    private IEnumerable<SqlUpdate> GetSqlUpdates(IModification modification) =>
        delegateBuilder.ToSqlUpdates(modification);
}