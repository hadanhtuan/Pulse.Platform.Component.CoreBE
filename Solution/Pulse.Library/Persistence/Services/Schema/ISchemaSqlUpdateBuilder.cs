using Database.Models.Schema.Modifications;

namespace Database.Services.Schema;


/// Builds SqlUpdates for a schema modification.

internal interface ISchemaSqlUpdateBuilder : ISqlUpdateBuilder<ICompositeModification>
{
}