using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace XBMC_Remote
{
    public class RepeatButton : System.Windows.Forms.PictureBox
    {
        private Timer m_timer;
        private int m_initdelay = 500;
        private int m_repdelay = 200;

        public RepeatButton()
        {
            this.MouseUp +=
                new MouseEventHandler(RepeatButton_MouseUp);
            this.MouseDown +=
                new MouseEventHandler(RepeatButton_MouseDown);

            m_timer = new Timer();
            m_timer.Tick += new EventHandler(timerproc);
            m_timer.Enabled = false;
        }

        private void timerproc(object o1, EventArgs e1)
        {
            m_timer.Interval = m_repdelay;
            m_timer.Enabled = true;
            base.OnClick(e1);
        }

        protected override void OnClick(EventArgs e)
        {
            //base.OnClick(e);
        }



        private void RepeatButton_MouseDown(object sender,
            System.Windows.Forms.MouseEventArgs e)
        {
            base.OnClick(e);
            m_timer.Interval = m_initdelay;
            m_timer.Enabled = true;
        }

        private void RepeatButton_MouseUp(object sender,
            System.Windows.Forms.MouseEventArgs e)
        {
            m_timer.Enabled = false;
        }

        public int InitialDelay
        {
            get
            {
                return m_initdelay;
            }
            set
            {
                m_initdelay = value;
            }
        }

        public void BeginInit()
        {

        }

        public void EndInit()
        {

        }

        public int RepeatDelay
        {
            get
            {
                return m_repdelay;
            }
            set
            {
                m_repdelay = value;
            }
        }
    }
}
