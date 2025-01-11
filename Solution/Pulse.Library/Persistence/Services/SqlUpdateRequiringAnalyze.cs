namespace Database.Services;


/// An SQL update that involves a change to indexes of a table and that
/// requires an ANALYZE to be performed on the table.

internal class SqlUpdateRequiringAnalyze : SqlUpdate
{
    public SqlUpdateRequiringAnalyze(string statement, string entityTypeName) : base(statement)
    {
        TableName = $"entity_{entityTypeName}";
    }

    
    /// Name of the table with index changes.
    
    public string TableName { get; }
}