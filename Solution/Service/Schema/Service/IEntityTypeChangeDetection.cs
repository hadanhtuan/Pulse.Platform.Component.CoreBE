using Database.Models.Schema;

namespace Service.Schema.Service;

/// <summary>
/// Checks for changes between two <see cref="EntityType"/>s.
/// </summary>
internal interface IEntityTypeChangeDetection
{
    bool HasChanges(EntityType a, EntityType b);
}