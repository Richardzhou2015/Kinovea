using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Kinovea.Root
{
    public partial class LoginSplash : Form
    {
        private readonly string fixedUser = "demo";
        private readonly string fixedPwd = "archery123";
        private int errorTimes = 0;

        // 输入框底线条颜色
        private Color normalLine = Color.FromArgb(80, 90, 110);
        private Color focusLine = Color.FromArgb(220, 80, 40);

        public LoginSplash()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            // 密码框属性
            txtPassword.PasswordChar = '*';
            txtPassword.UseSystemPasswordChar = false;

            // 关联焦点事件，模拟底部高亮条
            txtAccount.GotFocus += (s, e) => Invalidate();
            txtAccount.LostFocus += (s, e) => Invalidate();
            txtPassword.GotFocus += (s, e) => Invalidate();
            txtPassword.LostFocus += (s, e) => Invalidate();
        }

        /// <summary>
        /// Paint 背景渐变 + 输入框底部分隔线 + 边框圆角。
        /// </summary>
        private void LoginSplash_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. 顶部深色渐变区 (0~150px)
            Rectangle topRect = new Rectangle(0, 0, this.Width, 150);
            using (LinearGradientBrush topBrush = new LinearGradientBrush(
                topRect,
                Color.FromArgb(40, 48, 62),   // 顶
                Color.FromArgb(28, 32, 42),   // 底
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(topBrush, topRect);
            }

            // 2. 整个窗口圆角裁剪
            Region = System.Drawing.Region.FromHrgn(NativeMethods.CreateRoundRectRgn(0, 0, Width, Height, 16, 16));

            // 3. 边缘发光边框
            using (Pen pen = new Pen(Color.FromArgb(60, 70, 90), 2))
            {
                g.DrawRoundRect(pen, new Rectangle(1, 1, Width - 3, Height - 3), 16);
            }

            // 4. 输入框底部分隔线 — 账号
            DrawInputUnderline(g, txtAccount, txtAccount.Focused ? focusLine : normalLine);

            // 5. 输入框底部分隔线 — 密码
            DrawInputUnderline(g, txtPassword, txtPassword.Focused ? focusLine : normalLine);

            // 6. 底部柔和阴影区
            Rectangle bottomRect = new Rectangle(0, this.Height - 3, this.Width, 3);
            using (LinearGradientBrush bottomBrush = new LinearGradientBrush(
                bottomRect, Color.FromArgb(28, 32, 42), Color.FromArgb(60, 70, 90), LinearGradientMode.Vertical))
            {
                g.FillRectangle(bottomBrush, bottomRect);
            }
        }

        private void DrawInputUnderline(Graphics g, TextBox tb, Color color)
        {
            int x = tb.Left;
            int y = tb.Bottom + 2;
            int w = tb.Width;
            using (Pen pen = new Pen(color, 2))
            {
                g.DrawLine(pen, x, y, x + w, y);
            }
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

    // ---------- 扩展方法：绘制圆角矩形 ----------
    internal static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);
    }

    internal static class GraphicsExtensions
    {
        public static void DrawRoundRect(this Graphics g, Pen pen, Rectangle bounds, int radius)
        {
            if (bounds.Width <= 0 || bounds.Height <= 0) return;
            using (GraphicsPath path = new GraphicsPath())
            {
                int r2 = radius * 2;
                path.AddArc(bounds.X, bounds.Y, r2, r2, 180, 90);
                path.AddArc(bounds.Right - r2, bounds.Y, r2, r2, 270, 90);
                path.AddArc(bounds.Right - r2, bounds.Bottom - r2, r2, r2, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - r2, r2, r2, 90, 90);
                path.CloseFigure();
                g.DrawPath(pen, path);
            }
        }
    }
}