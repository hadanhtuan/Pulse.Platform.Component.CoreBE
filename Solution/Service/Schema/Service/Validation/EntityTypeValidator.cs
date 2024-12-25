using Database.Models.Schema;
using Database.Models.Schema.Modifications;
using Service.Schema.Service.Validation.Modifications;
using Service.Schema.Exceptions;

namespace Service.Schema.Service.Validation;

internal class EntityTypeValidator : IEntityTypeValidator
{
    private readonly IEntityTypeChangeDetection entityTypeChangeDetection;
    private readonly IAttributeTypesValidator attributeTypesValidator;
    private readonly IIndexesValidator indexesValidator;
    private readonly IUniqueConstraintsValidator uniqueConstraintsValidator;

    public EntityTypeValidator(
        IEntityTypeChangeDetection entityTypeChangeDetection,
        IAttributeTypesValidator attributeTypesValidator,
        IIndexesValidator indexesValidator,
        IUniqueConstraintsValidator uniqueConstraintsValidator)
    {
        this.entityTypeChangeDetection = entityTypeChangeDetection;
        this.attributeTypesValidator = attributeTypesValidator;
        this.indexesValidator = indexesValidator;
        this.uniqueConstraintsValidator = uniqueConstraintsValidator;
    }

    public IAddedEntity ValidateNewEntity(EntityType newEntityType)
    {
        ValidateEntityFields(newEntityType);
        attributeTypesValidator.ValidateAttributeTypes(newEntityType);
        indexesValidator.ValidateIndexes(newEntityType);
        uniqueConstraintsValidator.ValidateUniqueConstraints(newEntityType);

        return new AddedEntity(newEntityType);
    }

    public IModifiedEntity? ValidateComparedToBase(EntityType newEntityType, EntityType oldEntityType)
    {
        if (!((newEntityType is RootEntityType && oldEntityType is RootEntityType) ||
              (newEntityType is ComplexAttributeEntityType && oldEntityType is ComplexAttributeEntityType)))
        {
            throw new EntityTypeException("New schema tries to change entity type '{EntityTypeName}' from type "
                                          + "'{OldEntityTypeType}' to type '{NewEntityTypeType}', which is not allowed.",
                new Dictionary<string, object>
                {
                    { "EntityTypeName", newEntityType.InternalName },
                    { "OldEntityTypeType", oldEntityType.GetType().Name },
                    { "NewEntityTypeType", newEntityType.GetType().Name }
                });
        }

        ValidateEntityFields(newEntityType);
        attributeTypesValidator.ValidateAttributeTypes(newEntityType, oldEntityType);
        indexesValidator.ValidateIndexes(newEntityType);
        uniqueConstraintsValidator.ValidateUniqueConstraints(newEntityType);

        bool hasChanges = entityTypeChangeDetection.HasChanges(newEntityType, oldEntityType);

        return hasChanges ? BuildModifiedEntity(newEntityType, oldEntityType) : null;
    }

    private static void ValidateEntityFields(EntityType newEntityType)
    {
        if (!ValidationConstants.validInternalNameRegex.IsMatch(newEntityType.InternalName))
        {
            throw new EntityTypeInvalidPropertyException(newEntityType.InternalName,
                nameof(EntityType.InternalName), newEntityType.InternalName,
                ValidationConstants.validInternalNameExplanation);
        }

        if (newEntityType.PluralName == null)
        {
            throw new EntityTypeMissingPropertyException(
                newEntityType.InternalName, nameof(EntityType.PluralName));
        }

        if (newEntityType.PluralName != null &&
            !ValidationConstants.validInternalNameRegex.IsMatch(newEntityType.PluralName))
        {
            throw new EntityTypeInvalidPropertyException(newEntityType.InternalName,
                "PluralName", newEntityType.PluralName, ValidationConstants.validInternalNameExplanation);
        }

        if (string.IsNullOrWhiteSpace(newEntityType.DisplayName))
        {
            throw new EntityTypeMissingPropertyException(
                newEntityType.InternalName, nameof(EntityType.DisplayName));
        }
    }

    private ModifiedEntity BuildModifiedEntity(EntityType newType, EntityType oldType)
    {
        var modification = new ModifiedEntity(newType);
        modification.AddRange(attributeTypesValidator.GetAddedAttributeTypes(newType, oldType));
        modification.AddRange(indexesValidator.GetAddedIndexes(newType, oldType));
        modification.AddRange(indexesValidator.GetRemovedIndexes(newType, oldType));
        modification.AddRange(uniqueConstraintsValidator.GetAddedUniqueConstraints(newType, oldType));
        modification.AddRange(uniqueConstraintsValidator.GetRemovedUniqueConstraints(newType, oldType));
        return modification;
    }
}