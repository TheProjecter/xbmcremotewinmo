using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XbmcEventClient;

namespace XBMC_Remote
{
    public partial class RemoteForm : Form
    {
        

        EventClient RemoteClient = new EventClient();

        public RemoteForm()
        {
            InitializeComponent();
        }

        public void RemoteForm_Load(object sender, EventArgs e)
        {
            InitializeCommunication();
        }


        private void InitializeCommunication()
        {
            RemoteClient.Connect(App.Configuration.IpAddress);
        }

        private void butUp_Click(object sender, EventArgs e)
        {
           RemoteClient.SendButton("up", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butBack_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("back", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butEnter_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("select", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butLeft_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("left", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butRight_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("right", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butDown_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("down", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butLeftDown_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("menu", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butLeftUp_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("title", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butRightUp_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("info", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butFBackward_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("reverse", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butFForward_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("forward", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butPlay_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("play", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butPrev_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("skipminus", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butNext_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("skipplus", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butStop_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("stop", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void butPause_Click(object sender, EventArgs e)
        {
            RemoteClient.SendButton("pause", "R1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
        }

        private void menuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}