using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using XbmcJson;

using Microsoft.Drawing;

using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;
using StedySoft.SenseSDK.Localization;

namespace XBMC_Remote {
    public partial class SongForm : Form {

        #region Declarations
        private bool _buttonAnimation = true;
        private int albumId = -1;
        private int artistId = -1;
        private XbmcConnection JsonClient;
        private List<Song> Songs;
        public string IpAddress;
        #endregion

        #region Constructor
        public SongForm(int albumid)
        {
            albumId = albumid;
            InitializeComponent();
        }

        public SongForm(int artistid, int albumid)
        {
            artistId = artistid;
            albumId = albumid;
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        private bool _isVGA() {
            return StedySoft.SenseSDK.DrawingCE.Resolution.ScreenIsVGA;
        }
        #endregion

        #region Events
        private void frmListDemo_Load(object sender, EventArgs e) {
            JsonClient = new XbmcConnection(IpAddress, 8080, "", "");

            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = 15;
            this.senseListCtrl.ThreadSleep = 100;
            this.senseListCtrl.Velocity = .99f;
            this.senseListCtrl.Springback = .35f;
          
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            if (artistId != -1)
                if (albumId == -1)
                    Songs = JsonClient.AudioLibrary.GetSongsByArtistAllFields(artistId);
                else
                Songs = JsonClient.AudioLibrary.GetSongsByArtistAndAlbumAllFields(artistId, albumId);
            else
            {
                    Songs = JsonClient.AudioLibrary.GetSongsByAlbumAllFields(albumId);
            }

            foreach (Song s in Songs)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem(s._id.ToString());
                itm.ButtonAnimation = this._buttonAnimation;
                itm.PrimaryText = s.Label;
                itm.SecondaryText = s.Artist;
                itm.Tag = s._id;
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();

            // enable Tap n' Hold & auto SIP for SensePanelTextboxItem(s)
            SIP.Enable(this.senseListCtrl.Handle);
            this.sip.EnabledChanged += new EventHandler(sip_EnabledChanged);
        }

        void OnClickGeneric(object Sender) {
            JsonClient.Control.PlaySong((int)(Sender as SensePanelItem).Tag); 
        }

        void frmListDemo_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.senseListCtrl.Clear();
        }

        void frmListDemo_Closed(object sender, System.EventArgs e) {
            this.senseListCtrl.Dispose();
        }

        private int _sipOffset = 0;
        void sip_EnabledChanged(object sender, EventArgs e) {
            if (this.sip.Enabled) {
                SenseListControl.ISenseListItem IItem = this.senseListCtrl.FocusedItem;
                Rectangle r = IItem.ClientRectangle;
                r.Offset(0, this.senseListCtrl.Bounds.Top);
                if (IItem is SensePanelTextboxItem) {
                    if (r.Bottom > this.sip.VisibleDesktop.Height) {
                        this._sipOffset = Math.Abs(this.sip.VisibleDesktop.Height - r.Bottom);
                        this.senseListCtrl.ScrollList(-this._sipOffset);
                        this.senseListCtrl.Invalidate();
                    }
                }
            }
            else {
                if (!this._sipOffset.Equals(0)) {
                    this.senseListCtrl.ScrollList(this._sipOffset);
                    this.senseListCtrl.Invalidate();
                }
                this._sipOffset = 0;
            }
        }

        private void menuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}