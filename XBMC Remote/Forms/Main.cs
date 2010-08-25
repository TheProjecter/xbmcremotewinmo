using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

using XbmcEventClient;

using Microsoft.Drawing;

using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;
using StedySoft.SenseSDK.Localization;

namespace XBMC_Remote
{
    #region Delegated
    public delegate void SetIpDelegate(string Ip);
    public delegate void SetConnectLabelDelegate(string Status);
    #endregion

    public partial class MainForm : Form
    {
        #region Declarations
        public string IpAddress;

        private bool _buttonAnimation = true;
        private bool ButtonsEnabled = false;

        EventClient MainClient = new EventClient();
        BackgroundThread backgroundThread = new BackgroundThread();
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            InitializeSettings();
        }
        #endregion

        #region Private Methods
        private bool _isVGA()
        {
            return StedySoft.SenseSDK.DrawingCE.Resolution.ScreenIsVGA;
        }

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

        private void MainForm_Load(object sender, EventArgs e)
        {
            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = 25;
            this.senseListCtrl.ThreadSleep = 75;
            this.senseListCtrl.Velocity = .99f;
            this.senseListCtrl.Springback = .35f;
          
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            string[] list = { "Music", "Movies", "TV Shows", "Pictures", "Now Playing", "Remote Control" };

            foreach (string label in list)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem();
                itm.Height = 96;
                itm.ButtonAnimation = this._buttonAnimation;
                itm.PrimaryText = label;
                itm.IThumbnail = _getIImageFromResource("icon_home_" + label.Split(' ')[0].ToLower());
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();

            // enable Tap n' Hold & auto SIP for SensePanelTextboxItem(s)
            SIP.Enable(this.senseListCtrl.Handle);
        }

        void OnClickGeneric(object Sender)
        {
            MusicForm MusicForm = new MusicForm();
            MusicForm.IpAddress = IpAddress;
            MusicForm.Show();
        }

        void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.senseListCtrl.Clear();
        }

        void MainForm_Closed(object sender, System.EventArgs e)
        {
            this.senseListCtrl.Dispose();
        }

        private void menuConnect_Click(object sender, EventArgs e)
        {
            MainClient.Connect(IpAddress);
            if (MainClient.Connected.Equals(true))
            {
                MainClient.SendHelo("XBMC Remote for WinMo", XbmcEventClient.IconType.ICON_NONE, null);
                backgroundThread.IpAddress = IpAddress;
                backgroundThread.StartBackgroundWorker();
                menuConnect.Text = "Disconnect";
                ButtonsEnabled = true;
            }
            else
            {
                MessageBox.Show("Error connecting to XBMC server at " + IpAddress);
                menuConnect.Text = "Connect";
                ButtonsEnabled = false;
            }
        }

        private void menuOptions_Click(object sender, EventArgs e)
        {
            SettingsForm SettingsForm = new SettingsForm();
            SettingsForm.pIPAdress = IpAddress;
            SettingsForm.SetIpCallback = new SetIpDelegate(this.SetIpCallbackFn);
            SettingsForm.Show();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            MainClient.Disconnect();
            this.Close();
        }

        private void remoteBut_Click(object sender, EventArgs e)
        {
            if (ButtonsEnabled.Equals(true))
            {
                RemoteForm remf = new RemoteForm();
                remf.IpAddress = IpAddress;
                remf.Show();
            }
            else
            {
                MessageBox.Show("Please connect first.");
            }
        }

        private void playingBut_Click(object sender, EventArgs e)
        {
            if (ButtonsEnabled.Equals(true))
            {
                NowPlayingForm playF = new NowPlayingForm();
                playF.IpAddress = IpAddress;
                playF.Show();
            }
            else
            {
                MessageBox.Show("Please connect first.");
            }
        }

        private void moviesBut_Click(object sender, EventArgs e)
        {
            MovieForm MovieForm = new MovieForm();
            MovieForm.IpAddress = IpAddress;
            MovieForm.Show();
        }

        #region Functions
        private void InitializeSettings()
        {
            try
            {
                XmlTextReader settingsReader = new XmlTextReader("\\Application Data\\XBMC_Remote\\settings.xml");
                IpAddress = settingsReader.ReadElementString("IpAddress");
                settingsReader.Close();
            }
            catch
            {
            }
        }
        #endregion

        #region Callbacks
        private void SetConnectLabelCallbackFn(string Status)
        {
            menuConnect.Text = Status;
        }

        private void SetIpCallbackFn(string Ip)
        {
            IpAddress = Ip;
        }
        #endregion
    }
}