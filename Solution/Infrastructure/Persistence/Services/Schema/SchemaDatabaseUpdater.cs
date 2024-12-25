using DAP.Base.Logging;
using Database.Models.Schema.Modifications;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Services.Schema;

internal class SchemaDatabaseUpdater : ISchemaDatabaseUpdater
{
    private readonly DbContext dbContext;
    private readonly ISchemaSqlUpdateBuilder sqlBuilder;
    private readonly ILogger logger;

    public SchemaDatabaseUpdater(
        DbContext dbContext,
        ISchemaSqlUpdateBuilder sqlBuilder,
        ISystemLoggerFactory loggerFactory)
    {
        this.dbContext = dbContext;
        this.sqlBuilder = sqlBuilder;
        logger = loggerFactory.GetLogger<SchemaDatabaseUpdater>();
    }

    public async Task Update(ICompositeModification modification, CancellationToken cancellationToken = default)
    {
        var updates = sqlBuilder.ToSqlUpdates(modification).ToList();
        logger.Information("Perform {numberOfUpdates} for {modification}", updates.Count, modification);
        foreach (SqlUpdate update in updates)
        {
            logger.Information("Running update statement: {Statement}", update.Statement);
            await dbContext.Database.ExecuteSqlRawAsync(update.Statement, update.Parameters, cancellationToken);
        }
    }
}