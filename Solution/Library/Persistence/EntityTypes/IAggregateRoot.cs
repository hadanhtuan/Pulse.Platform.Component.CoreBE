namespace Library.Persistence.EntityTypes;

/// <summary>
/// Default IAggregateRoot type that uses guids as primary keys.
/// </summary>
public interface IAggregateRoot : IAggregateRoot<Guid>, IEntity
{
}

/// <summary>
/// Generic interface that allows you to define the type of the primary key in a derived interface/class.
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IAggregateRoot<out TKey> : IEntity<TKey> where TKey : struct
{
}