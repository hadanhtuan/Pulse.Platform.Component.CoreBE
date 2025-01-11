using Database.Models.Schema;
using Service.Schema.Service.Validation.Modifications;
using Service.Schema.Exceptions;
using System.Text.RegularExpressions;

namespace Service.Schema.Service.Validation;

internal class AttributeTypesValidator : IAttributeTypesValidator
{
    private static readonly Regex validInternalNameRegex = ValidationConstants.validInternalNameRegex;
    private static readonly Regex validGroupRegex = new("^[a-zA-Z_]+$");

    private static readonly string[] ReservedNames = new[] { "entity_id", "last_modified" };

    private static readonly string ReservedNameExplanation =
        "Reserved names cannot be used for attributes.";

    public void ValidateAttributeTypes(EntityType newEntityType)
    {
        UniqueAttributeInternalNames(newEntityType);
        ValidateAttributeFields(newEntityType);
    }

    public void ValidateAttributeTypes(EntityType newEntityType, EntityType oldEntityType)
    {
        var newAttributeNames = UniqueAttributeInternalNames(newEntityType);

        if (!oldEntityType.AttributeTypes.All(x => newAttributeNames.Contains(x.InternalName)))
        {
            throw new EntityTypeMissingAttributeException(newEntityType.InternalName);
        }

        ValidateValueAttributeTypes(newEntityType, oldEntityType);
        ValidateComplexAttributeTypes(newEntityType, oldEntityType);
        ValidateAttributeFields(newEntityType);
    }

    public IEnumerable<AddedAttribute> GetAddedAttributeTypes(
        EntityType newEntityType, EntityType existingEntityType)
    {
        var existingInternalNames = UniqueAttributeInternalNames(existingEntityType);
        return newEntityType.AttributeTypes.Where(x => !existingInternalNames.Contains(x.InternalName))
            .Select(x => new AddedAttribute(newEntityType, x));
    }

    private static HashSet<string> UniqueAttributeInternalNames(EntityType newEntityType)
    {
        var internalNames = new HashSet<string>();
        var isAttributeTypeInternalNameUnique =
            newEntityType.AttributeTypes.All(x => internalNames.Add(x.InternalName));
        return isAttributeTypeInternalNameUnique
            ? internalNames
            : throw new EntityTypeDuplicateAttributesException(newEntityType.InternalName);
    }

    
    /// Validates that new value attribute types either do not already exist, or that the existing
    /// match is also a value attribute type with the same ValueDataType.
    
    /// <param name="newEntityType"></param>
    /// <param name="oldEntityType"></param>
    private static void ValidateValueAttributeTypes(EntityType newEntityType, EntityType oldEntityType)
    {
        // validate only value attribute types for which data type may have changed
        var valueAttributeTypes = newEntityType.AttributeTypes.OfType<ValueAttributeType>();

        foreach (var attribute in valueAttributeTypes)
        {
            var oldAttributeType =
                oldEntityType.AttributeTypes.FirstOrDefault(a => a.InternalName == attribute.InternalName);
            // We don't check new attribute types
            if (oldAttributeType == null!)
            {
                continue;
            }

            var oldValueAttributeType = oldAttributeType as ValueAttributeType;

            var isEqualTypes = oldValueAttributeType != null
                               &&
                               (oldValueAttributeType.ValueDataType == attribute.ValueDataType
                                || (oldValueAttributeType.ValueDataType == DataType.Date &&
                                    attribute.ValueDataType == DataType.DateTime)
                                || (oldValueAttributeType.ValueDataType == DataType.DateTime &&
                                    attribute.ValueDataType == DataType.Date)
                                || (oldValueAttributeType.ValueDataType == DataType.Text &&
                                    attribute.ValueDataType == DataType.TextArea)
                                || (oldValueAttributeType.ValueDataType == DataType.TextArea &&
                                    attribute.ValueDataType == DataType.Text));

            if (!isEqualTypes)
            {
                throw new AttributeDataTypeChangedException(
                    oldEntityType.InternalName, oldAttributeType.InternalName);
            }
        }
    }

    
    /// Validates that new complex attribute types either do not already exist, or that the existing
    /// match is also a complex attribute type with the same ComplexDataType and value of IsCollection.
    
