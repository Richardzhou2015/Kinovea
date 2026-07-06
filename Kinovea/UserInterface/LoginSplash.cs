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

        // Color palette
        private Color bgTop = Color.FromArgb(8, 22, 40);
        private Color bgBottom = Color.FromArgb(15, 40, 55);
        private Color accTeal = Color.FromArgb(0, 180, 200);
        private Color accGold = Color.FromArgb(220, 180, 60);
        private Color cardBg = Color.FromArgb(180, 10, 25, 42);
        private Color textLight = Color.FromArgb(210, 220, 230);
        private Color textDim = Color.FromArgb(140, 160, 180);
        private Color inputLine = Color.FromArgb(60, 100, 120);
        private Color inputFocus = Color.FromArgb(0, 200, 220);
        private Color btnColor = Color.FromArgb(0, 180, 200);

        // Geometric pattern seed
        private readonly int patternSeed;

        public LoginSplash()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            patternSeed = DateTime.Now.Millisecond;

            txtPassword.PasswordChar = '*';
            txtPassword.UseSystemPasswordChar = false;

            txtAccount.GotFocus += (s, e) => Invalidate();
            txtAccount.LostFocus += (s, e) => Invalidate();
            txtPassword.GotFocus += (s, e) => Invalidate();
            txtPassword.LostFocus += (s, e) => Invalidate();

            // 悬浮效果
            btnLogin.MouseEnter += (s, e) => { btnLogin.BackColor = Color.FromArgb(0, 200, 220); };
            btnLogin.MouseLeave += (s, e) => { btnLogin.BackColor = btnColor; };
        }

        private void LoginSplash_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 窗口圆角
            Region = System.Drawing.Region.FromHrgn(NativeMethods.CreateRoundRectRgn(0, 0, Width, Height, 12, 12));

            // 1. 背景渐变
            using (LinearGradientBrush bgBrush = new LinearGradientBrush(
                new Rectangle(0, 0, Width, Height), bgTop, bgBottom, LinearGradientMode.Vertical))
            {
                g.FillRectangle(bgBrush, ClientRectangle);
            }

            // 2. 几何底纹（低多边形三角网格）
            DrawGeometricPattern(g);

            // 3. 光效粒子
            DrawParticles(g);

            // 4. 右侧弓箭剪影
            DrawBowAndArrow(g);

            // 5. 左侧靶心装饰
            DrawTargetIcon(g, 70, 85, 35);

            // 6. 中心登录卡片（半透明背景）
            Rectangle cardRect = new Rectangle(80, 120, 440, 280);
            using (GraphicsPath cardPath = RoundRect(cardRect, 10))
            using (SolidBrush cardBrush = new SolidBrush(cardBg))
            {
                g.FillPath(cardBrush, cardPath);
            }
            // 卡片边框
            using (Pen borderPen = new Pen(Color.FromArgb(50, 100, 130), 1))
            using (GraphicsPath cardPath = RoundRect(cardRect, 10))
            {
                g.DrawPath(borderPen, cardPath);
            }

            // 7. 底部功能区（三个特性项）
            DrawFeatureItems(g);

            // 8. 输入框底部分隔线
            DrawInputUnderline(g, txtAccount, txtAccount.Focused ? inputFocus : inputLine);
            DrawInputUnderline(g, txtPassword, txtPassword.Focused ? inputFocus : inputLine);
        }

        private void DrawGeometricPattern(Graphics g)
        {
            Random rnd = new Random(patternSeed);
            int spacing = 80;
            using (Pen linePen = new Pen(Color.FromArgb(18, 60, 80), 1))
            {
                for (int x = 0; x < Width + spacing; x += spacing)
                {
                    for (int y = 0; y < Height + spacing; y += spacing)
                    {
                        int ox = rnd.Next(-15, 15);
                        int oy = rnd.Next(-15, 15);
                        Point p1 = new Point(x + ox, y + oy);
                        Point p2 = new Point(x + spacing + rnd.Next(-10, 10), y + rnd.Next(-10, 10));
                        Point p3 = new Point(x + rnd.Next(-10, 10), y + spacing + rnd.Next(-10, 10));
                        g.DrawLine(linePen, p1, p2);
                        g.DrawLine(linePen, p1, p3);
                        g.DrawLine(linePen, p2, p3);
                    }
                }
            }
        }

        private void DrawParticles(Graphics g)
        {
            Random rnd = new Random(patternSeed + 1);
            for (int i = 0; i < 30; i++)
            {
                int x = rnd.Next(0, Width);
                int y = rnd.Next(0, Height);
                int r = rnd.Next(2, 6);
                int alpha = rnd.Next(30, 90);
                using (SolidBrush b = new SolidBrush(Color.FromArgb(alpha, 180, 220, 255)))
                {
                    g.FillEllipse(b, x, y, r, r);
                }
            }
        }

        private void DrawBowAndArrow(Graphics g)
        {
            int cx = Width - 60;
            int cy = 140;

            // 弓 — 用曲线画半圆弧
            using (Pen bowPen = new Pen(Color.FromArgb(60, 150, 180), 2))
            {
                Point p1 = new Point(cx - 10, cy - 60);
                Point p2 = new Point(cx - 10, cy + 60);
                // 弓的弧形（左凸）
                g.DrawArc(bowPen, cx - 45, cy - 60, 70, 120, 270, 180);
                // 弓弦
                g.DrawLine(bowPen, p1, p2);
            }

            // 箭 — 直线+箭头
            using (Pen arrowPen = new Pen(Color.FromArgb(180, 200, 210), 2))
            {
                // 箭杆
                g.DrawLine(arrowPen, cx - 10, cy, cx + 40, cy);
                // 箭头（三角形）
                PointF[] arrowHead = {
                    new PointF(cx + 40, cy),
                    new PointF(cx + 50, cy - 6),
                    new PointF(cx + 50, cy + 6)
                };
                g.FillPolygon(Brushes.White, arrowHead);
            }

            // 箭尾羽毛
            using (Pen featherPen = new Pen(Color.FromArgb(100, 200, 210), 1))
            {
                g.DrawLine(featherPen, cx - 10, cy, cx - 18, cy - 8);
                g.DrawLine(featherPen, cx - 10, cy, cx - 18, cy + 8);
            }
        }

        private void DrawTargetIcon(Graphics g, int cx, int cy, int r)
        {
            int[] radii = { r, r * 3 / 4, r / 2, r / 4 };
            Color[] colors = { Color.FromArgb(40, 100, 120), Color.FromArgb(60, 130, 150), Color.FromArgb(80, 160, 180), Color.FromArgb(200, 180, 60) };

            for (int i = 0; i < radii.Length; i++)
            {
                using (SolidBrush b = new SolidBrush(colors[i]))
                {
                    int d = radii[i] * 2;
                    g.FillEllipse(b, cx - radii[i], cy - radii[i], d, d);
                }
            }

            // 准星十字
            using (Pen crossPen = new Pen(Color.FromArgb(180, 200, 220), 1))
            {
                g.DrawLine(crossPen, cx - 8, cy, cx + 8, cy);
                g.DrawLine(crossPen, cx, cy - 8, cx, cy + 8);
            }
        }

        private void DrawInputUnderline(Graphics g, TextBox tb, Color color)
        {
            using (Pen pen = new Pen(color, 2))
            {
                g.DrawLine(pen, tb.Left, tb.Bottom + 3, tb.Right, tb.Bottom + 3);
            }
        }

        private void DrawFeatureItems(Graphics g)
        {
            string[] items = { "动作序列分析", "关节角度评估", "训练报告生成" };
            int startX = 120;
            int y = 425;
            int spacing = 130;

            using (Font font = new Font("Microsoft YaHei", 9F, FontStyle.Regular))
            {
                for (int i = 0; i < items.Length; i++)
                {
                    int x = startX + i * spacing;

                    // 小圆点
                    using (SolidBrush dotBrush = new SolidBrush(accTeal))
                    {
                        g.FillEllipse(dotBrush, x, y + 4, 6, 6);
                    }

                    // 文字
                    using (SolidBrush textBrush = new SolidBrush(textDim))
                    {
                        g.DrawString(items[i], font, textBrush, x + 12, y);
                    }
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

        private void LoginSplash_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                Application.Exit();
            }
        }

        // ---------- Helper: 圆角矩形 Path ----------
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

    internal static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);
    }
}