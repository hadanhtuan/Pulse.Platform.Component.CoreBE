namespace Pulse.Library.Core.Actions
{
    public interface IEntityInput
    {
        
        /// The message source.
        
        public string MessageSource { get; set; }

        
        /// The message type.
        
        public string MessageType { get; set; }

        
        /// Received time of the message
        
        public DateTime? Received { get; set; }

        
        /// Property names of attributes that should be included in the output
        /// regardless if value is null.
        
        public IEnumerable<string> IncludedAttributes { get; }

        
        /// Adds or removes the attribute to or from the <cref>IncludedAttributes</cref> list.
        /// To be used for included null values for specific attributes or to remove previously included values
        
        /// <param name="attribute">The property name of the attribute to be included (that is, 
        /// as provided by <c>nameof</c> as in <c>nameof(PropertyName)</c>).</param>
        /// <param name="include">True if the value should be added to the list</param>
        public void UpdateIncludedAttributes(string attribute, bool include);
    }
}