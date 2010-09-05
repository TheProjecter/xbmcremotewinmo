using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Drawing;
using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;
using XbmcJson;

namespace XBMC_Remote {
    public partial class TvForm : Form {

        #region Declarations
        private XbmcConnection JsonClient;

        private List<TvShow> TvShows;
        #endregion

        #region Constructor
        public TvForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void TvForm_Load(object sender, EventArgs e) {
            JsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);

            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = App.Configuration.MinimumMovement;
            this.senseListCtrl.ThreadSleep = App.Configuration.ThreadSleep;
            this.senseListCtrl.Velocity = App.Configuration.Velocity;
            this.senseListCtrl.Springback = App.Configuration.Springback;
          
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            TvShows = JsonClient.VideoLibrary.GetTvShows();

            if (TvShows == null)
            {
                if (SenseAPIs.SenseMessageBox.Show("There are no TV Shows in your library", "Error", SenseMessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.Close();
                }
            }

            foreach (TvShow t in TvShows)
            {
                SenseSDKExtended.SensePanelPictureBox.Style1 itm = new SenseSDKExtended.SensePanelPictureBox.Style1(t._id.ToString());
                
                IImage iimg = Functions.GetTvShowThumbnail(JsonClient, t.Thumbnail);
                iimg.GetThumbnail((uint)StedySoft.SenseSDK.DrawingCE.Resolution.ScreenWidth, 100, out iimg);
                ImageInfo imageinfo;
                iimg.GetImageInfo(out imageinfo);
                
                itm.Image = iimg;
                itm.ImageSize = new Size((int)(imageinfo.Width), (int)(imageinfo.Height));
                iimg = null;
                itm.ButtonAnimation = true;
                itm.Height = (int)(imageinfo.Height);
                itm.Tag = t._id;
                itm.OnClick += new SenseSDKExtended.SensePanelPictureBox.Style1.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();
        }

        void OnClickGeneric(object Sender) {
            SeasonForm SeasonForm = new SeasonForm((int)(Sender as SenseSDKExtended.SensePanelPictureBox.Style1).Tag);
            SeasonForm.Show();
        }

        void TvForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[0]);
            this.senseListCtrl.Clear();
        }

        void TvForm_Closed(object sender, System.EventArgs e) {
            this.senseListCtrl.Dispose();
        }

        private void menuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}