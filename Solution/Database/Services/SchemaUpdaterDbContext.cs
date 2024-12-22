using Microsoft.EntityFrameworkCore;

namespace Database.Services;

/// <summary>
/// A DbContext that can be configured with a command timeout setting.
/// </summary>
public sealed class SchemaUpdaterDbContext : DbContext
{
    public SchemaUpdaterDbContext(DbContextOptions<SchemaUpdaterDbContext> options)
        : base(options)
    {
    }
}