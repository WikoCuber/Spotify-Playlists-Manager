namespace SPM_API.Data
{
    public class TrackData
    {
        public string Id { get; set; } = default!;
        public DateOnly AddedAt { get; set; }
        public List<string> Artists { get; set; } = [];
        public string? PreviewUrl { get; set; }
        public string Name { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
    }
}
