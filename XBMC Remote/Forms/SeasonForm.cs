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
    public partial class SeasonForm : Form {

        #region Declarations
        private bool _buttonAnimation = true;
        private XbmcConnection JsonClient;
        private new int Show = -1;
        private List<Season> Seasons;
        
        #endregion

        #region Constructor
        public SeasonForm(int show)
        {
            InitializeComponent();
            Show = show;
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

            Seasons = JsonClient.VideoLibrary.GetSeasonsAllFields(Show, new SortParams("label", null, null, null));

            foreach (Season s in Seasons)
            {
                var episodes = JsonClient.VideoLibrary.GetEpisodes(Show, s._Season, new string[] { });
                episodes.Count.ToString();
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem(s._Season.ToString());
                itm.ButtonAnimation = this._buttonAnimation;
                itm.PrimaryText = s.Label;
                itm.SecondaryText = episodes.Count.ToString() + "Episodes";
                itm.Tag = s._Season;
                IImage thumbImage = Functions.GetTvSeasonThumbnail(JsonClient, s.Thumbnail);
                ImageInfo info;
                thumbImage.GetImageInfo(out info);
                itm.IThumbnail = thumbImage;
                itm.Height = (int)(info.Height);

                thumbImage = null;

                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();
        }

        void OnClickGeneric(object Sender) {
            EpisodeForm EpisodeForm = new EpisodeForm(Show, (int)(Sender as SensePanelItem).Tag);
            EpisodeForm.Show();
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
        #endregion
    }
}