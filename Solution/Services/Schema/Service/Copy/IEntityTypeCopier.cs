using Database.Models.Schema;
using System.Collections.Generic;

namespace Services.Schema.Service.Copy;

internal interface IEntityTypeCopier
{
    void CopyInto(EntityType source, EntityType target);

    void AddComplexAttributeTypes(EntityType source, IEnumerable<EntityType> targetEntities);

    void AddChildEntityAttributeTypes(EntityType source, IEnumerable<EntityType> targetEntities);

    void AddEntityReferenceTypes(EntityType source, IEnumerable<EntityType> targetEntities);
}