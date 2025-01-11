namespace Database.Models.Schema
{
    
    /// Definition of an entity type in the AODB that is received, stored and published by the 
    
    /// <example>
    /// Examples are Flight legs, Ground legs, Visits, Operational events.
    /// </example>
    public class RootEntityType : EntityType
    {
        
        /// Defines the retention policy.
        
        public virtual Retention Retention { get; set; } = new Retention();
    }
}