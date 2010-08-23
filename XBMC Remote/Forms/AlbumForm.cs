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
    public partial class AlbumForm : Form {

        #region Declarations
        private bool _processEntireButton = false;
        private bool _buttonAnimation = true;
        private XbmcConnection JsonClient;
        private List<Album> Albums;
        private List<Song> Songs;
        public string IpAddress;
        #endregion

        #region Constructor
        public AlbumForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        private bool _isVGA() {
            return StedySoft.SenseSDK.DrawingCE.Resolution.ScreenIsVGA;
        }

        private IImage _getIImageFromResource(string resource) {
            IImage iimg;
            using (MemoryStream strm = (MemoryStream)Assembly.GetExecutingAssembly().GetManifestResourceStream("SenseSDKDemo.Resources." + resource + ".png")) {
                (ImagingFactory.GetImaging()).CreateImageFromBuffer(strm.GetBuffer(), (uint)strm.Length, BufferDisposalFlag.BufferDisposalFlagNone, out iimg);
            }
            return iimg;
        }
        #endregion

        #region Events
        private void frmListDemo_Load(object sender, EventArgs e) {
            JsonClient = new XbmcConnection(IpAddress, 8080, "", "");

            // set the list event handlers
            this.senseListCtrl.OnBeforeShowItemChildren += new SenseListControl.BeforeShowItemChildrenHandler(OnBeforeShowItemChildren);
            this.senseListCtrl.OnShowItemChildren += new SenseListControl.ShowItemChildrenHandler(OnShowItemChildren);
            this.senseListCtrl.OnHideItemChildren += new SenseListControl.HideItemChildrenHandler(OnHideItemChildren);

            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = 25;
            this.senseListCtrl.ThreadSleep = 75;
            this.senseListCtrl.Velocity = .99f;
            this.senseListCtrl.Springback = .35f;
          
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            Albums = JsonClient.AudioLibrary.GetAlbumsAllFields("artist");

            // add SensePanelItem(s) w/thumbnail image
            //this.senseListCtrl.AddItem(new StedySoft.SenseSDK.SensePanelDividerItem("DividerItem" + (this._itmCounter++).ToString("0#"), "Panel Items with Thumbnail"));
            foreach (Album a in Albums)
            {
                StedySoft.SenseSDK.SensePanelMoreItem itmMore = new StedySoft.SenseSDK.SensePanelMoreItem("PanelItem" + a._id + a.Label);
                itmMore.ButtonAnimation = this._buttonAnimation;
                itmMore.ProcessEntireButton = this._processEntireButton;
                itmMore.PrimaryText = a.Label;
                itmMore.SecondaryText = a.Artist;
                itmMore.Tag = a._id;
                //itmMore.Thumbnail = a.Thumbnail;
                itmMore.OnClick += new SensePanelMoreItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itmMore);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();

            // enable Tap n' Hold & auto SIP for SensePanelTextboxItem(s)
            SIP.Enable(this.senseListCtrl.Handle);
            this.sip.EnabledChanged += new EventHandler(sip_EnabledChanged);
        }

        void OnBeforeShowItemChildren(SenseListControl.ISenseListItem Sender, ref bool Cancel)
        {
            SensePanelMoreItem itmMore = Sender as SensePanelMoreItem;
            // only add child items once!!
            if (itmMore.Children.Count != 0) { return; }
            // add SensePanelRadioItem(s) in Child Container

            Songs = JsonClient.AudioLibrary.GetSongsByAlbum((int)Sender.Tag);

            foreach (Song s in Songs)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem("PanelItem" + s.Label);
                itm.ButtonAnimation = this._buttonAnimation;
                itm.PrimaryText = s.Label;
                itm.Tag = s._id;
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickSong);
                itmMore.Children.Add(itm);
            }
        }

        private int _childLevel = -1;
        void OnShowItemChildren(SenseListControl.ISenseListItem Sender)
        {
            this._childLevel++;
        }

        void OnHideItemChildren(SenseListControl.ISenseListItem Sender)
        {
            this._childLevel--;
        }

        void OnClickGeneric(object Sender) {
            senseListCtrl.ShowChildren();
        }

        void OnClickSong(object Sender) {
            JsonClient.Control.PlaySong((int)(Sender as SensePanelItem).Tag); 
        }

        void frmListDemo_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.senseListCtrl.OnBeforeShowItemChildren -= new SenseListControl.BeforeShowItemChildrenHandler(OnBeforeShowItemChildren);
            this.senseListCtrl.OnShowItemChildren -= new SenseListControl.ShowItemChildrenHandler(OnShowItemChildren);
            this.senseListCtrl.OnHideItemChildren -= new SenseListControl.HideItemChildrenHandler(OnHideItemChildren);
            this.senseListCtrl.Clear();
        }

        void frmListDemo_Closed(object sender, System.EventArgs e) {
            this.senseListCtrl.Dispose();
        }

        private int _sipOffset = 0;
        void sip_EnabledChanged(object sender, EventArgs e) {
            if (this.sip.Enabled) {
                SenseListControl.ISenseListItem IItem = this.senseListCtrl.FocusedItem;
                Rectangle r = IItem.ClientRectangle;
                r.Offset(0, this.senseListCtrl.Bounds.Top);
                if (IItem is SensePanelTextboxItem) {
                    if (r.Bottom > this.sip.VisibleDesktop.Height) {
                        this._sipOffset = Math.Abs(this.sip.VisibleDesktop.Height - r.Bottom);
                        this.senseListCtrl.ScrollList(-this._sipOffset);
                        this.senseListCtrl.Invalidate();
                    }
                }
            }
            else {
                if (!this._sipOffset.Equals(0)) {
                    this.senseListCtrl.ScrollList(this._sipOffset);
                    this.senseListCtrl.Invalidate();
                }
                this._sipOffset = 0;
            }
        }

        private void menuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}