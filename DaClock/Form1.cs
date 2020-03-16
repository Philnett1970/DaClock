using DaClock.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaClock
{
    public partial class Form1 : Form
    {
       
       
        //float borderSize = Settings.Default.borderSize;
        Color Bcolor = Settings.Default.Border;
        Color tip = Settings.Default.Tip;
        Color c1 = Settings.Default.C1;
        Color c2 = Settings.Default.C2;
        Color c3 = Settings.Default.C3;
        Color c4 = Settings.Default.C4;
        Color c5 = Settings.Default.C5;
        Color c6 = Settings.Default.C6;

       
        StreamWriter sw;

        //Form2 frm = new Form2();


        public Form1()
        {
            InitializeComponent();

            if (!Directory.Exists(@"C:\Philnett\DaClock"))
            {
                Directory.CreateDirectory(@"C:\Philnett\DaClock");
            }
            if (!File.Exists(@"C:\Philnett\DaClock\cSchemes.txt"))
            { sw = File.CreateText(@"C:\Philnett\DaClock\cSchemes.txt"); sw.Write(Resources.cSchemes); sw.Close(); }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notify1.Visible = true;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 10);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime timenow = DateTime.Now;
            string time = timenow.GetDateTimeFormats('T')[0];
            lblTime.Text = time;
            if (lblTime.Text == "12:00:00 AM")
            {
                //  lblDate.Text = String.Format("{0:D}", timenow);
                this.panel2.Invalidate();
            }
           
            this.panel1.Invalidate();
        }


        public void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, 350, 49);
            LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Chocolate, Color.White, LinearGradientMode.Vertical);

            // Create color and points arrays
            Color[] clrArray =
            {
             tip, c1, c2,  c3, c4, c5,c6
            };

            float[] posArray =
            {    
                      //  tip\c1     c1\2   c2\3     c3\4        c4\5     c5\6
                     0f,   0.3f,     0.4f,  0.6f,    0.7f,       0.8f,     1f
            };

            // Create a ColorBlend object and set its Colors and Positions properties
            ColorBlend colorBlend = new ColorBlend();
            colorBlend.Colors = clrArray;
            colorBlend.Positions = posArray;

            // Set InterpolationColors property
            lBrush.InterpolationColors = colorBlend;

            // Draw shapes
            //     g.FillRectangle(lBrush, rect);
            DateTime timenow = DateTime.Now;
            string time = timenow.GetDateTimeFormats('t')[0];
            FontFamily fontFamily = new FontFamily("Comic Sans MS");
            StringFormat strformat = new StringFormat();
            GraphicsPath path = new GraphicsPath();
            string szbuf = time;
            path.AddString(szbuf, fontFamily,
                (int)FontStyle.Bold, 54.0f, new Point(0, -15), strformat);
            Pen pen = new Pen(Bcolor, Settings.Default.borderSize);
            pen.LineJoin = LineJoin.Round;
            e.Graphics.DrawPath(pen, path);
            e.Graphics.FillPath(lBrush, path);

            // Dispose of object
            fontFamily.Dispose();
            path.Dispose();
            pen.Dispose();
            e.Graphics.Dispose();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 9, 375, 45);
            LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Chocolate, Color.White, LinearGradientMode.Vertical);

            // Create color and points arrays
            Color[] clrArray =
            {
             tip, c1, c2,  c3, c4, c5,c6
            };

            float[] posArray =
            {     // P     ?     c3
         //     0.0f, 0.4f, 0.3f, 0.5f, 0.6f, 1.0f
            //    0.0f, 0.4f, 0.5f, 0.5f, 0.6f, 0.7f, 1f
                      //                 tip \        c1     c1\2   c2\3     c3\4        c4\5     c5\6
                      //                 0f,        0.3f,    0.3f,  0.4f,    0.5f,       0.6f,     1f

                     //  tip\c1     c1\2   c2\3     c3\4        c4\5     c5\6
                    0f,   0.3f,     0.4f,  0.6f,    0.7f,       0.8f,     1f

            };

            // Create a ColorBlend object and set its Colors and Positions properties
            ColorBlend colorBlend = new ColorBlend();
            colorBlend.Colors = clrArray;
            colorBlend.Positions = posArray;

            // Set InterpolationColors property
            lBrush.InterpolationColors = colorBlend;

            // Draw shapes
            //   g.FillRectangle(lBrush, rect);
            DateTime timenow = DateTime.Now;
            FontFamily fontFamily = new FontFamily("Comic Sans MS");
            StringFormat strformat = new StringFormat();
            GraphicsPath path = new GraphicsPath();
            string s = String.Format("{0:M/d/yy}", timenow);
            string szbuf = s;
            path.AddString(szbuf, fontFamily,
                (int)FontStyle.Bold, 55.0f, new Point(0, -8), strformat);
            Pen pen = new Pen(Bcolor, 2);
            pen.LineJoin = LineJoin.Round;
            e.Graphics.DrawPath(pen, path);
            e.Graphics.FillPath(lBrush, path);

            // Dispose of object
            g.Dispose();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams result = base.CreateParams;
                result.ExStyle |= 0x02000000; // WS_EX_COMPOSITED 
                return result;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notify1.Visible = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notify1.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notify1.Visible = false;
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Form2();
            frm.Show();
        }


        public static void AddApplicationToStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("DaClock", Application.StartupPath + "\\DaClock.exe", RegistryValueKind.String);
                key.Close();
            }
        }

        public static void RemoveApplicationFromStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("DaClock", false);
                key.Close();
            }
        }

        private void notify1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                contextMenuStrip1.Show(this, this.PointToClient(Cursor.Position));
            }
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddApplicationToStartup();
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveApplicationFromStartup();
        }
    }
}
