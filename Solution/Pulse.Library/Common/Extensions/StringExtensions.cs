using System.Globalization;

namespace Pulse.Library.Common.Extensions;

public static class StringExtensions
{
    public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1)
            };

    public static string CamelCaseAndRemoveUnderscore(this string input) =>
input switch
{
    null => throw new ArgumentNullException(nameof(input)),
    "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
    _ => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(input).Replace("_", string.Empty)
};
}
