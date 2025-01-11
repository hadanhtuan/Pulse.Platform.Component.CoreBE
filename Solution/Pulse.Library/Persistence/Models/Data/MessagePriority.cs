namespace Database.Models.Outbox
{
    
    /// Priority of a message that can be used to expedite some messages over others.
    /// Later enum values have higher int value and higher priority.
    
    public enum MessagePriority
    {
        Default,
        Priority
    }
}