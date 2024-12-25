namespace Service.Schema.Service.Validation;

public interface ISchemaVersionValidatorFactory
{
    ISchemaVersionValidator Get();
}