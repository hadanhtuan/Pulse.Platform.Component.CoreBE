using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Database.Services;

/// <summary>
/// Loads entity records from JSON. Developed for temporary use in loading
/// mock data via an API endpoint.
/// </summary>
public class RecordLoader
{
    private readonly DbContext dbContext;

    public RecordLoader(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public int LoadFromUrl(string url, string internalName)
    {
        var json = ReadToString(url);
        return LoadFromJson(json, internalName);
    }

    public int LoadFromJson(string json, string internalName)
    {
        var records = ParseRecordsFromJson(json);

        var recordNumber = 0;
        foreach (var rec in records)
        {
            rec.Add("entity_id", Guid.NewGuid());
            rec.Add("last_modified", rec.ContainsKey("lastupdated") ? rec["lastupdated"] : DateTime.Now);

            ++recordNumber;

            var (statement, parameters) = InsertRow(internalName, rec);
            dbContext.Database.ExecuteSqlRaw(statement, parameters);
        }

        return recordNumber;
    }

    private static string ReadToString(string url)
    {
        return new HttpClient().GetAsync(new Uri(url)).Result.Content.ReadAsStringAsync().Result;
    }

    private static (string statement, object[] parameters) InsertRow(string entityName,
        Dictionary<string, object> attributes)
    {
        var insertParameters = Enumerable.Range(0, attributes.Count).Select(x => $"{{{x}}}").ToList();

        var sql = $"INSERT INTO entity_{entityName} " +
                  $"({string.Join(',', attributes.Keys)}) " +
                  $"VALUES ({string.Join(',', insertParameters)})";

        return (sql, attributes.Values.ToArray());
    }

    private static Dictionary<string, object>[] ParseRecordsFromJson(string json)
    {
        var settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringEnumConverter(), new IsoDateTimeConverter(), }
        };
        return JsonConvert.DeserializeObject<Dictionary<string, object>[]>(json, settings);
    }
}