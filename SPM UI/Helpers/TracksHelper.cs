using SPM_API;
using SPM_API.Data;
using SPM_Data;
using System.Text;
using System.Text.Json;

namespace SPM_UI.Helpers
{
    public static class TracksHelper
    {
        public static void LoadTracks(List<SPM_API.Data.PlaylistData> output, SpotifyApi api)
        {
            foreach (var playlist in api.Playlists)
            {
                //All tracks that are saved
                var savedTracks = PlaylistsFile.GetTracks(playlist.Id);

                //For all tracks that are now in playlist
                foreach (var track in playlist.Tracks)
                {
                    //Check is that track is saved
                    var savedTrack = savedTracks.Where(x => x.TrackID == track.Id).FirstOrDefault();
                    if (savedTrack == null)
                        PlaylistsFile.SaveTrack(playlist.Id, track.Id, track.AddedAt, track.Name);
                    else
                        savedTracks.Remove(savedTrack); //Remove from list to correct union
                }

                //For all tracks that aren`t in playlist
                foreach (var savedTrack in savedTracks)
                {
                    TrackData track;

                    byte[]? cache = Cache.Get(savedTrack.TrackID);
                    if (cache != null)
                        track = JsonSerializer.Deserialize<TrackData>(cache)!;
                    else
                    {
                        track = api.GetTrack(savedTrack.TrackID);
                        track.AddedAt = savedTrack.AddedDate;

                        string json = JsonSerializer.Serialize(track);
                        Cache.Set(track.Id, Encoding.UTF8.GetBytes(json));
                    }

                    //Add to main list
                    output.Where(x => x.Id == playlist.Id).First().Tracks.Add(track);
                }
            }
        }
    }
}
