namespace SPM_API.Response_Classes
{
    public class PlaylistsResponse
    {
        public string? next { get; set; }
        public Item[] items { get; set; } = default!;

        public class Item
        {
            public string id { get; set; } = default!;
            public string name { get; set; } = default!;
            public string snapshot_id { get; set; } = default!;
            public Tracks tracks { get; set; } = default!;
        }

        public class Tracks
        {
            public string href { get; set; } = default!;
        }
    }
}
