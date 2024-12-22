using Database.Models.Schema;
using Database.Models.Schema.Modifications;

namespace Services.Schema.Service.Validation.Modifications;

public class ModifiedEntity : CompositeModification, IModifiedEntity
{
    public EntityType EntityType { get; }

    public ModifiedEntity(EntityType entityType)
    {
        EntityType = entityType;
    }

    public override string ToString() =>
        $"{nameof(ModifiedEntity)}({EntityType.InternalName})";
}