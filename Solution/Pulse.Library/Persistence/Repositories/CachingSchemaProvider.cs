﻿using Database.Models.Schema;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Database.Repositories
{
    
    /// Implementation of <see cref="ISchemaProvider"/> that caches the schema
    /// loaded from the database for its lifetime; the instance should therefore
    /// only be used for scoped lifetimes.
    
    public sealed class CachingSchemaProvider : ISchemaProvider
    {
        private readonly ISchemaProvider schemaProvider;
        private readonly Lazy<SchemaVersion?> currentSchemaVersion;

        
        /// Constructs an instance backed by the <see cref="ISchemaProvider"/>
        
        /// <param name="schemaProvider">Provider called once to get schema</param>
        public CachingSchemaProvider(ISchemaProvider schemaProvider)
        {
            this.schemaProvider = schemaProvider;
            currentSchemaVersion = new Lazy<SchemaVersion?>(LoadSchemaVersion);
        }

        private SchemaVersion? LoadSchemaVersion() => schemaProvider.SchemaVersion;

        public SchemaVersion? SchemaVersion => currentSchemaVersion.Value;
    }
}