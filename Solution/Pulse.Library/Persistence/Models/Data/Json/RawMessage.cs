using System;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json
{
    public class RawMessage
    {
        protected RawMessage(Guid id, DateTime received, InvokingReason reason, Guid? matchedEntityValueId)
        {
            Id = id;
            Received = received;
            Reason = reason;
            MatchedEntityValueId = matchedEntityValueId;
        }

        
        /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
        /// in order to use <see cref="ReferenceHandler.Preserve"/>.
        
        [JsonConstructor]
        [Obsolete("This constructor is only intended for the System.Text.Json.JsonSerializer.")]
        public RawMessage()
        {
        }

        
        /// Constructor for creating a RawMessage that represents a SystemMessage.
        
        public RawMessage(SystemMessage createdBy, Guid? matchedEntityValueId)
            : this(createdBy.Id, createdBy.Received, createdBy.Reason, matchedEntityValueId)
        {
            SystemMessage = new(createdBy.Source, createdBy.MessageType, createdBy.DisplayedMessageType);
        }

        
        /// Constructor for creating a RawMessage that represents an ActionMesage.
        
        public RawMessage(ActionMessage createdBy, Guid? matchedEntityValueId)
            : this(createdBy.Id, createdBy.Received, createdBy.Reason, matchedEntityValueId)
        {
            ActionMessage = new(createdBy.Status, createdBy.ExecutingAction, createdBy.UserId, createdBy.ActiveRole,
                createdBy.RevertAction, createdBy.Requester);
        }

        
        /// Identifier of the message - can be used to relate the RawMessage inside the Json value
        /// of the EntityValue with the <see cref="Data.RawMessage"/> stored in the RawMessage table.
        
        [JsonInclude]
        public Guid Id { get; private set; }

        
        /// The time when this message was received.
        
        /// <remarks>
        /// Value is normally set from metadata in the incoming kafka message.
        /// This means the received time can be considerably earlier than the
        /// time when the message was consumed by the 
        /// </remarks>
        [JsonInclude]
        public DateTime Received { get; private set; }

        
        /// The time when this message was persisted in the AODB
        
        /// <remarks>
        /// Difference from <see cref="Received"/> is that this time is set at the end of the pipeline execution. 
        /// </remarks>
        [JsonInclude]
        public DateTime? Persisted { get; internal set; }

        
        /// Reason for this raw message to invoke the enrichment (normal EntityValueUpdate, API, MasterData recalc., etc.).
        
        [JsonInclude]
        public InvokingReason Reason { get; private set; }

        
        /// If this message represents a system (adapter) message then this contains the adapter information
        
        [JsonInclude]
        public RawSystemMessage? SystemMessage { get; private set; }

        
        /// If this message represents an Action (UI) message then this contains the Action information
        
        [JsonInclude]
        public RawActionMessage? ActionMessage { get; private set; }

        
        /// Optional id if this message was matched to another <see cref="EntityValue"/> but has still updated attributes
        /// on this <see cref="EntityValue"/>. Will be null if the message was matched to this <see cref="EntityValue"/>.
        
        /// <remarks>
        /// Note, that a message can only be matched to one <see cref="EntityValue"/> but can have 
        /// <see cref="EntityAttributeMessageValue"/>s across several <see cref="EntityValue"/>s 
        /// (e.g. a Visit message that updates ground legs).
        /// </remarks>
        [JsonInclude]
        public Guid? MatchedEntityValueId { get; internal set; }
    }
}