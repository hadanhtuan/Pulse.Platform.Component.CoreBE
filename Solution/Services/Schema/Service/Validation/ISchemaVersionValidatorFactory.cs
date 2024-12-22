namespace Services.Schema.Service.Validation;

public interface ISchemaVersionValidatorFactory
{
    ISchemaVersionValidator Get();
}