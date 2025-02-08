namespace Pulse.Library.CLI.Generator.Services.FileHelper;

public class FileWriter
{
    public void Write(string folder, IEnumerable<FileWriterInput> filesToWrite, bool highlightGenerationInFileName = true)
    {
        Directory.CreateDirectory(folder);

        string fileNameExtension = highlightGenerationInFileName ? "_gen.cs" : ".cs";

        foreach (var fileToWrite in filesToWrite)
        {
            File.WriteAllText(Path.GetFullPath($"{fileToWrite.ClassName}{fileNameExtension}", folder), fileToWrite.ClassCode);
        }
    }
}