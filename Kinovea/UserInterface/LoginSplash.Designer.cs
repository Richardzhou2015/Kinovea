using System.Windows.Forms;

namespace Kinovea.Root
{
    partial class LoginSplash
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtAccount;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblTip;
        private Label lblTitle;
        private Label label1;
        private Label label2;

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

            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 16F);
            this.lblTitle.Location = new System.Drawing.Point(150, 40);
            this.lblTitle.Size = new System.Drawing.Size(300, 40);
            this.lblTitle.Text = "Kinovea 动作分析登录";

            this.label1.Location = new System.Drawing.Point(120, 110);
            this.label1.Size = new System.Drawing.Size(60, 23);
            this.label1.Text = "账号：";

            this.txtAccount.Location = new System.Drawing.Point(190, 110);
            this.txtAccount.Size = new System.Drawing.Size(220, 23);

            this.label2.Location = new System.Drawing.Point(120, 150);
            this.label2.Size = new System.Drawing.Size(60, 23);
            this.label2.Text = "密码：";

            this.txtPassword.Location = new System.Drawing.Point(190, 150);
            this.txtPassword.Size = new System.Drawing.Size(220, 23);

            this.lblTip.Location = new System.Drawing.Point(120, 190);
            this.lblTip.Size = new System.Drawing.Size(290, 23);

            this.btnLogin.Location = new System.Drawing.Point(180, 230);
            this.btnLogin.Size = new System.Drawing.Size(160, 35);
            this.btnLogin.Text = "登录";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            this.ClientSize = new System.Drawing.Size(520, 320);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.btnLogin);
            this.FormClosing += new FormClosingEventHandler(this.LoginSplash_FormClosing);
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}