using Database.Models.Schema;

namespace Services.Schema.Service;

/// <summary>
/// Checks for changes between two <see cref="EntityType"/>s.
/// </summary>
internal interface IEntityTypeChangeDetection
{
    bool HasChanges(EntityType a, EntityType b);
}