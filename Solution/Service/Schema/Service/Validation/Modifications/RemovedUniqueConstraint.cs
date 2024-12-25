using Database.Models.Schema;
using Database.Models.Schema.Modifications;

namespace Service.Schema.Service.Validation.Modifications;

public class RemovedUniqueConstraint : IRemovedUniqueConstraint
{
    public EntityType EntityType { get; }

    public UniqueConstraint UniqueConstraint { get; }

    public RemovedUniqueConstraint(EntityType entityType, UniqueConstraint uniqueConstraint)
    {
        EntityType = entityType;
        UniqueConstraint = uniqueConstraint;
    }

    public override string ToString() =>
        $"{nameof(RemovedUniqueConstraint)}({EntityType.InternalName}.{UniqueConstraint.InternalName})";
}