using Pulse.Library.Core.Schema;

namespace Pulse.Library.Services.Schema.Validator;

public interface ISchemaVersionValidator
{
    ICompositeModification ValidateChangeFrom(SchemaVersion newSchema, SchemaVersion oldSchema);
}