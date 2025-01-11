using Database.Models.Schema;

namespace Service.Schema.Service;


/// Checks for changes between two <see cref="EntityType"/>s.

internal interface IEntityTypeChangeDetection
{
    bool HasChanges(EntityType a, EntityType b);
}