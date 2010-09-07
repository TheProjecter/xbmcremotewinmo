using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Drawing;
using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;
using XbmcJson;

namespace XBMC_Remote {
    public partial class MovieForm : Form {

        #region Declarations
        private XbmcConnection JsonClient;

        private List<Movie> Movies;
        #endregion

        #region Constructor
        public MovieForm() {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        private IImage _getIImageFromResource(string resource)
        {
            IImage iimg;

            using (MemoryStream strm = (MemoryStream)Assembly.GetExecutingAssembly().GetManifestResourceStream("XBMC_Remote.Resources." + resource + ".png"))
            {
                (ImagingFactory.GetImaging()).CreateImageFromBuffer(strm.GetBuffer(), (uint)strm.Length, BufferDisposalFlag.BufferDisposalFlagNone, out iimg);
            }
            return iimg;
        }
        #endregion

        #region Events
        private void MovieForm_Load(object sender, EventArgs e) {
            JsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);
            
            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = App.Configuration.MinimumMovement;
            this.senseListCtrl.ThreadSleep = App.Configuration.ThreadSleep;
            this.senseListCtrl.Velocity = App.Configuration.Velocity;
            this.senseListCtrl.Springback = App.Configuration.Springback;

            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            Movies = JsonClient.VideoLibrary.GetMovies(new string[] { "movieid" });

            if (Movies == null)
            {
                if (SenseAPIs.SenseMessageBox.Show("There are no movies in your library", "Error", SenseMessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.Close();
                }
            }

            foreach (Movie m in Movies)
            {
                SensePanelItem itm = new SensePanelItem(m._id.ToString());
                itm.ButtonAnimation = true;
                itm.PrimaryText = m.Label;
                itm.Tag = m._id;
                itm.OnClick += new SensePanelItem.ClickEventHandler(itm_OnClick);
                this.senseListCtrl.AddItem(itm);
            }
            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();
        }

        void itm_OnClick(object sender)
        {
            JsonClient.Control.PlayMovie((int)(sender as SensePanelItem).Tag);
            NowPlayingForm NowPlayingForm = new NowPlayingForm();
            NowPlayingForm.Show();
        }

        void MovieForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[0]);
            this.senseListCtrl.Clear();
        }

        void MovieForm_Closed(object sender, System.EventArgs e)
        {
            this.senseListCtrl.Dispose();
        }

        private void menuBack_Click(object sender, EventArgs e)
        {
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