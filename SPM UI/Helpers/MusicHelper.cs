using NAudio.Wave;

namespace SPM_UI.Helpers
{
    public static class MusicHelper
    {
        public static void PlayTaskMethod(byte[] bytes, CancellationToken ct)
        {
            //Save to temp
            string path = Path.GetTempFileName();
            File.WriteAllBytes(path, bytes);

            WaveOut waveOut = new();
            using AudioFileReader audioFileReader = new(path);
            waveOut.Init(audioFileReader);
            waveOut.Play();

            while (waveOut.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(100);
                if (ct.IsCancellationRequested)
                    waveOut.Stop();
            }

            File.Delete(path);
        }
    }
}
