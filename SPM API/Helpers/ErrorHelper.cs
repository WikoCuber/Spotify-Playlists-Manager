using SPM_API.Exceptions;
using SPM_API.Response_Classes;
using System.Text.Json;

namespace SPM_API.Helpers
{
    public static class ErrorHelper
    {
        public static RequestException CreateRequestException(HttpResponseMessage response) =>
            new(GetMessage(response.Content.ReadAsStream()), (int)response.StatusCode);

        private static string GetMessage(Stream stream) => JsonSerializer.Deserialize<ErrorResponse>(stream)!.error.message;
    }
}
