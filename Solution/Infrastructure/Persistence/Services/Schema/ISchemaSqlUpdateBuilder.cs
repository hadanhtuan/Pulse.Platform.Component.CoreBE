using Database.Models.Schema.Modifications;

namespace Database.Services.Schema;

/// <summary>
/// Builds SqlUpdates for a schema modification.
/// </summary>
internal interface ISchemaSqlUpdateBuilder : ISqlUpdateBuilder<ICompositeModification>
{
}