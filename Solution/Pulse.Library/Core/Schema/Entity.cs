using System.ComponentModel.DataAnnotations.Schema;

namespace Pulse.Library.Core.Schema;

public class Entity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; protected internal set; }

    protected Entity() => this.Id = Guid.NewGuid();
}