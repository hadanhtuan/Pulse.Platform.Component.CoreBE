namespace Services.Schema.Service.Validation;

internal class SchemaVersionValidatorFactory : ISchemaVersionValidatorFactory
{
    private readonly IEntityTypeValidator entityTypeValidator;
    private readonly IUniqueConstraintsValidator uniqueConstraintsValidator;
    private readonly IStandardSchemaValidator standardSchemaValidator;

    public SchemaVersionValidatorFactory(
        IEntityTypeValidator entityTypeValidator,
        IUniqueConstraintsValidator uniqueConstraintsValidator,
        IStandardSchemaValidator standardSchemaValidator)
    {
        this.entityTypeValidator = entityTypeValidator;
        this.uniqueConstraintsValidator = uniqueConstraintsValidator;
        this.standardSchemaValidator = standardSchemaValidator;
    }

    public ISchemaVersionValidator Get() =>
        new SchemaVersionValidator(entityTypeValidator, uniqueConstraintsValidator, standardSchemaValidator);
}