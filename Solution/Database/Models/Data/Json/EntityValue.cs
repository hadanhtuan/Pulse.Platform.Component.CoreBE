using Pipeline.Steps.MessageReceiver;
using Database.Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json
{
    public class EntityValue : IEntityValueState
    {
        public EntityValue(EntityType entityType, Guid id)
        {
            this.Id = id;
            this.entityType = entityType;
        }

        /// <summary>
        /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
        /// in order to use <see cref="System.Text.Json.Serialization.ReferenceHandler.Preserve"/>.
        /// </summary>
        /// <remarks>DO NOT USE THIS FROM CODE: This constructor is only intended for the System.Text.Json.JsonSerializer.</remarks>
        [JsonConstructor]
        public EntityValue() { }

        private EntityType? entityType;

        [JsonIgnore]
        public EntityType EntityType
        {
            get => entityType!;
            internal set
            {
                entityType = value;
                foreach (EntityAttribute attribute in attributesByInternalName.Values)
                {
                    AttributeType attributeType
                        = entityType.AttributeTypes.First(at => at.InternalName == attribute.AttributeTypeInternalName);
                    attribute.AttributeType = attributeType;
                }
            }
        }

        /// <inheritdoc/>
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <inheritdoc/>
        [JsonIgnore]
        public Data.RawMessage? PipelineMessage { get; set; }

        /// <summary>
        /// A reference to the <see cref="RawMessage"/> build from the raw message received in a given pipeline run.
        /// </summary>
        /// <remarks>
        /// Can be used to read or update the json state of the pipeline message during processing.
        /// </remarks>
        [JsonIgnore]
        internal RawMessage? JsonPipelineMessage { get; private set; }

        /// <inheritdoc/>
        [JsonIgnore]
        public IEnumerable<MessageEntityAttributeValue>? PipelineMessageAttributes { get; private set; }

        private List<RawMessage> modifiedBy = new();

        /// <inheritdoc/>
        [JsonInclude]
        public IEnumerable<RawMessage> ModifiedBy
        {
            get { return modifiedBy; }
            internal set { modifiedBy = new(value); }
        }

        private Dictionary<string, HashSet<string>> invalidIdentifiers = new();

        /// <inheritdoc/>
        [JsonInclude]
        public IDictionary<string, HashSet<string>> InvalidIdentifiers
        {
            get { return invalidIdentifiers; }
            internal set { invalidIdentifiers = new(value); }
        }

        /// <inheritdoc/>
        public RawMessage AddRawMessage(Data.RawMessage dbRawMessage,
            IEnumerable<MessageEntityAttributeValue>? messageEAVs = null,
            bool isConversionFromRelationalModel = false)
        {
            Guid? matchedToOtherEntityValueId = null;
            if (dbRawMessage.EntityValue != null && dbRawMessage.EntityValue.Id != this.Id)
            {
                matchedToOtherEntityValueId = dbRawMessage.EntityValue.Id;
            }

            if (dbRawMessage.EntityValueModifiedId != null && dbRawMessage.EntityValueModifiedId != this.Id)
            {
                matchedToOtherEntityValueId = dbRawMessage.EntityValueModifiedId;
            }

            RawMessage jsonRawMessage = dbRawMessage switch
            {
                SystemMessage sMessage => new RawMessage(sMessage, matchedToOtherEntityValueId),
                ActionMessage aMessage => new RawMessage(aMessage, matchedToOtherEntityValueId),
                _ => throw new NotSupportedException($"RawMessage type '{dbRawMessage.GetType()}' not supported.")
            };

            UpdateModifiedBy(jsonRawMessage);

            if (!isConversionFromRelationalModel)
            {
                PipelineMessage = dbRawMessage;
                JsonPipelineMessage = jsonRawMessage;
                PipelineMessageAttributes = messageEAVs;
            }

            if (messageEAVs != null)
            {
                foreach (MessageEntityAttributeValue mValue in messageEAVs)
                {
                    EntityAttributeValue eav = AddAttributeMessageValue(
                        mValue.AttributeType.InternalName,
                        mValue.Id,
                        mValue.ToObjectValue(),
                        jsonRawMessage,
                        status: mValue.Status);
                    eav.Message!.ActionEffect = mValue.ActionEffect;
                    eav.Message!.AffectedByRawMsgId = mValue.AffectedBy?.Id;
                    if (isConversionFromRelationalModel)
                    {
                        eav.ValueOrMetadataLastChanged = mValue.ValueLastModified ?? mValue.RawMessage.Received;
                    }
                }
            }

            return jsonRawMessage;
        }

        private void UpdateModifiedBy(RawMessage jsonRawMessage)
        {
            if (modifiedBy.Exists(x => x.Id == jsonRawMessage.Id))
            {
                throw new InvalidOperationException(
                    $"EntityValue already contains a message with ID {jsonRawMessage.Id}");
            }

            if (jsonRawMessage.Reason.IsRecalculation())
            {
                modifiedBy = modifiedBy.Where(x => !x.Reason.IsRecalculation()).ToList();
            }
            else
            {
                modifiedBy.Add(jsonRawMessage);
            }
        }

        /// <inheritdoc/>
        public void RemoveRawMessage(Guid id, bool removeAttributes)
        {
            RawMessage msg = modifiedBy.Single(m => m.Id == id);
            modifiedBy.Remove(msg);

            if (removeAttributes)
            {
                foreach (EntityAttribute entityAttribute in Attributes)
                {
                    entityAttribute.RemoveRawMessageAttributes(msg);
                }
            }
        }

        /// <inheritdoc/>
        public void RevertActionMessage(ActionMessage revertActionMessage)
        {
            RawMessage? actionMsgToRevert = modifiedBy
                .OrderByDescending(m => m.Received)
                .FirstOrDefault(m => m.ActionMessage?.Status == ActionStatus.Executed &&
                                     m.ActionMessage?.ExecutingAction == revertActionMessage.RevertAction);

            if (actionMsgToRevert == null)
            {
                return;
            }

            actionMsgToRevert.ActionMessage!.Status = ActionStatus.Reverted;

            foreach (EntityAttribute attr in Attributes)
            {
                attr.RevertActionMessage(actionMsgToRevert.Id, revertActionMessage.Id);
            }
        }

        /// <inheritdoc/>
        public void DeleteMessageValuesFromAttributes(IEnumerable<string> attributeInternalNames,
            Data.RawMessage message)
        {
            var attributesToDelete =
                Attributes.Where(a => attributeInternalNames.Contains(a.AttributeTypeInternalName));

            foreach (var attribute in attributesToDelete)
            {
                attribute.DeleteMessageValues(message);
            }
        }

        private readonly Dictionary<string, EntityAttribute> attributesByInternalName = new();

        /// <inheritdoc/>
        [JsonInclude]
        public IEnumerable<EntityAttribute> Attributes
        {
            get => attributesByInternalName.Values;
            private set
            {
                attributesByInternalName.Clear();
                foreach (EntityAttribute a in value)
                {
                    attributesByInternalName.Add(a.AttributeTypeInternalName, a);
                }
            }
        }

        /// <inheritdoc/>
        public void ResetEnrichmentContexts(Func<Guid, bool>? enrichmentContextFilter = null)
        {
            foreach (EntityAttribute attribute in Attributes)
            {
                attribute.ClearEnrichmentAttributeValues(enrichmentContextFilter);
            }

            IEnumerable<EntityValueAlert> alertsToRemove = Alerts.Where(a => a.AlertStatus == AlertStatus.Active);
            if (enrichmentContextFilter != null)
            {
                alertsToRemove = alertsToRemove.Where(a => enrichmentContextFilter(a.EnrichmentContextId));
            }

            foreach (EntityValueAlert alert in alertsToRemove)
            {
                alert.SetAlertStatus(AlertStatus.Inactive);
            }
        }

        /// <summary>
        /// Adds a new <see cref="EntityAttributeValue"/> with the value and rawMessage reference.
        /// </summary>
        /// <remarks>
        /// For raw messages that represent system messages, only the most recent value per 
        /// <see cref="RawSystemMessage.Source"/> and <see cref="RawSystemMessage.MessageType"/> is kept.
        /// </remarks>
        private EntityAttributeValue AddAttributeMessageValue(string attributeTypeInternalName,
            Guid id,
            object? value,
            RawMessage rawMessage,
            EntityAttributeStatus status = EntityAttributeStatus.Active)
        {
            return GetAttribute(attributeTypeInternalName).AddMessageValue(id, value, rawMessage, status: status);
        }

        /// <inheritdoc/>
        public EntityAttributeValue AddAttributeEnrichmentValue(EnrichmentEntityAttributeValue relEav)
        {
            IEnumerable<MasterDataReference>? mdRefs = null;
            if (relEav.MasterDataSourceTable != null && relEav.MasterDataSourceId.HasValue)
            {
                mdRefs = new MasterDataReference[]
                {
                    new MasterDataReference(relEav.MasterDataSourceTable, relEav.MasterDataSourceId.Value)
                };
            }

            IEnumerable<EntityAttributeValue>? attrRefs = null;
            if (relEav.AodbSource != null)
            {
                EntityAttributeValue attrRef =
                    Attributes.SelectMany(a => a.Values.Union(a.PreviousValues))
                        .First(v => v.Id == relEav.AodbSource.Id);

                attrRefs = new EntityAttributeValue[] { attrRef };
            }

            EntityAttributeValue result = GetAttribute(relEav.AttributeType.InternalName).AddEnrichmentValue(
                relEav.Id,
                relEav.ToObjectValue(),
                relEav.Received,
                relEav.CausedBy,
                relEav.EnrichmentContext?.Id ?? this.Id,
                relEav.Priority,
                null,
                mdRefs,
                attrRefs,
                status: relEav.Status);

            if (relEav.ValueLastModified.HasValue)
            {
                result.ValueOrMetadataLastChanged = relEav.ValueLastModified.Value;
            }

            return result;
        }

        /// <inheritdoc/>
        public EntityAttributeValue AddAttributeEnrichmentValue(
            string attributeInternalName,
            object? value,
            string causedBy,
            Guid enrichmentContextId,
            int priority,
            string? sourceDescription,
            MasterDataReference? masterDataReference,
            EntityAttributeValue? aodbReference)
        {
            var attribute = GetAttribute(attributeInternalName);
            var eav = attribute.AddEnrichmentValue(
                Guid.NewGuid(),
                value,
                DateTime.UtcNow,
                causedBy,
                enrichmentContextId,
                priority,
                sourceDescription,
                masterDataReference != null ? new MasterDataReference[] { masterDataReference } : null,
                aodbReference != null ? new EntityAttributeValue[] { aodbReference } : null);

            EnsureActionMessageEavPrioritization(attribute, eav);

            return eav;
        }

        /// <summary>
        /// Makes sure that any Action (UI) value gets prioritized if it has just been received or if the Enrichment has not changed
        /// value or metadata since before the Action (UI) value was received.
        /// </summary>
        /// <remarks>
        /// Relies on the <see cref="EntityAttributeValue.ValueOrMetadataLastChanged"/> value to see if an 
        /// enrichment has kept its value/metadata since before the newest Action (UI) value for this attribute.
        /// </remarks>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        private void EnsureActionMessageEavPrioritization(EntityAttribute attribute, EntityAttributeValue value)
        {
            var actionValueToPrioritize = GetActionValueToPrioritizeOrNull(attribute, value);

            if (actionValueToPrioritize != null)
            {
                // Add an enrichment that prioritizes the Action (UI) value with the same priority and a newer
                // created date so that it is 'better' than the enrichment that was just added.
                attribute.AddEnrichmentValue(
                    Guid.NewGuid(),
                    actionValueToPrioritize.GetValue(),
                    DateTime.UtcNow,
                    "N/A (disabled)",
                    value.Enrichment!.EnrichmentContextId,
                    value.Enrichment!.Priority,
                    null,
                    null,
                    new EntityAttributeValue[] { actionValueToPrioritize });
            }

            // Everytime a new enrichment is created referencing UI input, ensure only relevant enrichments are kept
            if (actionValueToPrioritize != null || value.HasReferenceToEntityAttributeValueFromActionMessage)
            {
                // Ensure that there is only one Action/UI prioritization (the highest)
                List<EntityAttributeValue> irrelevantActionPrioritizations =
                    attribute.Values
                        .Where(a => a.HasReferenceToEntityAttributeValueFromActionMessage)
                        .Skip(1).ToList();
                irrelevantActionPrioritizations
                    .ForEach(attribute.Remove);
            }
        }

        /// <summary>
        /// Gets Action (UI) value to prioritize.
        /// Action Value should be prioritized if it has just been received or if the Enrichment has not changed
        /// value or metadata since before the Action value was received.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <returns>Action value to prioritize or null if none found</returns>
        private EntityAttributeValue? GetActionValueToPrioritizeOrNull(EntityAttribute attribute,
            EntityAttributeValue value)
        {
            if (value.Status != EntityAttributeStatus.Active
                // Do not add prioritized UI value if the enrichment value itself has lower priority than messages
                || value.Enrichment?.Priority < 0
                // Do not add prioritized UI value for enrichments of UI values
                || value.HasReferenceToEntityAttributeValueFromActionMessage)
            {
                return null;
            }

            EntityAttributeValue? newestActionValue = attribute.Values
                .FirstOrDefault(a => a.IsFromActionMessage && a.Status == EntityAttributeStatus.Active);

            var valueAddedTime = newestActionValue?.Message?.RawMessage.Persisted ?? newestActionValue?.Created;

            if (valueAddedTime >= value.ValueOrMetadataLastChanged ||
                newestActionValue?.Message?.RawMessage.Id == PipelineMessage?.Id)
            {
                return newestActionValue;
            }

            return null;
        }

        /// <inheritdoc/>
        public EntityAttribute CreateFixedAttributeValue(
            AttributeType attributeType,
            object? value,
            Guid enrichmentContextId,
            int priority,
            int credibility)
        {
            EntityAttribute ea = new(attributeType);
            ea.AddEnrichmentValue(
                Guid.NewGuid(),
                value,
                DateTime.UtcNow,
                null!,
                enrichmentContextId,
                priority,
                null,
                null,
                null);
            return ea;
        }

        /// <inheritdoc/>
        public EntityAttribute GetAttribute(string attributeTypeInternalName)
        {
            if (!attributesByInternalName.TryGetValue(attributeTypeInternalName, out EntityAttribute? entityAttribute))
            {
                AttributeType attributeType =
                    entityType!.AttributeTypes.First(at => at.InternalName == attributeTypeInternalName);
                entityAttribute = new(attributeType);
                attributesByInternalName.Add(attributeType.InternalName, entityAttribute);
            }

            return entityAttribute;
        }

        private List<EntityValueAlert> alerts = new();

        /// <inheritdoc/>
        [JsonInclude]
        public IEnumerable<EntityValueAlert> Alerts
        {
            get { return alerts; }
            internal set { alerts = new(value); }
        }

        /// <inheritdoc/>
        public IEnumerable<EntityValueAlert> GetActiveAlerts()
        {
            return Alerts
                .Where(a => a.AlertStatus == AlertStatus.Active && !a.Hidden)
                .OrderBy(a => a.LastChangedState)
                .GroupBy(a => new { a.AlertType, a.AlertMessage, a.MasterDataSourceTable, a.MasterDataLookUpValue })
                .Select(g => g.First());
        }

        /// <inheritdoc/>
        public EntityValueAlert AddAlert(EntityValueAlert newAlert)
        {
            EntityValueAlert? matchingExistingAlert = alerts
                .FirstOrDefault(existingAlert => existingAlert.AlertMessage == newAlert.AlertMessage
                                                 && existingAlert.AlertType == newAlert.AlertType
                                                 && existingAlert.MasterDataSourceTable ==
                                                 newAlert.MasterDataSourceTable
                                                 && existingAlert.MasterDataLookUpValue ==
                                                 newAlert.MasterDataLookUpValue
                                                 && existingAlert.EnrichmentContextId == newAlert.EnrichmentContextId);

            if (matchingExistingAlert != null)
            {
                if (matchingExistingAlert.AlertStatus == AlertStatus.Active &&
                    matchingExistingAlert.AlertLevel > newAlert.AlertLevel)
                {
                    // Do not add a duplicate alert with lower alert level - simply leave the old one
                    return matchingExistingAlert;
                }

                // Reactivate the old alert (if necessary) 
                matchingExistingAlert.SetAlertStatus(AlertStatus.Active);
                // Update relevant metadata based on the latest SetAlert information from business rules
                matchingExistingAlert.AlertLevel = newAlert.AlertLevel;
                matchingExistingAlert.AlertTypeDescription = newAlert.AlertTypeDescription;
                matchingExistingAlert.CausedBy = newAlert.CausedBy;
                matchingExistingAlert.Hidden = newAlert.Hidden;
                matchingExistingAlert.UserRole = newAlert.UserRole;
                return matchingExistingAlert;
            }
            else
            {
                alerts.Add(newAlert);
                return newAlert;
            }
        }

        /// <summary>
        /// Sets the current time on relevant json elements,
        /// such as <see cref="RawMessage.Persisted"/> property on <see cref="JsonPipelineMessage"/> 
        /// </summary>
        /// <returns>The DateTime value used</returns>
        internal void SetUpdateTime()
        {
            var updateTime = DateTime.UtcNow;

            if (JsonPipelineMessage != null)
            {
                JsonPipelineMessage.Persisted = updateTime;
            }
        }

        /// <summary>
        /// Flag whether the .Best property of each attribute has ever been persisted with a non-empty value.
        /// </summary>
        internal void SetAttributeHasHadPersistedValueIndicators()
        {
            foreach (var attribute in Attributes)
            {
                attribute.BestHasHadPersistedNonNullValue |= attribute.Best?.GetValue() != null;
            }
        }

        public void RemoveInvalidIdentifiers()
        {
            var identifierAttributes = invalidIdentifiers
                .Select(x => KeyValuePair.Create(x.Key, GetAttribute(x.Key).Best?.GetValue()?.ToString()));

            foreach (var kvp in identifierAttributes.Where(x => x.Value != null))
            {
                invalidIdentifiers[kvp.Key].Remove(kvp.Value!);
            }
        }

        public void AddInvalidIdentifiers(string identifier, string values)
        {
            if (!invalidIdentifiers.ContainsKey(identifier))
            {
                invalidIdentifiers[identifier] = new HashSet<string>();
            }

            invalidIdentifiers[identifier].Add(values);
        }
    }
}