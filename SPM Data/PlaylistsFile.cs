namespace SPM_Data
{
    public static class PlaylistsFile
    {
        private readonly static string DIR = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SPM\";
        private static readonly ReaderWriterLockSlim locker; //For async

        static PlaylistsFile()
        {
            if (!Directory.Exists(DIR))
                Directory.CreateDirectory(DIR);

            if (!File.Exists(DIR + "playlists.txt"))
            {
                var file = File.Create(DIR + "playlists.txt");
                file.Close();
            }

            locker = new();
        }

        public static void SaveTrack(string playlistId, string trackId, DateOnly date, string title)
        {
            locker.EnterWriteLock();

            StreamWriter sw = new(DIR + "playlists.txt", true);

            //playlist id;track id;date;title (title is like a comment)
            sw.WriteLine(playlistId + ";" + trackId + ";" + date.ToShortDateString() + ";" + title);

            sw.Close();
            locker.ExitWriteLock();
        }

        public static List<PlaylistFileData> GetTracks(string playlistId)
        {
            locker.EnterReadLock();

            List<PlaylistFileData> result = [];

            StreamReader sr = new(DIR + "playlists.txt", true);

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split(';');
                if (data[0] == playlistId)
                    result.Add(new PlaylistFileData(data[1], DateOnly.Parse(data[2])));
            }

            sr.Close();
            locker.ExitReadLock();

            return result;
        }
    }

    public class PlaylistFileData(string trackId, DateOnly addedDate)
    {
        public string TrackID { get; set; } = trackId;
        public DateOnly AddedDate { get; set; } = addedDate;
    }
}
