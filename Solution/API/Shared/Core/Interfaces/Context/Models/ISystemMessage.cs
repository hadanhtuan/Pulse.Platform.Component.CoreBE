using System;
using System.Collections.Generic;

namespace API.Shared.Core.Interfaces.Context.Models
{
    /// <summary>
    /// Message received from a system source.
    /// </summary>
    public interface ISystemMessage
    {
        string Source { get; }
        string MessageType { get; }
        DateTime Received { get; }
        IReadOnlyDictionary<string,object> AttributeValuePairs { get; }
    }
}