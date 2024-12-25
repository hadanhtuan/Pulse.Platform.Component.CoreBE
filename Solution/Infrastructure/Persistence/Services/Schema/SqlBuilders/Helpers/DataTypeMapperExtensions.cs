using Database.Models.Schema;
using System;

namespace Database.Services.Schema.SqlBuilders.Helpers;

internal static class DataTypeMapperExtensions
{
    public static string ToSqlType(this DataType valueAttributeType) =>
        valueAttributeType switch
        {
            DataType.Integer => "INT4",
            DataType.Text => "TEXT",
            DataType.DateTime => "TIMESTAMP",
            DataType.Date => "TIMESTAMP",
            DataType.Duration => "INTERVAL SECOND",
            DataType.Dropdown => "TEXT",
            DataType.Boolean => "BOOLEAN",
            DataType.Decimal => "NUMERIC",
            DataType.Guid => "UUID",
            DataType.TextArea => "TEXT",
            _ => throw new ArgumentOutOfRangeException(nameof(valueAttributeType), valueAttributeType, null)
        };
}