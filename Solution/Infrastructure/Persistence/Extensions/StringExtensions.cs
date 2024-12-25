namespace Database.Extensions
{
    public static class StringExtensions
    {
        private static string complexAttrDateWithoutZuluPattern = "([0-9]{4}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}(\\.[0-9]*)?)(?=[^Z0-9.])";
        private static readonly System.Text.RegularExpressions.Regex complexAttrDateWithoutZuluRegex = new(complexAttrDateWithoutZuluPattern);
        private static string complexAttrDateWithoutZuluReplacement = "$1Z";

        /// <summary>
        /// Helper to compare relational vs json complex attribute values as the 'Z' seems to disappear on the old relational model.
        /// </summary>
        /// <param name="stringWithJsonDateValues"></param>
        /// <returns></returns>
        public static string AddZuluToNonZuluStrings(this string stringWithJsonDateValues)
        {
            return complexAttrDateWithoutZuluRegex.Replace(stringWithJsonDateValues, complexAttrDateWithoutZuluReplacement);
        }
    }
}
