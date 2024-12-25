namespace Database.Services;

/// <summary>
/// An SQL update that involves a change to indexes of a table and that
/// requires an ANALYZE to be performed on the table.
/// </summary>
internal class SqlUpdateRequiringAnalyze : SqlUpdate
{
    public SqlUpdateRequiringAnalyze(string statement, string entityTypeName) : base(statement)
    {
        TableName = $"entity_{entityTypeName}";
    }

    /// <summary>
    /// Name of the table with index changes.
    /// </summary>
    public string TableName { get; }
}