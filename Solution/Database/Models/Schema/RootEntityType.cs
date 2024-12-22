namespace Database.Models.Schema
{
    /// <summary>
    /// Definition of an entity type in the AODB that is received, stored and published by the 
    /// </summary>
    /// <example>
    /// Examples are Flight legs, Ground legs, Visits, Operational events.
    /// </example>
    public class RootEntityType : EntityType
    {
        /// <summary>
        /// Defines the retention policy.
        /// </summary>
        public virtual Retention Retention { get; set; } = new Retention();
    }
}