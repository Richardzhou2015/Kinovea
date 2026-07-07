namespace Kinovea.Root
{
    partial class FormSplashScreen
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSplashScreen));
      this.lblVersion = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.lblStatus = new System.Windows.Forms.Label();
      this.lblSplashTitle = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // lblVersion
      // 

      this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lblVersion.AutoSize = true;
      this.lblVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
      this.lblVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lblVersion.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblVersion.ForeColor = System.Drawing.Color.White;
      this.lblVersion.Location = new System.Drawing.Point(411, 301);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new System.Drawing.Size(70, 22);
      this.lblVersion.TabIndex = 1;
      this.lblVersion.Text = "2025.1";
      this.lblVersion.Visible = false;
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
      this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Enabled = false;
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(560, 353);
      this.pictureBox1.TabIndex = 2;
      this.pictureBox1.TabStop = false;
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(60, 200);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(440, 23);
      this.progressBar1.TabIndex = 3;
      this.progressBar1.Visible = false;
      // 
      // lblStatus
      // 
      // 
      // lblSplashTitle
      //
      this.lblSplashTitle.AutoSize = false;
      this.lblSplashTitle.Font = new System.Drawing.Font("Microsoft YaHei", 20F, System.Drawing.FontStyle.Bold);
      this.lblSplashTitle.ForeColor = System.Drawing.Color.White;
      this.lblSplashTitle.Location = new System.Drawing.Point(30, 300);
      this.lblSplashTitle.Name = "lblSplashTitle";
      this.lblSplashTitle.Size = new System.Drawing.Size(500, 40);
      this.lblSplashTitle.TabIndex = 5;
      this.lblSplashTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblSplashTitle.Visible = false;
      // 
      // lblStatus
      // 
      this.lblStatus.AutoSize = false;
      this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblStatus.ForeColor = System.Drawing.Color.White;
      this.lblStatus.Location = new System.Drawing.Point(60, 160);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(440, 30);
      this.lblStatus.TabIndex = 4;
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblStatus.Visible = false;
      // 
      // FormSplashScreen
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(560, 353);
      this.ControlBox = false;
      this.Controls.Add(this.lblSplashTitle);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.lblVersion);
      this.Controls.Add(this.pictureBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormSplashScreen";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSplashTitle;
    }
}