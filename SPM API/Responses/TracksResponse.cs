namespace SPM_API.Response_Classes
{
    public class TracksResponse
    {
        public string? next { get; set; }
        public int total { get; set; }
        public Item[] items { get; set; } = default!;

        public class Item
        {
            public string added_at { get; set; } = default!;
            public bool is_local { get; set; }
            public Track track { get; set; } = default!;
        }

        public class Track
        {
            public string id { get; set; } = default!;
            public Album album { get; set; } = default!;
            public Artist[] artists { get; set; } = default!;
            public int duration_ms { get; set; }
            public string name { get; set; } = default!;
            public string? preview_url { get; set; }
        }

        public class Album
        {
            public Image[] images { get; set; } = default!;
        }

        public class Image
        {
            public string url { get; set; } = default!;
            public int height { get; set; }
            public int width { get; set; }
        }

        public class Artist
        {
            public string name { get; set; } = default!;
        }
    }
}