using Database.Models.Schema.Modifications;
using System.Collections.Generic;
using System.Linq;

namespace Services.Schema.Service.Validation.Modifications;

public class CompositeModification : ICompositeModification, IPossiblyDisruptive
{
    private readonly List<IModification> modifications = new();

    public IEnumerable<IModification> Modifications => modifications;

    public bool CanContainDisruptiveDatabaseChanges() =>
        modifications.OfType<IPossiblyDisruptive>()
            .Any(x => x.CanContainDisruptiveDatabaseChanges());

    internal void AddIfNotNull(IModification? modification)
    {
        if (modification != null)
        {
            modifications.Add(modification);
        }
    }

    internal void AddRange(IEnumerable<IModification> modifications) =>
        this.modifications.AddRange(modifications);
}