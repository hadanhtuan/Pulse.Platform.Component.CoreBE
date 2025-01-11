using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Pulse.Library.Core.Persistence.EntityTypes;

public class Entity : Entity<Guid>, IEntity
{
}


/// Generic implementation that allows you to define the Type of the Primary Key in a derived interface/class.

/// <typeparam name="TKey"></typeparam>
public abstract class Entity<TKey> : IEntity<TKey> where TKey : struct
{
    [Column("id")] public virtual TKey Id { get; protected internal set; }

    [SuppressMessage(
        "Blocker Code Smell",
        "S3875:\"operator==\" should not be overloaded on reference types",
        Justification = "For Entity pattern, objects are equal if they refer to the same Id in the database.")]
    public static bool operator ==(Entity<TKey>? left, Entity<TKey>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TKey>? left, Entity<TKey>? right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((Entity<TKey>)obj);
    }

    protected virtual bool Equals(Entity<TKey> other)
    {
        return Id.Equals(other.Id);
    }
}