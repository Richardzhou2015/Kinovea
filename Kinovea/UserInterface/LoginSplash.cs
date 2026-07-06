using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kinovea.Root
{
    public partial class LoginSplash : Form
    {
        private readonly string fixedUser = "demo";
        private readonly string fixedPwd = "archery123";
        private int errorTimes = 0;

        public LoginSplash()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            lblTip.ForeColor = Color.Red;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblTip.Text = string.Empty;

            string user = txtAccount.Text.Trim();
            string pwd = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pwd))
            {
                lblTip.Text = "账号/密码不能为空";
                return;
            }

            errorTimes++;

            if (user == fixedUser && pwd == fixedPwd)
            {
                ClientSession.AuthToken = "local_demo_token_001";
                ClientSession.UserName = "DemoUser";
                ClientSession.UserId = "user_001";
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            int remain = 3 - errorTimes;
            lblTip.Text = $"账号密码错误，剩余尝试：{remain}";
            txtPassword.Clear();
            txtPassword.Focus();

            if (errorTimes >= 3)
            {
                MessageBox.Show("错误次数超限，程序即将退出");
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void LoginSplash_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}