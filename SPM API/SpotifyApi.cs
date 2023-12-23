using SPM_API.Data;
using SPM_API.Helpers;
using SPM_API.Response_Classes;
using SPM_Data;
using System.Text;
using System.Text.Json;

namespace SPM_API
{
    public class SpotifyApi
    {
        readonly HttpClient _client;
        readonly UserData _user;

        public List<PlaylistData> Playlists { get; private set; }

        public SpotifyApi()
        {
            _client = new();
            _user = Connector.Connect(_client); //Authorization token will assign here

            Playlists = GetPlaylists();
        }

        public TrackData GetTrack(string id)
        {
            var response = HttpHelper.Get(_client, "https://api.spotify.com/v1/tracks/" + id);

            if (!response.IsSuccessStatusCode)
                throw ErrorHelper.CreateRequestException(response);
            else
            {
                TracksResponse.Track trackResponse = JsonSerializer.Deserialize<TracksResponse.Track>(response.Content.ReadAsStream())!;
                return ApiHelper.ToTrack(trackResponse);
            }
        }

        public async Task GetSavedTracksAsync(List<TrackData> outputList, CancellationToken? ct = null)
        {
            string? url = "https://api.spotify.com/v1/me/tracks?limit=50";

            do
            {
                var response = HttpHelper.Get(_client, url);

                if (!response.IsSuccessStatusCode)
                    throw ErrorHelper.CreateRequestException(response);
                else
                {
                    TracksResponse trackResponse = JsonSerializer.Deserialize<TracksResponse>(await response.Content.ReadAsStringAsync())!;

                    if (outputList.Capacity == 0)
                        outputList.Capacity = trackResponse.total;

                    foreach (var item in trackResponse.items)
                    {
                        if (item.is_local)
                        {
                            outputList.Capacity--;
                            continue;
                        }

                        TrackData track = ApiHelper.ToTrack(item.track);
                        track.AddedAt = DateOnly.FromDateTime(DateTime.Parse(item.added_at));
                        outputList.Add(track);
                    }

                    url = trackResponse.next;

                    if (ct != null && (bool)ct?.IsCancellationRequested!)
                        break;
                }
            } while (url != null);
        }

        public string CreatePlaylist(string name)
        {
            //Http body
            Dictionary<string, string> body = new()
            {
                { "name", name },
                { "public", "false" }
            };

            var response = HttpHelper.Post(_client, "https://api.spotify.com/v1/users/" + _user.Id + "/playlists", body, "application/json");

            if (!response.IsSuccessStatusCode)
                throw ErrorHelper.CreateRequestException(response);
            else
                return JsonSerializer.Deserialize<UserResponse>(response.Content.ReadAsStream())!.id; //Only id
        }

        public void AddTracks(string playlistId, List<TrackData> tracks)
        {
            const string PREFIX = "spotify:track:";
            int addedTracks = 0;

            while (tracks.Count - addedTracks != 0)
            {
                int times;
                List<string> uris = [];

                //Limit is 100 tracks per request
                times = tracks.Count - addedTracks;

                if (times >= 100)
                    times = 100;

                for (int i = 0; i < times; i++)
                    uris.Add(PREFIX + tracks[i + addedTracks].Id);

                ApiHelper.AddTracksContent body = new(uris); //For correct json
                var response = HttpHelper.Post(_client, "https://api.spotify.com/v1/playlists/" + playlistId + "/tracks", body, "application/json");

                if (!response.IsSuccessStatusCode)
                    throw ErrorHelper.CreateRequestException(response);

                addedTracks += times;
            }
        }

        private List<PlaylistData> GetPlaylists()
        {
            List<PlaylistData> playlists = [];
            string? url = "https://api.spotify.com/v1/users/" + _user.Id + "/playlists";

            do
            {
                var response = HttpHelper.Get(_client, url);

                if (!response.IsSuccessStatusCode)
                    throw ErrorHelper.CreateRequestException(response);
                else
                {
                    PlaylistsResponse playlistsResponse = JsonSerializer.Deserialize<PlaylistsResponse>(response.Content.ReadAsStream())!;

                    foreach (var item in playlistsResponse.items)
                    {
                        //If playlist isn`t created by this app
                        if (item.name.StartsWith("SPM: "))
                            continue;

                        PlaylistData playlist;

                        byte[]? cache = Cache.Get(item.snapshot_id + "s");
                        if (cache != null)
                            playlist = JsonSerializer.Deserialize<PlaylistData>(cache)!;
                        else
                        {
                            playlist = new(item.id, item.name)
                            {
                                Tracks = GetTracks(item.tracks.href)
                            };

                            string json = JsonSerializer.Serialize(playlist);
                            Cache.Set(item.snapshot_id + "s", Encoding.UTF8.GetBytes(json));
                        }

                        playlists.Add(playlist);
                    }

                    url = playlistsResponse.next;
                }
            } while (url != null);

            //Remove all empty or local playlists
            playlists.RemoveAll(x => x.Tracks.Count == 0);

            return playlists;
        }

        private List<TrackData> GetTracks(string tracksUrl)
        {
            List<TrackData> tracks = [];
            string? url = tracksUrl;

            do
            {
                var response = HttpHelper.Get(_client, url);

                if (!response.IsSuccessStatusCode)
                    throw ErrorHelper.CreateRequestException(response);
                else
                {
                    TracksResponse trackResponse = JsonSerializer.Deserialize<TracksResponse>(response.Content.ReadAsStream())!;
                    foreach (var item in trackResponse.items)
                    {
                        if (item.is_local) //Skip local track
                            continue;

                        TrackData track = ApiHelper.ToTrack(item.track);
                        track.AddedAt = DateOnly.FromDateTime(DateTime.Parse(item.added_at));
                        tracks.Add(track);
                    }

                    url = trackResponse.next;
                }
            } while (url != null);

            return tracks;
        }
    }
}