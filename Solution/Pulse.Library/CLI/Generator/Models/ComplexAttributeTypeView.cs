namespace Pulse.Library.CLI.Generator.Models;


/// Definition of a type of attribute that is defined by a ComplexAttributeEntityType",
/// i.e. a sub-entity stored under a root entity as a JSON string/object.    

public class ComplexAttributeTypeView : AttributeTypeView
{
    
    /// The InternalName of the ComplexAttributeEntityType in the Schema that defines
    /// the sub-structure (i.e. attribute definition) of this ComplexAttributeType.
    
    /// <remarks>
    /// The InternalName must be present in <see cref="SchemaVersionView.ComplexAttributeEntityTypes"/> list.
    /// </remarks>
    public string ComplexDataTypeInternalName { get; set; } = null!;

    
    /// Determines if the attribute stores a list records 
    /// (with attributes according to the related ComplexAttributeEntityType) or just a single value.
    
    public bool IsCollection { get; set; }

    
    /// Used by UI to format the content of a single value of this complex attribute type when showing 
    /// the value inside a single cell in a grid.
    
    /// <remarks>
    /// Must be written as an Angular template expression in order to make it easy for the UI to render it.
    /// The template expression can used the internal name of the attributes on the related 
    /// ComplexAttributeEntityType as properties for data binding.
    /// </remarks>
    /// <example>
    /// {{flight_number | zeroPadding:4 }}
    /// </example>
    public string? ShortDisplayTemplate { get; set; }

    
    /// If IsCollection=true (i.e. a list of records is stored), this string value will be used by the UI when 
    /// concatenating the records to show the value of the ComplexAttributeType inside a single cell in a grid.
    
    /// <remarks>
    /// If not present, UI will concatenate attribute values without any separator.
    /// </remarks>
    public string? ShortDisplaySeparator { get; set; }

    
    /// Used by UI to format the content of a single value of this complex attribute type when hovering on the 
    /// value inside a single cell in a grid.
    
    /// <remarks>
    /// Just like <see cref="ShortDisplayTemplate"/>, this template must be written as an Angular template
    /// expression, but this template will typically combine information from several attributes on the related
    /// ComplexAttributeEntityType in order to show a little more details than is shown in the grid cell. 
    /// </remarks>
    /// <example>
    /// {{flight_number | zeroPadding:4 }} ( {{data_source}} )
    /// </example>
    public string? HoverDisplayTemplate { get; set; }

    
    /// If IsCollection=true (i.e. a list of records is stored), this string value will be used by the UI when 
    /// concatenating the records to show the hover value of the ComplexAttributeType.
    
    /// <remarks>
    /// If not present, UI will concatenate attribute values without any separator.
    /// </remarks>
    public string? HoverDisplaySeparator { get; set; }

    
    /// Used by the UI for advanced filtering.
    /// Converts the string into a complex attribute to construct the Odata filter.
    
    /// <remarks>
    /// If present, the string inserted in the filter, will be split into the different attributes 
    /// inside the complex attribute
    /// </remarks>
    public string? AdvancedFilteringDefinition { get; set; }
}