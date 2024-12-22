using Database.Models.Schema;
using Database.Models.Schema.Modifications;
using Index = Database.Models.Schema.Index;

namespace Services.Schema.Service.Validation.Modifications;

public class AddedIndex : DisruptiveDatabaseModification, IAddedIndex
{
    public EntityType EntityType { get; }

    public Index Index { get; }

    public AddedIndex(EntityType entityType, Index index)
    {
        EntityType = entityType;
        Index = index;
    }

    public override string ToString() =>
        $"{nameof(AddedIndex)}({EntityType.InternalName}.{Index.Columns})";
}