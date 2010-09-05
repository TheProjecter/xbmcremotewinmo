using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Drawing;
using StedySoft.SenseSDK;
using XbmcJson;

namespace XBMC_Remote {
    public partial class SeasonForm : Form {

        #region Declarations
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

        #region Events
        private void SeasonForm_Load(object sender, EventArgs e) {
            JsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);

            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = App.Configuration.MinimumMovement;
            this.senseListCtrl.ThreadSleep = App.Configuration.ThreadSleep;
            this.senseListCtrl.Velocity = App.Configuration.Velocity;
            this.senseListCtrl.Springback = App.Configuration.Springback;

            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            Seasons = JsonClient.VideoLibrary.GetSeasonsAllFields(Show, new SortParams("label", null, null, null));

            foreach (Season s in Seasons)
            {
                var episodes = JsonClient.VideoLibrary.GetEpisodes(Show, s._Season);

                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem(s._Season.ToString());
                itm.ButtonAnimation = true;
                itm.PrimaryText = s.Label;
                itm.SecondaryText = episodes.Count.ToString() + " episodes";
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

        void SeasonForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[0]);
            this.senseListCtrl.Clear();
        }

        void SeasonForm_Closed(object sender, System.EventArgs e) {
            this.senseListCtrl.Dispose();
        }

        private void menuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}