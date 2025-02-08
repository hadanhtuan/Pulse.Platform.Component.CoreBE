using Pulse.Library.Common.Extensions;
using Pulse.Library.Core.Schema;

namespace Pulse.Library.Services.Schema.Validators;

public class SchemaVersionValidator
{
    public static void Validate(SchemaVersion schema)
    {
        bool schemaIsNull = schema?.EntityTypes == null || !schema.EntityTypes.Any();
        if (schemaIsNull)
        {
            throw new ArgumentNullException(nameof(schema));
        }

        foreach (EntityType entityType in schema.GetAllEntityTypes())
        {
            EntityTypeValidator.Validate(entityType);
            AttributeTypeValidator.Validate(entityType);
        }

        foreach (EntityType rootEntityType in schema.EntityTypes)
        {
            EntityTypeValidator.ValidateRootEntityType(rootEntityType);
        }
    }
}
