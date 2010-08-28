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
    public partial class MusicForm : Form {

        #region Declarations
        private bool _buttonAnimation = true;
        private XbmcConnection JsonClient;
        public string IpAddress;
        #endregion

        #region Constructor
        public MusicForm()
        {
            InitializeComponent();
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

            List<Song> songs = JsonClient.AudioLibrary.GetSongs();

            if (songs == null)
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
                itm.ButtonAnimation = this._buttonAnimation;
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
                        ArtistForm.IpAddress = IpAddress;
                        ArtistForm.Show();
                    }
                    break;
                case "By Album":
                    {
                        AlbumForm AlbumForm = new AlbumForm();
                        AlbumForm.IpAddress = IpAddress;
                        AlbumForm.Show();
                    }
                    break;
            }
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
        #endregion
    }
}