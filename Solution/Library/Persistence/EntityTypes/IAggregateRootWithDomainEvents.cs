using System;
using System.Collections.Generic;

namespace Library.Persistence.EntityTypes;

/// <summary>
/// Default IAggregateRootWithDomainEvents type that uses guids as primary keys.
/// </summary>
public interface IAggregateRootWithDomainEvents : IAggregateRootWithDomainEvents<Guid>, IAggregateRoot
{
}

/// <summary>
/// Generic interface that allows you to define the type of the primary key in a derived interface/class.
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IAggregateRootWithDomainEvents<out TKey> : IAggregateRoot<TKey> where TKey : struct
{
    IList<IDomainEvent> RaisedEvents { get; }
}