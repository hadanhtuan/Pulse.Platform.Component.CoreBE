using Services.Schema.Service.Validation.Modifications;
using Database.Models.Schema;
using Database.Models.Schema.Modifications;

namespace Services.Schema.Service.Validation;

public interface IEntityTypeValidator
{
    /// <summary>
    /// Validates the <paramref name="newEntityType"/> as a possible change to 
    /// <paramref name="oldEntityType"/>, returning a <see cref="IModifiedEntity"/>
    /// that may or may not contain any changes to the parts of the entity or 
    /// <see langword="null"/> if the entity types are determined to be the same.
    /// </summary>
    /// <param name="newEntityType"></param>
    /// <param name="oldEntityType"></param>
    /// <returns><see cref="IModifiedEntity"/> or <see langword="null"/></returns>
    IModifiedEntity? ValidateComparedToBase(EntityType newEntityType, EntityType oldEntityType);

    /// <summary>
    /// Validates the <paramref name="newEntityType"/> as a new definition
    /// not in existing schema.
    /// </summary>
    /// <param name="newEntityType"></param>
    /// <returns><see cref="AddedEntity"/></returns>
    IAddedEntity ValidateNewEntity(EntityType newEntityType);
}