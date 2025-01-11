namespace Database.Models.Schema
{
    public enum DataType
    {
        
        /// A text string value.
        
        Text,

        
        /// An integer value.
        
        Integer,

        
        /// A boolean value (true, false).
        
        Boolean,

        
        /// A duration value (corresponding to TimeSpan in C#).
        
        Duration,

        
        /// A date and time in UTC.
        
        DateTime,

        
        /// A dropdown value (enumeration).
        
        Dropdown,

        
        /// A decimal value.
        
        Decimal,

        
        /// A universally unique identifier (UUID, or GUID in Microsoft terms).
        
        Guid,

        
        /// A multiline text field
        
        TextArea,

        
        /// A date without time.
        
        Date,
    }
}