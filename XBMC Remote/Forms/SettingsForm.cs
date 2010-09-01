using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using Microsoft.Drawing;

using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;
using StedySoft.SenseSDK.Localization;

namespace XBMC_Remote
{
    public partial class SettingsForm : Form
    {
        #region Private Methods
        private bool _isVGA()
        {
            return StedySoft.SenseSDK.DrawingCE.Resolution.ScreenIsVGA;
        }

        private IImage _getIImageFromResource(string resource)
        {
            return null;
        }
        #endregion

        #region Events
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // set the list scroll fluidness
            this.senseListCtrl.MinimumMovement = 15;
            this.senseListCtrl.ThreadSleep = 100;
            this.senseListCtrl.Velocity = .99f;
            this.senseListCtrl.Springback = .35f;

            // turn off UI updating
            this.senseListCtrl.BeginUpdate();

            StedySoft.SenseSDK.SensePanelTextboxItem TbItm = new StedySoft.SenseSDK.SensePanelTextboxItem("IpAddress");
            TbItm.LabelWidth = 40;
            TbItm.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
            TbItm.ShowSeparator = true;
            TbItm.LabelText = "Ip address:";
            TbItm.MaxLength = 15;
            TbItm.Text = App.Configuration.IpAddress;
            TbItm.GotFocus += new EventHandler(TbItm_GotFocus);
            TbItm.LostFocus += new EventHandler(TbItm_LostFocus);
            this.senseListCtrl.AddItem(TbItm);

            StedySoft.SenseSDK.SensePanelTextboxItem MeItm = new StedySoft.SenseSDK.SensePanelTextboxItem("WebPort");
            MeItm.LabelWidth = 40;
            MeItm.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
            MeItm.ShowSeparator = true;
            MeItm.LabelText = "Web Port:";
            MeItm.MaxLength = 5;
            MeItm.Text = App.Configuration.WebPort;
            MeItm.GotFocus += new EventHandler(TbItm_GotFocus);
            MeItm.LostFocus += new EventHandler(TbItm_LostFocus);
            this.senseListCtrl.AddItem(MeItm);

            StedySoft.SenseSDK.SensePanelTextboxItem TbItm2 = new StedySoft.SenseSDK.SensePanelTextboxItem("Username");
            TbItm2.LabelWidth = 40;
            TbItm2.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
            TbItm2.ShowSeparator = true;
            TbItm2.LabelText = "Username:";
            TbItm2.MaxLength = 25;
            TbItm2.Text = App.Configuration.Username;
            TbItm2.GotFocus += new EventHandler(TbItm_GotFocus);
            TbItm2.LostFocus += new EventHandler(TbItm_LostFocus);
            this.senseListCtrl.AddItem(TbItm2);

            StedySoft.SenseSDK.SensePanelTextboxItem TbItm3 = new StedySoft.SenseSDK.SensePanelTextboxItem("Password");
            TbItm3.LabelWidth = 40;
            TbItm3.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
            TbItm3.ShowSeparator = true;
            TbItm3.LabelText = "Password:";
            TbItm3.MaxLength = 25;
            TbItm3.Text = App.Configuration.Password;
            TbItm3.GotFocus += new EventHandler(TbItm_GotFocus);
            TbItm3.LostFocus += new EventHandler(TbItm_LostFocus);
            this.senseListCtrl.AddItem(TbItm3);

            StedySoft.SenseSDK.SensePanelTextboxItem TbItm4 = new StedySoft.SenseSDK.SensePanelTextboxItem("Timeout");
            TbItm4.LabelWidth = 40;
            TbItm4.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
            TbItm4.ShowSeparator = true;
            TbItm4.LabelText = "Custom timeout (power users only):";
            TbItm4.MaxLength = 25;
            TbItm4.Text = App.Configuration.Timeout;
            TbItm4.GotFocus += new EventHandler(TbItm_GotFocus);
            TbItm4.LostFocus += new EventHandler(TbItm_LostFocus);
            this.senseListCtrl.AddItem(TbItm4);

            /*StedySoft.SenseSDK.SensePanelDividerItem div = new StedySoft.SenseSDK.SensePanelDividerItem();
            div.Name = "Warning! These functions will reboot your device";
            this.senseListCtrl.AddItem(div);

            StedySoft.SenseSDK.SensePanelButtonItem butItm = new StedySoft.SenseSDK.SensePanelButtonItem("Install MsgInterceptor Hack");
            butItm.ShowSeparator = true;
            butItm.Text = "Install MsgInterceptor Hack";
            butItm.Height = 100;
            butItm.OnClick += new SensePanelButtonItem.ClickEventHandler(butItm_OnClick);
            this.senseListCtrl.AddItem(butItm);

            StedySoft.SenseSDK.SensePanelButtonItem butItm2 = new StedySoft.SenseSDK.SensePanelButtonItem("Remove MsgInterceptor Hack");
            butItm2.ShowSeparator = true;
            butItm2.Text = "Remove MsgInterceptor Hack";
            butItm2.Height = 100;
            butItm2.OnClick += new SensePanelButtonItem.ClickEventHandler(butItm2_OnClick);
            this.senseListCtrl.AddItem(butItm2);
            */

            this.senseListCtrl.EndUpdate();
        }

        void butItm_OnClick(object Sender)
        {
            MsgInterceptor msgI = new MsgInterceptor();
            msgI.CreateInterceptorMethod2();
        }

        void butItm2_OnClick(object Sender)
        {
            MsgInterceptor msgI = new MsgInterceptor();
            msgI.RemoveInterceptorMethod2();
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

        private void menuCancel_Click(object sender, EventArgs e)
        {
            this.senseListCtrl.ScrollIntoView(senseListCtrl[1]);
            this.Close();
        }

        void SettingsForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.senseListCtrl.Clear();
        }

        void SettingsForm_Closed(object sender, System.EventArgs e)
        {
            this.senseListCtrl.Dispose();
        }
        #endregion
    }
}