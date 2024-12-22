namespace Database.Models.Schema;

/// <summary>
/// Definition of a type of attribute that is defined by a <see cref="ComplexAttributeEntityType"/>,
/// i.e. a sub-entity stored under a root entity as a JSON string/object.    
/// </summary>
/// <example>
/// A complex attribute for CodeshareEnriched could be defined as a ComplexAttributeEntityType
/// that has attributes for flight number and either external data source or master data reference to 
/// codeshare agreement used to assign the flight number.
/// </example>
public class ComplexAttributeType : AttributeType
{
    public ComplexAttributeType(ComplexAttributeEntityType complexDataType, bool isCollection)
    {
        ComplexDataType = complexDataType;
        IsCollection = isCollection;
    }

    /// <summary>
    /// Parameter-less constructor for Entity Framework
    /// </summary>
    protected ComplexAttributeType()
    {
    }

    /// <summary>
    /// The <see cref="ComplexAttributeEntityType"/> used to define this attribute type.
    /// Defines which sub-attributes can be placed on values of this complex attribute type.
    /// </summary>
    /// <remarks>
    /// Private setter is for Entity Framework (which guarentees not null through required foreign key)
    /// </remarks>
    public virtual ComplexAttributeEntityType ComplexDataType { get; private set; } = null!;

    /// <summary>
    /// Determines if the attribute stores a list records 
    /// (with attributes according to the related ComplexAttributeEntityType) or just a single value.
    /// </summary>
    public bool IsCollection { get; private set; }

    /// <summary>
    /// Used by UI to format the content of a single value of this complex attribute type when showing 
    /// the value inside a single cell in a grid.
    /// </summary>
    /// <remarks>
    /// Must be written as an Angular template expression in order to make it easy for the UI to render it.
    /// The template expression can used the internal name of the attributes on the related 
    /// ComplexAttributeEntityType as properties for data binding.
    /// </remarks>
    /// <example>
    /// {{flight_number | zeroPadding:4 }}
    /// </example>
    public string? ShortDisplayTemplate { get; set; }

    /// <summary>
    /// If IsCollection=true (i.e. a list of records is stored), this string value will be used by the UI when 
    /// concatenating the records to show the value of the ComplexAttributeType inside a single cell in a grid.
    /// </summary>
    /// <remarks>
    /// If not present, UI will concatenate attribute values without any separator.
    /// </remarks>
    public string? ShortDisplaySeparator { get; set; }

    /// <summary>
    /// Used by UI to format the content of a single value of this complex attribute type when hovering on the 
    /// value inside a single cell in a grid.
    /// </summary>
    /// <remarks>
    /// Just like <see cref="ShortDisplayTemplate"/>, this template must be written as an Angular template
    /// expression, but this template will typically combine information from several attributes on the related
    /// ComplexAttributeEntityType in order to show a little more details than is shown in the grid cell. 
    /// </remarks>
    /// <example>
    /// {{flight_number | zeroPadding:4 }} ( {{data_source}} )
    /// </example>
    public string? HoverDisplayTemplate { get; set; }

    /// <summary>
    /// If IsCollection=true (i.e. a list of records is stored), this string value will be used by the UI when 
    /// concatenating the records to show the hover value of the ComplexAttributeType.
    /// </summary>
    /// <remarks>
    /// If not present, UI will concatenate attribute values without any separator.
    /// </remarks>
    public string? HoverDisplaySeparator { get; set; }

    /// <summary>
    /// Used by the UI for advanced filtering.
    /// Converts the string into a complex attribute to construct the Odata filter.
    /// </summary>
    /// <remarks>
    /// If present, the string inserted in the filter, will be split into the different attributes 
    /// inside the complex attribute
    /// </remarks>
    public string? AdvancedFilteringDefinition { get; set; }
}