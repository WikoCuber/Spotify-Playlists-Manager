using SPM_API;
using SPM_API.Data;
using SPM_API.Helpers;
using SPM_Data;
using SPM_UI.Helpers;
using System.Text;
using System.Text.Json;

namespace SPM_UI.Forms
{
    public partial class CompleteForm : Form
    {
        private readonly List<PlaylistData> _playlists;
        private readonly List<TrackData> _tracks;
        private readonly HttpClient _client;

        private readonly int total;

        private readonly CancellationTokenSource? backgroundLoaderTokenSource;
        private readonly CancellationToken backgroundLoaderCt;

        private CancellationToken playerCt;
        private CancellationTokenSource? playerTokenSource;

        private int currentIndex = -1;
        private bool isPlaying = false;

        public CompleteForm(SpotifyApi api, List<PlaylistData> playlists)
        {
            InitializeComponent();

            _playlists = playlists;
            _client = new();

            _tracks = [];

            backgroundLoaderTokenSource = new();
            backgroundLoaderCt = backgroundLoaderTokenSource.Token;
            Task.Run(() => api.GetSavedTracksAsync(_tracks, backgroundLoaderCt));

            while (_tracks.Count == 0)
                Thread.Sleep(100);

            total = _tracks.Capacity;

            playButton.Text = "\x25B6"; //Play symbol

            foreach (var playlist in _playlists)
                playlistComboBox.Items.Add(playlist.Name);

            playlistComboBox.SelectedIndex = 0;

            playButton.Click += (s, e) => Play(_tracks[currentIndex].PreviewUrl, _tracks[currentIndex].Id + "p");
            NextTrack();
        }

        private void NextTrack()
        {
            //Last track
            if (currentIndex + 1 == total)
            {
                Close();
                return;
            }

            //When track hasn`t loaded yet
            while (_tracks.Count - 1 == currentIndex && _tracks.Count != total)
                Thread.Sleep(100);

            currentIndex++;

            //Image
            albumPictureBox.Size = new(_tracks[currentIndex].ImageWidth, _tracks[currentIndex].ImageHeight);

            byte[]? cache = Cache.Get(_tracks[currentIndex].Id + "i");
            if (cache != null)
            {
                MemoryStream stream = new(cache);
                albumPictureBox.Image = Image.FromStream(stream);
            }
            else
            {
                var response = HttpHelper.Get(_client, _tracks[currentIndex].ImageUrl);

                albumPictureBox.Image = Image.FromStream(response.Content.ReadAsStream());

                Cache.Set(_tracks[currentIndex].Id + "i", response.Content.ReadAsByteArrayAsync().Result);
            }

            //Title
            titleLabel.Text = "";
            foreach (string artist in _tracks[currentIndex].Artists)
                titleLabel.Text += artist + ", ";

            titleLabel.Text = titleLabel.Text.Remove(titleLabel.Text.Length - 2); //Remove last comma
            titleLabel.Text += " - " + _tracks[currentIndex].Name;

            //Progress
            progressLabel.Text = currentIndex + 1 + "/" + total;

            //Preview
            isPlaying = false;
            playerTokenSource?.Cancel();

            //Warning (is track is already somewhere in playlist)
            warningLabel.Text = "";
            foreach (var playlist in _playlists)
            {
                if (playlist.Tracks.Where(x => x.Id == _tracks[currentIndex].Id).Any())
                    warningLabel.Text += playlist.Name + ", ";
            }

            if (warningLabel.Text.Length > 0)
                warningLabel.Text = warningLabel.Text.Remove(warningLabel.Text.Length - 2); //Remove last comma
        }

        private void Play(string? preview, string id)
        {
            //Some tracks don`t have preview
            if (preview == null)
            {
                MessageBox.Show("Ten utwór nie ma podglądu!");
                return;
            }

            //Pause current player
            if (isPlaying)
            {
                playerTokenSource!.Cancel();
                isPlaying = false;
            }
            else //Play
            {
                isPlaying = true;

                //Reset token
                playerTokenSource = new CancellationTokenSource();
                playerCt = playerTokenSource.Token;

                byte[]? cache = Cache.Get(id);
                if (cache == null)
                {
                    var response = HttpHelper.Get(_client, preview);
                    byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;

                    //Save in cache
                    Cache.Set(id, bytes);

                    cache = bytes;
                }

                Task.Run(() => MusicHelper.PlayTaskMethod(cache, playerCt)).ContinueWith((t) =>
                {
                    if (playButton.InvokeRequired)
                        BeginInvoke(() => playButton.Text = "\x25B6");
                });

                playButton.Text = "\x23F8"; //Pause symbol
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            TrackData track = _tracks[currentIndex];
            string name = playlistComboBox.SelectedItem!.ToString()!;
            PlaylistData playlist = _playlists.First(x => x.Name == name);

            playlist.Tracks.Add(track);
            PlaylistsFile.SaveTrack(playlist.Id, track.Id, track.AddedAt, track.Name);

            string json = JsonSerializer.Serialize(track);
            Cache.Set(track.Id, Encoding.UTF8.GetBytes(json));

            NextTrack();
        }

        private void noneButton_Click(object sender, EventArgs e) => NextTrack();

        //Stop player and background loader after close form
        private void CompleteForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            backgroundLoaderTokenSource?.Cancel();
            playerTokenSource?.Cancel();
        }
    }
}
