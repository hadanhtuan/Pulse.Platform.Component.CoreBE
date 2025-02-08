namespace Pulse.Library.CLI.Generator.Configurations;

public class SchemaProviderOption
{
    public const string Section = "Schema";
    public string SchemaFile { get; set; } = string.Empty;
    public string OutPutPath { get; set; } = string.Empty;
}
