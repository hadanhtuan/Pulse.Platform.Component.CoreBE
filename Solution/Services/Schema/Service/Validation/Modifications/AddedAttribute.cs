using Database.Models.Schema;
using Database.Models.Schema.Modifications;

namespace Services.Schema.Service.Validation.Modifications;

public class AddedAttribute : IAddedAttribute
{
    public EntityType EntityType { get; }

    public AttributeType AttributeType { get; }

    public AddedAttribute(EntityType entityType, AttributeType attributeType)
    {
        EntityType = entityType;
        AttributeType = attributeType;
    }

    public override string ToString() =>
        $"{nameof(AddedAttribute)}({EntityType.InternalName}.{AttributeType.InternalName})";
}