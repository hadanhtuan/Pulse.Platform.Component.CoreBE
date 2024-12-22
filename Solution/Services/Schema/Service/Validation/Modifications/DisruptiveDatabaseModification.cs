namespace Services.Schema.Service.Validation.Modifications;

/// <summary>
/// Base class for disruptive database modification.
/// </summary>
public class DisruptiveDatabaseModification : IPossiblyDisruptive
{
    public bool CanContainDisruptiveDatabaseChanges() => true;
}