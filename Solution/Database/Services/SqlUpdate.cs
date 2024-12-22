using System;

namespace Database.Services;

/// <summary>
/// An SQL update statement with parameters.
/// </summary>
internal class SqlUpdate
{
    public SqlUpdate(string statement)
    {
        Statement = statement;
        Parameters = Array.Empty<object>();
    }

    public SqlUpdate(string statement, object[] parameters)
    {
        Statement = statement;
        Parameters = parameters;
    }

    /// <summary>
    /// SQL statement.
    /// </summary>
    public string Statement { get; }

    /// <summary>
    /// Parameter values for any placeholders in the statement.
    /// </summary>
    public object[] Parameters { get; }
}