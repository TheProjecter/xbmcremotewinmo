using System;
using System.Windows.Forms;
using Microsoft.Drawing;
using StedySoft.SenseSDK;

namespace XBMC_Remote
{
    public partial class SettingsForm : Form
    {
        #region Events
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = App.Configuration.MinimumMovement;
            this.senseListCtrl.ThreadSleep = App.Configuration.ThreadSleep;
            this.senseListCtrl.Velocity = App.Configuration.Velocity;
            this.senseListCtrl.Springback = App.Configuration.Springback;

            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            StedySoft.SenseSDK.SensePanelTextboxItem TbIpAddress = new StedySoft.SenseSDK.SensePanelTextboxItem("IpAddress");
            TbIpAddress.LabelWidth = 40;
            TbIpAddress.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
            TbIpAddress.ShowSeparator = true;
            TbIpAddress.LabelText = "Ip address:";
            TbIpAddress.MaxLength = 15;
            TbIpAddress.Text = App.Configuration.IpAddress;
            TbIpAddress.GotFocus += new EventHandler(TbItm_GotFocus);
            TbIpAddress.LostFocus += new EventHandler(TbItm_LostFocus);
            this.senseListCtrl.AddItem(TbIpAddress);

            StedySoft.SenseSDK.SensePanelTextboxItem TbWebPort = new StedySoft.SenseSDK.SensePanelTextboxItem("WebPort");
            TbWebPort.LabelWidth = 40;
            TbWebPort.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
            TbWebPort.ShowSeparator = true;
            TbWebPort.LabelText = "Web Port:";
            TbWebPort.MaxLength = 5;
            TbWebPort.Text = App.Configuration.WebPort;
            TbWebPort.GotFocus += new EventHandler(TbItm_GotFocus);
            TbWebPort.LostFocus += new EventHandler(TbItm_LostFocus);
            this.senseListCtrl.AddItem(TbWebPort);

            StedySoft.SenseSDK.SensePanelTextboxItem TbUsername = new StedySoft.SenseSDK.SensePanelTextboxItem("Username");
            TbUsername.LabelWidth = 40;
            TbUsername.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
            TbUsername.ShowSeparator = true;
            TbUsername.LabelText = "Username:";
            TbUsername.MaxLength = 25;
            TbUsername.Text = App.Configuration.Username;
            TbUsername.GotFocus += new EventHandler(TbItm_GotFocus);
            TbUsername.LostFocus += new EventHandler(TbItm_LostFocus);
            this.senseListCtrl.AddItem(TbUsername);

            StedySoft.SenseSDK.SensePanelTextboxItem TbPassword = new StedySoft.SenseSDK.SensePanelTextboxItem("Password");
            TbPassword.LabelWidth = 40;
            TbPassword.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
            TbPassword.ShowSeparator = true;
            TbPassword.LabelText = "Password:";
            TbPassword.MaxLength = 25;
            TbPassword.Text = App.Configuration.Password;
            TbPassword.GotFocus += new EventHandler(TbItm_GotFocus);
            TbPassword.LostFocus += new EventHandler(TbItm_LostFocus);
            this.senseListCtrl.AddItem(TbPassword);

            StedySoft.SenseSDK.SensePanelTextboxItem TbTimeOut = new StedySoft.SenseSDK.SensePanelTextboxItem("Timeout");
            TbTimeOut.LabelWidth = 40;
            TbTimeOut.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
            TbTimeOut.ShowSeparator = true;
            TbTimeOut.LabelText = "Custom timeout:";
            TbTimeOut.MaxLength = 25;
            TbTimeOut.Text = App.Configuration.Timeout;
            TbTimeOut.GotFocus += new EventHandler(TbItm_GotFocus);
            TbTimeOut.LostFocus += new EventHandler(TbItm_LostFocus);
            this.senseListCtrl.AddItem(TbTimeOut);

            this.senseListCtrl.EndUpdate();
        }

        void TbItm_GotFocus(object sender, EventArgs e)
        {
            sip.Enabled = true;
        }

        void TbItm_LostFocus(object sender, EventArgs e)
        {
            sip.Enabled = false;
        }

        private void menuOK_Click(object sender, EventArgs e)
        {
            App.Configuration.IpAddress = (senseListCtrl["IpAddress"] as SensePanelTextboxItem).Text;
            App.Configuration.WebPort = (senseListCtrl["WebPort"] as SensePanelTextboxItem).Text;
            App.Configuration.Username = (senseListCtrl["Username"] as SensePanelTextboxItem).Text;
            App.Configuration.Password = (senseListCtrl["Password"] as SensePanelTextboxItem).Text;
            App.Configuration.Timeout = (senseListCtrl["Timeout"] as SensePanelTextboxItem).Text;
            App.Configuration.WriteSettings();
            this.Close();
        }

        void SettingsForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[0]);
            this.senseListCtrl.Clear();
        }

        void SettingsForm_Closed(object sender, System.EventArgs e)
        {
            this.senseListCtrl.Dispose();
        }

        private void menuCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}