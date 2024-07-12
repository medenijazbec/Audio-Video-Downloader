
namespace MMDownloader6_0
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.ComboBox cmbAudioBitrate;
        private System.Windows.Forms.ComboBox cmbVideoQuality;
        private System.Windows.Forms.Button btnDownloadAudio;
        private System.Windows.Forms.Button btnDownloadVideo;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBoxThumbnail;
        private System.Windows.Forms.CheckBox chkOpenLocation;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUrl = new TextBox();
            cmbAudioBitrate = new ComboBox();
            cmbVideoQuality = new ComboBox();
            btnDownloadAudio = new Button();
            btnDownloadVideo = new Button();
            progressBar = new ProgressBar();
            lblTitle = new Label();
            pictureBoxThumbnail = new PictureBox();
            chkOpenLocation = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxThumbnail).BeginInit();
            SuspendLayout();
            // 
            // txtUrl
            // 
            txtUrl.Location = new Point(12, 12);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(360, 23);
            txtUrl.TabIndex = 0;
            txtUrl.TextChanged += txtUrl_TextChanged;
            // 
            // cmbAudioBitrate
            // 
            cmbAudioBitrate.FormattingEnabled = true;
            cmbAudioBitrate.Location = new Point(12, 38);
            cmbAudioBitrate.Name = "cmbAudioBitrate";
            cmbAudioBitrate.Size = new Size(121, 23);
            cmbAudioBitrate.TabIndex = 1;
            // 
            // cmbVideoQuality
            // 
            cmbVideoQuality.FormattingEnabled = true;
            cmbVideoQuality.Location = new Point(12, 65);
            cmbVideoQuality.Name = "cmbVideoQuality";
            cmbVideoQuality.Size = new Size(121, 23);
            cmbVideoQuality.TabIndex = 2;
            // 
            // btnDownloadAudio
            // 
            btnDownloadAudio.Location = new Point(12, 92);
            btnDownloadAudio.Name = "btnDownloadAudio";
            btnDownloadAudio.Size = new Size(121, 23);
            btnDownloadAudio.TabIndex = 3;
            btnDownloadAudio.Text = "Download Audio";
            btnDownloadAudio.UseVisualStyleBackColor = true;
            btnDownloadAudio.Click += btnDownloadAudio_Click;
            // 
            // btnDownloadVideo
            // 
            btnDownloadVideo.Location = new Point(12, 121);
            btnDownloadVideo.Name = "btnDownloadVideo";
            btnDownloadVideo.Size = new Size(121, 23);
            btnDownloadVideo.TabIndex = 4;
            btnDownloadVideo.Text = "Download Video";
            btnDownloadVideo.UseVisualStyleBackColor = true;
            btnDownloadVideo.Click += btnDownloadVideo_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 150);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(360, 23);
            progressBar.TabIndex = 5;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(12, 180);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(29, 15);
            lblTitle.TabIndex = 6;
            lblTitle.Text = "Title";
            // 
            // pictureBoxThumbnail
            // 
            pictureBoxThumbnail.Location = new Point(12, 200);
            pictureBoxThumbnail.Name = "pictureBoxThumbnail";
            pictureBoxThumbnail.Size = new Size(100, 100);
            pictureBoxThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxThumbnail.TabIndex = 7;
            pictureBoxThumbnail.TabStop = false;
            // 
            // chkOpenLocation
            // 
            chkOpenLocation.AutoSize = true;
            chkOpenLocation.Location = new Point(12, 310);
            chkOpenLocation.Name = "chkOpenLocation";
            chkOpenLocation.Size = new Size(211, 19);
            chkOpenLocation.TabIndex = 8;
            chkOpenLocation.Text = "Open File Location After Download";
            chkOpenLocation.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            ClientSize = new Size(634, 351);
            Controls.Add(chkOpenLocation);
            Controls.Add(pictureBoxThumbnail);
            Controls.Add(lblTitle);
            Controls.Add(progressBar);
            Controls.Add(btnDownloadVideo);
            Controls.Add(btnDownloadAudio);
            Controls.Add(cmbVideoQuality);
            Controls.Add(cmbAudioBitrate);
            Controls.Add(txtUrl);
            Name = "Form1";
            Text = "YouTube Downloader";
            ((System.ComponentModel.ISupportInitialize)pictureBoxThumbnail).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }



    #endregion
}

