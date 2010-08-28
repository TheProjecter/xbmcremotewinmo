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
    public partial class ArtistForm : Form {

        #region Declarations
        private bool _buttonAnimation = true;
        private XbmcConnection JsonClient;
        private int Genre;
        private List<Artist> Artists;
        public string IpAddress;
        #endregion

        #region Constructor
        public ArtistForm()
        {
            InitializeComponent();
        }
        public ArtistForm(int genre)
        {
            InitializeComponent();
            Genre = genre;
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
            this.senseListCtrl.MinimumMovement = 25;
            this.senseListCtrl.ThreadSleep = 75;
            this.senseListCtrl.Velocity = .99f;
            this.senseListCtrl.Springback = .35f;
          
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            Artists = JsonClient.AudioLibrary.GetArtists(new SortParams("artist", null, null, null));

            // add SensePanelItem(s) w/thumbnail image
            //this.senseListCtrl.AddItem(new StedySoft.SenseSDK.SensePanelDividerItem("DividerItem" + (this._itmCounter++).ToString("0#"), "Panel Items with Thumbnail"));
            foreach (Artist a in Artists)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem(a._id.ToString());
                itm.ButtonAnimation = this._buttonAnimation;
                itm.PrimaryText = a.Label;
                itm.Tag = a._id;
                //itm.Thumbnail = a.Thumbnail;
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();

            // enable Tap n' Hold & auto SIP for SensePanelTextboxItem(s)
            SIP.Enable(this.senseListCtrl.Handle);
        }

        void OnClickGeneric(object Sender) {
            AlbumForm AlbumForm = new AlbumForm((int)(Sender as SensePanelItem).Tag);
            AlbumForm.IpAddress = IpAddress;
            AlbumForm.Show();
        }

        void frmListDemo_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.senseListCtrl.Clear();
        }

        void frmListDemo_Closed(object sender, System.EventArgs e) {
            this.senseListCtrl.Dispose();
        }

        private void menuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ArtistForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Artist found = Artists.Find(delegate(Artist a) { return (a.Label.Substring(0, 1).ToLower() == e.KeyChar.ToString()); });
            if (found != null)
            {
                this.senseListCtrl.ScrollIntoView(senseListCtrl[found._id.ToString()]);
                this.sip.Enabled = false;
            }
        }
        #endregion
    }
}