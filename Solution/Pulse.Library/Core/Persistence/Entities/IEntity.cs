namespace Pulse.Library.Core.Persistence.Entities;

public interface IEntity
{
}

public interface IEntity<out TKey> : IDomainObject where TKey : struct
{
    TKey Id { get; }
}