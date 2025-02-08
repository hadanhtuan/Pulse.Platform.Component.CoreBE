using Pulse.Library.Core.Schema;

namespace Pulse.Library.Common.Extensions;

public static class SchemaVersionExtensions
{
    public static IEnumerable<EntityType> GetAllEntityTypes(this SchemaVersion schemaVersion)
    {
        return schemaVersion.EntityTypes.Concat(schemaVersion.ComplexDataTypes)
                    .OrderBy(a => a.InternalName, StringComparer.InvariantCultureIgnoreCase);
    }
}
