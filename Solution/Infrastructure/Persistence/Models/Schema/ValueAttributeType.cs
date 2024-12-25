namespace Database.Models.Schema
{
    /// <summary>
    /// Definition of a type of attribute that holds a value.
    /// </summary>
    public class ValueAttributeType : AttributeType
    {
        /// <summary>
        /// The data type of the value;
        /// set only for ValueAttributeTypes.
        /// </summary>
        public DataType ValueDataType { get; set; }

        /// <summary>
        /// Defines a specific format for displaying the AttributeType value
        /// The format should be interpretable as an Angular Pipe
        /// </summary>
        public string? DisplayFormat { get; set; }

        /// <summary>
        /// Dropdown lookup entity name in masterdata
        /// </summary>
        public string? MasterdataTable { get; set; }

        /// <summary>
        /// Dropdown lookup attribute name in masterdata
        /// </summary>
        public string? MasterdataValueAttributeName { get; set; }

        /// <summary>
        /// If the <see cref="ValueDataType"/> is <see cref="DataType.Guid"/> then this
        /// can be used to specify that the <see cref="System.Guid"/> is in fact a reference to an 
        /// entity of the type <see cref="EntityType"/>.
        /// </summary>
        public virtual EntityType? EntityReferenceType { get; set; }
    }
}