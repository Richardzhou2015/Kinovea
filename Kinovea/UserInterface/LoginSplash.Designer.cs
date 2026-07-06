using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kinovea.Root
{
    partial class LoginSplash
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblAppTitle;
        private Label lblSubTitle;
        private Label label1;
        private TextBox txtAccount;
        private Label label2;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblTip;
        private PictureBox picLogo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblAppTitle = new Label();
            this.lblSubTitle = new Label();
            this.label1 = new Label();
            this.txtAccount = new TextBox();
            this.label2 = new Label();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.lblTip = new Label();
            this.picLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();

            // lblAppTitle
            this.lblAppTitle.AutoSize = false;
            this.lblAppTitle.Font = new Font("Microsoft YaHei", 26F, FontStyle.Bold);
            this.lblAppTitle.ForeColor = Color.White;
            this.lblAppTitle.Location = new Point(60, 55);
            this.lblAppTitle.Size = new Size(480, 50);
            this.lblAppTitle.Text = "射箭运动姿态评估分析系统";
            this.lblAppTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblSubTitle
            this.lblSubTitle.AutoSize = false;
            this.lblSubTitle.Font = new Font("Microsoft YaHei", 14F, FontStyle.Regular);
            this.lblSubTitle.ForeColor = Color.FromArgb(180, 200, 220);
            this.lblSubTitle.Location = new Point(60, 108);
            this.lblSubTitle.Size = new Size(480, 30);
            this.lblSubTitle.Text = "Archery Posture Analysis System V1.0";
            this.lblSubTitle.TextAlign = ContentAlignment.MiddleCenter;

            // picLogo
            this.picLogo.BackColor = Color.Transparent;
            this.picLogo.Location = new Point(260, 18);
            this.picLogo.Size = new Size(80, 34);
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.Visible = false;  // 预留 logo 区域，有图片资源后改为 true

            // label1
            this.label1.ForeColor = Color.FromArgb(200, 210, 220);
            this.label1.Font = new Font("Microsoft YaHei", 11F);
            this.label1.Location = new Point(130, 180);
            this.label1.Size = new Size(70, 26);
            this.label1.Text = "账号";

            // txtAccount
            this.txtAccount.BackColor = Color.FromArgb(40, 45, 55);
            this.txtAccount.BorderStyle = BorderStyle.None;
            this.txtAccount.Font = new Font("Microsoft YaHei", 12F);
            this.txtAccount.ForeColor = Color.White;
            this.txtAccount.Location = new Point(205, 180);
            this.txtAccount.Size = new Size(260, 26);

            // label2
            this.label2.ForeColor = Color.FromArgb(200, 210, 220);
            this.label2.Font = new Font("Microsoft YaHei", 11F);
            this.label2.Location = new Point(130, 225);
            this.label2.Size = new Size(70, 26);
            this.label2.Text = "密码";

            // txtPassword
            this.txtPassword.BackColor = Color.FromArgb(40, 45, 55);
            this.txtPassword.BorderStyle = BorderStyle.None;
            this.txtPassword.Font = new Font("Microsoft YaHei", 12F);
            this.txtPassword.ForeColor = Color.White;
            this.txtPassword.Location = new Point(205, 225);
            this.txtPassword.Size = new Size(260, 26);

            // lblTip
            this.lblTip.Location = new Point(130, 268);
            this.lblTip.Size = new Size(340, 22);
            this.lblTip.Font = new Font("Microsoft YaHei", 10F);
            this.lblTip.ForeColor = Color.FromArgb(255, 100, 100);
            this.lblTip.TextAlign = ContentAlignment.MiddleCenter;

            // btnLogin
            this.btnLogin.BackColor = Color.FromArgb(220, 80, 40);
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Font = new Font("Microsoft YaHei", 13F, FontStyle.Bold);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(190, 310);
            this.btnLogin.Size = new Size(220, 44);
            this.btnLogin.Text = "登  录";
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // Form
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(28, 32, 42);
            this.ClientSize = new Size(600, 420);
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.Paint += new PaintEventHandler(this.LoginSplash_Paint);

            this.Controls.Add(this.lblAppTitle);
            this.Controls.Add(this.lblSubTitle);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.btnLogin);
            this.FormClosing += new FormClosingEventHandler(this.LoginSplash_FormClosing);

            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}