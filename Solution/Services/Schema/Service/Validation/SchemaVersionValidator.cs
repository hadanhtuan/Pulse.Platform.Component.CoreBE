using Database.Models.Schema;
using Database.Models.Schema.Modifications;
using Services.Schema.Exceptions;
using Services.Schema.Service.Validation.Modifications;

namespace Services.Schema.Service.Validation;

internal class SchemaVersionValidator : ISchemaVersionValidator
{
    private readonly IEntityTypeValidator entityTypeValidator;
    private readonly IUniqueConstraintsValidator uniqueConstraintsValidator;
    private readonly IStandardSchemaValidator standardSchemaValidator;

    private readonly CompositeModification schemaModification = new();

    public SchemaVersionValidator(
        IEntityTypeValidator entityTypeValidator,
        IUniqueConstraintsValidator uniqueConstraintsValidator,
        IStandardSchemaValidator standardSchemaValidator)
    {
        this.entityTypeValidator = entityTypeValidator;
        this.uniqueConstraintsValidator = uniqueConstraintsValidator;
        this.standardSchemaValidator = standardSchemaValidator;
    }

    public ICompositeModification ValidateChangeFrom(SchemaVersion newSchema, SchemaVersion oldSchema)
    {
        standardSchemaValidator.ValidateStandardElements(newSchema);
        uniqueConstraintsValidator.Validate(newSchema, oldSchema);

        Dictionary<string, EntityType> oldSchemaDictionary = GetValidatedNewSchemaDictionary(oldSchema);
        Dictionary<string, EntityType> newSchemaDictionary = GetValidatedNewSchemaDictionary(newSchema);

        Dictionary<string, string> diff = oldSchemaDictionary.ToDictionary(e => e.Key, e => "Removed");

        foreach (var key in newSchemaDictionary.Keys)
        {
            diff[key] = oldSchemaDictionary.ContainsKey(key) ? "Existing" : "Added";
        }

        foreach (var (key, value) in diff)
        {
            switch (value)
            {
                case "Existing":
                    OnExistingEntity(newSchemaDictionary[key], oldSchemaDictionary[key]);
                    break;
                case "Added":
                    OnAddedEntity(newSchemaDictionary[key]);
                    break;
                case "Removed":
                    throw new SchemaMissingEntityTypeException(key);
            }
        }

        return schemaModification;
    }

    private void OnExistingEntity(EntityType newEntityType, EntityType existingEntityType)
    {
        IModifiedEntity? change = entityTypeValidator.ValidateComparedToBase(newEntityType, existingEntityType);
        schemaModification.AddIfNotNull(change);
    }

    private void OnAddedEntity(EntityType entityType)
    {
        IAddedEntity change = entityTypeValidator.ValidateNewEntity(entityType);
        schemaModification.AddIfNotNull(change);
    }

    private static Dictionary<string, EntityType> GetValidatedNewSchemaDictionary(SchemaVersion newSchema)
    {
        try
        {
            return newSchema.EntityTypes.ToDictionary(x => x.InternalName);
        }
        catch (ArgumentException ex)
        {
            // C# ToDictionary throws this exception only on duplicate key
            var split = ex.Message.Split("Key: ");
            var duplicateKey = split.Length > 1 ? split[1] : "undetermined";
            throw new SchemaHasDuplicateEntityTypeException(duplicateKey);
        }
    }
}