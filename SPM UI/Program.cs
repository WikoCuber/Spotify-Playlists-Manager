using SPM_API;
using SPM_API.Data;
using SPM_API.Exceptions;
using SPM_UI.Forms;
using SPM_UI.Helpers;

namespace SPM_UI
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                ApplicationConfiguration.Initialize();

                SpotifyApi api;

                //Connect to Spotify API
                try { api = new(); }
                catch (Exception e)
                {
                    if (e is AccessDeniedException)
                    {
                        MessageBox.Show("Nie udzielono dostêpu", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                        throw;
                }

                List<PlaylistData> playlists = api.Playlists;

                if (playlists.Count == 0)
                {
                    MessageBox.Show("Na tym koncie nie ma ¿adnych playlist");
                    return;
                }

                //Load tracks
                TracksHelper.LoadTracks(playlists, api);

                Application.Run(new MainForm(playlists, api));
            }
            catch (Exception e) //In error case display message box
            {
                string title = "B³¹d krytyczny";

                if (e is RequestException exception)
                    title += " - " + exception.StatusCode;

                MessageBox.Show(e.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}