using Database.Models.Schema;
using System.Collections.Generic;
using System.Linq;

namespace Services.Schema.Service.Copy;

internal class SchemaVersionCopier : ISchemaVersionCopier
{
    private readonly IEntityTypeCopier entityTypeCopier;

    public SchemaVersionCopier(IEntityTypeCopier entityTypeCopier)
    {
        this.entityTypeCopier = entityTypeCopier;
    }

    internal readonly List<string> ignoredSchemaVersionProperties = new List<string>()
    {
        nameof(SchemaVersion.Id), nameof(SchemaVersion.EntityTypes)
    };

    public SchemaVersion CopyInto(SchemaVersion source, SchemaVersion target)
    {
        source.CopyPropertiesInto(target, ignoredSchemaVersionProperties);
        target.EntityTypes = Copy(source.EntityTypes, target.EntityTypes.ToList());
        return target;
    }

    private ICollection<EntityType> Copy(ICollection<EntityType> source, List<EntityType> target)
    {
        foreach (var entity in source)
        {
            var targetEntity = target.FirstOrDefault(e => e.InternalName == entity.InternalName);

            if (targetEntity == null)
            {
                targetEntity = (entity) switch
                {
                    RootEntityType _ => new RootEntityType(),
                    ComplexAttributeEntityType _ => new ComplexAttributeEntityType(),
                    ChildEntityType _ => new ChildEntityType(),
                    _ => throw new System.NotSupportedException($"Data type '{entity.GetType()}' is not supported.")
                };
                target.Add(targetEntity);
            }

            entityTypeCopier.CopyInto(entity, targetEntity);
        }

        foreach (var entity in source)
        {
            entityTypeCopier.AddComplexAttributeTypes(entity, target);
            entityTypeCopier.AddChildEntityAttributeTypes(entity, target);
            entityTypeCopier.AddEntityReferenceTypes(entity, target);
        }

        return target;
    }
}