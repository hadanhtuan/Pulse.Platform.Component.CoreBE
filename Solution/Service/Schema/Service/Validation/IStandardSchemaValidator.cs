using Database.Models.Schema;

namespace Service.Schema.Service.Validation;


/// Validates a schema for standard elements.

public interface IStandardSchemaValidator
{
    
    /// Throws an exception if the given schema does not contain all standard entity types and
    /// all standard attribute types.
    
    /// <param name="schema">SchemaVersion to be validated</param>
    void ValidateStandardElements(SchemaVersion schema);
}