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
    public partial class AlbumForm : Form {

        #region Declarations
        private bool _buttonAnimation = true;
        private XbmcConnection JsonClient;
        private int? Artist;
        private List<Album> Albums;
        public string IpAddress;
        #endregion

        #region Constructor
        public AlbumForm()
        {
            InitializeComponent();
        }

        public AlbumForm(int artist)
        {
            InitializeComponent();
            Artist = artist;
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

            if (Artist != null)
                Albums = JsonClient.AudioLibrary.GetAlbumsByArtist((int)Artist);
            else
                Albums = JsonClient.AudioLibrary.GetAlbums(new string[] { "artist" }, new SortParams("label", null, null, null));

            // add SensePanelItem(s) w/thumbnail image
            //this.senseListCtrl.AddItem(new StedySoft.SenseSDK.SensePanelDividerItem("DividerItem" + (this._itmCounter++).ToString("0#"), "Panel Items with Thumbnail"));
            foreach (Album a in Albums)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem(a._id.ToString());
                itm.ButtonAnimation = this._buttonAnimation;
                itm.PrimaryText = a.Label;
                itm.SecondaryText = a.Artist;
                itm.Tag = a._id;
                //itm.Thumbnail = a.Thumbnail;
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            if (Artist != -1)
            {
                StedySoft.SenseSDK.SensePanelDividerItem divider = new StedySoft.SenseSDK.SensePanelDividerItem();  
                this.senseListCtrl.AddItem(divider);

                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem("PanelItemAll");
                itm.ButtonAnimation = this._buttonAnimation;
                itm.PrimaryText = "All Songs";
                itm.Tag = -1;
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }
            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();

            // enable Tap n' Hold & auto SIP for SensePanelTextboxItem(s)
            SIP.Enable(this.senseListCtrl.Handle);
        }

        void OnClickGeneric(object Sender) {
            SongForm SongForm;
            if (Artist != null)
            {
                SongForm = new SongForm((int)Artist, (int)(Sender as SensePanelItem).Tag);
            }
            else
            {
                SongForm = new SongForm((int)(Sender as SensePanelItem).Tag);
            }
            SongForm.IpAddress = IpAddress;
            SongForm.Show();
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
        

        private void AlbumForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Album found = Albums.Find(delegate(Album a) { return (a.Label.Substring(0, 1).ToLower() == e.KeyChar.ToString()); });
            if (found != null)
            {
                this.senseListCtrl.ScrollIntoView(senseListCtrl[found._id.ToString()]);
                this.sip.Enabled = false;
            }
        }

        #endregion
    }
}