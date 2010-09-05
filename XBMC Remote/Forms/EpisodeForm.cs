using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StedySoft.SenseSDK;
using XbmcJson;

namespace XBMC_Remote {
    public partial class EpisodeForm : Form {

        #region Declarations
        private XbmcConnection JsonClient;

        private new int Show = -1;
        private int Season = -1;
        private List<Episode> Episodes;
        
        #endregion

        #region Constructor
        public EpisodeForm(int show, int season)
        {
            InitializeComponent();
            Show = show;
            Season = season;
        }
        #endregion

        #region Events
        private void EpisodeForm_Load(object sender, EventArgs e) {
            JsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);

            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = App.Configuration.MinimumMovement;
            this.senseListCtrl.ThreadSleep = App.Configuration.ThreadSleep;
            this.senseListCtrl.Velocity = App.Configuration.Velocity;
            this.senseListCtrl.Springback = App.Configuration.Springback;
          
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            Episodes = JsonClient.VideoLibrary.GetEpisodesAllFields(Show, Season, null);

            foreach (Episode ep in Episodes)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem(ep._id.ToString());
                itm.ButtonAnimation = true;
                itm.PrimaryText = ep.Label;
                itm.SecondaryText = "Episode: " + ep.EpisodeNum.ToString();
                itm.Tag = ep._id;
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();
        }

        void OnClickGeneric(object Sender) {
            JsonClient.Control.PlayEpisode(Show, Season, (int)(Sender as SensePanelItem).Tag);
            NowPlayingForm NowPlayingForm = new NowPlayingForm();
            NowPlayingForm.Show();
        }

        void EpisodeForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[0]);
            this.senseListCtrl.Clear();
        }

        void EpisodeForm_Closed(object sender, System.EventArgs e) 
        {
            this.senseListCtrl.Dispose();
        }

        private void menuBack_Click(object sender, EventArgs e)
        {    
            this.Close();
        }
        #endregion
    }
}