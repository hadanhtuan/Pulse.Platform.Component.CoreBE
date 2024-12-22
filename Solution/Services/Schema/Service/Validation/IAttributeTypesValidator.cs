using Database.Models.Schema;
using Services.Schema.Service.Validation.Modifications;

namespace Services.Schema.Service.Validation;

internal interface IAttributeTypesValidator
{
    void ValidateAttributeTypes(EntityType newEntityType);
    void ValidateAttributeTypes(EntityType newEntityType, EntityType oldEntityType);
    IEnumerable<AddedAttribute> GetAddedAttributeTypes(EntityType newEntityType, EntityType existingEntityType);
}