using System;
using System.Collections.Generic;

namespace Pulse.Library.Core.Persistence.Entities;


/// Default IAggregateRootWithDomainEvents type that uses guids as primary keys.

public interface IAggregateRootWithDomainEvents : IAggregateRootWithDomainEvents<Guid>, IAggregateRoot
{
}


/// Generic interface that allows you to define the type of the primary key in a derived interface/class.

/// <typeparam name="TKey"></typeparam>
public interface IAggregateRootWithDomainEvents<out TKey> : IAggregateRoot<TKey> where TKey : struct
{
    IList<IDomainEvent> RaisedEvents { get; }
}