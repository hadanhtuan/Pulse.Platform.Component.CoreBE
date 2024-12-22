namespace Library.Persistence.EntityTypes;

public interface IEntity
{
}

public interface IEntity<out TKey> : IDomainObject where TKey : struct
{
    TKey Id { get; }
}