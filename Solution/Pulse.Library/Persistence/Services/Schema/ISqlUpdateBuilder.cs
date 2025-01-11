using System.Collections.Generic;

namespace Database.Services.Schema;


/// Builds database SQL update commands for a given schema modification.

/// <typeparam name="TModification"></typeparam>
internal interface ISqlUpdateBuilder<TModification>
{
    
    /// Returns a list of <see cref="SqlUpdate"/>s for implementing the <paramref name="modification"/>
    /// in the database schema.
    
    /// <param name="modification">A modification to the schema</param>
    /// <returns><see cref="IEnumerable{SqlUpdate}"/></returns>
    IEnumerable<SqlUpdate> ToSqlUpdates(TModification modification);
}