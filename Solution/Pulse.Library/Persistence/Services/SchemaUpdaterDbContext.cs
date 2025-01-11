using Microsoft.EntityFrameworkCore;

namespace Database.Services;


/// A DbContext that can be configured with a command timeout setting.

public sealed class SchemaUpdaterDbContext : DbContext
{
    public SchemaUpdaterDbContext(DbContextOptions<SchemaUpdaterDbContext> options)
        : base(options)
    {
    }
}