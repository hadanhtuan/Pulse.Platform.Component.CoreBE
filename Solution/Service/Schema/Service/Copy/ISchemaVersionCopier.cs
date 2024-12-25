using Database.Models.Schema;

namespace Service.Schema.Service.Copy;

internal interface ISchemaVersionCopier
{
    /// <summary>
    /// Copies properties from source schema into target schema and
    /// copies changes to entity types from source schema into target schema;
    /// if an entity type exists in target schema, then properties are copied
    /// and changes to attribute types are copied from source schema.
    /// As a result, if the target schema contains id's in entity and attribute
    /// types, they are preserved.
    /// </summary>
    /// <param name="source">SchemaVersion to copy from</param>
    /// <param name="target">SchemaVersion to copy changes into</param>
    /// <returns>The target SchemaVersion after changes</returns>
    SchemaVersion CopyInto(SchemaVersion source, SchemaVersion target);
}