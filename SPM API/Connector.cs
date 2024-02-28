using SPM_API.Data;
using SPM_API.Exceptions;
using SPM_API.Helpers;
using SPM_API.Response_Classes;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;

namespace SPM_API
{
    public static class Connector
    {
        private static readonly string CLIENT_ID;
        private static readonly string CLIENT_SECRET_ID;
        private const string PERMISSIONS = "playlist-modify-public playlist-modify-private user-library-read user-read-private playlist-read-private playlist-read-collaborative";

        static Connector()
        {
            if (!File.Exists("IDs.cfg"))
                File.Create("IDs.cfg").Close();

            //Load IDs from config file
            string[] ids = File.ReadAllLines("IDs.cfg");

            if (ids.Length < 2)
                throw new Exception("Bad config file");

            CLIENT_ID = ids[0];
            CLIENT_SECRET_ID = ids[1];
        }

        public static UserData Connect(HttpClient client)
        {
            string code = GetAccessCode();
            string token = GetToken(client, code);

            return GetUserData(client, token);
        }

        private static string GetAccessCode()
        {
            //Get whether user grants permissions
            HttpListenerRequest result = DisplayUserAccessForm();
            var queryDictionary = HttpUtility.ParseQueryString(result.Url!.Query);

            if (queryDictionary.AllKeys.Contains("error"))
                throw new AccessDeniedException(queryDictionary["error"]!);
            else
                return queryDictionary["code"]!;
        }

        private static string GetToken(HttpClient client, string code)
        {
            //Id authorization
            byte[] bytes = Encoding.UTF8.GetBytes(CLIENT_ID + ":" + CLIENT_SECRET_ID);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

            //Http body
            Dictionary<string, string> body = new()
            {
                { "grant_type", "authorization_code" },
                { "redirect_uri", "http://localhost:57777/callback" },
                { "code", code }
            };

            var response = HttpHelper.Post(client, "https://accounts.spotify.com/api/token", body, "application/x-www-form-urlencoded");

            if (!response.IsSuccessStatusCode)
                throw ErrorHelper.CreateRequestException(response);
            else
            {
                TokenResponse tokenResponse = JsonSerializer.Deserialize<TokenResponse>(response.Content.ReadAsStream())!;
                return tokenResponse.access_token;
            }
        }

        private static UserData GetUserData(HttpClient client, string token)
        {
            //Token authorization
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = HttpHelper.Get(client, "https://api.spotify.com/v1/me");

            if (!response.IsSuccessStatusCode)
                throw ErrorHelper.CreateRequestException(response);
            else
            {
                UserResponse userResponse = JsonSerializer.Deserialize<UserResponse>(response.Content.ReadAsStream())!;
                return new UserData(token, userResponse.display_name, userResponse.id);
            }
        }

        private static HttpListenerRequest DisplayUserAccessForm()
        {
            //Open browser to get access
            Process p = new()
            {
                StartInfo = new()
                {
                    UseShellExecute = true,
                    FileName = "https://accounts.spotify.com/authorize?" +
                               "response_type=code" +
                               $"&client_id={HttpUtility.UrlEncode(CLIENT_ID)}" +
                               $"&scope={HttpUtility.UrlEncode(PERMISSIONS)}" +
                               $"&redirect_uri={HttpUtility.UrlEncode("http://localhost:57777/callback")}"
                }
            };
            p.Start();

            //Listener to spotify callback
            HttpListener listener = new();
            listener.Prefixes.Add("http://localhost:57777/callback/");
            listener.Start();

            //Waiting
            HttpListenerContext context = listener.GetContext();

            //Close tab
            string responseString = "<script>close();</script>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentType = "text/html";
            context.Response.OutputStream.Write(buffer, 0, buffer.Length); //Send response

            return context.Request;
        }
    }
}
