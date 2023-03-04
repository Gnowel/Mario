namespace MGame
{
    partial class Menu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Help = new MGame.MyButton();
            this.About = new MGame.MyButton();
            this.loadSave = new MGame.MyButton();
            this.Exit = new MGame.MyButton();
            this.Play = new MGame.MyButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MGame.Properties.Resources.Tile;
            this.pictureBox1.Location = new System.Drawing.Point(110, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(420, 224);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Help
            // 
            this.Help.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Help.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Help.Location = new System.Drawing.Point(287, 350);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(103, 28);
            this.Help.TabIndex = 5;
            this.Help.Text = "СПРАВКА";
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // About
            // 
            this.About.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.About.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.About.Location = new System.Drawing.Point(287, 314);
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(243, 28);
            this.About.TabIndex = 3;
            this.About.Text = "О ПРОГРАММЕ";
            this.About.Click += new System.EventHandler(this.About_Click);
            // 
            // loadSave
            // 
            this.loadSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadSave.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.loadSave.Location = new System.Drawing.Point(287, 278);
            this.loadSave.Name = "loadSave";
            this.loadSave.Size = new System.Drawing.Size(243, 28);
            this.loadSave.TabIndex = 2;
            this.loadSave.Text = "ЗАГРУЗИТЬ СОХРАНЕНИЕ";
            this.loadSave.Click += new System.EventHandler(this.loadSave_Click);
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Exit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Exit.Location = new System.Drawing.Point(287, 386);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(103, 28);
            this.Exit.TabIndex = 5;
            this.Exit.Text = "ВЫЙТИ";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Play
            // 
            this.Play.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Play.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Play.Location = new System.Drawing.Point(287, 242);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(103, 28);
            this.Play.TabIndex = 1;
            this.Play.Text = "ИГРАТЬ";
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.Help);
            this.Controls.Add(this.About);
            this.Controls.Add(this.loadSave);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mario Bros.";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Menu_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Menu_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private MyButton Play;
        private MyButton Exit;
        private MyButton loadSave;
        private MyButton About;
        private MyButton Help;
        private System.Windows.Forms.Timer timer1;
    }
}