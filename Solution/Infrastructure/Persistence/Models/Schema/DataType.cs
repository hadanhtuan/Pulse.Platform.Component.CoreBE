namespace Database.Models.Schema
{
    public enum DataType
    {
        /// <summary>
        /// A text string value.
        /// </summary>
        Text,

        /// <summary>
        /// An integer value.
        /// </summary>
        Integer,

        /// <summary>
        /// A boolean value (true, false).
        /// </summary>
        Boolean,

        /// <summary>
        /// A duration value (corresponding to TimeSpan in C#).
        /// </summary>
        Duration,

        /// <summary>
        /// A date and time in UTC.
        /// </summary>
        DateTime,

        /// <summary>
        /// A dropdown value (enumeration).
        /// </summary>
        Dropdown,

        /// <summary>
        /// A decimal value.
        /// </summary>
        Decimal,

        /// <summary>
        /// A universally unique identifier (UUID, or GUID in Microsoft terms).
        /// </summary>
        Guid,

        /// <summary>
        /// A multiline text field
        /// </summary>
        TextArea,

        /// <summary>
        /// A date without time.
        /// </summary>
        Date,
    }
}