
namespace API.Shared.Core.Interfaces.Context.Matching
{
    public interface IEntityInput
    {
        /// <summary>
        /// The message source.
        /// </summary>
        public string MessageSource { get; set; }

        /// <summary>
        /// The message type.
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// Received time of the message
        /// </summary>
        public DateTime? Received { get; set; }

        /// <summary>
        /// Property names of attributes that should be included in the output
        /// regardless if value is null.
        /// </summary>
        public IEnumerable<string> IncludedAttributes { get; }

        /// <summary>
        /// Adds or removes the attribute to or from the <cref>IncludedAttributes</cref> list.
        /// To be used for included null values for specific attributes or to remove previously included values
        /// </summary>
        /// <param name="attribute">The property name of the attribute to be included (that is, 
        /// as provided by <c>nameof</c> as in <c>nameof(PropertyName)</c>).</param>
        /// <param name="include">True if the value should be added to the list</param>
        public void UpdateIncludedAttributes(string attribute, bool include);
    }
}