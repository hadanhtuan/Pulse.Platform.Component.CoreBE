using Database.Models.Schema;

namespace Services.Schema.Service.Validation;

/// <summary>
/// Validates a schema for standard elements.
/// </summary>
public interface IStandardSchemaValidator
{
    /// <summary>
    /// Throws an exception if the given schema does not contain all standard entity types and
    /// all standard attribute types.
    /// </summary>
    /// <param name="schema">SchemaVersion to be validated</param>
    void ValidateStandardElements(SchemaVersion schema);
}