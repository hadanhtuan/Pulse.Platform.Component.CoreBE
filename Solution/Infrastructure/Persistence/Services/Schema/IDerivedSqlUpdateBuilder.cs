using System.Collections.Generic;

namespace Database.Services.Schema;

/// <summary>
/// Builds <see cref="SqlUpdate"/>s based on input <see cref="SqlUpdate"/>s.
/// </summary>
internal interface IDerivedSqlUpdateBuilder
{
    /// <summary>
    /// Returns <see cref="SqlUpdate"/>s derived from the given <paramref name="updates"/>.
    /// </summary>
    /// <param name="updates">Input updates to derive other updates from</param>
    /// <returns>Derived updates</returns>
    IEnumerable<SqlUpdate> Create(IEnumerable<SqlUpdate> updates);
}