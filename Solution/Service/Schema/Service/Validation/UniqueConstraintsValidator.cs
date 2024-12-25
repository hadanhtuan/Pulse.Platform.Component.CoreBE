using Database.Models.Schema;
using Service.Schema.Service.Validation.Modifications;
using Service.Schema.Exceptions;
using System.Text.RegularExpressions;

namespace Service.Schema.Service.Validation;

internal class UniqueConstraintsValidator : IUniqueConstraintsValidator
{
    private static readonly Regex validInternalNameRegex = new("^[a-z_]+$");

    private static readonly string validInternalNameExplanation =
        "Only lowercase english letters a-z and the special character _ are allowed. " +
        "No spaces, numbers or other special characters are allowed";

    private static readonly string validRowExpression =
        "Only simple Row Expressions are allowed. " +
        "It is not allowed to include any other statements.";

    public void Validate(SchemaVersion newSchema, SchemaVersion oldSchema)
    {
        var allInternalNamesOfUniqueConstraints = newSchema.EntityTypes
            .SelectMany(e => e.UniqueConstraints)
            .Select(uc => uc.InternalName);

        var uniqueInternalNamesOfUniqueConstraints = new HashSet<string>();
        foreach (string internalName in allInternalNamesOfUniqueConstraints)
        {
            if (!uniqueInternalNamesOfUniqueConstraints.Add(internalName))
            {
                var entityTypeInternalNames = newSchema.EntityTypes
                    .Where(e => e.UniqueConstraints.Any(uc => uc.InternalName == internalName))
                    .Select(x => x.InternalName);

                throw new SchemaContainsNonUniqueConstraintsException(string.Join(", ", entityTypeInternalNames));
            }
        }

        var oldUniqueConstraints = oldSchema.EntityTypes
            .SelectMany(e => e.UniqueConstraints);

        var newUniqueConstraints = newSchema.EntityTypes
            .SelectMany(e => e.UniqueConstraints);

        foreach (var oldConstraint in oldUniqueConstraints)
        {
            var correspondingNewConstraint =
                newUniqueConstraints.FirstOrDefault(c => c.InternalName == oldConstraint.InternalName);

            if (correspondingNewConstraint != null && !oldConstraint.IsEquivalentTo(correspondingNewConstraint))
            {
                throw new UniqueConstraintChangedException(correspondingNewConstraint.InternalName);
            }
        }
    }

    public IEnumerable<AddedUniqueConstraint> GetAddedUniqueConstraints(
        EntityType newEntityType, EntityType existingEntityType) =>
        newEntityType.UniqueConstraints
            .Where(c => !existingEntityType.UniqueConstraints.Any(ec => ec.IsEquivalentTo(c)))
            .Select(c => new AddedUniqueConstraint(newEntityType, c));

    public IEnumerable<RemovedUniqueConstraint> GetRemovedUniqueConstraints(
        EntityType newEntityType, EntityType existingEntityType) =>
        existingEntityType.UniqueConstraints
            .Where(c => !newEntityType.UniqueConstraints.Any(ec => ec.IsEquivalentTo(c)))
            .Select(c => new RemovedUniqueConstraint(newEntityType, c));

    public void ValidateUniqueConstraints(EntityType newEntityType)
    {
        if (newEntityType.UniqueConstraints == null || !newEntityType.UniqueConstraints.Any())
        {
            return;
        }

        if (newEntityType is not RootEntityType)
        {
            throw new UniqueConstraintUnsupportedForEntityTypeException(newEntityType.InternalName);
        }

        foreach (var uniqueConstraint in newEntityType.UniqueConstraints)
        {
            ValidateUniqueConstraintInternalName(uniqueConstraint, newEntityType);
            ValidateUniqueConstraintDisplayName(uniqueConstraint, newEntityType);
            ValidateUniqueConstraintColumnExpressions(uniqueConstraint, newEntityType);
            ValidateUniqueConstraintRowFilterExpression(uniqueConstraint, newEntityType);
        }
    }

    private static void ValidateUniqueConstraintInternalName(
        UniqueConstraint uniqueConstraint, EntityType newEntityType)
    {
        if (string.IsNullOrWhiteSpace(uniqueConstraint.InternalName))
        {
            throw new UniqueConstraintMissingPropertyException(
                newEntityType.InternalName, uniqueConstraint.InternalName, nameof(UniqueConstraint.InternalName));
        }

        if (uniqueConstraint.InternalName.Length > 62)
        {
            throw new UniqueConstraintInternalNameInvalidLengthException(
                newEntityType.InternalName, uniqueConstraint.InternalName);
        }

        if (!validInternalNameRegex.IsMatch(uniqueConstraint.InternalName))
        {
            throw new EntityTypeInvalidPropertyException(newEntityType.InternalName,
                nameof(EntityType.UniqueConstraints), uniqueConstraint.InternalName,
                validInternalNameExplanation);
        }
    }

    private static void ValidateUniqueConstraintDisplayName(
        UniqueConstraint uniqueConstraint, EntityType newEntityType)
    {
        if (string.IsNullOrWhiteSpace(uniqueConstraint.DisplayName))
        {
            throw new UniqueConstraintMissingPropertyException(
                newEntityType.InternalName, uniqueConstraint.InternalName, nameof(UniqueConstraint.DisplayName));
        }
    }

    private static void ValidateUniqueConstraintColumnExpressions(
        UniqueConstraint uniqueConstraint, EntityType newEntityType)
    {
        if (!uniqueConstraint.ColumnExpressions.Any())
        {
            throw new UniqueConstraintMissingPropertyException(
                newEntityType.InternalName, uniqueConstraint.InternalName, nameof(UniqueConstraint.ColumnExpressions));
        }
    }

    private static void ValidateUniqueConstraintRowFilterExpression(
        UniqueConstraint uniqueConstraint, EntityType newEntityType)
    {
        if (uniqueConstraint.RowFilterExpression != null &&
            uniqueConstraint.RowFilterExpression.Contains(";"))
        {
            throw new EntityTypeInvalidPropertyException(newEntityType.InternalName,
                nameof(UniqueConstraint.RowFilterExpression), uniqueConstraint.RowFilterExpression,
                validRowExpression);
        }
    }
}