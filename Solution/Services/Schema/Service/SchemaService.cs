using Database.Models.Schema;
using Database.Models.Schema.Modifications;
using Database.Services.Schema;
using Library.Persistence.Repositories;
using Services.Schema.Service.Copy;
using Services.Schema.Service.Validation;
using Services.Schema.Service.Validation.Modifications;

namespace Services.Schema.Service;

internal class SchemaService : ISchemaService
{
    private readonly IRepository<SchemaVersion> schemaRepository;
    private readonly ISchemaDatabaseUpdater schemaDatabaseUpdater;
    private readonly ISchemaVersionValidatorFactory schemaVersionValidatorFactory;
    private readonly ISchemaVersionCopier schemaCopier;
    private readonly ISchemaServiceListener listener;

    public SchemaService(
        IRepository<SchemaVersion> schemaRepository,
        ISchemaDatabaseUpdater schemaUpdater,
        ISchemaVersionValidatorFactory schemaVersionValidatorFactory,
        ISchemaVersionCopier schemaCopier,
        ISchemaServiceListener listener)
    {
        this.schemaRepository = schemaRepository;
        this.schemaDatabaseUpdater = schemaUpdater;
        this.schemaVersionValidatorFactory = schemaVersionValidatorFactory;
        this.schemaCopier = schemaCopier;
        this.listener = listener;
    }

    public SchemaVersion ReadSchema() =>
        // There is always only one schema
        schemaRepository.Single(_ => true);

    public async Task<SchemaUpdateResult> UpdateSchema(SchemaVersion newSchema,
        bool validateOnly, CancellationToken cancellationToken)
    {
        var currentSchema = ReadSchema();

        ICompositeModification modification = ValidateChangeFrom(newSchema, currentSchema);

        bool hasChanges = modification.Modifications.Any();
        bool hasDisruptiveChanges = hasChanges && HasDisruptiveChanges(modification);

        bool skipDisruptiveUpdate = validateOnly && hasDisruptiveChanges;

        if (hasChanges && !skipDisruptiveUpdate)
        {
            UpdateSchema(newSchema, currentSchema);
            await schemaDatabaseUpdater.Update(modification, cancellationToken);
            await listener.UpdatedSchema(currentSchema, cancellationToken);
        }

        return new(currentSchema, hasChanges, hasDisruptiveChanges);
    }

    private ICompositeModification ValidateChangeFrom(SchemaVersion newSchema, SchemaVersion currentSchema) =>
        schemaVersionValidatorFactory.Get().ValidateChangeFrom(newSchema, currentSchema);

    private static bool HasDisruptiveChanges(ICompositeModification modification) =>
        modification is IPossiblyDisruptive possiblyDisruptive &&
        possiblyDisruptive.CanContainDisruptiveDatabaseChanges();

    /// <summary>
    /// Updates the current version with the new schema version, incrementing
    /// the version number.
    /// </summary>
    /// <param name="newSchema">Incoming schema version with changes</param>
    /// <param name="currentSchema">Current schema version to be updated</param>
    private void UpdateSchema(SchemaVersion newSchema, SchemaVersion currentSchema)
    {
        var newVersion = currentSchema.Version + 1;
        schemaCopier.CopyInto(newSchema, currentSchema);
        currentSchema.Version = newVersion;
        currentSchema.ValidFrom = DateTime.UtcNow;
    }

    public bool CheckIfTypeExists(string typeName)
    {
        var et = ReadSchema().EntityTypes;

        return et.Any(x => x.InternalName == typeName);
    }
}