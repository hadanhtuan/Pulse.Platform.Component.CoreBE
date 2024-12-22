using Database.Models.Data;
using Library.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Database.Extensions
{
    public static class VisitRepositoryExtensions
    {
        [Obsolete(
            "Included to be able to read old rows from the database. For all new queries, the new JsonState storage model should be used instead.")]
        public static Visit? SingleOrDefaultWithEagerLoadForMigrationToJsonModel(
            this IRepository<Visit> visitRepository,
            Expression<Func<Visit, bool>>? predicate)
        {
            return visitRepository.Many(predicate)
                .EagerLoadEntityValueDataForMigrationToJsonModel()
                .IncludeWithEagerLoadForMigrationToJsonModel(v => v.InboundFlight)
                .IncludeWithEagerLoadForMigrationToJsonModel(v => v.OutboundFlight)
                .IncludeWithEagerLoadForMigrationToJsonModel(v => v.GroundLegs)
                .AsSplitQuery()
                .SingleOrDefault();
        }

        public static Visit? SingleOrDefaultWithEagerLoad(this IRepository<Visit> visitRepository,
            Expression<Func<Visit, bool>>? predicate)
        {
            return visitRepository.Many(predicate)
                .Include(v => v.InboundFlight)
                .Include(v => v.OutboundFlight)
                .Include(v => v.GroundLegs)
                .AsSplitQuery()
                .SingleOrDefault();
        }
    }
}