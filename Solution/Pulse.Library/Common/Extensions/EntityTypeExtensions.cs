using Pulse.Library.Core.Schema;

namespace Pulse.Library.Common.Extensions;

public static class EntityTypeExtensions
{
    public static IEnumerable<AttributeType> GetAllAttributeTypes(this EntityType entityType)
    {
        return entityType.AttributeTypes.OfType<AttributeType>()
        .OrderByDescending(a => a.IsRequired)
        .Concat(entityType.ComplexAttributeTypes);
    }
}
