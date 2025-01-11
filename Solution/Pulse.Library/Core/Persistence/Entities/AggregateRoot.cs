using System.ComponentModel.DataAnnotations.Schema;

namespace Pulse.Library.Core.Persistence.Entities;


/// Default AggregateRoot type that uses guids as primary keys.

public abstract class AggregateRoot : AggregateRoot<Guid>, IAggregateRootWithDomainEvents
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public override Guid Id { get; protected internal set; }

    protected AggregateRoot()
    {
        Id = Guid.NewGuid();
    }
}


/// Generic implementation that allows you to define the type of the primary key in a derived interface/class.

/// <typeparam name="TKey"></typeparam>
public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRootWithDomainEvents<TKey> where TKey : struct
{
    private readonly IList<Func<IDomainEvent>> domainEventsToRaise = new List<Func<IDomainEvent>>();

    private IList<IDomainEvent>? raisedEvents = null;

    public IList<IDomainEvent> RaisedEvents
    {
        get
        {
            if (raisedEvents == null)
            {
                raisedEvents = domainEventsToRaise.Select(t => t.Invoke()).ToList();
            }

            return raisedEvents;
        }
    }

    protected void Raise(Func<IDomainEvent> @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        domainEventsToRaise.Add(@event);
    }
}