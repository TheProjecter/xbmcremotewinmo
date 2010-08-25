namespace XBMC_Remote
{
    partial class RemoteForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.PictureBox butEnter;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteForm));
            this.butFForward = new System.Windows.Forms.PictureBox();
            this.butPlay = new System.Windows.Forms.PictureBox();
            this.butFBackward = new System.Windows.Forms.PictureBox();
            this.butStop = new System.Windows.Forms.PictureBox();
            this.butPause = new System.Windows.Forms.PictureBox();
            this.butNext = new System.Windows.Forms.PictureBox();
            this.butPrev = new System.Windows.Forms.PictureBox();
            this.butLeftDown = new System.Windows.Forms.PictureBox();
            this.butRightDown = new System.Windows.Forms.PictureBox();
            this.butRightUp = new System.Windows.Forms.PictureBox();
            this.butLeftUp = new System.Windows.Forms.PictureBox();
            this.butDown = new XBMC_Remote.RepeatButton();
            this.butUp = new XBMC_Remote.RepeatButton();
            this.butRight = new XBMC_Remote.RepeatButton();
            this.butLeft = new XBMC_Remote.RepeatButton();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuBack = new System.Windows.Forms.MenuItem();
            butEnter = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // butEnter
            // 
            butEnter.Image = ((System.Drawing.Image)(resources.GetObject("butEnter.Image")));
            butEnter.Location = new System.Drawing.Point(148, 414);
            butEnter.Name = "butEnter";
            butEnter.Size = new System.Drawing.Size(184, 144);
            butEnter.Click += new System.EventHandler(this.butEnter_Click);
            // 
            // butFForward
            // 
            this.butFForward.Image = ((System.Drawing.Image)(resources.GetObject("butFForward.Image")));
            this.butFForward.Location = new System.Drawing.Point(308, 0);
            this.butFForward.Name = "butFForward";
            this.butFForward.Size = new System.Drawing.Size(172, 112);
            this.butFForward.Click += new System.EventHandler(this.butFForward_Click);
            // 
            // butPlay
            // 
            this.butPlay.Image = ((System.Drawing.Image)(resources.GetObject("butPlay.Image")));
            this.butPlay.Location = new System.Drawing.Point(172, 0);
            this.butPlay.Name = "butPlay";
            this.butPlay.Size = new System.Drawing.Size(138, 112);
            this.butPlay.Click += new System.EventHandler(this.butPlay_Click);
            // 
            // butFBackward
            // 
            this.butFBackward.Image = ((System.Drawing.Image)(resources.GetObject("butFBackward.Image")));
            this.butFBackward.Location = new System.Drawing.Point(0, 0);
            this.butFBackward.Name = "butFBackward";
            this.butFBackward.Size = new System.Drawing.Size(172, 112);
            this.butFBackward.Click += new System.EventHandler(this.butFBackward_Click);
            // 
            // butStop
            // 
            this.butStop.Image = ((System.Drawing.Image)(resources.GetObject("butStop.Image")));
            this.butStop.Location = new System.Drawing.Point(142, 112);
            this.butStop.Name = "butStop";
            this.butStop.Size = new System.Drawing.Size(98, 120);
            this.butStop.Click += new System.EventHandler(this.butStop_Click);
            // 
            // butPause
            // 
            this.butPause.Image = ((System.Drawing.Image)(resources.GetObject("butPause.Image")));
            this.butPause.Location = new System.Drawing.Point(240, 112);
            this.butPause.Name = "butPause";
            this.butPause.Size = new System.Drawing.Size(98, 120);
            this.butPause.Click += new System.EventHandler(this.butPause_Click);
            // 
            // butNext
            // 
            this.butNext.Image = ((System.Drawing.Image)(resources.GetObject("butNext.Image")));
            this.butNext.Location = new System.Drawing.Point(338, 112);
            this.butNext.Name = "butNext";
            this.butNext.Size = new System.Drawing.Size(142, 120);
            this.butNext.Click += new System.EventHandler(this.butNext_Click);
            // 
            // butPrev
            // 
            this.butPrev.Image = ((System.Drawing.Image)(resources.GetObject("butPrev.Image")));
            this.butPrev.Location = new System.Drawing.Point(0, 112);
            this.butPrev.Name = "butPrev";
            this.butPrev.Size = new System.Drawing.Size(142, 120);
            this.butPrev.Click += new System.EventHandler(this.butPrev_Click);
            // 
            // butLeftDown
            // 
            this.butLeftDown.Image = ((System.Drawing.Image)(resources.GetObject("butLeftDown.Image")));
            this.butLeftDown.Location = new System.Drawing.Point(0, 558);
            this.butLeftDown.Name = "butLeftDown";
            this.butLeftDown.Size = new System.Drawing.Size(148, 138);
            this.butLeftDown.Click += new System.EventHandler(this.butLeftDown_Click);
            // 
            // butRightDown
            // 
            this.butRightDown.Image = ((System.Drawing.Image)(resources.GetObject("butRightDown.Image")));
            this.butRightDown.Location = new System.Drawing.Point(332, 558);
            this.butRightDown.Name = "butRightDown";
            this.butRightDown.Size = new System.Drawing.Size(148, 138);
            this.butRightDown.Click += new System.EventHandler(this.butBack_Click);
            // 
            // butRightUp
            // 
            this.butRightUp.Image = ((System.Drawing.Image)(resources.GetObject("butRightUp.Image")));
            this.butRightUp.Location = new System.Drawing.Point(332, 290);
            this.butRightUp.Name = "butRightUp";
            this.butRightUp.Size = new System.Drawing.Size(148, 124);
            this.butRightUp.Click += new System.EventHandler(this.butRightUp_Click);
            // 
            // butLeftUp
            // 
            this.butLeftUp.Image = ((System.Drawing.Image)(resources.GetObject("butLeftUp.Image")));
            this.butLeftUp.Location = new System.Drawing.Point(0, 290);
            this.butLeftUp.Name = "butLeftUp";
            this.butLeftUp.Size = new System.Drawing.Size(148, 124);
            this.butLeftUp.Click += new System.EventHandler(this.butLeftUp_Click);
            // 
            // butDown
            // 
            this.butDown.Image = ((System.Drawing.Image)(resources.GetObject("butDown.Image")));
            this.butDown.InitialDelay = 300;
            this.butDown.Location = new System.Drawing.Point(148, 558);
            this.butDown.Name = "butDown";
            this.butDown.RepeatDelay = 75;
            this.butDown.Size = new System.Drawing.Size(184, 138);
            this.butDown.Click += new System.EventHandler(this.butDown_Click);
            // 
            // butUp
            // 
            this.butUp.Image = ((System.Drawing.Image)(resources.GetObject("butUp.Image")));
            this.butUp.InitialDelay = 300;
            this.butUp.Location = new System.Drawing.Point(148, 290);
            this.butUp.Name = "butUp";
            this.butUp.RepeatDelay = 75;
            this.butUp.Size = new System.Drawing.Size(184, 124);
            this.butUp.Click += new System.EventHandler(this.butUp_Click);
            // 
            // butRight
            // 
            this.butRight.Image = ((System.Drawing.Image)(resources.GetObject("butRight.Image")));
            this.butRight.InitialDelay = 300;
            this.butRight.Location = new System.Drawing.Point(332, 414);
            this.butRight.Name = "butRight";
            this.butRight.RepeatDelay = 75;
            this.butRight.Size = new System.Drawing.Size(148, 144);
            this.butRight.Click += new System.EventHandler(this.butRight_Click);
            // 
            // butLeft
            // 
            this.butLeft.Image = ((System.Drawing.Image)(resources.GetObject("butLeft.Image")));
            this.butLeft.InitialDelay = 300;
            this.butLeft.Location = new System.Drawing.Point(0, 414);
            this.butLeft.Name = "butLeft";
            this.butLeft.RepeatDelay = 75;
            this.butLeft.Size = new System.Drawing.Size(148, 144);
            this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuBack);
            // 
            // menuBack
            // 
            this.menuBack.Text = "Back";
            this.menuBack.Click += new System.EventHandler(this.menuBack_Click);
            // 
            // RemoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(480, 696);
            this.Controls.Add(this.butFForward);
            this.Controls.Add(this.butPlay);
            this.Controls.Add(this.butFBackward);
            this.Controls.Add(this.butStop);
            this.Controls.Add(this.butPause);
            this.Controls.Add(this.butNext);
            this.Controls.Add(this.butPrev);
            this.Controls.Add(this.butLeftDown);
            this.Controls.Add(this.butRightDown);
            this.Controls.Add(butEnter);
            this.Controls.Add(this.butRightUp);
            this.Controls.Add(this.butLeftUp);
            this.Controls.Add(this.butDown);
            this.Controls.Add(this.butUp);
            this.Controls.Add(this.butRight);
            this.Controls.Add(this.butLeft);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu;
            this.Name = "RemoteForm";
            this.Text = "Remote Control";
            this.Load += new System.EventHandler(this.RemoteForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox butFForward;
        private System.Windows.Forms.PictureBox butPlay;
        private System.Windows.Forms.PictureBox butFBackward;
        private System.Windows.Forms.PictureBox butStop;
        private System.Windows.Forms.PictureBox butPause;
        private System.Windows.Forms.PictureBox butNext;
        private System.Windows.Forms.PictureBox butPrev;
        private System.Windows.Forms.PictureBox butLeftDown;
        private System.Windows.Forms.PictureBox butRightDown;
        private System.Windows.Forms.PictureBox butRightUp;
        private System.Windows.Forms.PictureBox butLeftUp;
        private RepeatButton butDown;
        private RepeatButton butUp;
        private RepeatButton butRight;
        private RepeatButton butLeft;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuBack;

    }
}