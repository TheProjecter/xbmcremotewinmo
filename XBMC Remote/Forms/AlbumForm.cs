using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StedySoft.SenseSDK;
using XbmcJson;

namespace XBMC_Remote {
    public partial class AlbumForm : Form {

        #region Declarations
        private XbmcConnection JsonClient;

        private int? Artist;
        private List<Album> Albums;
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

        #region Events
        private void AlbumForm_load (object sender, EventArgs e) {
            JsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);

            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = App.Configuration.MinimumMovement;
            this.senseListCtrl.ThreadSleep = App.Configuration.ThreadSleep;
            this.senseListCtrl.Velocity = App.Configuration.Velocity;
            this.senseListCtrl.Springback = App.Configuration.Springback;
          
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            if (Artist != null)
                Albums = JsonClient.AudioLibrary.GetAlbumsByArtist((int)Artist);
            else
                Albums = JsonClient.AudioLibrary.GetAlbums(new string[] { "artist" }, new SortParams("label", null, null, null));

            foreach (Album a in Albums)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem(a._id.ToString());
                itm.ButtonAnimation = true;
                itm.PrimaryText = a.Label;
                itm.SecondaryText = a.Artist;
                itm.Tag = a._id;
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            if (Artist != null)
            {
                StedySoft.SenseSDK.SensePanelDividerItem divider = new StedySoft.SenseSDK.SensePanelDividerItem();  
                this.senseListCtrl.AddItem(divider);

                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem("PanelItemAll");
                itm.ButtonAnimation = true;
                itm.PrimaryText = "All Songs";
                itm.Tag = -1;
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();
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
            SongForm.Show();
        }

        void AlbumForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[0]);
            this.senseListCtrl.Clear();
        }

        void AlbumForm_Closed(object sender, System.EventArgs e)
        {
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