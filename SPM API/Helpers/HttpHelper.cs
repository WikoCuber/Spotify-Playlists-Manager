using System.Net.Http.Headers;
using System.Text.Json;

namespace SPM_API.Helpers
{
    public static class HttpHelper
    {
        public static HttpResponseMessage Get(HttpClient client, string url, object? body = null, string? contentType = null)
        {
            HttpRequestMessage message = new(HttpMethod.Get, url)
            {
                Content = FormContent(body, contentType)
            };

            return client.Send(message);
        }

        public static HttpResponseMessage Post(HttpClient client, string url, object? body = null, string? contentType = null)
        {
            HttpRequestMessage message = new(HttpMethod.Post, url)
            {
                Content = FormContent(body, contentType)
            };

            return client.Send(message);
        }

        private static HttpContent? FormContent(object? body, string? contentType)
        {
            HttpContent? content = null;

            if (body != null && contentType != null)
            {
                content = contentType switch
                {
                    "application/x-www-form-urlencoded" => new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)body),
                    "application/json" => new StringContent(JsonSerializer.Serialize(body)),
                    _ => null
                };

                if (content != null)
                    content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            }

            return content;
        }
    }
}
