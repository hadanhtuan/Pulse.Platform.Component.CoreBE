
namespace API.Shared.Core.Interfaces.Context.Matching
{
    /// <summary>
    /// Generic wrapper that can be used when an EntityInput has attributes that point to
    /// child EntityInput structures and you need the child Entities to be matched and 
    /// assigned either an Identitifer or mark to be created.
    /// Example are Flight Legs and Ground Legs on Visits.
    /// </summary>
    /// <typeparam name="TEntity">The specific EntityInput interface that should be 
    /// wrapped with matching information.
    /// </typeparam>
    public interface IMatchableEntityInput<out TEntityInput> where TEntityInput : IEntityInput
    {
        /// <summary>
        /// Can be assigned a value if the child EntityInput can be matched to an existing record.
        /// </summary>
        Guid? Identifier { get; set; }

        /// <summary>
        /// Marks whether a child EntityInput should be created along with the parent IEntityInput.
        /// </summary>
        bool ShouldBeCreated { get; set; }

        /// <summary>
        /// The actual EntityInput that is matched or should be created.
        /// </summary>
        TEntityInput Entity { get; }
    }
}
