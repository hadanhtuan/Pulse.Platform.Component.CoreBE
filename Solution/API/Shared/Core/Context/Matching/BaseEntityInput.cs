using AODB.Rules.ContextInterfaces.Matching;
using System;
using System.Collections.Generic;

namespace API.Shared.Core.Context.Matching
{
    /// <inheritdoc cref="IEntityInput"/>
    public abstract class BaseEntityInput : IEntityInput
    {
        /// <inheritdoc/>
        public string MessageSource { get; set; } = null!;
        /// <inheritdoc/>
        public string MessageType { get; set; } = null!;
        /// <inheritdoc/>
        public DateTime? Received { get; set; } = null!;
        /// <inheritdoc/>
        public string EntityType { get; protected set; } = null!;

        private readonly List<string> _includedAttributes = new ();
        /// <inheritdoc/>
        public IEnumerable<string> IncludedAttributes => _includedAttributes.AsReadOnly();

        /// <inheritdoc/>
        public void AlwaysIncludeValueFor(string attribute)
        {
            _includedAttributes.Add(attribute);
        }

        /// <inheritdoc/>
        public void UpdateIncludedAttributes(string attribute, bool include)
        {
            if (include)
            {
                _includedAttributes.Add(attribute);
            }
            else
            {
                _includedAttributes.Remove(attribute);
            }
        }
    }
}
