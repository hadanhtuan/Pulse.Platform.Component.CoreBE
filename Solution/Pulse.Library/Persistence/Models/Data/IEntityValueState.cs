using Database.Models.Data.Json;
using Database.Models.Schema;
using System;
using System.Collections.Generic;

namespace Database.Models.Data
{
    
    /// The state of an <see cref="EntityValue"/>. 
    
    /// <remarks>
    /// This is obtained as an adapter by passing the EntityValue object to
    /// <see cref="AdapterFactory.IAdapterManager.GetAdapter{TAdapter}(object?)"/>
    /// with <see cref="IEntityValueState"/> as the type parameter.
    /// </remarks>
    public interface IEntityValueState
    {
        
        /// Unique id for this <see cref="EntityValue"/>.
        
        Guid Id { get; }

        
        /// A reference to the raw message received in a given pipeline run and stored in the RawMessage Table. 
        
        /// <remarks>
        /// Can be used to build e.g. mapped message as part of pipeline run, but is not serialized to/from Json.
        /// The property will only have a value if 
        /// <see cref="AddRawMessage(RawMessage, IEnumerable{MessageEntityAttributeValue}?)"/>
        /// has been called in the current pipeline run.
        /// </remarks>
        RawMessage? PipelineMessage { get; set; }

        
        /// A reference to the message attributes received in a given pipeline run as part of adding a raw message. 
        
        /// <remarks>
        /// Can be used to build e.g. mapped message as part of pipeline run, but is not serialized to/from Json.
        /// The property will only have a value if 
        /// <see cref="AddRawMessage(RawMessage, IEnumerable{MessageEntityAttributeValue}?)"/>
        /// has been called in the current pipeline run.
        /// </remarks>
        IEnumerable<MessageEntityAttributeValue>? PipelineMessageAttributes { get; }

        
        /// This holds a reference to all raw messages that have modified an instance of an EntityValue.
        
        IEnumerable<Json.RawMessage> ModifiedBy { get; }

        
        /// This holds all identifiers which have been manually removed from the entity
        
        // Should this be dictionary/class such that we can map attribute -> value. Should this be updated in configuration? E.g. in rules, current message == man unmatch -> add identifier.
        // For now much better to do in receiver actions, but should probably be moved at some point as it is quite client specific (but so are some of the actions right now).
        IDictionary<string, HashSet<string>> InvalidIdentifiers { get; }

        
        /// Adds a new <see cref="Json.RawMessage"/> to the <see cref="ModifiedBy"/> collection as well as adding the
        /// <see cref="MessageEntityAttributeValue"/> values to the attributes of the <see cref="EntityValue"/>.
        
        /// <remarks>
        /// The <see cref="Json.RawMessage"/> added is exposed to later steps in the pipeline in <see cref="PipelineMessage"/>
        /// and the <see cref="MessageEntityAttributeValue"/> values are exposed in <see cref="PipelineMessageAttributes"/>.
        /// </remarks>
        /// <param name="dbRawMessage"></param>
        /// <param name="messageEAVs"></param>
        /// <returns>The <see cref="Json.RawMessage"/> created from <paramref name="dbRawMessage"/></returns>
        Json.RawMessage AddRawMessage(RawMessage dbRawMessage,
            IEnumerable<MessageEntityAttributeValue>? messageEAVs = null,
            bool isConversionFromRelationalModel = false);

        
        /// Removes the <see cref="Json.RawMessage"/> with the specified id from the <see cref="ModifiedBy"/> collection and
        /// optionally also removes the attributes related to the <see cref="Json.RawMessage"/> from this <see cref="IEntityValueState"/>.
        
        /// <remarks>
        /// Does not take into account that a <see cref="Json.RawMessage"/> can be applied to several
        /// <see cref="EntityValue"/>s, so this should not be used on Visit messages that also
        /// update Ground Legs.
        /// </remarks>
        /// <exception cref="InvalidOperationException">If there is no <see cref="Json.RawMessage"/> with the specified id.</exception>
        /// <param name="id"></param>
        /// <param name="removeAttributes"></param>
        void RemoveRawMessage(Guid id, bool removeAttributes);

        
        /// Reverts the most recently executed <see cref="ActionMessage"/> with the action name specified
        /// in the revertActionMessage.RevertAction.
        /// This reverts the attribute values affected by the reverted message.
        
        void RevertActionMessage(ActionMessage revertActionMessage);

        
        /// Deletes message values from attributes with the attributeInternalNames 
        /// as an effect of the given <see cref="RawMessage"/>. 
        /// Note that a message may indicate removal of an attribute that the message also contains a value
        /// for, in which case the new attribute value is kept as added.
        /// If the message is a SystemMessage then only values from the specified source are deleted.
        
