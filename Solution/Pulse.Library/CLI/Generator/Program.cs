using Pulse.Library.CLI.Generator.Models;
using Pulse.Library.Core.CLI;
using Pulse.Library.Core.CLI.Exceptions;

namespace Pulse.Library.CLI.Generator;

public static class Program
{
    private static string id;

    public static void Main(string[] args)
    {
        
    }

    public static async Task MainAsync(string[] args)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Environment.ExitCode = DetermineErrorCodeAndLog(ex);
            // Sleep for 10 seconds after failure, to ensure the logs are available
            // in the pipeline running the updater job pod
            Thread.Sleep(10000);
        }
    }
    
    private static int DetermineErrorCodeAndLog(Exception ex)
    {
        if (ex is ApiException apiException &&
            apiException.StatusCode == System.Net.HttpStatusCode.BadRequest &&
            apiException.ResponseContent.Contains("ConfigurationVersionHasDisruptiveChangesBadRequestException"))
        {
            Log("Terminated without completing update.");
            Log("Update was cancelled because configuration version has disruptive changes " +
                "and option CancelIfDisruptiveSchemaChanges=True was specified.");
            Log("Component should be scaled down before running the update without this option.");
            return ExitCodes.DisruptiveSchemaChanges;
        }

        Log($"Terminated without completing update: {ex}");
        return ExitCodes.UnspecifiedError;
    }
    
    private static void Log(string s)
    {
        Console.WriteLine($"[{id}] {DateTime.UtcNow}: {s}");
    }
}