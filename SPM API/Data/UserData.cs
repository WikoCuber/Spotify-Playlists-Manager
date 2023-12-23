namespace SPM_API.Data
{
    public class UserData(string token, string displayName, string id)
    {
        public string Token { get; set; } = token;
        public string DisplayName { get; set; } = displayName;
        public string Id { get; set; } = id;
    }
}
