namespace Database.Models.Outbox
{
    /// <summary>
    /// Priority of a message that can be used to expedite some messages over others.
    /// Later enum values have higher int value and higher priority.
    /// </summary>
    public enum MessagePriority
    {
        Default,
        Priority
    }
}