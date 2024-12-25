using Database.Models.Schema;
using Service.Schema.Service.Validation.Modifications;

namespace Service.Schema.Service.Validation;

internal interface IAttributeTypesValidator
{
    void ValidateAttributeTypes(EntityType newEntityType);
    void ValidateAttributeTypes(EntityType newEntityType, EntityType oldEntityType);
    IEnumerable<AddedAttribute> GetAddedAttributeTypes(EntityType newEntityType, EntityType existingEntityType);
}