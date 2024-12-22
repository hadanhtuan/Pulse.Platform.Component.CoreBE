using DAP.Base.Logging;
using Database.Models.Caching;
using Database.Models.Data;
using Database.Models.Plugin;
using Database.Models.Schema;
using Database.Models.Versioning;
using Database.Plugins.Models;
using Database.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Database.Data
{
    public class SchemaContext : DbContext
    {
        private readonly IServiceProvider? serviceProvider;

        public ISystemLoggerFactory? GetSystemLoggerFactory() =>
            serviceProvider?.GetService(typeof(ISystemLoggerFactory)) as ISystemLoggerFactory;

        public ISchemaProvider? GetSchemaProvider() =>
            serviceProvider?.GetService(typeof(ISchemaProvider)) as ISchemaProvider;

        public DbSet<SchemaVersion> SchemaVersionModel { get; set; } = null!;

        public DbSet<EntityValue> EntityValues { get; set; } = null!;

        public DbSet<EntityAttributeValue> EntityAttributeValues { get; set; } = null!;

        public DbSet<EntityValueProcessingState> EntityValueProcessingStates { get; set; } = null!;

        public DbSet<TopicPartitionOffset> TopicPartitionOffsets { get; set; } = null!;

        public DbSet<RawMessage> RawMessages { get; set; } = null!;

        public DbSet<Visit> Visits { get; set; } = null!;

        public DbSet<FlightLeg> FlightLegs { get; set; } = null!;

        public DbSet<GroundLeg> GroundLegs { get; set; } = null!;

        public DbSet<Alert> Alerts { get; set; } = null!;

        public DbSet<CacheInvalidationKey> CacheInvalidationKeys { get; set; } = null!;

        public DbSet<VersionIdentifier> VersionIdentifiers { get; set; } = null!;

        public DbSet<DefaultAtcMessageUserStatus> DefaultAtcMessageUserStatuses { get; set; } = null!;

        public DbSet<BackgroundEvent> BackgroundEvents { get; set; } = null!;

        // This entity is "owned" by the PluginContext and migrations are contained in Database.Plugins
        public DbSet<ConfigurationVersion> ConfigurationVersions { get; set; } = null!;

        // This entity is "owned" by the PluginContext and migrations are contained in Database.Plugins
        public DbSet<PluginAssembly> PluginAssembly { get; set; } = null!;

        /// <summary>
        /// Used for unit tests and mocking
        /// </summary>
        public SchemaContext() : base() { }

        public SchemaContext(IServiceProvider? serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public SchemaContext(DbContextOptions<SchemaContext> options, IServiceProvider? serviceProvider)
            : base(options)
        {
            this.serviceProvider = serviceProvider;
        }

        // Allows for derived classes with options for those classes
        protected SchemaContext(DbContextOptions options, IServiceProvider? serviceProvider)
            : base(options)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionMessage>();
            modelBuilder.Entity<SystemMessage>();
            modelBuilder.Entity<MessageEntityAttributeValue>();
            modelBuilder.Entity<EnrichmentEntityAttributeValue>();
            modelBuilder.Entity<ValueAttributeType>();
            modelBuilder.Entity<ComplexAttributeType>();
            modelBuilder.Entity<ChildEntityAttributeType>();
            modelBuilder.Entity<EntityValueAlert>();
            modelBuilder.Entity<RawMessageAlert>();

            // Migrations for these entities are contained in Database.Plugins
            modelBuilder.Entity<ConfigurationVersion>()
                .ToTable("ConfigurationVersions", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<PluginAssembly>()
                .ToTable("PluginAssembly", t => t.ExcludeFromMigrations());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}