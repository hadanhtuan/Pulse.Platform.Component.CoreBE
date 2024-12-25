using System;

namespace Database.Services.Schema.SqlBuilders;

/// <summary>
/// Thrown if 
/// </summary>
[Serializable]
public class SqlUpdateBuilderTypeNotFoundException : Exception
{
    public SqlUpdateBuilderTypeNotFoundException(string message) : base(message) { }

    protected SqlUpdateBuilderTypeNotFoundException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
}