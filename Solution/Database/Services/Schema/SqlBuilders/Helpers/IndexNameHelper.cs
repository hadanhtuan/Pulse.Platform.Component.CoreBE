using System;
using System.Text.RegularExpressions;

namespace Database.Services.Schema.SqlBuilders.Helpers;

internal class IndexNameHelper : IIndexNameHelper
{
    public string IndexNameFrom(string entityInternalName, string indexColumns)
    {
        static int GetDeterministicHashCode(string str)
        {
            unchecked
            {
                var hash1 = (5381 << 16) + 5381;
                var hash2 = hash1;

                for (var i = 0; i < str.Length; i += 2)
                {
                    hash1 = (hash1 << 5) + hash1 ^ str[i];
                    if (i == str.Length - 1)
                    {
                        break;
                    }

                    hash2 = (hash2 << 5) + hash2 ^ str[i + 1];
                }

                return hash1 + hash2 * 1566083941;
            }
        }

        string attributeNames = Regex.Replace(indexColumns, @"\s+", "").Replace(',', '_');

        if (entityInternalName.Length + indexColumns.Length < 61)
        {
            return $"{entityInternalName}_{attributeNames}_i";
        }

        return
            $"{OfMaxLength(entityInternalName, 40)}" +
            $"_{OfMaxLength(Math.Abs(GetDeterministicHashCode(attributeNames)).ToString(), 20)}" +
            $"_i";
    }

    private static string OfMaxLength(string s, int length)
    {
        return s.Substring(0, Math.Min(length, s.Length));
    }
}