namespace API.Shared.Core.Interfaces.Context.Models
{
    /// <summary>
    /// Alert raised in the context of an entity.
    /// </summary>
    public interface IEntityValueAlert : IAlert
    {
        /// <summary>
        /// The alert message that is potentially overwritten by the Master data lookup in 
        /// the alert enrichment pipeline
        /// </summary>
        public new string AlertMessage { get; set; }

        /// <summary>
        /// The alert type description that is potentially overwritten by the Master data lookup in
        /// the alert enrichment pipeline
        /// </summary>
        public new string AlertTypeDescription { get; set; }

        /// <summary>
        /// The alert level that is potentially overwritten by the Master data lookup in 
        /// the alert enrichment pipeline
        /// </summary>
        public new AlertLevel AlertLevel { get; set; }
    }
}