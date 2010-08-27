namespace XBMC_Remote
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuOK = new System.Windows.Forms.MenuItem();
            this.menuCancel = new System.Windows.Forms.MenuItem();
            this.senseListCtrl = new StedySoft.SenseSDK.SenseListControl();
            this.senseHeaderCtrl = new StedySoft.SenseSDK.SenseHeaderControl();
            this.sip = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuOK);
            this.mainMenu1.MenuItems.Add(this.menuCancel);
            // 
            // menuOK
            // 
            this.menuOK.Text = "OK";
            this.menuOK.Click += new System.EventHandler(this.menuOK_Click);
            // 
            // menuCancel
            // 
            this.menuCancel.Text = "Cancel";
            this.menuCancel.Click += new System.EventHandler(this.menuCancel_Click);
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
            this.senseListCtrl.TabIndex = 3;
            this.senseListCtrl.ThreadSleep = 100;
            this.senseListCtrl.TopIndex = 0;
            this.senseListCtrl.Velocity = 0.9F;
            // 
            // senseHeaderCtrl
            // 
            this.senseHeaderCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.senseHeaderCtrl.Location = new System.Drawing.Point(0, 0);
            this.senseHeaderCtrl.Name = "senseHeaderCtrl";
            this.senseHeaderCtrl.Size = new System.Drawing.Size(480, 25);
            this.senseHeaderCtrl.TabIndex = 2;
            this.senseHeaderCtrl.Text = "Settings";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 748);
            this.Controls.Add(this.senseListCtrl);
            this.Controls.Add(this.senseHeaderCtrl);
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.Closed += new System.EventHandler(this.SettingsForm_Closed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingsForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuOK;
        private System.Windows.Forms.MenuItem menuCancel;
        private StedySoft.SenseSDK.SenseListControl senseListCtrl;
        private StedySoft.SenseSDK.SenseHeaderControl senseHeaderCtrl;
        private Microsoft.WindowsCE.Forms.InputPanel sip;
    }
}