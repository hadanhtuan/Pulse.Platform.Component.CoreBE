namespace Database.Models.Data
{
    /// <summary>
    /// "System" status of SystemMessages, which are set automatically based
    /// on the matching status of the message.
    /// </summary>
    public enum MessageSystemStatus
    {
        Matched,
        Unmatched,
        MatchAvailable,
        MatchedSkipped
    }
}
