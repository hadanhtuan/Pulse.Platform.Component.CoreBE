
namespace Database.Models.Schema
{
    /// <summary>
    /// Enumeration used when checking for access to component actions via the <see cref="PermissionClientLibrary"/>.
    /// The individual enumeration values corresponds to the string value of the action in the Permission Component.
    /// </summary>
    public enum ComponentActions
    {
        /// <summary>
        /// Ability to read schema information from API
        /// </summary>
        ReadSchema,
        /// <summary>
        /// Ability to update schema information through API
        /// </summary>
        UpdateSchema,
        /// <summary>
        /// Ability to update the assemblies used to execute business rules in the pipeline.
        /// </summary>
        UpdateAssembly,
        /// <summary>
        /// Ability to manually update a received message.
        /// </summary>
        UpdateMessage,
        /// <summary>
        /// Ability to manually match a received message.
        /// </summary>
        MatchMessage,
        /// <summary>
        /// Ability to manually request specific entities to be recalculated.
        /// </summary>
        RequestRecalculations,
        /// <summary>
        /// Ability to read all alerts.
        /// </summary>
        ReadAlerts
    }
}
