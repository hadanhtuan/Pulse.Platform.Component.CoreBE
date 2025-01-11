
namespace Database.Models.Schema
{
    
    /// Enumeration used when checking for access to component actions via the <see cref="PermissionClientLibrary"/>.
    /// The individual enumeration values corresponds to the string value of the action in the Permission Component.
    
    public enum ComponentActions
    {
        
        /// Ability to read schema information from API
        
        ReadSchema,
        
        /// Ability to update schema information through API
        
        UpdateSchema,
        
        /// Ability to update the assemblies used to execute business rules in the pipeline.
        
        UpdateAssembly,
        
        /// Ability to manually update a received message.
        
        UpdateMessage,
        
        /// Ability to manually match a received message.
        
        MatchMessage,
        
        /// Ability to manually request specific entities to be recalculated.
        
        RequestRecalculations,
        
        /// Ability to read all alerts.
        
        ReadAlerts
    }
}
