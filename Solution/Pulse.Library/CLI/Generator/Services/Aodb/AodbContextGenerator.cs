using Pulse.Library.CLI.Generator.Services.FileHelper;
using Pulse.Library.Core.Schema;
using Pulse.Library.Services.Schema.Validators;

namespace Pulse.Library.CLI.Generator.Services.Aodb;

public class AodbContextGenerator
{
    private readonly AodbCodeGenerator aodbCodeGenerator;
    private readonly FileWriter fileWriter;

    public AodbContextGenerator(FileWriter fileWriter, AodbCodeGenerator aodbCodeGenerator)
    {
        this.fileWriter = fileWriter;
        this.aodbCodeGenerator = aodbCodeGenerator;
    }

    public void GenerateContext(SchemaVersion unpreparedSchema, string solutionPath)
    {
        SchemaVersion schema = Prepare(unpreparedSchema);

        var contextPath = Path.GetFullPath(
            "Model", solutionPath);
        // var interfacePath = Path.GetFullPath(
        //     "Model", solutionPath);
        var mockPath = Path.GetFullPath(
            "Mock", solutionPath);

        // Generate entities
        WriteToFile(
            contextPath,
            "Entities/",
            aodbCodeGenerator.CreateEntityImplementation(schema)
        );

        // Generate complex datatypes
        WriteToFile(
           contextPath,
           "ComplexDataTypes/",
           aodbCodeGenerator.CreateComplexDataTypes(schema)
       );
    }

    private SchemaVersion Prepare(SchemaVersion schema)
    {
        SchemaVersionValidator.Validate(schema);

        return new SchemaVersion
        {
            EntityTypes = schema.EntityTypes
                    .Select(WithSortedActiveAttributes)
                    .OrderBy(e => e.InternalName, StringComparer.InvariantCultureIgnoreCase),
            ComplexDataTypes = schema.ComplexDataTypes
                    .Select(WithSortedActiveAttributes)
                    .OrderBy(e => e.InternalName, StringComparer.InvariantCultureIgnoreCase),
            Version = schema.Version
        };
    }

    private EntityType WithSortedActiveAttributes(EntityType entity)
    {
        entity.AttributeTypes = entity.AttributeTypes
            .Where(a => a.Status == Status.Active)
            .OrderBy(a => a.InternalName, StringComparer.InvariantCultureIgnoreCase).ToList();
        return entity;
    }

    public void WriteToFile(string baseFolder, string folder, Dictionary<string, string> content,
          bool highlightGenerationInFileName = true)
    {

        var filesToWrite = content.Select(kv =>
            new FileWriterInput(kv.Key, kv.Value));
        fileWriter.Write(Path.GetFullPath(Path.TrimEndingDirectorySeparator(folder), baseFolder),
            filesToWrite, highlightGenerationInFileName);
    }
}
