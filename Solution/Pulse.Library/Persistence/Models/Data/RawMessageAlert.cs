namespace Database.Models.Data
{
    
    /// Alert on a message, typically on problems matching the message
    /// to an entity.
    
    public class RawMessageAlert : Alert
    {
        
        /// The message relating to this alert.
        
        public virtual RawMessage RawMessageWithAlert { get; set; } = null!;
    }
}