    /// <param name="newEntityType"></param>
    /// <param name="oldEntityType"></param>
    private static void ValidateComplexAttributeTypes(EntityType newEntityType, EntityType oldEntityType)
    {
        var complexAttributeTypes = newEntityType.AttributeTypes.OfType<ComplexAttributeType>();

        foreach (var attribute in complexAttributeTypes)
        {
            var oldAttributeType =
                oldEntityType.AttributeTypes.FirstOrDefault(a => a.InternalName == attribute.InternalName);
            // We don't check new attribute types
            if (oldAttributeType == null!)
            {
                continue;
            }

            var oldComplexAttributeType = oldAttributeType as ComplexAttributeType;
            if (oldComplexAttributeType == null ||
                oldComplexAttributeType.IsCollection != attribute.IsCollection ||
                oldComplexAttributeType.ComplexDataType.InternalName != attribute.ComplexDataType.InternalName)
            {
                throw new ComplexAttributeTypeChangedException(
                    oldEntityType.InternalName, oldAttributeType.InternalName);
            }
        }
    }

    private static void ValidateAttributeFields(EntityType entityType)
    {
        foreach (var attributeType in entityType.AttributeTypes)
        {
            if (!validInternalNameRegex.IsMatch(attributeType.InternalName))
            {
                throw new AttributeTypeNameInvalidException(entityType.InternalName,
                    nameof(AttributeType.InternalName), attributeType.InternalName,
                    ValidationConstants.validInternalNameExplanation);
            }

            if (ReservedNames.Contains(attributeType.InternalName))
            {
                throw new AttributeTypeNameInvalidException(entityType.InternalName,
                    nameof(AttributeType.InternalName), attributeType.InternalName,
                    ReservedNameExplanation);
            }

            if (attributeType.Group != null && !validGroupRegex.IsMatch(attributeType.Group))
            {
                throw new AttributeTypePropertyInvalidException(entityType.InternalName,
                    attributeType.InternalName, nameof(AttributeType.Group),
                    attributeType.Group, ValidationConstants.validInternalNameExplanation);
            }

            if (string.IsNullOrWhiteSpace(attributeType.ShortDisplayName))
            {
                throw new AttributeTypePropertyMissingException(entityType.InternalName,
                    attributeType.InternalName, nameof(AttributeType.ShortDisplayName));
            }

            if (string.IsNullOrWhiteSpace(attributeType.LongDisplayName))
            {
                throw new AttributeTypePropertyMissingException(entityType.InternalName,
                    attributeType.InternalName, nameof(AttributeType.LongDisplayName));
            }

            if (attributeType is ValueAttributeType valueAttributeType)
            {
                ValidateMasterDataInformationForDropDowns(valueAttributeType);
                ValidateEntityReferenceType(valueAttributeType);
            }
        }
    }

    private static void ValidateMasterDataInformationForDropDowns(ValueAttributeType valueAttributeType)
    {
        if (valueAttributeType.ValueDataType == DataType.Dropdown &&
            (!validInternalNameRegex.IsMatch(valueAttributeType.MasterdataTable ?? "") ||
             !validInternalNameRegex.IsMatch(valueAttributeType.MasterdataValueAttributeName ?? "")))
        {
            throw new EntityTypeException(
                "Attribute with internal name '{valueAttributeTypeName}' - MasterdataTable and " +
                "MasterdataValueAttributeName must be set when ValueDataType is Dropdown",
                new Dictionary<string, object> { { "valueAttributeTypeName", valueAttributeType.InternalName } });
        }
    }

    private static void ValidateEntityReferenceType(ValueAttributeType valueAttributeType)
    {
        if (valueAttributeType.EntityReferenceType != null && valueAttributeType.ValueDataType != DataType.Guid)
        {
            throw new EntityTypeException(
                "Attribute with internal name '{valueAttributeType.InternalName}' - When EntityReferenceType " +
                " points to an Entity Type ('{valueAttributeType.EntityReferenceType.InternalName}') then the ValueDataType" +
                " cannot be '{valueAttributeType.ValueDataType}' (it must be Guid).",
                new Dictionary<string, object>
                {
                    { "valueAttributeType.InternalName", valueAttributeType.InternalName },
                    {
                        "valueAttributeType.EntityReferenceType.InternalName",
                        valueAttributeType.EntityReferenceType.InternalName
                    },
                    { "valueAttributeType.ValueDataType", valueAttributeType.ValueDataType },
                });
        }
    }
}