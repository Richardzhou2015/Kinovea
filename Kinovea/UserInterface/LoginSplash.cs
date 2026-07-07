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

        private Color borderGlow = Color.FromArgb(80, 210, 180, 60);
        private Color accGold = Color.FromArgb(220, 190, 70);
        private Color accCyan = Color.FromArgb(0, 200, 220);

        public LoginSplash()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            txtPassword.PasswordChar = '*';
            txtPassword.UseSystemPasswordChar = false;

            // 密码框回车 = 登录
            txtPassword.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    e.Handled = true;
                    btnLogin_Click(s, e);
                }
            };
        }

        // ═══════════════════════════════════════════════════════════════════
        // Region 生命周期管理：将窗口圆角从 Paint 事件中移出
        // ───────────────────────────────────────────────────────────────────
        // 修复前：LoginSplash_Paint 每帧 new Region.FromHrgn(...) 赋值给
        //   this.Region，旧 Region 从不 Dispose → GDI 句柄泄漏 → 登录闪退
        // 修复后：OnLoad 创建一次，OnResize 重建，UpdateWindowRegion 中
        //   swap 旧 Region 并 Dispose。同时 Designer.cs Dispose 中追加
        //   this.Region.Dispose() 和 backgroundImage.Dispose()。
        // ═══════════════════════════════════════════════════════════════════

        /// <summary>窗口加载时创建一次窗口圆角 Region</summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateWindowRegion();
        }

        /// <summary>窗口大小变化时重建窗口圆角 Region</summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateWindowRegion();
        }

        /// <summary>
        /// 更新窗口圆角 Region，自动释放旧 Region 防止 GDI 句柄泄漏。
        /// 流程：CreateRoundRectRgn → FromHrgn 复制 → DeleteObject 释放 GDI 句柄
        ///       → swap this.Region → Dispose 旧 Region
        /// </summary>
        private void UpdateWindowRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            IntPtr hrgn = NativeMethods.CreateRoundRectRgn(0, 0, Width, Height, 12, 12);
            if (hrgn != IntPtr.Zero)
            {
                // FromHrgn 内部复制 HRGN 数据到托管 Region，原始 GDI 句柄可立即释放
                Region newRegion = Region.FromHrgn(hrgn);
                NativeMethods.DeleteObject(hrgn);

                // swap：this.Region 赋值前先保存旧引用，赋值后 Dispose
                Region oldRegion = this.Region;
                this.Region = newRegion;
                if (oldRegion != null)
                    oldRegion.Dispose();
            }
        }

        private void LoginSplash_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 1. 背景渐变（深青体育主题，与控件风格统一）
            using (LinearGradientBrush bg = new LinearGradientBrush(
                ClientRectangle, Color.FromArgb(8, 22, 40), Color.FromArgb(18, 48, 68), LinearGradientMode.Vertical))
            {
                g.FillRectangle(bg, ClientRectangle);
            }

            // 2. 微妙径向光晕（窗口中央偏上，增加层次感）
            using (GraphicsPath glowPath = new GraphicsPath())
            {
                int glowR = Math.Max(Width, Height) / 2;
                glowPath.AddEllipse(Width / 2 - glowR / 2, -glowR / 2, glowR, glowR);
                using (PathGradientBrush glowBrush = new PathGradientBrush(glowPath))
                {
                    glowBrush.CenterPoint = new PointF(Width / 2f, Height / 4f);
                    glowBrush.CenterColor = Color.FromArgb(40, 60, 90);
                    glowBrush.SurroundColors = new Color[] { Color.Transparent };
                    g.FillRectangle(glowBrush, ClientRectangle);
                }
            }

            // 3. 中间登录卡片
            Rectangle cardRect = new Rectangle(65, 100, 470, 270);
            using (GraphicsPath cardPath = RoundRect(cardRect, 8))
            {
                // 卡片内部半透填充
                using (SolidBrush cardFill = new SolidBrush(Color.FromArgb(170, 8, 16, 28)))
                {
                    g.FillPath(cardFill, cardPath);
                }
                // 卡片发光边框
                using (Pen borderPen = new Pen(borderGlow, 2))
                {
                    g.DrawPath(borderPen, cardPath);
                }
                // 卡片顶部高光条
                Rectangle highlightBar = new Rectangle(cardRect.X + 2, cardRect.Y + 2, cardRect.Width - 4, 3);
                using (LinearGradientBrush highBrush = new LinearGradientBrush(
                    highlightBar, accGold, Color.Transparent, LinearGradientMode.Horizontal))
                {
                    g.FillRectangle(highBrush, highlightBar);
                }
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

        /// <summary>取消按钮：关闭程序</summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LoginSplash_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private GraphicsPath RoundRect(Rectangle r, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
