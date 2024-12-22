using Database.Models.Schema;

namespace Services.Schema.Service.Copy;

internal class AttributeTypeCopier : IAttributeTypeCopier
{
    internal readonly List<string> ignoredAttributeTypeProperties = new List<string>()
    {
        nameof(AttributeType.Id),
        nameof(AttributeType.EntityType),
        nameof(ComplexAttributeType.ComplexDataType),
        nameof(ComplexAttributeType.IsCollection),
        nameof(ValueAttributeType.EntityReferenceType),
        nameof(ChildEntityAttributeType.DataStructure),
        nameof(ChildEntityAttributeType.ChildEntityType)
    };

    public void CopyInto(AttributeType source, AttributeType target)
    {
        if (source is ValueAttributeType sourceAsValueAttributeType &&
            target is ValueAttributeType targetAsValueAttributeType)
        {
            sourceAsValueAttributeType.CopyPropertiesInto(targetAsValueAttributeType, ignoredAttributeTypeProperties);
            return;
        }

        if (source is ComplexAttributeType sourceAsComplexAttributeType &&
            target is ComplexAttributeType targetAsComplexAttributeType)
        {
            sourceAsComplexAttributeType.CopyPropertiesInto(targetAsComplexAttributeType,
                ignoredAttributeTypeProperties);
            return;
        }

        if (source is ChildEntityAttributeType sourceAsChildEntityAttributeType &&
            target is ChildEntityAttributeType targetAsChildEntityAttributeType)
        {
            sourceAsChildEntityAttributeType.CopyPropertiesInto(targetAsChildEntityAttributeType,
                ignoredAttributeTypeProperties);
            return;
        }

        throw new InvalidOperationException(
            $"Cannot copy from type {source.GetType()} to type {target.GetType()}");
    }
}