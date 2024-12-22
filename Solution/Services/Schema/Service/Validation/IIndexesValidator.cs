using Services.Schema.Service.Validation.Modifications;
using Database.Models.Schema;

namespace Services.Schema.Service.Validation;

internal interface IIndexesValidator
{
    IEnumerable<AddedIndex> GetAddedIndexes(EntityType newEntityType, EntityType existingEntityType);
    IEnumerable<RemovedIndex> GetRemovedIndexes(EntityType newEntityType, EntityType existingEntityType);
    void ValidateIndexes(EntityType newEntityType);
}