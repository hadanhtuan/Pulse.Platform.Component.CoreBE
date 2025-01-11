
using Domain.AdapterFactory;

namespace Database.Models.Data.Json
{
    internal class EntityValueStateProvider : IAdapterFactory<Data.EntityValue, IEntityValueState>
    {
        public IEntityValueState? GetAdapter(Data.EntityValue adaptee) => adaptee.JsonState;
    }
}