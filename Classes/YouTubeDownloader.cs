using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;
using YoutubeDLSharp.Metadata;

namespace MMDownloader6_0.Classes
{
    public class YouTubeDownloader
    {
        private readonly YoutubeDL ytdl;

        public YouTubeDownloader()
        {
            ytdl = new YoutubeDL();
        }
        public void SetPaths(string ytDlpPath, string ffmpegPath)
        {
            ytdl.YoutubeDLPath = ytDlpPath;
            ytdl.FFmpegPath = ffmpegPath;
        }
        public async Task<VideoData> GetVideoDetails(string url)
        {
            var result = await ytdl.RunVideoDataFetch(url);
            var videoData = result?.Data;

            if (videoData != null && videoData.Entries != null && videoData.Entries.Length > 0)
            {
                return videoData.Entries.First();
            }
            return videoData;
        }


        public async Task<string> DownloadAudio(string url, string bitrate, string outputPath, IProgress<DownloadProgress> progress)
        {
            var opts = new OptionSet
            {
                ExtractAudio = true,
                AudioFormat = AudioConversionFormat.Mp3,
                AudioQuality = byte.TryParse(bitrate, out byte quality) ? (byte?)quality : null, //Convert bitrate to nullable byte
                
                Output = outputPath
            };

            var downloadResult = await ytdl.RunAudioDownload(url, AudioConversionFormat.Mp3, progress: progress, overrideOptions: opts);
            return Convert.ToString(downloadResult?.Data?.FirstOrDefault());
        }

        public async Task<string> DownloadVideo(string url, string formatId, string outputPath, IProgress<DownloadProgress> progress)
        {
            var opts = new OptionSet
            {
                Format = formatId, //Set the desired video format ID
                Output = outputPath //Specify the output path
            };

            //Ensure the output directory exists
            var directory = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            //Download the video
            var downloadResult = await ytdl.RunVideoDownload(url, progress: progress, overrideOptions: opts);

            //Log error output if any
            if (downloadResult?.ErrorOutput != null && downloadResult.ErrorOutput.Length > 0)
            {
                string errorOutput = string.Join(Environment.NewLine, downloadResult.ErrorOutput);
                File.WriteAllText(@"C:\Users\matic\Desktop\Personal projects\MMDownloader6_0\Tests\Logs\yt-dlp.log", errorOutput);
            }

            //Check if the downloadResult contains data and return the path of the downloaded video
            return Convert.ToString(downloadResult?.Data?.FirstOrDefault());
        }





        //Sanitize filename to remove illegal characters
        public static string SanitizeFilename(string filename)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var invalidCharsPattern = new string(invalidChars);
            var regex = new Regex($"[{Regex.Escape(invalidCharsPattern)}]");
            return regex.Replace(filename, "_");
        }
    }
}
