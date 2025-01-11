namespace Service.Schema.Service.Validation.Modifications;


/// Indicates a database modification that is possibly disruptive for the 
/// operation of the component.

internal interface IPossiblyDisruptive
{
    
    /// Returns true if this modification results in the database changes that may 
    /// disrupt operation of the component.
    
    bool CanContainDisruptiveDatabaseChanges();
}