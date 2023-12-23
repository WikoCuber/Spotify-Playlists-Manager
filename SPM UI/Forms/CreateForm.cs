using SPM_API;
using SPM_API.Data;

namespace SPM_UI.Forms
{
    public partial class CreateForm : Form
    {
        private readonly SpotifyApi _api;
        private readonly List<PlaylistData> _playlists;

        public CreateForm(SpotifyApi api, List<PlaylistData> playlists)
        {
            InitializeComponent();
            _api = api;
            _playlists = playlists;

            foreach (var playlist in playlists)
                playlistComboBox.Items.Add(playlist.Name);
            playlistComboBox.SelectedIndex = 0;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            PlaylistData playlist = _playlists[playlistComboBox.SelectedIndex];

            string playlistId = _api.CreatePlaylist("SPM: " + playlist.Name);

            _api.AddTracks(playlistId, playlist.Tracks.OrderBy(x => x.AddedAt).ToList());

            MessageBox.Show("Pomyślnie utworzono playlistę");
            Close();
        }
    }
}
