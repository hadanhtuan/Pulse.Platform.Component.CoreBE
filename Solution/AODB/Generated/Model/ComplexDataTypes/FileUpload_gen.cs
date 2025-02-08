
namespace AODB.Generated.Model.ComplexDataTypes;

/// <summary>
/// Files attached to an entity
/// </summary>
/// <remarks>
/// This class is autogenerated, do not make changes here!
/// </remarks>
public class FileUpload
{

    /// <summary>
    /// 
    /// </summary>
    public string? CreatedDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? FileReference { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? FullFileName { get; set; }

        public bool ValueIsEmpty()
        {
            return CreatedDate == null && FileName == null && FileReference == null && FullFileName == null;
        }
}