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

using XbmcEventClient;
using XbmcJson;

using Microsoft.Drawing;

using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;
using StedySoft.SenseSDK.Localization;

using Microsoft.WindowsMobile.PocketOutlook;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;

using OpenNETCF.Net;
using OpenNETCF.Net.NetworkInformation;

namespace XBMC_Remote
{
    #region Delegated
    public delegate void SetIpDelegate(string Ip);
    public delegate void SetConnectLabelDelegate(string Status);
    #endregion

    public partial class MainForm : Form
    {
        #region Declarations
        private string Caller;

        private bool _buttonAnimation = true;
        private EventClient MainEventClient = new EventClient();
        private XbmcJson.XbmcConnection MainJsonClient;

        private System.Windows.Forms.Timer backgroundTimer = new System.Windows.Forms.Timer();

        // the message window to watch for for notification from RTRule.dll
        private NewMsgWindow msgWin;

        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
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

        #region Events
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set up SMS interception
            // RTRule.dll will send a predefined message to this window
            // when a new incoming text message is received.
            msgWin = new NewMsgWindow();
            msgWin.Text = "NewSMSWatcher";
            msgWin.OnNewTextMessage += new NewMsgWindow.NewTextMessageEventHandler(OnSmsReceived);

            // erase the json-log
            XbmcJson.DebugLog.EraseLog();

            // setup the timer
            backgroundTimer.Tick += new EventHandler(backgroundTimer_Tick);
            backgroundTimer.Interval = 500;

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
                itm.Height = 100;
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
            MainJsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);

            if (MainJsonClient.Status.IsConnected == true)
            {
                Cursor.Current = Cursors.WaitCursor;
                Cursor.Show();
                switch ((Sender as SensePanelItem).PrimaryText.Split(' ')[0].ToLower())
                {
                    case "music":
                        MusicForm MusicForm = new MusicForm();
                        MusicForm.Show();
                        break;
                    case "movies":
                        MovieForm MovieForm = new MovieForm();
                        MovieForm.Show();
                        break;
                    case "tv":
                        TvForm TvForm = new TvForm();
                        TvForm.Show();
                        break;
                    case "pictures":
                        break;
                    case "now":
                        NowPlayingForm NowPlayingForm = new NowPlayingForm();
                        NowPlayingForm.Show();
                        break;
                    case "remote":
                        RemoteForm RemoteForm = new RemoteForm();
                        RemoteForm.Show();
                        break;
                }
                Cursor.Current = Cursors.Default;
                Cursor.Hide();
            }
            else
            {
                SenseAPIs.SenseMessageBox.Show("Could not connect! Please check your settings", "Error", SenseMessageBoxButtons.OK);
            }
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
            if (menuConnect.Text == "Connect")
            {
                MainEventClient.Connect(App.Configuration.IpAddress);
                if (MainEventClient.Connected.Equals(true))
                {
                    MainEventClient.SendHelo("XBMC Remote for WinMo", XbmcEventClient.IconType.ICON_NONE, null);
                    menuConnect.Text = "Disconnect";
                    backgroundTimer.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Error connecting to XBMC server at " + App.Configuration.IpAddress);
                    menuConnect.Text = "Connect";
                }
            }
            else
            {
                backgroundTimer.Enabled = false;
                menuConnect.Text = "Connect";
            }
        }

        private void menuOptions_Click(object sender, EventArgs e)
        {
            SettingsForm SettingsForm = new SettingsForm();
            SettingsForm.Show();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            MainEventClient.Disconnect();
            this.Close();
        }

        void backgroundTimer_Tick(object sender, EventArgs e)
        {
            if (Microsoft.WindowsMobile.Status.SystemState.PhoneIncomingCall == true)
            {
                if (Microsoft.WindowsMobile.Status.SystemState.PhoneIncomingCallerName != null)
                {
                    Caller = Microsoft.WindowsMobile.Status.SystemState.PhoneIncomingCallerName;
                }
                else
                {
                    Caller = Microsoft.WindowsMobile.Status.SystemState.PhoneIncomingCallerNumber;
                }
                MainEventClient.SendButton("pause", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
                MainEventClient.SendNotification("Incoming Call", Caller, IconType.ICON_NONE, null);
                while (Microsoft.WindowsMobile.Status.SystemState.PhoneIncomingCall != false)
                {
                }
                while (Microsoft.WindowsMobile.Status.SystemState.PhoneCallTalking != false)
                {
                }
                MainEventClient.SendButton("play", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
            }
        }

        private bool OnSmsReceived(string sender, string messageText)
        {
            Contact contact = null;
            using (OutlookSession session = new OutlookSession())
            {
                foreach (Contact c in session.Contacts.Items)
                {
                    if (c.MobileTelephoneNumber.Length != 0
                        && PhoneNumbersMatch(c.MobileTelephoneNumber, sender))
                    {
                        // We have a match
                        contact = c;
                        break;
                    }
                }

                if (contact == null)
                {
                    // There is no contact for this phone number
                    // so create one
                    contact = new Contact();
                    contact.FirstName = "Unknown";
                    contact.MobileTelephoneNumber = sender;
                }
            }
            MainEventClient.SendNotification("Incoming message from " + contact.FirstName, messageText, IconType.ICON_NONE, null);
            contact.Delete();
            return true;
        }

        private static string NormalizePhoneNumber(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= '0' && s[i] <= '9')
                {
                    sb.Append(s[i]);
                }
            }
            return sb.ToString();
        }

        private static bool PhoneNumbersMatch(string s1, string s2)
        {
            string num1 = NormalizePhoneNumber(s1);
            string num2 = NormalizePhoneNumber(s2);
            return num1.Substring(Math.Max(0, num1.Length - 7)) ==
                   num2.Substring(Math.Max(0, num2.Length - 7));
        }
        #endregion
    }
}