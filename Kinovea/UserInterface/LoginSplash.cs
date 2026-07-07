using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Kinovea.Root
{
    public partial class LoginSplash : Form
    {
        private readonly string fixedUser = "demo";
        private readonly string fixedPwd = "archery123";
        private int errorTimes = 0;

        private Image backgroundImage;

        private Color overlayBg = Color.FromArgb(160, 5, 10, 20);
        private Color borderGlow = Color.FromArgb(80, 210, 180, 60);
        private Color accGold = Color.FromArgb(220, 190, 70);
        private Color accCyan = Color.FromArgb(0, 200, 220);
        private Color inputLine = Color.FromArgb(80, 110, 130);
        private Color inputFocusColor = Color.FromArgb(0, 220, 240);

        public LoginSplash()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // 从嵌入式资源加载背景图
            backgroundImage = LoadBackgroundImage();

            txtPassword.PasswordChar = '*';
            txtPassword.UseSystemPasswordChar = false;

            txtAccount.GotFocus += (s, e) => Invalidate();
            txtAccount.LostFocus += (s, e) => Invalidate();
            txtPassword.GotFocus += (s, e) => Invalidate();
            txtPassword.LostFocus += (s, e) => Invalidate();

            btnLogin.MouseEnter += (s, e) => { btnLogin.BackColor = Color.FromArgb(0, 210, 230); };
            btnLogin.MouseLeave += (s, e) => { btnLogin.BackColor = Color.FromArgb(0, 190, 210); };
        }

        /// <summary>
        /// 窗口加载：创建一次圆角 Region，后续通过 OnResize 更新。
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateWindowRegion();
        }

        /// <summary>
        /// 窗口大小变化时释放旧 Region，创建新 Region。
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateWindowRegion();
        }

        /// <summary>
        /// 释放当前 Region 及背景图。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Region != null)
                {
                    this.Region.Dispose();
                }
                if (backgroundImage != null)
                {
                    backgroundImage.Dispose();
                    backgroundImage = null;
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 创建/更新窗口圆角 Region，自动释放旧 Region 防止 GDI 句柄泄漏。
        /// </summary>
        private void UpdateWindowRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            IntPtr hrgn = NativeMethods.CreateRoundRectRgn(0, 0, Width, Height, 12, 12);
            if (hrgn != IntPtr.Zero)
            {
                Region newRegion = Region.FromHrgn(hrgn);
                // 立即释放 GDI 句柄（Region.FromHrgn 已复制数据，句柄可释放）
                NativeMethods.DeleteObject(hrgn);

                // 交换：赋新 Region，释放旧 Region
                Region oldRegion = this.Region;
                this.Region = newRegion;
                if (oldRegion != null)
                    oldRegion.Dispose();
            }
        }

        private Image LoadBackgroundImage()
        {
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                string resourceName = "Kinovea.Root.Resources.LoginBackground.png";
                using (Stream stream = asm.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                        return Image.FromStream(stream);
                }
            }
            catch { }
            return null;
        }

        private void LoginSplash_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 1. 背景图片（等比例缩放填满窗口）
            if (backgroundImage != null)
            {
                Rectangle srcRect, dstRect;
                float imgAspect = (float)backgroundImage.Width / backgroundImage.Height;
                float formAspect = (float)Width / Height;

                if (imgAspect > formAspect)
                {
                    int cropW = (int)(backgroundImage.Height * formAspect);
                    srcRect = new Rectangle((backgroundImage.Width - cropW) / 2, 0, cropW, backgroundImage.Height);
                }
                else
                {
                    int cropH = (int)(backgroundImage.Width / formAspect);
                    srcRect = new Rectangle(0, (backgroundImage.Height - cropH) / 2, backgroundImage.Width, cropH);
                }
                dstRect = new Rectangle(0, 0, Width, Height);
                g.DrawImage(backgroundImage, dstRect, srcRect, GraphicsUnit.Pixel);
            }
            else
            {
                // 无背景图时的纯色底
                using (LinearGradientBrush bg = new LinearGradientBrush(
                    ClientRectangle, Color.FromArgb(8, 22, 40), Color.FromArgb(15, 40, 55), LinearGradientMode.Vertical))
                {
                    g.FillRectangle(bg, ClientRectangle);
                }
            }

            // 2. 整体暗色叠加层（让 UI 控件更清晰）
            using (SolidBrush dimBrush = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
            {
                g.FillRectangle(dimBrush, ClientRectangle);
            }

            // 3. 顶部/底部渐隐遮罩
            using (LinearGradientBrush topFade = new LinearGradientBrush(
                new Rectangle(0, 0, Width, 120), Color.FromArgb(120, 0, 0, 0), Color.Transparent, LinearGradientMode.Vertical))
            {
                g.FillRectangle(topFade, 0, 0, Width, 120);
            }
            using (LinearGradientBrush bottomFade = new LinearGradientBrush(
                new Rectangle(0, Height - 80, Width, 80), Color.Transparent, Color.FromArgb(160, 0, 0, 0), LinearGradientMode.Vertical))
            {
                g.FillRectangle(bottomFade, 0, Height - 80, Width, 80);
            }

            // 4. 中间登录卡片
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

            // 5. HUD 装饰元素 - 左上角靶心
            DrawHUDTarget(g, 35, 35, 18);

            // 6. 右侧功能面板（参考图右侧 System Functions）
            DrawRightPanel(g);

            // 7. 底部三个特性项
            DrawFeatureItems(g);

            // 8. 输入框底部高亮分隔线
            DrawInputUnderline(g, txtAccount, txtAccount.Focused ? inputFocusColor : inputLine);
            DrawInputUnderline(g, txtPassword, txtPassword.Focused ? inputFocusColor : inputLine);
        }

        private void DrawHUDTarget(Graphics g, int cx, int cy, int r)
        {
            int[] radii = { r, r * 3 / 4, r / 2, r / 4 };
            Color[] colors = {
                Color.FromArgb(40, 50, 70),
                Color.FromArgb(60, 80, 100),
                Color.FromArgb(80, 120, 140),
                Color.FromArgb(220, 190, 70)
            };
            for (int i = 0; i < radii.Length; i++)
            {
                using (SolidBrush b = new SolidBrush(colors[i]))
                {
                    int d = radii[i] * 2;
                    g.FillEllipse(b, cx - radii[i], cy - radii[i], d, d);
                }
            }

            // 十字准星
            using (Pen p = new Pen(Color.FromArgb(180, 220, 240), 1))
            {
                g.DrawLine(p, cx - r, cy, cx + r, cy);
                g.DrawLine(p, cx, cy - r, cx, cy + r);
            }
        }

        private void DrawRightPanel(Graphics g)
        {
            Rectangle panelRect = new Rectangle(400, 130, 170, 220);
            using (GraphicsPath path = RoundRect(panelRect, 6))
            using (SolidBrush fill = new SolidBrush(Color.FromArgb(100, 5, 12, 22)))
            using (Pen border = new Pen(Color.FromArgb(60, 180, 200, 220), 1))
            {
                g.FillPath(fill, path);
                g.DrawPath(border, path);
            }

            // 面板标题
            using (Font titleFont = new Font("Microsoft YaHei", 10F, FontStyle.Bold))
            using (SolidBrush titleBrush = new SolidBrush(accCyan))
            {
                g.DrawString("系统功能", titleFont, titleBrush, panelRect.X + 10, panelRect.Y + 12);
            }

            // 分隔线
            using (Pen sep = new Pen(Color.FromArgb(40, 160, 190, 220), 1))
            {
                g.DrawLine(sep, panelRect.X + 8, panelRect.Y + 36, panelRect.Right - 8, panelRect.Y + 36);
            }

            // 功能项
            string[] items = { "视频动作分析", "关节角度评估", "训练报告生成", "数据导出" };
            int y = panelRect.Y + 48;
            using (Font itemFont = new Font("Microsoft YaHei", 9F))
            using (SolidBrush itemBrush = new SolidBrush(Color.FromArgb(170, 200, 220)))
            {
                for (int i = 0; i < items.Length; i++)
                {
                    // 小圆点指示
                    g.FillEllipse(Brushes.White, panelRect.X + 12, y + 5, 5, 5);
                    g.DrawString(items[i], itemFont, itemBrush, panelRect.X + 24, y);
                    y += 36;
                }
            }
        }

        private void DrawFeatureItems(Graphics g)
        {
            string[] items = { "动作序列分析", "关节角度评估", "训练报告生成" };
            int startX = 90;
            int y = 400;
            int spacing = 120;

            using (Font font = new Font("Microsoft YaHei", 9F, FontStyle.Regular))
            using (SolidBrush dotBrush = new SolidBrush(accCyan))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(150, 190, 210)))
            {
                for (int i = 0; i < items.Length; i++)
                {
                    int x = startX + i * spacing;
                    g.FillEllipse(dotBrush, x, y + 4, 6, 6);
                    g.DrawString(items[i], font, textBrush, x + 12, y);
                }
            }
        }

        private void DrawInputUnderline(Graphics g, TextBox tb, Color color)
        {
            using (Pen pen = new Pen(color, 2))
            {
                g.DrawLine(pen, tb.Left, tb.Bottom + 3, tb.Right, tb.Bottom + 3);
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
