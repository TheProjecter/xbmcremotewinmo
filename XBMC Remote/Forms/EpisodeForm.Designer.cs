namespace XBMC_Remote{
    partial class EpisodeForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.senseHeaderCtrl = new StedySoft.SenseSDK.SenseHeaderControl();
            this.senseListCtrl = new StedySoft.SenseSDK.SenseListControl();
            this.mnuControls = new System.Windows.Forms.MenuItem();
            this.mnuDateTime = new System.Windows.Forms.MenuItem();
            this.sip = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuBack = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // senseHeaderCtrl
            // 
            this.senseHeaderCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.senseHeaderCtrl.Location = new System.Drawing.Point(0, 0);
            this.senseHeaderCtrl.Name = "senseHeaderCtrl";
            this.senseHeaderCtrl.Size = new System.Drawing.Size(480, 25);
            this.senseHeaderCtrl.TabIndex = 0;
            this.senseHeaderCtrl.Text = "Episodes";
            // 
            // senseListCtrl
            // 
            this.senseListCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.senseListCtrl.FocusedItem = null;
            this.senseListCtrl.IsSecondaryScrollType = false;
            this.senseListCtrl.Location = new System.Drawing.Point(0, 25);
            this.senseListCtrl.MinimumMovement = 15;
            this.senseListCtrl.Name = "senseListCtrl";
            this.senseListCtrl.ShowScrollIndicator = true;
            this.senseListCtrl.Size = new System.Drawing.Size(480, 723);
            this.senseListCtrl.Springback = 0.35F;
            this.senseListCtrl.TabIndex = 1;
            this.senseListCtrl.ThreadSleep = 100;
            this.senseListCtrl.TopIndex = 0;
            this.senseListCtrl.Velocity = 0.9F;
            // 
            // mnuControls
            // 
            this.mnuControls.Text = "";
            // 
            // mnuDateTime
            // 
            this.mnuDateTime.Text = "";
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
            // EpisodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 748);
            this.ControlBox = false;
            this.Controls.Add(this.senseListCtrl);
            this.Controls.Add(this.senseHeaderCtrl);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "EpisodeForm";
            this.Text = "Episodes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EpisodeForm_Load);
            this.Closed += new System.EventHandler(this.EpisodeForm_Closed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.EpisodeForm_Closing);
            this.ResumeLayout(false);

        }
        #endregion

        private StedySoft.SenseSDK.SenseHeaderControl senseHeaderCtrl;
        private StedySoft.SenseSDK.SenseListControl senseListCtrl;
        private System.Windows.Forms.MenuItem mnuControls;
        private System.Windows.Forms.MenuItem mnuDateTime;
        private Microsoft.WindowsCE.Forms.InputPanel sip;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuBack;
    }
}

