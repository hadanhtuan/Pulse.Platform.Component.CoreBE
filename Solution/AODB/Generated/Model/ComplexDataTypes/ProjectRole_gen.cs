using System;

namespace AODB.Generated.Model.ComplexDataTypes;

/// <summary>
/// Holder of a single role in the project
/// </summary>
/// <remarks>
/// This class is autogenerated, do not make changes here!
/// </remarks>
public class ProjectRole
{

    /// <summary>
    /// User holding the role.
    /// </summary>
    public Guid? Contact { get; set; }

    /// <summary>
    /// Used to display email in the UI. (Value contain user guid ID)
    /// </summary>
    public Guid? Email { get; set; }

    /// <summary>
    /// Name of role
    /// </summary>
    public string? Role { get; set; }

        public bool ValueIsEmpty()
        {
            return Contact == null && Email == null && Role == null;
        }
}