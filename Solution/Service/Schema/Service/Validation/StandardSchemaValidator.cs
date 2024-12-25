using Database.Models.Schema;
using Service.Schema.Exceptions;

namespace Service.Schema.Service.Validation;

/// <inheritdoc cref="IStandardSchemaValidator"/>
public class StandardSchemaValidator : IStandardSchemaValidator
{
    // Exposed internally for testing
    internal IDictionary<string, IEnumerable<string>> StandardEntityAttributeTypes { get; set; } =
        StandardElements.EntityAttributeTypes;

    public void ValidateStandardElements(SchemaVersion schema)
    {
        foreach (var (entityTypeName, attributeTypeNames) in StandardEntityAttributeTypes)
        {
            var entityType = schema.EntityTypes.SingleOrDefault(e => e.InternalName == entityTypeName &&
                                                                     e.Status == Status.Active)
                             ?? throw new SchemaMissingStandardEntityTypeException(entityTypeName);

            foreach (string attributeTypeName in attributeTypeNames)
            {
                if (entityType.AttributeTypes.All(a => a.InternalName != attributeTypeName &&
                                                       a.Status == Status.Active))
                {
                    throw new EntityTypeMissingStandardAttributeException(entityTypeName, attributeTypeName);
                }
            }
        }
    }
}