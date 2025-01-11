using Pulse.Library.Persistence.Models.Schema;

namespace Pulse.Library.CLI.Generator.Models;

/// Definition of an entity action(API) in the AODB.
public class ActionView
{
    /// API Route, Example: bulk_update, create_questionnaire
    public string InternalName { get; set; } = null!;

    /// Short-form name of the action as displayed in a user interface.
    public string? ShortDisplayName { get; set; }
        
    /// Long-form name of the action as displayed in a user interface.
    public string? LongDisplayName { get; set; }
        
    /// Description of the action.
    public string? Description { get; set; }

    /// The status of the attribute type.
    public Status? Status { get; set; }
}