using System;
using System.Collections.Generic;

namespace API.Shared.Core.Interfaces.Rules.Matching
{
    /// <summary>
    /// Contains an Entity that has come out as a result of running mapping + matching.
    /// This can either be a root entity (in which case it will be extended to IAdapterHandlerResult)
    /// or it can be a sub entity that another entity contains in either <see cref="AttributeEntityValuePairs"/>
    /// or in <see cref="AttributeEntityValueCollectionPairs"/>.
    /// </summary>
    public interface IAdapterHandlerEntityResult
    {
        /// <summary>
        /// If the IAdapterHandlerEntityResult has been matched, this holds the identifier of 
        /// the record it was matched to.
        /// </summary>
        Guid? Identifier { get; }

        /// <summary>
        /// Internal name (in the AODB Schema) of the entity type of this IAdapterHandlerEntityResult
        /// </summary>
        string EntityType { get; }

        /// <summary>
        /// Contains all the mapped attribute/value pairs of the received message 
        /// (for simple, schema-based attributes).
        /// If values are mapped to related sub-entities, these will be stored in either 
        /// <see cref="AttributeEntityValuePairs"/> or in <see cref="AttributeEntityValueCollectionPairs"/>.
        /// </summary>
        IDictionary<string, object?> 
            AttributeValuePairs { get; }

        /// <summary>
        /// Contains non-simple attributes that are independant, related EntityValues that should be updated/created
        /// (e.g. the ArrivalFlightLeg of a Visit).
        /// </summary>
        IDictionary<string, IAdapterHandlerEntityResult> 
            AttributeEntityValuePairs { get; }

        /// <summary>
        /// Contains non-simple attributes that are independant, related lists of EntityValues that should 
        /// be updated/created (e.g. the Ground Legs of a Visit).
        /// </summary>
        IDictionary<string, IEnumerable<IAdapterHandlerEntityResult>> 
            AttributeEntityValueCollectionPairs { get; }

        /// <summary>
        /// Contains the internal names of all attributes that should be removed in the context
        /// of the message source.
        /// </summary>
        IEnumerable<string>
            ResetAttributeValues { get; }
    }
}
