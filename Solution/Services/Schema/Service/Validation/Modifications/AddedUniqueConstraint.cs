using Database.Models.Schema;
using Database.Models.Schema.Modifications;

namespace Services.Schema.Service.Validation.Modifications;

public class AddedUniqueConstraint : DisruptiveDatabaseModification, IAddedUniqueConstraint
{
    public EntityType EntityType { get; }

    public UniqueConstraint UniqueConstraint { get; }

    public AddedUniqueConstraint(EntityType entityType, UniqueConstraint uniqueConstraint)
    {
        EntityType = entityType;
        UniqueConstraint = uniqueConstraint;
    }

    public override string ToString() =>
        $"{nameof(AddedUniqueConstraint)}({EntityType.InternalName}.{UniqueConstraint.InternalName})";
}