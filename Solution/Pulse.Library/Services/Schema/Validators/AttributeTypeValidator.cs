using Pulse.Library.Core.Schema;
using Pulse.Library.Services.Schema.Exceptions;
using System.Text.RegularExpressions;

namespace Pulse.Library.Services.Schema.Validators;

public static class AttributeTypeValidator
{
    private static readonly Regex validInternalNameRegex = EntityTypeValidator.validInternalNameRegex;
    private static readonly Regex validGroupRegex = new("^[a-zA-Z_]+$");

    private static readonly string[] reservedNames = ["entity_id", "last_modified"];

    public static void Validate(EntityType entityType)
    {
        ValidateUniqueAttributeInternalNames(entityType);
        ValidateAttributeFields(entityType);
    }

    private static void ValidateUniqueAttributeInternalNames(EntityType newEntityType)
    {
        var internalNames = new HashSet<string>();
        var isAttributeTypeInternalNameUnique =
            newEntityType.AttributeTypes.All(x => internalNames.Add(x.InternalName));

        if (!isAttributeTypeInternalNameUnique)
        {
            throw new SchemaInvalidException($"Entity with internal name '{newEntityType.InternalName}' has duplicate attribute internal names.");
        }
    }

    private static void ValidateAttributeFields(EntityType entityType)
    {
        foreach (var attributeType in entityType.AttributeTypes)
        {
            if (!validInternalNameRegex.IsMatch(attributeType.InternalName))
            {
                throw new SchemaInvalidException($"Entity with internal name '{entityType.InternalName}' has invalid attribute internal name: '{attributeType.InternalName}'");
            }

            if (reservedNames.Contains(attributeType.InternalName))
            {
                throw new SchemaInvalidException($"Entity with internal name '{entityType.InternalName}' is using a reserved attribute internal name: '{attributeType.InternalName}'");
            }

            if (attributeType.Group != null && !validGroupRegex.IsMatch(attributeType.Group))
            {
                throw new SchemaInvalidException($"Entity with internal name '{entityType.InternalName}' has invalid group name: '{attributeType.Group}'");
            }

            if (string.IsNullOrWhiteSpace(attributeType.DisplayName))
            {
                throw new SchemaInvalidException($"Entity with internal name '{entityType.InternalName}' is missing a required display name on the attribute: '{attributeType.InternalName}'");
            }
        }
    }
}