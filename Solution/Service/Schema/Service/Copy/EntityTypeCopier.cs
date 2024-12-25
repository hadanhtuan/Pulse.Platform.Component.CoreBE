using Database.Models.Schema;
using Action = Database.Models.Schema.Action;

namespace Service.Schema.Service.Copy;

internal class EntityTypeCopier : IEntityTypeCopier
{
    private readonly IAttributeTypeCopier attributeTypeCopier;

    public EntityTypeCopier(IAttributeTypeCopier attributeTypeCopier)
    {
        this.attributeTypeCopier = attributeTypeCopier;
    }

    internal static readonly List<string> ignoredEntityTypeProperties = new List<string>()
    {
        nameof(EntityType.Id),
        nameof(EntityType.SchemaVersion),
        nameof(EntityType.AttributeTypes),
        nameof(ComplexAttributeEntityType.ComplexAttributeTypes),
        nameof(ChildEntityType.ChildEntityAttributeTypes),
        nameof(EntityType.Actions)
    };

    internal static readonly List<string> ignoredActionTypeProperties = new List<string>() { nameof(Action.Id), };

    public void CopyInto(EntityType source, EntityType target)
    {
        // Clear these as new input is authorative and old information should just be discarded.
        // Note: It may improve performance to compare indexes/constraints so that the later updates
        // only have to take changes into account.
        target.Indexes.Clear();
        target.UniqueConstraints.Clear();

        source.CopyPropertiesInto(target, ignoredEntityTypeProperties);

        target.AttributeTypes ??= new List<AttributeType>();

        foreach (var attribute in source.AttributeTypes.OfType<ValueAttributeType>())
        {
            var targetAttribute = target.AttributeTypes
                .FirstOrDefault(e => e.InternalName == attribute.InternalName);
            if (targetAttribute == null)
            {
                targetAttribute = new ValueAttributeType();
                target.AttributeTypes.Add(targetAttribute);
            }

            attributeTypeCopier.CopyInto(attribute, targetAttribute);
        }

        // Update/add actions
        foreach (var action in source.Actions)
        {
            var targetAction = target.Actions
                .FirstOrDefault(e => e.InternalName == action.InternalName);
            if (targetAction != null)
            {
                action.CopyPropertiesInto(targetAction, ignoredActionTypeProperties);
            }
            else
            {
                target.Actions.Add(action);
            }
        }

        // Archive actions that are no longer present in new schema version
        foreach (var action in target.Actions)
        {
            var sourceAction = source.Actions
                .FirstOrDefault(e => e.InternalName == action.InternalName);
            if (sourceAction == null)
            {
                action.Status = Status.Archived;
            }
        }
    }

    public void AddComplexAttributeTypes(EntityType source, IEnumerable<EntityType> targetEntities)
    {
        var target = targetEntities.First(target => target.InternalName == source.InternalName);

        foreach (var complexAttributeType in source.AttributeTypes.OfType<ComplexAttributeType>())
        {
            var targetComplexAttributeEntityType = targetEntities.OfType<ComplexAttributeEntityType>().First(
                entity => entity.InternalName == complexAttributeType.ComplexDataType.InternalName);

            var targetAttribute = target.AttributeTypes
                .FirstOrDefault(e => e.InternalName == complexAttributeType.InternalName);
            if (targetAttribute == null)
            {
                targetAttribute =
                    new ComplexAttributeType(targetComplexAttributeEntityType, complexAttributeType.IsCollection);
                target.AttributeTypes.Add(targetAttribute);
            }

            attributeTypeCopier.CopyInto(complexAttributeType, targetAttribute);
        }
    }

    public void AddChildEntityAttributeTypes(EntityType source, IEnumerable<EntityType> targetEntities)
    {
        var target = targetEntities.First(target => target.InternalName == source.InternalName);

        foreach (var childEntityAttributeType in source.AttributeTypes.OfType<ChildEntityAttributeType>())
        {
            var targetChildEntityAttributeEntityType = targetEntities.OfType<ChildEntityType>().First(
                entity => entity.InternalName == childEntityAttributeType.ChildEntityType.InternalName);

            var targetAttribute = target.AttributeTypes
                .FirstOrDefault(e => e.InternalName == childEntityAttributeType.InternalName);
            if (targetAttribute == null)
            {
                targetAttribute =
                    new ChildEntityAttributeType(targetChildEntityAttributeEntityType,
                        childEntityAttributeType.DataStructure);
                target.AttributeTypes.Add(targetAttribute);
            }

            attributeTypeCopier.CopyInto(childEntityAttributeType, targetAttribute);
        }
    }

    public void AddEntityReferenceTypes(EntityType source, IEnumerable<EntityType> targetEntities)
    {
        var target = targetEntities.Single(target => target.InternalName == source.InternalName);

        foreach (var attributeWithReferenceType in source.AttributeTypes.OfType<ValueAttributeType>()
                     .Where(aType => aType.EntityReferenceType != null))
        {
            var targetAttribute = (ValueAttributeType)target.AttributeTypes
                .Single(aType => aType.InternalName == attributeWithReferenceType.InternalName);

            var targetReference = targetEntities
                .Single(eType => eType.InternalName == attributeWithReferenceType.EntityReferenceType?.InternalName);

            targetAttribute.EntityReferenceType = targetReference;
        }
    }
}