namespace Database.Models.Data
{
    /// <summary>
    /// Alert on a message, typically on problems matching the message
    /// to an entity.
    /// </summary>
    public class RawMessageAlert : Alert
    {
        /// <summary>
        /// The message relating to this alert.
        /// </summary>
        public virtual RawMessage RawMessageWithAlert { get; set; } = null!;
    }
}