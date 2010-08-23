namespace XBMC_Remote
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuOptions = new System.Windows.Forms.MenuItem();
            this.menuMenu = new System.Windows.Forms.MenuItem();
            this.menuConnect = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.remoteBut = new System.Windows.Forms.PictureBox();
            this.playingBut = new System.Windows.Forms.PictureBox();
            this.moviesBut = new System.Windows.Forms.PictureBox();
            this.musicBut = new System.Windows.Forms.PictureBox();
            this.pictureBut = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "Connect";
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "Exit";
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Options";
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Menu";
            // 
            // menuOptions
            // 
            this.menuOptions.Text = "Options";
            this.menuOptions.Click += new System.EventHandler(this.menuOptions_Click);
            // 
            // menuMenu
            // 
            this.menuMenu.MenuItems.Add(this.menuOptions);
            this.menuMenu.MenuItems.Add(this.menuConnect);
            this.menuMenu.Text = "Menu";
            // 
            // menuConnect
            // 
            this.menuConnect.Text = "Connect";
            this.menuConnect.Click += new System.EventHandler(this.menuConnect_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuExit);
            this.mainMenu1.MenuItems.Add(this.menuMenu);
            // 
            // menuExit
            // 
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // remoteBut
            // 
            this.remoteBut.Image = ((System.Drawing.Image)(resources.GetObject("remoteBut.Image")));
            this.remoteBut.Location = new System.Drawing.Point(6, 405);
            this.remoteBut.Name = "remoteBut";
            this.remoteBut.Size = new System.Drawing.Size(128, 128);
            this.remoteBut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.remoteBut.Click += new System.EventHandler(this.remoteBut_Click);
            // 
            // playingBut
            // 
            this.playingBut.Image = ((System.Drawing.Image)(resources.GetObject("playingBut.Image")));
            this.playingBut.Location = new System.Drawing.Point(6, 539);
            this.playingBut.Name = "playingBut";
            this.playingBut.Size = new System.Drawing.Size(128, 128);
            this.playingBut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playingBut.Click += new System.EventHandler(this.playingBut_Click);
            // 
            // moviesBut
            // 
            this.moviesBut.Image = ((System.Drawing.Image)(resources.GetObject("moviesBut.Image")));
            this.moviesBut.Location = new System.Drawing.Point(6, 3);
            this.moviesBut.Name = "moviesBut";
            this.moviesBut.Size = new System.Drawing.Size(128, 128);
            this.moviesBut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.moviesBut.Click += new System.EventHandler(this.moviesBut_Click);
            // 
            // musicBut
            // 
            this.musicBut.Image = ((System.Drawing.Image)(resources.GetObject("musicBut.Image")));
            this.musicBut.Location = new System.Drawing.Point(6, 137);
            this.musicBut.Name = "musicBut";
            this.musicBut.Size = new System.Drawing.Size(128, 128);
            this.musicBut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.musicBut.Click += new System.EventHandler(this.musicBut_Click);
            // 
            // pictureBut
            // 
            this.pictureBut.Image = ((System.Drawing.Image)(resources.GetObject("pictureBut.Image")));
            this.pictureBut.Location = new System.Drawing.Point(6, 271);
            this.pictureBut.Name = "pictureBut";
            this.pictureBut.Size = new System.Drawing.Size(128, 128);
            this.pictureBut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(146, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 64);
            this.label1.Text = "Movies";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(146, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(328, 64);
            this.label2.Text = "Music";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(146, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(328, 62);
            this.label3.Text = "Pictures";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(146, 439);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(328, 62);
            this.label4.Text = "Remote";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(146, 565);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(328, 62);
            this.label5.Text = "Playing";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(480, 696);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBut);
            this.Controls.Add(this.musicBut);
            this.Controls.Add(this.moviesBut);
            this.Controls.Add(this.playingBut);
            this.Controls.Add(this.remoteBut);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "XBMC Remote";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.MenuItem menuItem4;
        protected System.Windows.Forms.MenuItem menuItem5;
        protected System.Windows.Forms.MenuItem menuItem3;
        protected System.Windows.Forms.MenuItem menuItem1;
        protected System.Windows.Forms.MenuItem menuOptions;
        protected System.Windows.Forms.MenuItem menuMenu;
        protected System.Windows.Forms.MenuItem menuConnect;
        protected System.Windows.Forms.MainMenu mainMenu1;
        protected System.Windows.Forms.MenuItem menuExit;
        private System.Windows.Forms.PictureBox remoteBut;
        private System.Windows.Forms.PictureBox playingBut;
        private System.Windows.Forms.PictureBox moviesBut;
        private System.Windows.Forms.PictureBox musicBut;
        private System.Windows.Forms.PictureBox pictureBut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;






    }
}

