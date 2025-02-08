using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pulse.Library.CLI.Generator.Configurations;
using Pulse.Library.CLI.Generator.Services.Aodb;
using Pulse.Library.Core.Schema;

namespace Pulse.Library.CLI.Generator.Services;

public class GeneratorRunner
{
    private readonly SchemaProviderOption options;
    private readonly AodbContextGenerator aodbContextGenerator;


    public GeneratorRunner(IOptions<SchemaProviderOption> options, AodbContextGenerator aodbContextGenerator)
    {
        this.options = options.Value;
        this.aodbContextGenerator = aodbContextGenerator;
    }


    public void Generate()
    {
        SchemaVersion schema = GetSchema();
        if (schema == null)
        {
            throw new InvalidOperationException("No schema found.");
        }

        DeleteExistingContents();
        Directory.CreateDirectory(options.OutPutPath);

        aodbContextGenerator.GenerateContext(schema, options.OutPutPath);
    }

    public virtual SchemaVersion GetSchema()
    {
        if (options.SchemaFile == null || !File.Exists(options.SchemaFile))
        {
            throw new InvalidOperationException("No valid path to AODB schema JSON file specified.");
        }

        Console.WriteLine($"Reading schema from {options.SchemaFile}");

        using StreamReader r = new StreamReader(options.SchemaFile);
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<SchemaVersion>(json);
    }

    private void DeleteExistingContents()
    {
        if (!Directory.Exists(options.OutPutPath))
        {
            return;
        }

        foreach (var file in Directory.GetFiles(options.OutPutPath))
        {
            var path = ToOutputPath(file, options.OutPutPath);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        foreach (var directory in Directory.GetDirectories(options.OutPutPath))
        {
            var path = ToOutputPath(directory, options.OutPutPath);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }

    // private void CreateSolutionFromTemplate()
    // {
    //     var templateSolutionPath = templateSolutionProvider.GetTemplateSolution();

    //     Directory.CreateDirectory(options.OutPutPath);

    //     DeleteExistingContents(templateSolutionPath);

    //     new DirectoryInfo(templateSolutionPath).CopyTo(options.OutPutPath);
    // }

    private string ToOutputPath(string file, string templateSolutionPath)
    {
        return Path.GetFullPath(Path.GetRelativePath(templateSolutionPath, file), options.OutPutPath);
    }
}
