using Database.Models.Schema.Modifications;
using System.Collections.Generic;

namespace Database.Services.Schema.SqlBuilders;


/// Instantiates and delegates <see cref="ISqlUpdateBuilder{TModification}"/> for a given 
/// TModification if it exists and returns the built <see cref="SqlUpdate"/>s.

internal interface IGenericTypeSqlUpdateBuilder
{
    
    /// Delegates to <see cref="ISqlUpdateBuilder{TModification}"/> for the type of
    /// <paramref name="modification"/> and returns the <see cref="SqlUpdate"/>s returned
    /// by that builder.
    
    /// <param name="modification">A modification of some type</param>
    /// <returns><see cref="SqlUpdate"/>s resulting from modification</returns>
    IEnumerable<SqlUpdate> ToSqlUpdates(IModification modification);
}