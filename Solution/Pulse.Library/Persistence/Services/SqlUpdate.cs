using System;

namespace Database.Services;


/// An SQL update statement with parameters.

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

    
    /// SQL statement.
    
    public string Statement { get; }

    
    /// Parameter values for any placeholders in the statement.
    
    public object[] Parameters { get; }
}