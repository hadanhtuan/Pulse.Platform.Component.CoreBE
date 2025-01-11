namespace Pulse.Library.Core.Persistence.EntityTypes;


/// Default IAggregateRoot type that uses guids as primary keys.

public interface IAggregateRoot : IAggregateRoot<Guid>, IEntity
{
}


/// Generic interface that allows you to define the type of the primary key in a derived interface/class.

/// <typeparam name="TKey"></typeparam>
public interface IAggregateRoot<out TKey> : IEntity<TKey> where TKey : struct
{
}