using Service.Schema.Service.Validation.Modifications;
using Database.Models.Schema;

namespace Service.Schema.Service.Validation;


/// Performs validation of unique constraints in a schema and changes in unique
/// constraints across schemas.

internal interface IUniqueConstraintsValidator
{
    void Validate(SchemaVersion newSchema, SchemaVersion oldSchema);

    IEnumerable<AddedUniqueConstraint> GetAddedUniqueConstraints(
        EntityType newEntityType, EntityType existingEntityType);

    IEnumerable<RemovedUniqueConstraint> GetRemovedUniqueConstraints(
        EntityType newEntityType, EntityType existingEntityType);

    void ValidateUniqueConstraints(EntityType newEntityType);
}