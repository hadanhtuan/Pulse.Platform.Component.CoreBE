using Database.Models.Schema;
using Database.Models.Schema.Modifications;
using Index = Database.Models.Schema.Index;

namespace Services.Schema.Service.Validation.Modifications;

public class RemovedIndex : IRemovedIndex
{
    public EntityType EntityType { get; }

    public Index Index { get; }

    public RemovedIndex(EntityType entityType, Index index)
    {
        EntityType = entityType;
        Index = index;
    }

    public override string ToString() =>
        $"{nameof(RemovedIndex)}({EntityType.InternalName}.{Index.Columns})";
}