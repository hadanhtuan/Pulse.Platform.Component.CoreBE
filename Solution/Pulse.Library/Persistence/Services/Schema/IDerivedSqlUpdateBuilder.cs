using System.Collections.Generic;

namespace Database.Services.Schema;


/// Builds <see cref="SqlUpdate"/>s based on input <see cref="SqlUpdate"/>s.

internal interface IDerivedSqlUpdateBuilder
{
    
    /// Returns <see cref="SqlUpdate"/>s derived from the given <paramref name="updates"/>.
    
    /// <param name="updates">Input updates to derive other updates from</param>
    /// <returns>Derived updates</returns>
    IEnumerable<SqlUpdate> Create(IEnumerable<SqlUpdate> updates);
}