using Database.Models.Schema;
using System.Collections.Generic;
using System.Linq;

namespace Service.Schema.Service.Validation.Modifications;

public static class StandardAttributeBuilder
{
    public static IEnumerable<ValueAttributeType> BuildStandardAttributes(EntityType entityType)
    {
        var attributes = new List<ValueAttributeType>();
        switch (entityType.InternalName)
        {
            case "visit":
                attributes.Add(BuildAttribute("masterdata_table_name", DataType.Text));
                attributes.Add(BuildAttribute("masterdata_used", DataType.Guid));
                attributes.Add(BuildAttribute("inbound_flight", DataType.Guid));
                attributes.Add(BuildAttribute("outbound_flight", DataType.Guid));
                if (!entityType.AttributeTypes.Any(a => a.InternalName == "linked_due_to"))
                {
                    attributes.Add(BuildAttribute("linked_due_to", DataType.Text));
                }

                if (!entityType.AttributeTypes.Any(a => a.InternalName == "priority"))
                {
                    attributes.Add(BuildAttribute("priority", DataType.Integer));
                }

                break;
            case "ground_leg":
                attributes.Add(BuildAttribute("visit", DataType.Guid));
                break;
        }

        ;
        return attributes;
    }

    private static ValueAttributeType BuildAttribute(string name, DataType type) =>
        new() { InternalName = name, ValueDataType = type };
}