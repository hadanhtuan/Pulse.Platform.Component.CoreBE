namespace Database.Services.Schema.SqlBuilders.Helpers;

internal interface IIndexNameHelper
{
    /// <summary>
    /// Returns a unique name for an index, which conforms to the length 
    /// requirements and allowed characters, based on the given input.
    /// </summary>
    /// <param name="entityInternalName"></param>
    /// <param name="indexColumns"></param>
    /// <returns></returns>
    string IndexNameFrom(string entityInternalName, string indexColumns);
}