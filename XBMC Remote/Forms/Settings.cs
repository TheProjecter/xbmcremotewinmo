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
        #region Declarations
        public string pIPAdress;
        public SetIpDelegate SetIpCallback;
        #endregion

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

            StedySoft.SenseSDK.SensePanelTextboxItem TbItm = new StedySoft.SenseSDK.SensePanelTextboxItem("Ipaddress");
            TbItm.LabelWidth = 25;
            TbItm.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
            TbItm.ShowSeparator = true;
            TbItm.LabelText = "Ip address:";
            TbItm.MaxLength = 15;
            TbItm.Text = pIPAdress;
            TbItm.GotFocus += new EventHandler(TbItm_GotFocus);
            TbItm.LostFocus += new EventHandler(TbItm_LostFocus);
            this.senseListCtrl.AddItem(TbItm);

            StedySoft.SenseSDK.SensePanelDividerItem div = new StedySoft.SenseSDK.SensePanelDividerItem();
            div.Name = "Warning! These functions will reboot your device";
            this.senseListCtrl.AddItem(div);

            StedySoft.SenseSDK.SensePanelButtonItem butItm = new StedySoft.SenseSDK.SensePanelButtonItem("Install MsgInterceptor Hack");
            butItm.ShowSeparator = true;
            butItm.Text = "Install MsgInceptor Hack";
            butItm.Height = 100;
            butItm.OnClick += new SensePanelButtonItem.ClickEventHandler(butItm_OnClick);
            this.senseListCtrl.AddItem(butItm);

            StedySoft.SenseSDK.SensePanelButtonItem butItm2 = new StedySoft.SenseSDK.SensePanelButtonItem("Remove MsgInterceptor Hack");
            butItm2.ShowSeparator = true;
            butItm2.Text = "Remove MsgInceptor Hack";
            butItm2.Height = 100;
            butItm2.OnClick += new SensePanelButtonItem.ClickEventHandler(butItm2_OnClick);
            this.senseListCtrl.AddItem(butItm2);

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
            try
            {
                XmlTextWriter textWriter = new XmlTextWriter("\\Application Data\\XBMC_Remote\\settings.xml", null);
                textWriter.WriteStartDocument();
                textWriter.WriteComment("XBMC Remote settings file.");
                textWriter.WriteStartElement("IpAddress");
                textWriter.WriteString((this.senseListCtrl["Ipaddress"] as SensePanelTextboxItem).Text);
                textWriter.WriteEndElement();
                textWriter.WriteEndDocument();
                textWriter.Close();
                SetIpCallback.Invoke((this.senseListCtrl["Ipaddress"] as SensePanelTextboxItem).Text);
                this.Close();
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                System.IO.Directory.CreateDirectory("\\Application Data\\XBMC_Remote");
                menuOK_Click(sender, e);
            }
        }

        private void menuCancel_Click(object sender, EventArgs e)
        {
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