namespace Database.Models.Data
{
    
    /// "System" status of SystemMessages, which are set automatically based
    /// on the matching status of the message.
    
    public enum MessageSystemStatus
    {
        Matched,
        Unmatched,
        MatchAvailable,
        MatchedSkipped
    }
}
