using Database.Models.Data;
using System.Collections.Generic;

namespace Database.Services;

public interface IEntityUpdater
{
    /// <summary>
    /// Updates the table rows for the given entity in the query database.
    /// </summary>
    /// <param name="entityValue"> EntityValue to be updated </param>
    List<(string statement, object?[] parameters)> UpdateEntity(EntityValue entityValue);

    /// <summary>
    /// Deletes the table rows for the given entity in the query database.
    /// </summary>
    /// <param name="entityValue">EntityValue to be deleted</param>
    void DeleteEntity(EntityValue entityValue);
}