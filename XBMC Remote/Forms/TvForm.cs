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
    public partial class TvForm : Form {

        #region Declarations
        private bool _buttonAnimation = true;
        private XbmcConnection JsonClient;
        private List<TvShow> TvShows;
        public string IpAddress;
        #endregion

        #region Constructor
        public TvForm()
        {
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
            this.senseListCtrl.MinimumMovement = 25;
            this.senseListCtrl.ThreadSleep = 75;
            this.senseListCtrl.Velocity = .99f;
            this.senseListCtrl.Springback = .35f;
          
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
                imageinfo = null;
                iimg = null;
                itm.ButtonAnimation = this._buttonAnimation;
                itm.Height = (int)(imageinfo.Height);
                itm.Tag = t._id;
                itm.OnClick += new SenseSDKExtended.SensePanelPictureBox.Style1.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();

            // enable Tap n' Hold & auto SIP for SensePanelTextboxItem(s)
            SIP.Enable(this.senseListCtrl.Handle);
        }

        void OnClickGeneric(object Sender) {
            SeasonForm SeasonForm = new SeasonForm((int)(Sender as SenseSDKExtended.SensePanelPictureBox.Style1).Tag);
            SeasonForm.IpAddress = IpAddress;
            SeasonForm.Show();
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