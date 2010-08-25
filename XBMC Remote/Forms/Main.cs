using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using XbmcEventClient;

namespace XBMC_Remote
{
    public delegate void SetIpDelegate(string Ip);
    public delegate void SetConnectLabelDelegate(string Status);

    public partial class MainForm : Form
    {
        public string IpAddress;

        EventClient MainClient = new EventClient();
        BackgroundThread backgroundThread = new BackgroundThread();

        bool ButtonsEnabled = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeProgram();
            InitializeSettings();
        }

        private void InitializeProgram()
        {
        }

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

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void SetConnectLabelCallbackFn(string Status)
        {
            menuConnect.Text = Status;
        }

        private void SetIpCallbackFn(string Ip)
        {
            IpAddress = Ip;
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

        private void musicBut_Click(object sender, EventArgs e)
        {
            MusicForm MusicForm = new MusicForm();
            MusicForm.IpAddress = IpAddress;
            MusicForm.Show();
        }
    }
}