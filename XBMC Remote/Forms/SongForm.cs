using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StedySoft.SenseSDK;
using XbmcJson;

namespace XBMC_Remote {
    public partial class SongForm : Form {

        #region Declarations
        private XbmcConnection JsonClient;

        private int? albumId;
        private int? artistId;
        private List<Song> Songs;
        #endregion

        #region Constructor
        public SongForm(int albumid)
        {
            InitializeComponent();
            albumId = albumid;
        }

        public SongForm(int artistid, int albumid)
        {
            InitializeComponent();
            artistId = artistid;
            albumId = albumid;
        }
        #endregion

        #region Events
        private void SongForm_Load(object sender, EventArgs e) {
            JsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);

            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = App.Configuration.MinimumMovement;
            this.senseListCtrl.ThreadSleep = App.Configuration.ThreadSleep;
            this.senseListCtrl.Velocity = App.Configuration.Velocity;
            this.senseListCtrl.Springback = App.Configuration.Springback;
          
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            if (artistId != null)
            {
                if (albumId == null)
                    Songs = JsonClient.AudioLibrary.GetSongsByArtistAllFields((int)artistId);
                else
                    Songs = JsonClient.AudioLibrary.GetSongsByArtistAndAlbumAllFields((int)artistId, (int)albumId);
            }
            else
            {
                Songs = JsonClient.AudioLibrary.GetSongsByAlbumAllFields((int)albumId);
            }

            foreach (Song s in Songs)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem(s._id.ToString());
                itm.ButtonAnimation = true;
                itm.PrimaryText = s.Label;
                
                itm.SecondaryText = s.Artist;
                itm.Tag = s._id;
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();
        }

        void OnClickGeneric(object Sender) {
            JsonClient.Control.PlaySong((int)(Sender as SensePanelItem).Tag);
            NowPlayingForm NowPlayingForm = new NowPlayingForm();
            NowPlayingForm.Show();
        }

        void SongForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[0]);
            this.senseListCtrl.Clear();
        }

        void SongForm_Closed(object sender, System.EventArgs e)
        {
            this.senseListCtrl.Dispose();
        }

        private void menuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}