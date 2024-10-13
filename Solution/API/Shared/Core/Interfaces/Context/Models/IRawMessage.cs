using System;

namespace API.Shared.Core.Interfaces.Context.Models
{
    public interface IRawMessage
    {
        DateTime Received { get; }
        string? Payload { get; }
        string? SystemSource { get; }
        string? SystemMessageType { get; }
        string? UserRole { get; }
        public string? ExecutingAction { get; }
        public InvokingReason Reason { get; }
    }
}
