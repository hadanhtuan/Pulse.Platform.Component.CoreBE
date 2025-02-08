namespace Pulse.Library.Core.Schema;

/// Definition of an entity action(API) in the AODB.
public class Action
{
    /// API Route, Example: bulk_update, create_questionnaire
    public string InternalName { get; set; } = null!;

    public string? DisplayName { get; set; }

    /// Description of the action.
    public string? Description { get; set; }

    /// The status of the attribute type.
    public Status? Status { get; set; }
}