using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MMDownloader6_0.Classes;
using YoutubeDLSharp;
using YoutubeDLSharp.Metadata;

namespace MMDownloader6_0
{
    public partial class Form1 : Form
    {
        private readonly YouTubeDownloader downloader;

        public Form1()
        {
            InitializeComponent();
            downloader = new YouTubeDownloader();
            DownloadDependencies().Wait();
        }
        private async Task DownloadDependencies()
        {
            string ytDlpPath = @"C:\Users\matic\Desktop\Personal projects\MMDownloader6_0\yt-dlp\yt-dlp.exe"; 
            string ffmpegPath = @"C:\Users\matic\Desktop\Personal projects\MMDownloader6_0\ffmpeg\ffmpeg.exe"; 
           
            if (!File.Exists(ytDlpPath))
            {
                await YoutubeDLSharp.Utils.DownloadYtDlp();
                MessageBox.Show("yt-dlp has been downloaded.");
            }

            if (!File.Exists(ffmpegPath))
            {
                await YoutubeDLSharp.Utils.DownloadFFmpeg();
                MessageBox.Show("ffmpeg has been downloaded.");
            }

            downloader.SetPaths(ytDlpPath, ffmpegPath);
        }
        private async void txtUrl_TextChanged(object sender, EventArgs e)
        {
            string url = txtUrl.Text;
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    VideoData videoData = await downloader.GetVideoDetails(url);
                    if (videoData != null)
                    {
                        lblTitle.Text = videoData.Title;
                        pictureBoxThumbnail.ImageLocation = videoData.Thumbnail;

                        cmbAudioBitrate.Items.Clear();
                        cmbVideoQuality.Items.Clear();

                        foreach (var format in videoData.Formats)
                        {
                            if (!string.IsNullOrEmpty(format.AudioCodec))
                            {
                                string bitrate = format.AudioBitrate > 0 ? $"{format.AudioBitrate} kbps" : "Unknown";
                                if (!cmbAudioBitrate.Items.Contains(bitrate))
                                {
                                    cmbAudioBitrate.Items.Add(bitrate);
                                }
                            }
                            if (!string.IsNullOrEmpty(format.VideoCodec))
                            {
                                string quality = format.FormatId + " - " + (format.Height > 0 ? $"{format.Height}p" : format.FormatNote);
                                if (!cmbVideoQuality.Items.Contains(quality))
                                {
                                    cmbVideoQuality.Items.Add(quality);
                                }
                            }
                        }

                        if (cmbAudioBitrate.Items.Count > 0) cmbAudioBitrate.SelectedIndex = 0;
                        if (cmbVideoQuality.Items.Count > 0) cmbVideoQuality.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching video details: " + ex.Message);
                }
            }
        }



        private async void btnDownloadAudio_Click(object sender, EventArgs e)
        {
            string url = txtUrl.Text;
            string bitrate = cmbAudioBitrate.SelectedItem.ToString().Replace(" kbps", "");

            var progress = new Progress<DownloadProgress>(p => progressBar.Value = (int)(p.Progress * 100));

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                sfd.Filter = "MP3 Files|*.mp3";
                sfd.FileName = YouTubeDownloader.SanitizeFilename(lblTitle.Text) + ".mp3";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string tempFilePath = await downloader.DownloadAudio(url, bitrate, sfd.FileName, progress);
                        string destinationPath = sfd.FileName;

                        if (File.Exists(tempFilePath))
                        {
                            File.Move(tempFilePath, destinationPath);
                            MessageBox.Show("Audio Download Complete: " + destinationPath);

                            if (chkOpenLocation.Checked)
                            {
                                Process.Start("explorer.exe", $"/select, \"{destinationPath}\"");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error: Downloaded file not found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private async void btnDownloadVideo_Click(object sender, EventArgs e)
        {
            string url = txtUrl.Text;
            string selectedFormat = cmbVideoQuality.SelectedItem.ToString();
            string formatId = selectedFormat.Split(' ')[0]; //Extract the format ID

            var progress = new Progress<DownloadProgress>(p => progressBar.Value = (int)(p.Progress * 100));

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                sfd.Filter = "MP4 Files|*.mp4";
                sfd.FileName = YouTubeDownloader.SanitizeFilename(lblTitle.Text) + ".mp4";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string destinationPath = sfd.FileName;
                        string directory = Path.GetDirectoryName(destinationPath);
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(destinationPath);
                        string extension = Path.GetExtension(destinationPath);

                        //Temporary path for yt-dlp output
                        string tempFilePath = Path.Combine(directory, fileNameWithoutExtension + ".temp" + extension);

                        //Download the video
                        string downloadedFilePath = await downloader.DownloadVideo(url, formatId, tempFilePath, progress);

                        if (!string.IsNullOrEmpty(downloadedFilePath) && File.Exists(downloadedFilePath))
                        {
                            File.Move(downloadedFilePath, destinationPath);
                            MessageBox.Show("Video Download Complete: " + destinationPath);

                            if (chkOpenLocation.Checked)
                            {
                                Process.Start("explorer.exe", $"/select, \"{destinationPath}\"");
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }








    }
}
