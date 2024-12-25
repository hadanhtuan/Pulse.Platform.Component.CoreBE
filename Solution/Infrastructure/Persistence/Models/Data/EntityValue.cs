using Library.Persistence.EntityTypes;
using Database.Data;
using Database.Extensions;
using Database.Models.Schema;
using Database.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Database.Models.Data
{
    /// <summary>
    /// A unique entity of some type (e.g., a particular flight leg).
    /// </summary>
    public class EntityValue : AggregateRoot
    {
        private ILogger? logger = null;
        private ISchemaProvider? schemaProvider = null;

        private readonly bool isNewlyCreated = false;

        public EntityValue()
        {
            isNewlyCreated = true;
        }

        public EntityValue(SchemaContext context)
        {
            AssignSchemaContext(context);
        }

        /// <summary>
        /// Should be used if default constructor is used to create a new EntityValue (i.e. it is not loaded via Entity Framework).
        /// Will ensure that loggers etc. are assigned so that JSON mode logging works.
        /// </summary>
        /// <param name="schemaContext"></param>
        public void AssignSchemaContext(SchemaContext? schemaContext)
        {
            logger = schemaContext?.GetSystemLoggerFactory()?.GetLogger<EntityValue>();
            schemaProvider = schemaContext?.GetSchemaProvider();
        }

        /// <summary>
        /// The id of the latest updating transaction - PostgreSQL system column 
        /// used as ConcurrencyToken to prevent parallel updates of the same object.
        /// </summary>
        public uint xmin { get; set; }

        public string BusinessRulesVersionUsed { get; set; } = string.Empty;

        /// <summary>
        /// The time this EntityValue was last processed.
        /// </summary>
        public DateTime LastModified { get; set; }

        #region Recalculation processing state

        /// <summary>
        /// The processing status of this EntityValue.
        /// </summary>
        [Obsolete("Replaced by standalone entity EntityValueProcessingState")]
        public ProcessingStatus ProcessingStatus { get; set; } = ProcessingStatus.Unspecified;

        /// <summary>
        /// The time of the last update of the processing status. 
        /// </summary>
        /// <remarks>
        /// This will always be set when an EntityValue has been processed, but may be <see langword="null"/>
        /// for entities that have not been updated since the property was added.
        /// </remarks>
        [Obsolete("Replaced by standalone entity EntityValueProcessingState")]
        public DateTime? ProcessingStatusLastUpdated { get; set; }

        /// <summary>
        /// The reasons for processing the EntityValue. 
        /// </summary>
        [Obsolete("Replaced by standalone entity EntityValueProcessingState")]
        public ProcessingReasons ProcessingReasons { get; set; }

        /// <summary>
        /// The reasons provided from a rule triggering recalculation of this EntityValue.
        /// String containing possibly multiple reasons separated by "||".
        /// Also contains reasons from <see cref="ScheduledRecalculationReason"/> when the processing
        /// has been requested or is pending because of scheduled recalculation.
        /// </summary>
        [Obsolete("Replaced by standalone entity EntityValueProcessingState")]
        public string? ProcessingRuleTriggerReasons { get; set; }

        /// <summary>
        /// The future time in UTC when this entity should be triggered for recalculation
        /// </summary>
        public DateTime? ScheduledRecalculation { get; set; }

        /// <summary>
        /// The reason why this entity has been scheduled for future recalculation
        /// </summary>
        public string? ScheduledRecalculationReason { get; set; }

        #endregion

        private bool hasChanged = false;

        /// <summary>
        /// This is set to true if the EntityValue is changed during the current pipeline run.
        /// The value is not persisted and defaults to false, but also auto-detects attribute changes
        /// to Json attributes.
        /// </summary>
        public bool HasChanged
        {
            get
            {
                if (hasChanged)
                {
                    return true;
                }

                IEnumerable<Json.EntityAttribute> jsonAttributesWithChanges = JsonState.Attributes.Where(
                    attr =>
                        (attr.Best != null && attr.PreviousBest == null) ||
                        (attr.Best == null && attr.PreviousBest != null) ||
                        (attr.Best != null && attr.PreviousBest != null &&
                         !attr.Best.IsEquivalentTo(attr.PreviousBest)));

                if (jsonAttributesWithChanges.Any())
                {
                    return true;
                }

                return false;
            }
            set
            {
                hasChanged = value;
            }
        }

        // This holds a reference to all raw messages that have ever modified an instance of an EntityValue
        // The reference is in this class instead of the RawMessage due to EF Core configuration to keep
        // LastModifiedBY and ModifiedBy as separate relations.
        [Obsolete(
            "Included to be able to read old rows from the database. For all updates to data, the new JsonState storage model should be used instead.")]
        public virtual ICollection<RawMessage> ModifiedBy
        {
            get
            {
                LogErrorIfUsedInJsonMode("ModifiedBy was accessed in JSON-only mode");
                return _modifiedBy;
            }
            set
            {
                LogErrorIfUsedInJsonMode("ModifiedBy was assigned in JSON-only mode");
                _modifiedBy = value;
            }
        }

        private ICollection<RawMessage> _modifiedBy = new Collection<RawMessage>();

        /// <summary>
        /// Attributes values for this entity.
        /// </summary>
        [Obsolete(
            "Included to be able to read old rows from the database. For all updates to data, the new JsonState storage model should be used instead.")]
        public virtual ICollection<EntityAttributeValue> EntityAttributeValues
        {
            get
            {
                LogErrorIfUsedInJsonMode("EntityAttributeValues was accessed in JSON-only mode");
                return _entityAttributeValues;
            }
            set
            {
                LogErrorIfUsedInJsonMode("EntityAttributeValues was assigned in JSON-only mode");
                _entityAttributeValues = value;
            }
        }

        private ICollection<EntityAttributeValue> _entityAttributeValues = new Collection<EntityAttributeValue>();

        /// <summary>
        /// Alerts on this entity.
        /// </summary>
        [Obsolete(
            "Included to be able to read old rows from the database. For all updates to data, the new JsonState storage model should be used instead.")]
        public virtual ICollection<EntityValueAlert> Alerts
        {
            get
            {
                LogErrorIfUsedInJsonMode("Alerts was accessed in JSON-only mode");
                return _alerts;
            }
        }

        private readonly ICollection<EntityValueAlert> _alerts = new Collection<EntityValueAlert>();

        /// <summary>
        /// Definition of the type of this entity.
        /// </summary>
        public virtual RootEntityType EntityType { get; set; }

        public Guid EntityTypeId { get; set; }

        /// <summary>
        /// Obtains the <see cref="RootEntityType"/> of this EntityValue based on <see cref="EntityTypeId"/>, using
        /// a cached <see cref="SchemaVersion"/>,
        /// </summary>
        /// <remarks>
        /// Note, that this <see cref="RootEntityType"/> will belong to a different Entity Framework DbContext.
        /// </remarks>
        /// <returns></returns>
        public RootEntityType GetCachedEntityType()
        {
            if (schemaProvider != null)
            {
                return schemaProvider.SchemaVersion!.EntityTypes.OfType<RootEntityType>()
                    .First(eType => eType.Id == EntityTypeId);
            }

            return EntityType;
        }

        /// <summary>
        /// The points in time used for looking up masterdata for enrichments.
        /// </summary>
        public virtual ICollection<EntityValueMasterDataLookup> PointInTimes { get; } =
            new Collection<EntityValueMasterDataLookup>();

        /// <summary>
        /// Number of times the EntityValue has been updated.
        /// </summary>
        public int Revision { get; set; }

        /// <summary>
        /// Hash of the last published output message for this entity.
        /// </summary>
        public virtual EntityValueHash? LastHash { get; set; }

        /// <summary>
        /// Json document that contains all the detailed state of the EntityValue.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Exposed as a string to support custom serialization/deserialization, in particular allowing
        /// use of <see cref="System.Text.Json.Serialization.ReferenceHandler.Preserve"/>.
        /// </para>
        /// <para>
        /// Mapped to a database column of type "json" (not "jsonb") in order to support
        /// /// </para>
        /// </remarks>
        public string? JsonValue { get; private set; }

        /// <summary>
        /// Private instance variable used by <see cref="JsonState"/> to track if <see cref="JsonValue"/> has already been parsed.
        /// </summary>
        private Json.EntityValue? stateFromJson;

        /// <summary>
        /// Detailed state of the <see cref="EntityValue"/> (based on parsed data from <see cref="JsonValue"/>).
        /// </summary>
        /// <remarks>
        /// If there is no content in <see cref="JsonValue"/>, the result will be created based on the (obsolete) relational 
        /// properties (<see cref="Alerts"/>, <see cref="EntityAttributeValues"/>, etc.).
        /// JsonState is not exposed directly. Instead, users should obtain the state by calling 
        /// <c>IAdapterManager.GetAdapter<Json.EntityValue>()</c>.
        /// </remarks>
        internal Json.EntityValue JsonState
        {
            get
            {
                if (stateFromJson != null)
                {
                    return stateFromJson;
                }

                // Initialize Json field if it has content
                if (JsonValue != null)
                {
                    stateFromJson = JsonSerializer.Deserialize<Json.EntityValue>(JsonValue, jsonSerializerOptions);
                    if (stateFromJson != null)
                    {
                        // Inject EntityType from Schema into Json object model.
                        stateFromJson.EntityType = GetCachedEntityType();
                        return stateFromJson;
                    }
                }

                logger?.Information(
                    $"{EntityValueStorageInformationPrefix}: Creating new json state object for EV '{Id}' "
                    + (isNewlyCreated ? "as new, blank value." : " and migrating existing relational information."));

                stateFromJson = new(GetCachedEntityType(), this.Id);

                // Initialize from relational storage for automatic; migration of old records
                // Do not convert from relational for newly created objects
                if (!isNewlyCreated)
                {
                    PopulateJsonFromRelationalModel(stateFromJson);
                }

                return stateFromJson;
            }
        }

        /// <summary>
        /// Updates the <see cref="JsonValue"/> document with the state from <see cref="JsonState"/>.
        /// </summary>
        /// <remarks>
        /// Must be called before the <see cref="EntityValue"/> is saved via Entity Framework in order to save the latest state of <see cref="JsonState"/>
        /// to the database.
        /// </remarks>
        public void UpdateJsonState()
        {
            OnUpdateState();
            JsonState.SetUpdateTime();
            JsonState.SetAttributeHasHadPersistedValueIndicators();
            JsonValue = JsonSerializer.Serialize(JsonState, jsonSerializerOptions);
        }

        /// <summary>
        /// Called from <see cref="UpdateJsonState"/> before JsonState is serialized. This allows derived
        /// entity classes to manipulate their attributes before changes are stored.
        /// </summary>
        protected virtual void OnUpdateState() { }

        /// <summary>
        /// Static JsonSerializerOptions used to ensure that Json is saved and loaded with ReferenceHandler.Preserve.
        /// </summary>
        private static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            Converters = { new Json.TimeSpanJsonConverter() },
            ReferenceHandler = ReferenceHandler.Preserve,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
            IgnoreNullValues = true,
            WriteIndented = false
        };

        /// <summary>
        /// Flag used to indicate if usage of Obsolete methods for accessing old relational data model is 
        /// allowed for migration to new Json model (normally we log a warning if the old model is being used).
        /// </summary>
        private bool isCurrentlyMigratingToJson = false;

        private const string EntityValueStorageErrorPrefix = "JSON Storage Error";
        private const string EntityValueStorageInformationPrefix = "JSON Storage Information";

        private void LogErrorIfUsedInJsonMode(string msg)
        {
            if (logger != null && !isCurrentlyMigratingToJson)
            {
                logger.Error($"{EntityValueStorageErrorPrefix}: {msg} - Stack trace:" +
                             new StackTrace());
            }
        }

        private void PopulateJsonFromRelationalModel(Json.EntityValue jsonEntityValue)
        {
#pragma warning disable CS0618 // Type or member is obsolete

            isCurrentlyMigratingToJson = true;

            jsonEntityValue.Alerts = Alerts.Select(a => a.ToJsonModel());

            List<EntityAttributeValue> filteredAttributes = GetResetFilteredEntityAttributeValues();

            // Add raw messages with their associated MessageEntityAttributeValues
            var relatedRawMessages = ModifiedBy
                .Union(filteredAttributes.OfType<MessageEntityAttributeValue>().Select(meav => meav.RawMessage))
                .Distinct()
                .OrderBy(m => m.Received);
            foreach (RawMessage rawMsg in relatedRawMessages)
            {
                IEnumerable<MessageEntityAttributeValue> relevantMessageEavs =
                    rawMsg.MessageEntityAttributeValues.Where(meav => meav.EntityValueId == this.Id &&
                                                                      filteredAttributes.Contains(meav));

                jsonEntityValue.AddRawMessage(
                    rawMsg,
                    relevantMessageEavs,
                    isConversionFromRelationalModel: true);
            }

            // Add enrichment attribute values
            var enrichmentEavs = filteredAttributes.OfType<EnrichmentEntityAttributeValue>()
                .OrderBy(eav => eav.Received);
            foreach (var eav in enrichmentEavs)
            {
                jsonEntityValue.AddAttributeEnrichmentValue(eav);
            }

            // Add a dummy reset marker to match the updates with null values that these cause in query model
            foreach (var eeav in GetActiveResetFilters())
            {
                jsonEntityValue.GetAttribute(eeav.AttributeType.InternalName).ResetMarkers
                    = new Collection<Json.EntityAttributeResetMarker>
                    {
                        new Json.EntityAttributeResetMarker("Json Migration", eeav.EnrichmentContext?.Id ?? this.Id)
                    };
            }

            foreach (Json.EntityAttribute attr in jsonEntityValue.Attributes)
            {
                // Reload attributes to set Previous values and re-sort due to modified ValueOrMetadataLastChanged
                attr.Values = attr.Values.ToList();

                if (attr.Best != null)
                {
                    attr.BestValueLastChanged = attr.Best.ValueOrMetadataLastChanged;
                }
            }

            isCurrentlyMigratingToJson = false;

#pragma warning restore CS0618 // Type or member is obsolete
        }

        [Obsolete(
            "Included to be able to read old rows from the database. For all updates to data, the new JsonState storage model should be used instead.")]
        private List<EntityAttributeValue> GetResetFilteredEntityAttributeValues()
        {
            List<EntityAttributeValue> resetFilteredEntityAttributeValues = new();

            var groupedByAttributeInternalName = EntityAttributeValues.GroupBy(eav => eav.AttributeType.InternalName);
            foreach (var group in groupedByAttributeInternalName)
            {
                List<EntityAttributeValue> filteredEavs = group.ToList();
                filteredEavs.RemoveAllEavsReceivedBeforeLatestResetMarkFilter();
                filteredEavs.RemoveSourceSpecificResetEavs();

                resetFilteredEntityAttributeValues.AddRange(filteredEavs.OfType<MessageEntityAttributeValue>());
                resetFilteredEntityAttributeValues.AddRange(filteredEavs.OfType<EnrichmentEntityAttributeValue>()
                    .Where(eeav => !eeav.IsResetMark && eeav.ResetMarkSpecificSource == null));
            }

            return resetFilteredEntityAttributeValues;
        }

        [Obsolete(
            "Included to be able to read old rows from the database. For all updates to data, the new JsonState storage model should be used instead.")]
        private List<EnrichmentEntityAttributeValue> GetActiveResetFilters() => EntityAttributeValues
            .OfType<EnrichmentEntityAttributeValue>()
            .Where(eeav => eeav.IsResetMark || eeav.ResetMarkSpecificSource != null)
            .GroupBy(eeav => eeav.AttributeType.InternalName)
            .Select(group => group.OrderBy(eeav => eeav.Received).First())
            .ToList();
    }
}