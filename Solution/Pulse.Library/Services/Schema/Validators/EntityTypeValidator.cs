using Pulse.Library.Core.Schema;
using Pulse.Library.Services.Schema.Exceptions;
using System.Text.RegularExpressions;

namespace Pulse.Library.Services.Schema.Validators;

public static class EntityTypeValidator
{
    public static readonly Regex validInternalNameRegex = new("^[a-z_]+$");

    public static void Validate(EntityType entityType)
    {
        ValidateEntityFields(entityType);
    }

    public static void ValidateRootEntityType(EntityType entityType)
    {
        if (entityType.AttributeTypes.Any(at => at.InternalName == "id"))
        {
            throw new SchemaInvalidException($"Root Entity with internal name '{entityType.InternalName}' has an attribute with the reserved internal name 'id'.");
        }
    }

    private static void ValidateEntityFields(EntityType newEntityType)
    {
        if (newEntityType.InternalName == null)
        {
            throw new SchemaInvalidException($"Entity is missing required internal name.");
        }

        if (!validInternalNameRegex.IsMatch(newEntityType.InternalName))
        {
            throw new SchemaInvalidException($"Entity internal name is invalid: '{newEntityType.InternalName}'");
        }

        if (string.IsNullOrWhiteSpace(newEntityType.DisplayName))
        {
            throw new SchemaInvalidException($"Entity with internal name '{newEntityType.InternalName}' is missing a required display name.");
        }
    }
}
