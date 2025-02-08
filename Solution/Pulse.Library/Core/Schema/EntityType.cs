using System.ComponentModel.DataAnnotations;

namespace Pulse.Library.Core.Schema;


/// Definition of an ENTITY TYPE(defined in init.json) in the AODB.
public class EntityType
{
    public string InternalName { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string? Description { get; set; }

    public Status Status { get; set; }

    public IEnumerable<Index> Indexes { get; set; } = new List<Index>();

    public IEnumerable<UniqueConstraint> UniqueConstraints { get; set; } = new List<UniqueConstraint>();

    public virtual IEnumerable<AttributeType> AttributeTypes { get; set; } = new List<AttributeType>();


    public virtual IEnumerable<ComplexAttributeType> ComplexAttributeTypes { get; set; } = new List<ComplexAttributeType>();

    public virtual IEnumerable<Action> Actions { get; set; } = new List<Action>();
}