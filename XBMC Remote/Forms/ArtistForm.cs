using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StedySoft.SenseSDK;
using XbmcJson;

namespace XBMC_Remote {
    public partial class ArtistForm : Form {

        #region Declarations
        private XbmcConnection JsonClient;

        private int Genre;
        private List<Artist> Artists;
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

        #region Events
        private void ArtistForm_Load(object sender, EventArgs e) {
            JsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);

            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = App.Configuration.MinimumMovement;
            this.senseListCtrl.ThreadSleep = App.Configuration.ThreadSleep;
            this.senseListCtrl.Velocity = App.Configuration.Velocity;
            this.senseListCtrl.Springback = App.Configuration.Springback;

          
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            Artists = JsonClient.AudioLibrary.GetArtists(new SortParams("artist", null, null, null));

            foreach (Artist a in Artists)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem(a._id.ToString());
                itm.ButtonAnimation = true;
                itm.PrimaryText = a.Label;
                itm.Tag = a._id;
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();
        }

        void OnClickGeneric(object Sender) {
            AlbumForm AlbumForm = new AlbumForm((int)(Sender as SensePanelItem).Tag);
            AlbumForm.Show();
        }

        void ArtistForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[0]);
            this.senseListCtrl.Clear();
        }

        void ArtistForm_Closed(object sender, System.EventArgs e)
        {
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