using Database.Models.Schema;

namespace Database.Repositories
{
    public interface ICommonQueriesRepository
    {
        /// <summary>
        /// Loads the latest version of the Schema.
        /// </summary>
        /// <returns></returns>
        SchemaVersion? LoadLatestSchemaVersionOrDefault();

        /// <summary>
        /// Loads an EntityType from the latest schema.
        /// </summary>
        /// <param name="internalName">The internal name to query for.</param>
        /// <returns>An EntityType or default</returns>
        RootEntityType? LoadEntityTypeByInternalNameOrDefault(string internalName);

        /// <summary>
        /// Loads an AttributeType from the latest schema and EntityType with provided name.
        /// </summary>
        /// <param name="entityTypeInternalName">The internal name of the EntityType to
        /// query for.</param>
        /// <param name="attributeTypeInternalName">The internal name of the AttributeType to
        /// query for.</param>
        /// <returns></returns>
        AttributeType? LoadAttributeTypeByInternalNameOrDefault(string entityTypeInternalName,
            string attributeTypeInternalName);
    }
}