        /// <param name="message"></param>
        /// <param name="attributeNames">Internal names of the attribute types to delete values for.</param>
        void DeleteMessageValuesFromAttributes(IEnumerable<string> attributeInternalNames, RawMessage message);

        
        /// Attribute values for this entity.
        
        IEnumerable<EntityAttribute> Attributes { get; }

        
        /// Removes all <see cref="EntityAttributeEnrichmentValue"/>s and <see cref="Alerts"/>that fulfill a given
        /// enrichmentContextFilter - if called with null, all <see cref="EntityAttributeEnrichmentValue"/>s 
        /// and <see cref="Alerts"/>are removed.
        
        /// <remarks>
        /// Used in the pipeline to clear out previous enrichments before re-enriching.
        /// The entity matched by a message should be cleared completely, while entities being enriched in context of
        /// another entity should only have the enrichments from that context removed.
        /// </remarks>
        void ResetEnrichmentContexts(Func<Guid, bool>? enrichmentContextFilter = null);

        
        /// Adds a new <see cref="Json.EntityAttributeValue"/> with an <see cref="EntityAttributeEnrichmentValue"/>.
        
        /// <remarks>
        /// For the <see cref="EnrichmentEntityAttributeValue.AodbSource"/> to be converted correctly to the Json model,
        /// the referenced attribute must have been already added. This can be achieved by always adding attributes 
        /// in the order they were created.
        /// /// </remarks>
        /// <param name="relEav"></param>
        /// <returns></returns>
        Json.EntityAttributeValue AddAttributeEnrichmentValue(EnrichmentEntityAttributeValue relEav);

        
        /// Adds a new <see cref="Json.EntityAttributeValue"/>.
        
        /// <remarks>
        /// For the <see cref="EnrichmentEntityAttributeValue.AodbSource"/> to be converted correctly to the Json model,
        /// the referenced attribute must have been already added. This can be achieved by always adding attributes 
        /// in the order they were created.
        /// </remarks>
        Json.EntityAttributeValue AddAttributeEnrichmentValue(
            string attributeInternalName,
            object? value,
            string causedBy,
            Guid enrichmentContextId,
            int priority,
            string? sourceDescription,
            MasterDataReference? masterDataReference,
            Json.EntityAttributeValue? aodbReference);

        
        /// Creates and returns a new <see cref="EntityAttribute"/> of a fixed attribute (not defined in the schema),
        /// without adding it to this <see cref="EntityValue"/>.
        
        EntityAttribute CreateFixedAttributeValue(AttributeType attributeType,
            object? value, Guid enrichmentContextId, int priority, int credibility);

        
        /// Returns the <see cref="EntityAttribute"/> containing the attribute values for the 
        /// attribute type with the given name.
        
        /// <param name="attributeTypeInternalName"></param>
        /// <returns><see cref="EntityAttribute"/></returns>
        EntityAttribute GetAttribute(string attributeTypeInternalName);

        
        /// Alerts that have at some point been raised by the enrichment business rules but are not active right now.
        
        IEnumerable<Json.EntityValueAlert> Alerts { get; }

        
        /// Returns the <see cref="AlertStatus.Active"/> alerts that are not <see cref="Json.EntityValueAlert.Hidden"/> and
        /// in case of duplicates (alerts with the same <see cref="Json.EntityValueAlert.AlertType"/>, 
        /// <see cref="Json.EntityValueAlert.AlertMessage"/>, <see cref="Json.EntityValueAlert.MasterDataSourceTable"/> and
        /// <see cref="Json.EntityValueAlert.MasterDataLookUpValue"/>) then it returns the one with the oldest
        /// <see cref="Json.EntityValueAlert.LastChangedState"/>.
        
        /// <returns></returns>
        IEnumerable<Json.EntityValueAlert> GetActiveAlerts();

        
        /// Adds a new alert to the EntityValue - if it already exists, the old alert will be activated and reused 
        /// (with the same <see cref="Json.EntityValueAlert.Id"/> and with <see cref="Json.EntityValueAlert.LastChangedState"/> set according
        /// to whether the alert was previously active or not).
        
        /// <param name="newAlert"></param>
        Json.EntityValueAlert AddAlert(Json.EntityValueAlert newAlert);

        
        /// Removes all values from <see cref="InvalidIdentifiers"/> which are currently present on the EntityValue.
        
        void RemoveInvalidIdentifiers();

        
        /// Adds or updates an entry in <see cref="InvalidIdentifiers"/> with one or more values.
        
        /// <param name="identifier">Key matching the entry to add or update</param>
        /// <param name="value">Value to add to the entry</param>
        void AddInvalidIdentifiers(string identifier, string value);

        
        /// The <see cref="EntityType"/> of this entity.
        
        EntityType EntityType { get; }
    }
}