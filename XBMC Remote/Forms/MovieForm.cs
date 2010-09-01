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
    public partial class MovieForm : Form {

        #region Declarations
        private bool _buttonAnimation = true;
        private XbmcConnection JsonClient;
        private List<Movie> Movies;
        
        #endregion

        #region Constructor
        public MovieForm() {
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
            JsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);
            
            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = 25;
            this.senseListCtrl.ThreadSleep = 75;
            this.senseListCtrl.Velocity = .99f;
            this.senseListCtrl.Springback = .35f;

            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            Movies = JsonClient.VideoLibrary.GetMovies();

            if (Movies == null)
            {
                if (SenseAPIs.SenseMessageBox.Show("There are no movies in your library", "Error", SenseMessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.Close();
                }
            }

            // add SensePanelItem(s) w/thumbnail image
            foreach (Movie m in Movies)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem(m._id.ToString());

                itm.ButtonAnimation = this._buttonAnimation;
                itm.PrimaryText = m.Label;
                itm.Tag = m._id;
                //itm.Thumbnail = JsonClient.Files.GetImageFromThumbnail(m.Thumbnail);
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();
        }

        void OnClickGeneric(object Sender) {
            JsonClient.Control.PlayMovie((int)(Sender as SensePanelItem).Tag); 
        }

        void frmListDemo_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.senseListCtrl.Clear();
        }

        void frmListDemo_Closed(object sender, System.EventArgs e) {
            this.senseListCtrl.Dispose();
        }

        private void menuBack_Click(object sender, EventArgs e)
        {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[1]);
            this.Close();
        }

        private void MovieForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Movie found = Movies.Find(delegate(Movie m) { return (m.Label.Substring(0, 1).ToLower() == e.KeyChar.ToString()); });
            if (found != null)
            {
                this.senseListCtrl.ScrollIntoView(senseListCtrl[found._id.ToString()]);
                this.sip.Enabled = false;
            }
        }
        #endregion
    }
}