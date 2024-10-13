using NpgsqlTypes;

namespace API.Shared.Core.Interfaces.Context.MasterData
{
    public interface IEntity
    {
        public Guid Id { get; }

        public NpgsqlRange<DateTime> Registration { get; }

        public NpgsqlRange<DateTime> Valid { get; }

        public string? EntryName { get; }
    }
}