using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XBMC_Remote
{
    public partial class SettingsForm : Form
    {
        public string pIPAdress;

        public SetIpDelegate SetIpCallback;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            IPText.Text = pIPAdress;
        }

        private void menuOK_Click(object sender, EventArgs e)
        {
            try
            {
                XmlTextWriter textWriter = new XmlTextWriter("\\Application Data\\XBMC_Remote\\settings.xml", null);
                textWriter.WriteStartDocument();
                textWriter.WriteComment("XBMC Remote settings file.");
                textWriter.WriteStartElement("IpAddress");
                textWriter.WriteString(IPText.Text);
                textWriter.WriteEndElement();
                textWriter.WriteEndDocument();
                textWriter.Close();
                SetIpCallback.Invoke(IPText.Text);
                this.Dispose();
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                System.IO.Directory.CreateDirectory("\\Application Data\\XBMC_Remote");
            }
        }

        private void menuCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}