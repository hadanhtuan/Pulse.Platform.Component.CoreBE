using Database.Models.Schema;
using Database.Models.Schema.Modifications;
using Index = Database.Models.Schema.Index;

namespace Service.Schema.Service.Validation.Modifications;

public class AddedEntity : CompositeModification, IAddedEntity
{
    public EntityType EntityType { get; }

    public AddedEntity(EntityType entityType)
    {
        EntityType = entityType;
        AddRange(entityType.AttributeTypes.Select(x => new AddedAttribute(entityType, x)));
        AddRange(entityType.Indexes.Select(x => new AddedIndex(entityType, x)));
        AddRange(entityType.UniqueConstraints.Select(x => new AddedUniqueConstraint(entityType, x)));
        AddRange(BuildStandardAttributes().Select(x => new AddedAttribute(entityType, x)));
        AddRange(BuildStandardAttributeIndexes().Select(x => new AddedIndex(entityType, x)));
    }

    private IEnumerable<AttributeType> BuildStandardAttributes() =>
        StandardAttributeBuilder.BuildStandardAttributes(EntityType);

    private IEnumerable<Index> BuildStandardAttributeIndexes() =>
        StandardAttributeIndexBuilder.BuildStandardAttributeIndexes(EntityType);

    public override string ToString() =>
        $"{nameof(AddedEntity)}({EntityType.InternalName})";
}