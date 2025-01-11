using System;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json
{
    public class EntityAttributeMessageValue
    {
        internal EntityAttributeMessageValue(
            RawMessage rawMessage)
        {
            RawMessage = rawMessage;
            if (rawMessage.ActionMessage != null)
            {
                ActionEffect = ActionEffectStatus.Added;
                AffectedByRawMsgId = rawMessage.Id;
            }
        }

        
        /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
        /// in order to use <see cref="System.Text.Json.Serialization.ReferenceHandler.Preserve"/>.
        
        [JsonConstructor]
        [Obsolete("This constructor is only intended for the System.Text.Json.JsonSerializer.")]
        public EntityAttributeMessageValue() 
        {
            RawMessage = new();
        }

        
        /// The message that assigned this particular <see cref="EntityAttributeValue"/> to the <see cref="EntityValue"/>.
        
        [JsonInclude]
        public RawMessage RawMessage { get; private set; }

        
        /// If this <see cref="EntityAttributeValue"/> was <see cref="AffectedBy"/> an Action,
        /// this property tracks how it was affected, either added or deleted.
        
        [JsonInclude]
        public ActionEffectStatus? ActionEffect { get; internal set; }

        
        /// Reference to the <see cref="Database.Models.Data.Json.RawMessage"/> that had an <see cref="ActionEffect"/>
        /// on this <see cref="EntityAttributeValue"/>.
        
        [JsonInclude]
        public Guid? AffectedByRawMsgId { get; internal set; }

        public bool IsEquivalentTo(EntityAttributeMessageValue? other) =>
            other != null &&
            RawMessage.SystemMessage?.Source == other.RawMessage.SystemMessage?.Source &&
            RawMessage.SystemMessage?.MessageType == other.RawMessage.SystemMessage?.MessageType &&
            RawMessage.ActionMessage?.ExecutingAction == other.RawMessage.ActionMessage?.ExecutingAction &&
            ActionEffect == other.ActionEffect &&
            AffectedByRawMsgId == other.AffectedByRawMsgId;
    }
}
