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
        
        private HardwareButton hwb1, hwb4;

        public string IpAddress;

        public NowPlayingForm()
        {
            InitializeComponent();

            this.KeyPreview = true;

            // Call the method to configure
            // the hardware button.
            HBConfig();
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

        private void NowPlayingForm_LostFocus(object sender, EventArgs e)
        {
            /*
            updateTimer.Dispose();
            this.Dispose();
            this.Close();
            */
        }

        private void HBConfig()
        {
            try
            {
                hwb1 = new HardwareButton();
                hwb4 = new HardwareButton();
                hwb1.AssociatedControl = this;
                hwb4.AssociatedControl = this;
                hwb1.HardwareKey = HardwareKeys.ApplicationKey2;
                hwb4.HardwareKey = HardwareKeys.ApplicationKey3;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + " Check if the hardware button is physically available on this device.");
            }
        }

        private void NowPlayingForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch ((HardwareKeys)e.KeyCode)
            {
                case HardwareKeys.ApplicationKey1:
                    MessageBox.Show("Button 1 pressed.");
                    break;

                case HardwareKeys.ApplicationKey4:
                    MessageBox.Show("Button 4 pressed.");
                    break;

                default:
                    break;
            }
        }
    }
}