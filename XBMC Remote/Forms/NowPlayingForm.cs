using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using XbmcEventClient;
using XbmcJson;

namespace XBMC_Remote
{
    public partial class NowPlayingForm : Form
    {
        #region Declarations
        private EventClient EventClient = new EventClient();
        private XbmcConnection JsonClient;

        private System.Windows.Forms.Timer updateTimer;

        private string CurrentSong;
        #endregion

        #region Constructor
        public NowPlayingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void InitializeTimer()
        {
            RefreshNowPlaying();

            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 1000;
            updateTimer.Tick += new EventHandler(updateTimer_Tick);
            updateTimer.Enabled = true;
        } 

        private void NowPlayingForm_Load(object sender, EventArgs e)
        {
            JsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);
            EventClient.Connect(App.Configuration.IpAddress);

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

                if (NowPlaying == null)
                {
                    return;
                }

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
            else
            {
                npTitle.Text = "";
                npArtist.Text = "Nothing";
                npAlbum.Text = "";
                npRuntime.Text = "";
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
        #endregion
    }
}