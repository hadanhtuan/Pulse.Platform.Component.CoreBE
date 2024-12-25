using DAP.Base.Logging;
using Database.Data;
using Database.Models.Data;
using Database.Models.Schema;
using Database.Services;
using Domain.AdapterFactory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace AODB.QueryDatabase.Services
{
    internal class EntityUpdater : IEntityUpdater
    {
        private readonly DbContext dbContext;
        private readonly IAdapterManager adapterManager;
        private readonly ILogger logger;

        public EntityUpdater(
            SchemaContext dbContext,
            IAdapterManager adapterManager,
            ISystemLoggerFactory loggerFactory)
        {
            this.dbContext = dbContext;
            this.adapterManager = adapterManager;
            logger = loggerFactory.GetLogger<EntityUpdater>();
        }

        public List<(string statement, object?[] parameters)> UpdateEntity(EntityValue entityValue)
        {
            logger.Debug("Updating query table for entity {@EntityValueId}", entityValue.Id);
            List<(string statement, object?[] parameters)> statements = BuildUpdateStatements(entityValue);
            statements.ForEach(sql => dbContext.Database.ExecuteSqlRaw(sql.statement, sql.parameters));
            return statements;
        }

        public void DeleteEntity(EntityValue entityValue)
        {
            logger.Debug("Deleting query table for entity {@EntityValueId}", entityValue.Id);
            BuildDeleteStatements(entityValue).ForEach(sql =>
                dbContext.Database.ExecuteSqlRaw(sql.statement, sql.parameters));
        }

        private static List<(string statement, object?[] parameters)> BuildDeleteStatements(EntityValue entityValue)
        {
            var sql = $"DELETE FROM entity_{entityValue.GetCachedEntityType().InternalName} WHERE entity_id = {{0}}";

            var metaSql = $"DELETE FROM entity__metadata WHERE entity_id = {{0}}";

            return new List<(string, object?[])>
            {
                (sql, new object[] { entityValue.Id }), (metaSql, new object[] { entityValue.Id })
            };
        }

        private List<(string statement, object?[] parameters)> BuildUpdateStatements(EntityValue entityValue)
        {
            var entityValueState = adapterManager.GetAdapter<IEntityValueState>(entityValue)!;

            List<AttributeValue> attributeValues = entityValueState.Attributes
                .Where(attr => attr.Best is not null || attr.PreviousBest is not null || attr.ResetMarkers.Any())
                .Where(IsNotSpecialVisitAttribute)
                .Select(attr => new AttributeValue(
                    attr.AttributeType.InternalName,
                    attr.Best is not null ? ToDataValue(attr.Best) : null,
                    isJson: attr.AttributeType is ComplexAttributeType))
                .OrderBy(eav => eav.AttributeName)
                .ToList();

            attributeValues.Add(new AttributeValue("entity_id", entityValue.Id));
            attributeValues.Add(new AttributeValue("last_modified", entityValue.LastModified));

            AddSpecificAttributes(entityValue, attributeValues);

            var insertParameters = Enumerable.Range(0, attributeValues.Count)
                .Select(x => attributeValues[x].IsJson ? $"CAST({{{x}}} AS json)" : $"{{{x}}}").ToList();

            var sql = $"INSERT INTO entity_{entityValue.GetCachedEntityType().InternalName} " +
                      $"({string.Join(',', attributeValues.Select(av => Quote(av.AttributeName)))}) " +
                      $"VALUES ({string.Join(',', insertParameters)})";

            var updateParameters = Enumerable.Range(0, attributeValues.Count)
                .Select(x => (Quote(attributeValues[x].AttributeName),
                    attributeValues[x].IsJson ? $"CAST({{{x}}} AS json)" : $"{{{x}}}"))
                .ToList();

            sql = $"{sql} ON CONFLICT (entity_id) DO UPDATE SET " +
                  string.Join(',', updateParameters.Select(p => $"{p.Item1}={p.Item2}"));

            var attributeMetadata =
                entityValueState.Attributes
                    .Where(attr => attr.Best is not null || attr.PreviousBest is not null || attr.ResetMarkers.Any())
                    .Where(IsNotSpecialVisitAttribute)
                    .OrderBy(attr => attr.AttributeTypeInternalName)
                    .ToDictionary(eav => eav.AttributeType.InternalName, eav => EavMetadata.FromEav(entityValue, eav));
            var metadataAttributes = new List<object>
            {
                entityValue.Id,
                entityValue.GetCachedEntityType().InternalName,
                JsonConvert.SerializeObject(attributeMetadata)
            };

            var metaSql = $"INSERT INTO entity__metadata " +
                          $"(entity_id, entity_type, attributes) " +
                          $"VALUES({{0}}, {{1}}, {{2}}) " +
                          $"ON CONFLICT (entity_id) DO UPDATE SET " +
                          $"attributes = {{2}}";

            return new List<(string, object?[])>
            {
                (sql, attributeValues.Select(av => av.Value).ToArray()), (metaSql, metadataAttributes.ToArray())
            };
        }

        private bool IsNotSpecialVisitAttribute(Database.Models.Data.Json.EntityAttribute attr) =>
            !(attr.AttributeType.EntityType.InternalName == StandardElements.Visit &&
              (attr.AttributeType.InternalName == StandardElements.LinkedDueTo ||
               attr.AttributeType.InternalName == StandardElements.Priority));

        private string Quote(string text) => $"\"{text}\"";

        private static void AddSpecificAttributes(EntityValue entityValue, IList<AttributeValue> attributeList)
        {
            switch (entityValue)
            {
                case Visit visit:
                    attributeList.Add(new AttributeValue("priority", visit.Priority));
                    attributeList.Add(new AttributeValue("linked_due_to", visit.LinkedDueTo));
                    attributeList.Add(new AttributeValue("inbound_flight", visit.InboundFlight?.Id));
                    attributeList.Add(new AttributeValue("outbound_flight", visit.OutboundFlight?.Id));
                    break;
                case GroundLeg groundLeg:
                    attributeList.Add(new AttributeValue("visit", groundLeg.Visit?.Id));
                    break;
            }
        }

        private sealed class AttributeValue
        {
            public AttributeValue(string attributeName, object? value, bool isJson = false)
            {
                AttributeName = attributeName;
                Value = value;
                IsJson = isJson;
            }

            public object? Value { get; set; }

            public string AttributeName { get; set; }

            public bool IsJson { get; set; }
        }

        private static object? ToDataValue(Database.Models.Data.Json.EntityAttributeValue jsonEav) =>
            jsonEav.Status == EntityAttributeStatus.Archived || jsonEav.Status == EntityAttributeStatus.Deleted
                ? null
                : jsonEav.GetValue();
    }
}