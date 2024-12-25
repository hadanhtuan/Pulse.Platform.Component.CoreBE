using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Database.Services;

[DataContract]
public class EntityRecord
{
    public static readonly IImmutableList<string> StandardAttributeNames =
        new List<string> { "entity_id", "last_modified" }.ToImmutableList();

    [Key]
    [JsonPropertyName("entity_id")]
    [DataMember(Name = "entity_id")]
    public Guid? EntityId { get; set; }

    [JsonPropertyName("last_modified")]
    [DataMember(Name = "last_modified")]
    public DateTime? LastModified { get; set; }

    [Newtonsoft.Json.JsonConverter(typeof(AttributeConverter))]
    public IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

    public object this[string key]
    {
        get
        {
            switch (key)
            {
                case "entity_id":
                    return EntityId;
                case "last_modified":
                    return LastModified;
                default:
                    try
                    {
                        return Attributes[key];
                    }
                    catch (KeyNotFoundException)
                    {
                        return null;
                    }
            }
        }
    }

    public EntityRecord(IDictionary<string, object> row)
    {
        EntityId = row["entity_id"] as Guid?;
        LastModified = row["last_modified"] as DateTime?;
        Attributes = row.Where(x => !StandardAttributeNames.Contains(x.Key.ToLower()))
            .ToDictionary(x => x.Key, y => y.Value);
    }

    public EntityRecord()
    {
    }
}