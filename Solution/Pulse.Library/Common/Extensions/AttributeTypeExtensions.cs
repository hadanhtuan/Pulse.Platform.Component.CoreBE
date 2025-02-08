
using Pulse.Library.Core.Schema;

namespace Pulse.Library.Common.Extensions;
public static class AodbDataTypeExtensions
{
    public static string GetAttributeDataType(this AttributeType type)
    {

        if (type is ComplexAttributeType complexAttributeType)
        {
            return GetComplexDataType(complexAttributeType);
        }

        if (type.GetType() == typeof(AttributeType))
        {
            return GetSimpleDataType(type);
        }

        throw new NotImplementedException(
            "Creation of property (get/set) is not supported for Attribute Type " +
            $"'{type.GetType()}' (found on attribute with internal name ' {type.InternalName}').");
    }

    public static string GetSimpleDataType(AttributeType attributeType)
    {
        return attributeType.DataType switch
        {
            DataType.Text => "string",
            DataType.Integer => "int",
            DataType.Boolean => "bool",
            DataType.Duration => "TimeSpan",
            DataType.DateTime => "DateTime",
            DataType.Date => "DateTime",
            DataType.Dropdown => "string",
            DataType.Decimal => "decimal",
            DataType.Guid => "Guid",
            _ => "string",
        };

    }
    public static string GetComplexDataType(
        ComplexAttributeType complexAttributeType)
    {

        var complexTypeName = complexAttributeType.ComplexDataTypeInternalName.CamelCaseAndRemoveUnderscore();

        // only use class for complexAttributes, not use interface
        // write extension AddValue() for collection
        // ComplexAttributeCollection<ProjectRole> = ProjectRole[]
        if (complexAttributeType.IsCollection)
        {
            return $"ComplexAttributeCollection<{complexTypeName}>";
        }

        return complexTypeName;
    }

}
