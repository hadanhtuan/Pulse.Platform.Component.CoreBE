using Database.Models.Schema;
using Database.Models.Schema.Modifications;
using Database.Services.Schema.SqlBuilders.Helpers;

namespace Database.Services.Schema.SqlBuilders;

internal class AddedAttributeSqlUpdateBuilder : ISqlUpdateBuilder<IAddedAttribute>
{
    private readonly string sqlTemplate =
        @"ALTER TABLE public.entity_|entityName| ADD COLUMN IF NOT EXISTS ""|attributeName|"" |attributeType| NULL";

    public IEnumerable<SqlUpdate> ToSqlUpdates(IAddedAttribute modification)
    {
        var sql = sqlTemplate
            .Replace("|entityName|", modification.EntityType.InternalName)
            .Replace("|attributeName|", modification.AttributeType.InternalName)
            .Replace("|attributeType|", ToSqlType(modification.AttributeType));

        yield return new SqlUpdate(sql);
    }

    private static string ToSqlType(AttributeType attributeType)
    {
        if (attributeType is ValueAttributeType valueAttribute)
        {
            return valueAttribute.ValueDataType.ToSqlType();
        }

        if (attributeType is ComplexAttributeType)
        {
            return "JSONB";
        }

        throw new NotSupportedException("Unknown type of AttributeType");
    }
}