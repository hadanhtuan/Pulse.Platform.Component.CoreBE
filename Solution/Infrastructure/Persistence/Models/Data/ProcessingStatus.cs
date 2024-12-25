namespace Database.Models.Data
{
    /// <summary>
    /// Status of processing of an <see cref="EntityValue"/>.
    /// </summary>
    public enum ProcessingStatus
    {
        /// <summary>
        /// Status has not been set. Should be considered similar to <see cref="Processed"/>.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Processing of the EntityValue has been completed. 
        /// </summary>
        Processed = 1,

        /// <summary>
        /// Processing of the EntityValue has been requested.
        /// </summary>
        Requested = 2,

        /// <summary>
        /// The EntityValue has been enqueued for processing.
        /// </summary>
        Pending = 3
    }
}