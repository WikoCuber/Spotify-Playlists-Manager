namespace SPM_API.Response_Classes
{
    public class ErrorResponse
    {
        public Error error { get; set; } = default!;

        public class Error
        {
            public string message { get; set; } = default!;
        }
    }
}
