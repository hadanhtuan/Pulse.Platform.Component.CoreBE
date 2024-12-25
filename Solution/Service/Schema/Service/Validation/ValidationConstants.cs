using System.Text.RegularExpressions;

namespace Service.Schema.Service.Validation;

internal static class ValidationConstants
{
    public static readonly Regex validInternalNameRegex = new("^[a-z_]+$");

    public static readonly string validInternalNameExplanation =
        "Only lowercase english letters a-z and the special character _ are allowed. " +
        "No spaces, numbers or other special characters are allowed";
}