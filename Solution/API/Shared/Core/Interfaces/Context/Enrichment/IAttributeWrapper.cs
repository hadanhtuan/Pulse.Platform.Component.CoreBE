using API.Shared.Core.Interfaces.Context.Models;
using System;

namespace API.Shared.Core.Interfaces.Context.Enrichment
{
    /// <summary>
    /// Exposes all properties of an attribute value.
    /// </summary>
    /// <typeparam name="T">Data type of the value</typeparam>
    public interface IAttributeWrapper<out T>
    {
        /// <summary>
        /// The value of the eav. For example, 1 or "CPH". 
        /// Can be <see langword="null"/> if the attribute value from a message was <see langword="null"/>.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Priority of this attribute value in relation to other values of same attribute type.
        /// This is set by enrichment rules and defaults to 0 for message values.
        /// When choosing the best value for an attribute type, first priority is considered, next the 
        /// received time (or the time of enrichment, which results in order being significant).
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// <see cref="AttributeCategory"/> describing what created this value.
        /// </summary>
        AttributeCategory AttributeCategory { get; }

        /// <summary>
        /// The received time of the value, in case of a value that comes from a message,
        /// or creation time in case of an enrichment value. 
        /// For system message values, this is the time when the message was received in the adapter.
        /// For action/UI message values, this is the time when the action put a message 
        /// on the internal enrichment topic.
        /// </summary>
        DateTime Received { get; }

        /// <summary>
        /// The time the value or metadata last changed for the attribute wrapper.
        /// For system message values, this is when the value last changed for the current source and message type.
        /// For action message values, this is when the value or action metadata last changed.
        /// For enrichment values, this is when the value, priority, aodb source or masterdata source last changed.
        /// </summary>
        DateTime ValueOrMetadataLastChanged { get; }

        /// <summary>
        /// Identifies the external source (e.g., "SAS") of the value if it came from a system message. 
        /// This is <see langword="null"/> for values that come from enrichment or an action/UI message.
        /// </summary>
        string? SystemSource { get; }

        /// <summary>
        /// Identifies the type of the external adapter message (e.g., "MVT") containing this value, if
        /// it came from a system message.
        /// This is <see langword="null"/> if this value comes from enrichment or an action/UI message.
        /// </summary>
        string? SystemMessageType { get; }

        /// <summary>
        /// Identifies the role of the user that performed the action that contains this value, if this
        /// value is from an action message.
        /// This is <see langword="null"/> if this value comes from enrichment or a system message.
        /// </summary>
        string? UserRole { get; }

        /// <summary>
        /// Reference to the <see cref="IAttributeWrapper{T}"/> of another attribute value if
        /// specified during enrichment, if this is an enrichment value.
        /// This is <see langword="null"/> if this value does not come from enrichment.
        /// </summary>
        IAttributeWrapper<T>? AodbReference { get; }

        /// <summary>
        /// Name of the source of this attribute value, which depends on whether the value is
        /// from a message or an enrichment:
        /// <para>
        /// If this value is from a system message, then <see cref="SourceName"/> is <see cref="SystemSource"/>.
        /// </para>
        /// <para>
        /// If this value is from an action message, then <see cref="SourceName"/> is "UiMessage".
        /// </para>
        /// <para>
        /// If this value is from an enrichment, then the <see cref="SourceName"/> is the
        /// custom source name if it were provided in the call to SetValue on 
        /// <see cref="IPrioritizedAttributeWrapper{T}"/> in a rule; 
        /// the source of the system message if the (possibly indirectly) referenced value is from a system message;
        /// the short display name of the referenced value if that value is from another attribute type;
        /// "Masterdata" if a master data reference was provided in the call to SetValue on
        /// <see cref="IPrioritizedAttributeWrapper{T}"/> in a rule;
        /// or "AODB" otherwise.
        /// </para>
        /// </summary>
        string? SourceName { get; }
    }
}