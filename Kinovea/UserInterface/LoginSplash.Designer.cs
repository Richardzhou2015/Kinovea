using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kinovea.Root
{
    partial class LoginSplash
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label label1;
        private TextBox txtAccount;
        private Label label2;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblTip;

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
            this.lblTitle = new Label();
            this.label1 = new Label();
            this.txtAccount = new TextBox();
            this.label2 = new Label();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.lblTip = new Label();
            this.SuspendLayout();

            // lblTitle (系统名称，位于登录卡片标题区)
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new Font("Microsoft YaHei", 22F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(0, 200, 220);
            this.lblTitle.Location = new Point(80, 140);
            this.lblTitle.Size = new Size(440, 45);
            this.lblTitle.Text = "射箭运动姿态评估分析系统";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // label1
            this.label1.ForeColor = Color.FromArgb(160, 190, 210);
            this.label1.Font = new Font("Microsoft YaHei", 11F);
            this.label1.Location = new Point(140, 205);
            this.label1.Size = new Size(70, 26);
            this.label1.Text = "账号";

            // txtAccount
            this.txtAccount.BackColor = Color.FromArgb(20, 45, 60);
            this.txtAccount.BorderStyle = BorderStyle.None;
            this.txtAccount.Font = new Font("Microsoft YaHei", 12F);
            this.txtAccount.ForeColor = Color.White;
            this.txtAccount.Location = new Point(210, 205);
            this.txtAccount.Size = new Size(260, 26);

            // label2
            this.label2.ForeColor = Color.FromArgb(160, 190, 210);
            this.label2.Font = new Font("Microsoft YaHei", 11F);
            this.label2.Location = new Point(140, 250);
            this.label2.Size = new Size(70, 26);
            this.label2.Text = "密码";

            // txtPassword
            this.txtPassword.BackColor = Color.FromArgb(20, 45, 60);
            this.txtPassword.BorderStyle = BorderStyle.None;
            this.txtPassword.Font = new Font("Microsoft YaHei", 12F);
            this.txtPassword.ForeColor = Color.White;
            this.txtPassword.Location = new Point(210, 250);
            this.txtPassword.Size = new Size(260, 26);

            // lblTip (错误提示)
            this.lblTip.Location = new Point(140, 285);
            this.lblTip.Size = new Size(330, 22);
            this.lblTip.Font = new Font("Microsoft YaHei", 10F);
            this.lblTip.ForeColor = Color.FromArgb(255, 120, 100);
            this.lblTip.TextAlign = ContentAlignment.MiddleCenter;

            // btnLogin
            this.btnLogin.BackColor = Color.FromArgb(0, 180, 200);
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Font = new Font("Microsoft YaHei", 14F, FontStyle.Bold);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(170, 320);
            this.btnLogin.Size = new Size(260, 48);
            this.btnLogin.Text = "登  录  系  统";
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // ------------------- Form -------------------
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 470);
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.Paint += new PaintEventHandler(this.LoginSplash_Paint);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.btnLogin);
            this.FormClosing += new FormClosingEventHandler(this.LoginSplash_FormClosing);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}