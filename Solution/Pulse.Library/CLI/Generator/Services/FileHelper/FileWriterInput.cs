namespace Pulse.Library.CLI.Generator.Services.FileHelper;

public class FileWriterInput
{
    public FileWriterInput(string className, string classCode)
    {
        ClassName = className;
        ClassCode = classCode;
    }

    public string ClassName { get; }
    public string ClassCode { get; }
}
