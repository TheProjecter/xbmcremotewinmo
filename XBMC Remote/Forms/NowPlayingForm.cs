using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Microsoft.WindowsCE.Forms;

using XbmcEventClient;
using XbmcJson;

namespace XBMC_Remote
{
    public partial class NowPlayingForm : Form
    {
        private EventClient EventClient = new EventClient();
        private System.Windows.Forms.Timer updateTimer;
        private XbmcConnection JsonClient;
        private string CurrentSong;
        
        public string IpAddress;

        public NowPlayingForm()
        {
            InitializeComponent();
        }

        private void InitializeTimer()
        {
            EventClient.Connect(IpAddress);
            JsonClient = new XbmcConnection(IpAddress, 8080, "", "");

            updateTimer = new System.Windows.Forms.Timer();
            int seconds = 1;
            updateTimer.Interval = 1000 * seconds; // 1000 * n where n == seconds 
            updateTimer.Tick += new EventHandler(updateTimer_Tick);
            updateTimer.Enabled = true;
        } 

        private void NowPlayingForm_Load(object sender, EventArgs e)
        {
            InitializeTimer();
        }
        
        private void updateTimer_Tick(object o1, EventArgs e1)
        {
            RefreshNowPlaying();
        }

        public void RefreshNowPlaying()
        {
            if (JsonClient.Player.IsAudioPlayerActive())
            {
                Song NowPlaying = JsonClient.AudioPlaylist.GetCurrentItem(new string[] { "title", "artist", "album" });

                if (NowPlaying == null)
                {
                    return;
                }

                if (NowPlaying.Title != CurrentSong)
                {
                    CurrentSong = NowPlaying.Title;
                    npTitle.Text = NowPlaying.Title;
                    npArtist.Text = NowPlaying.Artist;
                    npAlbum.Text = NowPlaying.Album;

                    lbArtist.Text = "Artist";
                    lbAlbum.Text = "Album";

                    System.Drawing.Size size = new System.Drawing.Size(32, 32);

                    if (NowPlaying.Thumbnail == "")
                    {
                        //NowPlayingImage.Image = new Bitmap("Data\\" + "music-icon.jpg");
                    }
                    else
                    {
                        NowPlayingImage.Image = JsonClient.Files.GetImageFromThumbnail(NowPlaying.Thumbnail);
                    }
                }

                npRuntime.Text = JsonClient.AudioPlayer.GetTimeFormatted();
            }
            else if (JsonClient.Player.IsVideoPlayerActive())
            {
                PlaylistItem NowPlaying = JsonClient.VideoPlaylist.GetCurrentItemAllFields();

                npArtist.Text = NowPlaying.Genre;
                npTitle.Text = NowPlaying.Title;
                npAlbum.Text = NowPlaying.Year.ToString();
                lbArtist.Text = "Genre";
                lbAlbum.Text = "Year";
                npRuntime.Text = JsonClient.VideoPlayer.GetTimeFormatted();

                if (NowPlaying == null)
                {
                    return;
                }

                if (NowPlaying.Thumbnail == "")
                {
                    NowPlayingImage.Image = new Bitmap(".\\Data\\" + "music-icon.jpg");
                }
                else
                {
                    NowPlayingImage.Image = JsonClient.Files.GetImageFromThumbnail(NowPlaying.Thumbnail);
                }
            }
        } 
 
        private void butFBackward_Click(object sender, EventArgs e)
        {
            EventClient.SendButton("reverse", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butFForward_Click(object sender, EventArgs e)
        {
            EventClient.SendButton("forward", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butPlay_Click(object sender, EventArgs e)
        {
            EventClient.SendButton("play", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butPrev_Click(object sender, EventArgs e)
        {
            EventClient.SendButton("skipminus", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butNext_Click(object sender, EventArgs e)
        {
            EventClient.SendButton("skipplus", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butStop_Click(object sender, EventArgs e)
        {
            EventClient.SendButton("stop", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butPause_Click(object sender, EventArgs e)
        {
            EventClient.SendButton("pause", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void NowPlayingForm_Closing(object sender, CancelEventArgs e)
        {
            updateTimer.Enabled = false;
            JsonClient = null;
            EventClient = null;
        }

        private void menuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}