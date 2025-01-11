namespace Service.Schema.Service.Validation.Modifications;


/// Base class for disruptive database modification.

public class DisruptiveDatabaseModification : IPossiblyDisruptive
{
    public bool CanContainDisruptiveDatabaseChanges() => true;
}