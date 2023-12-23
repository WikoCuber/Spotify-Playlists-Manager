namespace SPM_API.Data
{
    public class PlaylistData(string id, string name)
    {
        public string Id { get; set; } = id;
        public string Name { get; set; } = name;
        public List<TrackData> Tracks { get; set; } = [];
    }
}
