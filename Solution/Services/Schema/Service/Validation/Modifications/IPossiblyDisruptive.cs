namespace Services.Schema.Service.Validation.Modifications;

/// <summary>
/// Indicates a database modification that is possibly disruptive for the 
/// operation of the component.
/// </summary>
internal interface IPossiblyDisruptive
{
    /// <summary>
    /// Returns true if this modification results in the database changes that may 
    /// disrupt operation of the component.
    /// </summary>
    bool CanContainDisruptiveDatabaseChanges();
}