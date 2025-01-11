using Database.Models.Schema;
using System.Linq;

namespace Database.Repositories
{
    public class CommonQueriesRepository : ICommonQueriesRepository
    {
        private readonly ISchemaProvider schemaProvider;

        public CommonQueriesRepository(ISchemaProvider schemaProvider)
        {
            this.schemaProvider = schemaProvider;
        }

        public SchemaVersion? LoadLatestSchemaVersionOrDefault() => schemaProvider.SchemaVersion;

        public RootEntityType? LoadEntityTypeByInternalNameOrDefault(string internalName)
        {
            return LoadLatestSchemaVersionOrDefault()?
                .EntityTypes.OfType<RootEntityType>()
                .SingleOrDefault(e => e.InternalName.Equals(internalName));
        }

        public AttributeType? LoadAttributeTypeByInternalNameOrDefault(string entityTypeInternalName,
            string attributeTypeInternalName)
        {
            return LoadLatestSchemaVersionOrDefault()?
                .EntityTypes
                .SingleOrDefault(e => e.InternalName.Equals(entityTypeInternalName))?
                .AttributeTypes
                .SingleOrDefault(a => a.InternalName.Equals(attributeTypeInternalName));
        }
    }
}