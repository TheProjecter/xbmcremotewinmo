using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.Drawing;
using Microsoft.WindowsMobile.PocketOutlook;
using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;
using XbmcEventClient;
using XbmcJson;


namespace XBMC_Remote
{
    public partial class MainForm : Form
    {
        #region Declarations
        private string Caller;

        private bool ListItemsShown = false;

        private EventClient MainEventClient = new EventClient();
        private XbmcJson.XbmcConnection MainJsonClient;

        private Timer backgroundTimer = new Timer();
        private Timer connectTimer = new Timer();

        private NewMsgWindow msgWin;

        #endregion

        #region Constructor
        public MainForm()
        {
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

        private void ShowListItems()
        {
            if (ListItemsShown)
                return;

            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            this.senseListCtrl.Clear();

            string[] list = { "Music", "Movies", "TV Shows", "Pictures", "Now Playing", "Remote Control" };

            foreach (string label in list)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem();
                itm.Height = 100;
                itm.ButtonAnimation = true;
                itm.PrimaryText = label;
                itm.IThumbnail = _getIImageFromResource("icon_home_" + label.Split(' ')[0].ToLower());
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();

            ListItemsShown = true;
        }

        private void HideListItems()
        {
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            this.senseListCtrl.Clear();

            string[] list = { "Connecting" };

            foreach (string label in list)
            {
                StedySoft.SenseSDK.SensePanelItem itm = new StedySoft.SenseSDK.SensePanelItem();
                itm.Height = 100;
                itm.ButtonAnimation = true;
                itm.PrimaryText = label;
                //itm.IThumbnail = _getIImageFromResource("icon_home_" + label.Split(' ')[0].ToLower());
                itm.OnClick += new SensePanelItem.ClickEventHandler(OnClickGeneric);
                this.senseListCtrl.AddItem(itm);
            }

            // we are done so turn on UI updating
            this.senseListCtrl.EndUpdate();
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

        #region Events
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set up SMS interception
            msgWin = new NewMsgWindow();
            msgWin.Text = "NewSMSWatcher";
            msgWin.OnNewTextMessage += new NewMsgWindow.NewTextMessageEventHandler(OnSmsReceived);

            // erase the json-log
            XbmcJson.DebugLog.EraseLog();

            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = App.Configuration.MinimumMovement;
            this.senseListCtrl.ThreadSleep = App.Configuration.ThreadSleep;
            this.senseListCtrl.Velocity = App.Configuration.Velocity;
            this.senseListCtrl.Springback = App.Configuration.Springback;

            // setup the timers
            backgroundTimer.Tick += new EventHandler(backgroundTimer_Tick);
            backgroundTimer.Interval = 500;

            connectTimer.Tick += new EventHandler(connectTimer_Tick);
            connectTimer.Interval = 2000;

            if (App.Configuration.IpAddress == null)
            {
                SettingsForm SettingsForm = new SettingsForm();
                SettingsForm.Closed += new EventHandler(SettingsForm_Closed);
                SettingsForm.Show();
            }
            else
            {
                MainJsonClient = new XbmcConnection(App.Configuration.IpAddress, Convert.ToInt32(App.Configuration.WebPort), App.Configuration.Username, App.Configuration.Password);
                connectTimer_Tick(null, null);
                connectTimer.Enabled = true;
            }
        }

        void SettingsForm_Closed(object sender, EventArgs e)
        {
            connectTimer.Enabled = true;
        }

        void connectTimer_Tick(object sender, EventArgs e)
        {
            if (MainJsonClient.Status.IsConnected)
            {
                if (MainEventClient.Connected.Equals(true))
                {
                    ShowListItems();
                    if (connectTimer.Interval != 8000)
                        MainEventClient.SendHelo("XBMC Remote for WinMo", XbmcEventClient.IconType.ICON_NONE, null);
                    backgroundTimer.Enabled = true;
                    connectTimer.Interval = 8000;
                }
                else
                    MainEventClient.Connect(App.Configuration.IpAddress);

            }
            else
            {
                HideListItems();
                backgroundTimer.Enabled = false;
                connectTimer.Enabled = false;
                if (SenseAPIs.SenseMessageBox.Show("Error connecting to XBMC server at " + App.Configuration.IpAddress, "Error", SenseMessageBoxButtons.OK) == DialogResult.OK)
                {
                    connectTimer.Enabled = true;
                    connectTimer.Interval = 5000;
                }
            }

        }

        void OnClickGeneric(object Sender)
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

        void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[0]);
            this.senseListCtrl.Clear();
        }

        void MainForm_Closed(object sender, System.EventArgs e)
        {
            this.senseListCtrl.Dispose();
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
            string msgFrom = null;
            using (OutlookSession session = new OutlookSession())
            {
                foreach (Contact c in session.Contacts.Items)
                {
                    if (c.MobileTelephoneNumber.Length != 0
                        && PhoneNumbersMatch(c.MobileTelephoneNumber, sender))
                    {
                        // We have a match
                        msgFrom = c.FirstName + c.LastName;
                        break;
                    }
                }

                if (msgFrom == null)
                {
                    msgFrom = sender;
                }
            }

            MainEventClient.SendNotification("Incoming message from " + msgFrom, messageText, IconType.ICON_NONE, null);
            return true;
        }
        #endregion
    }
}