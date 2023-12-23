namespace SPM_API.Response_Classes
{
    public class TokenResponse
    {
        public string access_token { get; set; } = default!;
        public string? refresh_token { get; set; }
    }
}
