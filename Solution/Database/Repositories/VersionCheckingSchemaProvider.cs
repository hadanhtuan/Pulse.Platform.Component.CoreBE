using Database.Models.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Database.Repositories
{
    /// <summary>
    /// An implementation of <see cref="ISchemaProvider"/> that does a cheap check on
    /// the latest schema version number before fetches a full <see cref="SchemaVersion"/>
    /// update from the database if a new version is available.
    /// </summary>
    public sealed class VersionCheckingSchemaProvider : ISchemaProvider
    {
        private SchemaVersion? currentSchemaVersion;
        private readonly DbContext context;
        private static readonly object schemaLock = new object();

        public VersionCheckingSchemaProvider(DbContext context)
        {
            this.context = context;
        }

        public SchemaVersion SchemaVersion => LoadLatestSchemaVersionOrDefault()!;

        private SchemaVersion? LoadLatestSchemaVersionOrDefault()
        {
            lock (schemaLock)
            {
                int? latestVersionNumber = LoadLatestSchemaVersionNumberOrDefault();
                if (SchemaVersionIsOutdated(currentSchemaVersion, latestVersionNumber))
                {
                    currentSchemaVersion = context.Set<SchemaVersion>()
                        .OrderByDescending(s => s.Version)
                        .Include(s => s.EntityTypes)
                        .ThenInclude(e => e.AttributeTypes)
                        .AsSplitQuery()
                        .FirstOrDefault();
                }
            }
            return currentSchemaVersion;
        }

        private int? LoadLatestSchemaVersionNumberOrDefault() =>
            context.Set<SchemaVersion>()
                .OrderByDescending(s => s.Version)
                .Select(s => s.Version)
                .FirstOrDefault();

        private bool SchemaVersionIsOutdated(SchemaVersion? currentSchemaVersion, int? latestVersionNumber) => 
            (latestVersionNumber ?? -1) > (currentSchemaVersion?.Version ?? -1);
    }
}