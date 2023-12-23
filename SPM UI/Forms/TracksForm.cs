using SPM_API.Data;
using SPM_API.Helpers;
using SPM_Data;
using SPM_UI.Helpers;

namespace SPM_UI.Forms
{
    public partial class TracksForm : Form
    {
        private readonly HttpClient _client;
        private readonly SPM_API.Data.PlaylistData _playlist;

        private const int TRACKS_PER_PAGE = 100;
        private int currentPage = 0;

        private Button? playerSender = null; //Which button is clicked

        private readonly List<Task> imageTasks;
        private Task? playerTask;
        private CancellationToken ct;
        private CancellationTokenSource? tokenSource;

        public TracksForm(SPM_API.Data.PlaylistData playlist)
        {
            InitializeComponent();

            _playlist = playlist;
            _client = new();
            imageTasks = [];

            mainPanel.Height = (int)(Screen.FromControl(this).Bounds.Height * 0.7); //Use 70% of screen height
            Text += playlist.Name; //Correct form name

            _playlist.Tracks = _playlist.Tracks.OrderBy(x => x.AddedAt).ToList(); //Order by date

            //Invoke to update status of buttons and label
            nextButton_Click(this, new EventArgs());
        }

        private void GenerateTable(int page)
        {
            tokenSource?.Cancel();
            imageTasks.Clear();
            mainPanel.Controls.Clear();

            List<TrackData> tracks = _playlist.Tracks;

            int times = tracks.Count - (page - 1) * TRACKS_PER_PAGE;

            if (times >= TRACKS_PER_PAGE)
                times = TRACKS_PER_PAGE;

            for (int i = 0; i < times; i++)
            {
                TrackData track = tracks[i + (currentPage - 1) * TRACKS_PER_PAGE];
                int x = 0;

                Panel panel = new()
                {
                    Size = new Size(1100, 70),
                    Location = new Point(12, i * 70),
                };

                Button playButton = new()
                {
                    Text = "\x25B6", //Play symbol
                    Font = new(Font.FontFamily, 10),
                    Size = new(30, 30),
                    Location = new(x, (70 - 30) / 2) //Center
                };
                playButton.Click += (s, e) => Play(s, track.PreviewUrl, track.Id + "p");

                panel.Controls.Add(playButton);
                x += playButton.Width + 12;

                PictureBox image = new()
                {
                    Size = new(track.ImageWidth, track.ImageHeight)
                };

                image.Location = new Point(x, (70 - image.Height) / 2); //Center

                panel.Controls.Add(image);
                x += image.Width + 12;

                //Download image async
                Task imageTask = new(() =>
                {
                    byte[]? cache = Cache.Get(track.Id + "i");
                    if (cache != null)
                    {
                        using MemoryStream stream = new(cache);
                        image.Image = Image.FromStream(stream);
                    }
                    else
                    {
                        var response = HttpHelper.Get(_client, track.ImageUrl);

                        image.Image = Image.FromStream(response.Content.ReadAsStream());

                        Cache.Set(track.Id + "i", response.Content.ReadAsByteArrayAsync().Result);
                    }
                });
                imageTasks.Add(imageTask);

                Label date = new()
                {
                    Text = track.AddedAt.ToShortDateString()
                };

                date.Font = new(date.Font.FontFamily, 10);
                date.Location = new(x, (70 - date.Height) / 2); //Center

                panel.Controls.Add(date);
                x += date.Width;

                Label title = new()
                {
                    AutoSize = true,
                    Font = new Font(Font.FontFamily, 13)
                };
                title.Location = new(x, (70 - title.Height) / 2); //Center

                foreach (string artist in track.Artists)
                    title.Text += artist + ", ";

                title.Text = title.Text.Remove(title.Text.Length - 2); //Remove last comma
                title.Text += " - " + track.Name;

                panel.Controls.Add(title);

                mainPanel.Controls.Add(panel);
            }

            //Start render
            foreach (var task in imageTasks)
                task.Start();
        }

        private void Play(object? sender, string? preview, string id)
        {
            //Some tracks don`t have preview
            if (preview == null)
            {
                MessageBox.Show("Ten utwór nie ma podglądu!");
                return;
            }

            //Pause current player
            if (playerSender != null && playerSender != sender as Button)
                tokenSource!.Cancel();

            if (playerSender == sender as Button) //Pause
            {
                tokenSource!.Cancel();
                playerSender = null;
            }
            else //Play
            {
                //Wait to flush
                playerTask?.Wait();

                //Reset token
                tokenSource = new CancellationTokenSource();
                ct = tokenSource.Token;

                //To stop play on click again
                playerSender = sender as Button;

                byte[]? cache = Cache.Get(id);
                if (cache == null)
                {
                    var response = _client.GetAsync(preview).Result;
                    byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;

                    //Save in cache
                    Cache.Set(id, bytes);

                    cache = bytes;
                }
                var senderCopy = playerSender; //To send only value, not reference

                playerTask = Task.Run(() => MusicHelper.PlayTaskMethod(cache, ct)).ContinueWith((t) =>
                {
                    if (senderCopy!.InvokeRequired)
                        BeginInvoke(() => senderCopy.Text = "\x25B6");
                });

                playerSender!.Text = "\x23F8"; //Pause symbol
            }
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            //Wait to complete load images
            Task.WaitAll(imageTasks.ToArray());

            GenerateTable(--currentPage);

            ChangeButtons();

            pageLabel.Text = currentPage + " / " + Math.Ceiling(_playlist.Tracks.Count / (float)TRACKS_PER_PAGE).ToString();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            //Wait to complete load images
            Task.WaitAll(imageTasks.ToArray());

            GenerateTable(++currentPage);

            ChangeButtons();

            pageLabel.Text = currentPage + " / " + Math.Ceiling(_playlist.Tracks.Count / (float)TRACKS_PER_PAGE).ToString();
        }

        private void ChangeButtons()
        {
            if (currentPage == 1)
                previousButton.Enabled = false;
            else
                previousButton.Enabled = true;

            if (Math.Ceiling(_playlist.Tracks.Count / (float)TRACKS_PER_PAGE) == currentPage)
                nextButton.Enabled = false;
            else
                nextButton.Enabled = true;
        }

        //Stop player and render after close form
        private void TracksForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Task.WaitAll(imageTasks.ToArray());
            tokenSource?.Cancel();
        }
    }
}
