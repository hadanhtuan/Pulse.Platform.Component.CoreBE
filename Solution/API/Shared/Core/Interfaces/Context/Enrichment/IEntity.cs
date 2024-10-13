using API.Shared.Core.Interfaces.Context.Models;
using System;
using System.Collections.Generic;

namespace API.Shared.Core.Interfaces.Context.Enrichment
{
    /// <summary>
    /// Entity being processed in the business rules interface.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The id of this entity.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Returns the set of alerts raised on this entity.
        /// </summary>
        ICollection<IEntityValueAlert> Alerts { get; }

        /// <summary>
        /// Adds an alert to this entity with a reference to a given Master data
        /// table and row.
        /// </summary>
        /// <param name="level">Alert level</param>
        /// <param name="type">Alert type</param>
        /// <param name="masterdataTable">Name of Master data table related to alert</param>
        /// <param name="lookUpValue">Value used during lookup in master data table</param>
        /// <returns>Created alert</returns>
        IAlert AddAlert(AlertLevel level, Enum type, string masterdataTable, string? lookUpValue);

        /// <summary>
        /// Adds an alert to this entity with a specific message in place of the
        /// alert description.
        /// </summary>
        /// <param name="level">Alert level</param>
        /// <param name="type">Alert type</param>
        /// <param name="message">Alert message</param>
        /// <returns>Created alert</returns>
        IAlert AddAlert(AlertLevel level, Enum type, string message);

        /// <summary>
        /// Adds an alert to this entity.
        /// </summary>
        /// <param name="level">Alert level</param>
        /// <param name="type">Alert type</param>
        /// <returns>Created alert</returns>
        IAlert AddAlert(AlertLevel level, Enum type);

        /// <summary>
        /// Returns a collection of entities that will be triggered for recalculation
        /// along with the reason for the recalculation.
        /// </summary>
        IReadOnlyCollection<IRecalculationTrigger> Recalculations { get; }

        /// <summary>
        /// Adds the entity with the identifier to the list of entities that should be triggered
        /// for recalculation after the current entity has finished processing.
        /// </summary>
        /// <param name="identifier">The internal identifier of the entity.</param>
        /// <param name="reason">The reason the entity should be recalculated.</param>
        /// <returns>The created recalculation</returns>
        IRecalculationTrigger AddRecalculation(Guid identifier, string reason);

        /// <summary>
        /// Adds a number of entities to the list of entities that should be triggered for
        /// recalculation after the current entity has finished processing.
        /// </summary>
        /// <param name="identifiers">A list of internal identifiers.</param>
        /// <param name="reason">The reason why all the entities should be recalculated.</param>
        /// <returns>The recalcaulation triggers that were added</returns>
        IEnumerable<IRecalculationTrigger> AddRecalculation(IEnumerable<Guid> identifiers, string reason);

        IRawMessage? LastModifiedBy { get; }
    }
}