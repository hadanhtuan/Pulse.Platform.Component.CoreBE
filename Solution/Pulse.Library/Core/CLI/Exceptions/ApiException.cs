using System.Net;
using System.Runtime.Serialization;

namespace Pulse.Library.Core.CLI.Exceptions;


/// Exception thrown in case of non-successful response from API endpoint.

[Serializable]
public class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string ResponseContent { get; }

    public static async Task<ApiException> FromResponseMessage(HttpResponseMessage message)
    {
        var content = await message.Content!.ReadAsStringAsync();
        return new ApiException(message.StatusCode, content);
    }

    public ApiException(HttpStatusCode statusCode, string content)
        : base($"Request failed with status code {statusCode}:{Environment.NewLine}" +
               $"{content}{Environment.NewLine}")
    {
        StatusCode = statusCode;
        ResponseContent = content;
    }

    protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}