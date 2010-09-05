using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;
using XbmcJson;

namespace XBMC_Remote {
    public partial class MusicForm : Form {

        #region Declarations
        private XbmcConnection JsonClient;
        #endregion

        #region Constructor
        public MusicForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void MusicForm_Load(object sender, EventArgs e) {
            JsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);

            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = App.Configuration.MinimumMovement;
            this.senseListCtrl.ThreadSleep = App.Configuration.ThreadSleep;
            this.senseListCtrl.Velocity = App.Configuration.Velocity;
            this.senseListCtrl.Springback = App.Configuration.Springback;
          
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            List<Song> Songs = JsonClient.AudioLibrary.GetSongs();

            if (Songs == null)
            {
                if (SenseAPIs.SenseMessageBox.Show("There is no music in your library", "Error", SenseMessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.Close();
                }
            }

            string[] list = {"By Genre", "By Artist", "By Album"};

            foreach (string Label in list)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem();
                itm.ButtonAnimation = true;
                itm.PrimaryText = Label;
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();
        }

        void OnClickGeneric(object Sender) {

            switch ((Sender as SensePanelItem).PrimaryText)
            {
                case "By Genre":
                    {
                    }
                    break;
                case "By Artist":
                    {
                        ArtistForm ArtistForm = new ArtistForm();
                        ArtistForm.Show();
                    }
                    break;
                case "By Album":
                    {
                        AlbumForm AlbumForm = new AlbumForm();
                        AlbumForm.Show();
                    }
                    break;
            }
        }

        void MusicForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[0]);
            this.senseListCtrl.Clear();
        }

        void MusicForm_Closed(object sender, System.EventArgs e) {
            this.senseListCtrl.Dispose();
        }

        private void menuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}