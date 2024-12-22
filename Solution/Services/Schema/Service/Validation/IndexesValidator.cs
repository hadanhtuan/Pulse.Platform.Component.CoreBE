using Database.Models.Schema;
using Services.Schema.Exceptions;
using Services.Schema.Service.Validation.Modifications;
using System.Text.RegularExpressions;

namespace Services.Schema.Service.Validation;

internal class IndexesValidator : IIndexesValidator
{
    private static readonly Regex validIndexRegex = new Regex("^[a-z_]+(,( )+[a-z_]+)*$");

    private static readonly string validIndexExplanation =
        "Only lowercase english letters a-z and the special character _ are allowed. " +
        "Columns should be separated by a comma and a space";

    public void ValidateIndexes(EntityType newEntityType)
    {
        if (newEntityType.Indexes == null || !newEntityType.Indexes.Any())
        {
            return;
        }

        if (newEntityType is not RootEntityType)
        {
            throw new IndexUnsupportedForEntityTypeException(newEntityType.InternalName);
        }

        foreach (var index in newEntityType.Indexes)
        {
            if (!validIndexRegex.IsMatch(index.Columns))
            {
                throw new EntityTypeInvalidPropertyException(newEntityType.InternalName,
                    nameof(EntityType.Indexes), index.Columns,
                    validIndexExplanation);
            }

            var columns = index.Columns.Split(", ");

            foreach (var column in columns)
            {
                var columnIsAnValidAttribute = newEntityType.AttributeTypes.Any(x => x.InternalName == column);

                if (!columnIsAnValidAttribute)
                {
                    throw new EntityTypeException(
                        $"Column '{column}' in entity {newEntityType.InternalName} " +
                        "is not found among its attributes.");
                }
            }
        }
    }

    public IEnumerable<AddedIndex> GetAddedIndexes(EntityType newEntityType, EntityType existingEntityType) =>
        // Disregard names and check for similar column definitions
        newEntityType.Indexes.Where(i => !existingEntityType.Indexes.Any(ei => i.Columns == ei.Columns))
            .Select(x => new AddedIndex(newEntityType, x));

    public IEnumerable<RemovedIndex> GetRemovedIndexes(EntityType newEntityType, EntityType existingEntityType) =>
        // Disregard names and check for similar column definitions
        existingEntityType.Indexes.Where(i => !newEntityType.Indexes.Any(ei => i.Columns == ei.Columns))
            .Select(x => new RemovedIndex(newEntityType, x));
}