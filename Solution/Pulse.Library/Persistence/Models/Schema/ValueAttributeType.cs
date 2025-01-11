namespace Database.Models.Schema
{
    
    /// Definition of a type of attribute that holds a value.
    
    public class ValueAttributeType : AttributeType
    {
        
        /// The data type of the value;
        /// set only for ValueAttributeTypes.
        
        public DataType ValueDataType { get; set; }

        
        /// Defines a specific format for displaying the AttributeType value
        /// The format should be interpretable as an Angular Pipe
        
        public string? DisplayFormat { get; set; }

        
        /// Dropdown lookup entity name in masterdata
        
        public string? MasterdataTable { get; set; }

        
        /// Dropdown lookup attribute name in masterdata
        
        public string? MasterdataValueAttributeName { get; set; }

        
        /// If the <see cref="ValueDataType"/> is <see cref="DataType.Guid"/> then this
        /// can be used to specify that the <see cref="System.Guid"/> is in fact a reference to an 
        /// entity of the type <see cref="EntityType"/>.
        
        public virtual EntityType? EntityReferenceType { get; set; }
    }
}