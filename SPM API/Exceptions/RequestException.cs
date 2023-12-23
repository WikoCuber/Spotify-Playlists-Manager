namespace SPM_API.Exceptions
{
    public class RequestException(string message, int statusCode) : Exception(message)
    {
        public int StatusCode { get; set; } = statusCode;
    }
}