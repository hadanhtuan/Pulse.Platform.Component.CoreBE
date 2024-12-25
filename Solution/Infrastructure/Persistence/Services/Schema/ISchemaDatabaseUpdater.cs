using Database.Models.Schema.Modifications;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Services.Schema;

/// <summary>
/// Updates the database to reflect modifications to the schema.
/// </summary>
public interface ISchemaDatabaseUpdater
{
    /// <summary>
    /// Performs database updates for the given <paramref name="modification"/>.
    /// </summary>
    /// <param name="modification"><see cref="ICompositeModification"/>s</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Update(ICompositeModification modification, CancellationToken cancellationToken = default);
}