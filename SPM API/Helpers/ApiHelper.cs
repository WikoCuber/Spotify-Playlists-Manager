using SPM_API.Data;
using SPM_API.Response_Classes;

namespace SPM_API.Helpers
{
    public static class ApiHelper
    {
        //Without AddedAt property
        public static TrackData ToTrack(TracksResponse.Track trackResponse)
        {
            TrackData track = new()
            {
                Name = trackResponse.name,
                PreviewUrl = trackResponse.preview_url,
                Id = trackResponse.id
            };

            var image = trackResponse.album.images.MinBy(x => x.width); //Smallest
            track.ImageUrl = image!.url;
            track.ImageWidth = (int)image.width;
            track.ImageHeight = (int)image.height;

            foreach (var artist in trackResponse.artists)
                track.Artists.Add(artist.name);

            return track;
        }

        public class AddTracksContent(List<string> uris)
        {
            //Lower case to correct json
            public List<string> uris { get; set; } = uris;
        }
    }
}
