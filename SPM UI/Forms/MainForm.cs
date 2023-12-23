using SPM_API;
using SPM_API.Data;

namespace SPM_UI.Forms
{
    public partial class MainForm : Form
    {
        private readonly List<PlaylistData> _playlists;
        private readonly SpotifyApi _api;

        public MainForm(List<PlaylistData> playlists, SpotifyApi api)
        {
            InitializeComponent();

            _playlists = playlists;
            _api = api;

            AddButtons();
        }

        private void AddButtons()
        {
            int x = 0, y = 0;

            foreach (PlaylistData playlist in _playlists)
            {
                Button button = new()
                {
                    Text = playlist.Name,
                    Location = new Point(x, y),
                    Size = new Size(170, 70)
                };
                button.Font = new Font(button.Font.FontFamily, 12);

                //Open track list form
                button.Click += (s, e) =>
                {
                    TracksForm form = new(playlist);
                    form.ShowDialog();
                };

                playlistsPanel.Controls.Add(button);

                x += button.Width + 30; //30 is margin

                if (x + button.Width > playlistsPanel.Width)
                {
                    y += button.Height + 30;
                    x = 0;
                }
            }
        }

        private void completeButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            CompleteForm form = new(_api, _playlists);
            form.ShowDialog();

            Cursor = Cursors.Default;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            CreateForm form = new(_api, _playlists);
            form.ShowDialog();
        }
    }
}