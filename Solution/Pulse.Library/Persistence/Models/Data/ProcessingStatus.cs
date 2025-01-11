namespace Database.Models.Data
{
    
    /// Status of processing of an <see cref="EntityValue"/>.
    
    public enum ProcessingStatus
    {
        
        /// Status has not been set. Should be considered similar to <see cref="Processed"/>.
        
        Unspecified = 0,

        
        /// Processing of the EntityValue has been completed. 
        
        Processed = 1,

        
        /// Processing of the EntityValue has been requested.
        
        Requested = 2,

        
        /// The EntityValue has been enqueued for processing.
        
        Pending = 3
    }
}