using Database.Models.Schema;
using System;
using System.Collections.Generic;

namespace Services.Schema.Service.Validation.Modifications;

public static class StandardAttributeIndexBuilder
{
    public static IEnumerable<Database.Models.Schema.Index> BuildStandardAttributeIndexes(EntityType entityType)
    {
        if (entityType.InternalName == "visit")
        {
            return new[] { BuildIndex("inbound_flight"), BuildIndex("outbound_flight") };
        }

        if (entityType.InternalName == "ground_leg")
        {
            return new[] { BuildIndex("visit") };
        }

        return Array.Empty<Database.Models.Schema.Index>();
    }

    private static Database.Models.Schema.Index BuildIndex(string columnName) =>
        new() { Columns = columnName };
}