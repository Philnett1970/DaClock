using DaClock.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaClock
{
    public partial class Form2 : Form
    {
        DateTime timenow = DateTime.Now;
        float borderSize = Settings.Default.borderSize;
        Color border = Settings.Default.Border;
        Color tip = Settings.Default.Tip;
        Color c1 = Settings.Default.C1;
        Color c2 = Settings.Default.C2;
        Color c3 = Settings.Default.C3;
        Color c4 = Settings.Default.C4;
        Color c5 = Settings.Default.C5;
        Color c6 = Settings.Default.C6;
        FontConverter fc = new FontConverter();
        Rectangle rect = new Rectangle(0, 0, 350, 49);
        FileStream fs = default(FileStream);
        StreamReader sr = default(StreamReader);
        BufferedStream bs = default(BufferedStream);
      



        public Form2()
        {
            InitializeComponent();
            pictureBox1.BackColor = Settings.Default.Border;
            pictureBox2.BackColor = Settings.Default.Tip;
            pictureBox3.BackColor = Settings.Default.C1;
            pictureBox4.BackColor = Settings.Default.C2;
            pictureBox5.BackColor = Settings.Default.C3;
            pictureBox6.BackColor = Settings.Default.C4;
            pictureBox7.BackColor = Settings.Default.C5;
            pictureBox8.BackColor = Settings.Default.C6;

            lblBorderCode.Text = ToHtml(pictureBox1.BackColor);
            lblGlossColorCode.Text = ToHtml(pictureBox2.BackColor);
            lblC1code.Text = ToHtml(pictureBox3.BackColor);
            lblC2Code.Text = ToHtml(pictureBox4.BackColor);
            lblC3.Text = ToHtml(pictureBox5.BackColor);
            lblC4code.Text = ToHtml(pictureBox6.BackColor);
            lblC5code.Text = ToHtml(pictureBox7.BackColor);
            lblC6code.Text = ToHtml(pictureBox8.BackColor);

            OpentextFile(@"C:\Philnett\DaClock\cSchemes.txt");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == ">>>")
            {
                button1.Text = "<<<";

                while (this.Width < 738)
                {
                    this.Width++;

                }
                this.Width = 738;

            }
            else if (button1.Text == "<<<")
            {
                button1.Text = ">>>";

                while (this.Width > 540)
                {
                    this.Width--;

                }
                this.Width = 540;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timenow = DateTime.Now;
            string time = timenow.GetDateTimeFormats('T')[0];
            lblTime.Text = time;
            if (lblTime.Text == "12:00:00 AM")
            {
                //      lblDate.Text = String.Format("{0:d}", timenow);
            }
        }

        public void RePaint(object sender, PaintEventArgs e)
        {
            try
            {
                //panel1.Invalidate(rect, true);
                panel2.Invalidate(rect, true);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Graphics g = e.Graphics;


                LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Chocolate, Color.White, LinearGradientMode.Vertical);
                // Create color and points arrays
                Color[] clrArray =
                {
             pictureBox2.BackColor, pictureBox3.BackColor,pictureBox4.BackColor,
                pictureBox5.BackColor, pictureBox6.BackColor,
                pictureBox7.BackColor, pictureBox8.BackColor
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
                string time = timenow.GetDateTimeFormats('T')[0];
                FontFamily fontFamily = new FontFamily("Comic Sans MS");
                StringFormat strformat = new StringFormat();
                string s = String.Format("{0:D}", timenow);
                string szbuf = s;

                GraphicsPath path = new GraphicsPath();

                path.AddString("DaClock", fontFamily,
                    (int)FontStyle.Bold, 54.0f, new Point(0, -15), strformat);
                Pen pen = new Pen(pictureBox1.BackColor, float.Parse(textBox1.Text,
                 System.Globalization.CultureInfo.InvariantCulture));
                pen.LineJoin = LineJoin.Round;
                e.Graphics.DrawPath(pen, path);
                e.Graphics.FillPath(lBrush, path);

                // Dispose of object
                fontFamily.Dispose();
                path.Dispose();
                pen.Dispose();
                e.Graphics.Dispose();
            }
            catch
            { }
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
            string time = timenow.GetDateTimeFormats('T')[0];
            FontFamily fontFamily = new FontFamily("Comic Sans MS");
            StringFormat strformat = new StringFormat();
            GraphicsPath path = new GraphicsPath();
            string szbuf = time;
            path.AddString("DaClock", fontFamily,
                (int)FontStyle.Bold, 54.0f, new Point(0, -15), strformat);
            Pen pen = new Pen(border, 4);
            pen.LineJoin = LineJoin.Round;
            e.Graphics.DrawPath(pen, path);
            e.Graphics.FillPath(lBrush, path);

            // Dispose of object
            fontFamily.Dispose();
            path.Dispose();
            pen.Dispose();
            e.Graphics.Dispose();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Visible = true;
            panel2.Visible = true;
            textBox1.Text = borderSize.ToString();
            comboBox1.Text = Settings.Default.activeColors;
            textBox1.Text = Settings.Default.borderSize.ToString();
            panel2.Paint += new PaintEventHandler(RePaint);

            // panel1.Paint += new PaintEventHandler(OnPaint);
            // lblDate.Paint += new PaintEventHandler(OnPaint);
        }

        private void btnBorderColor_Click(object sender, EventArgs e)
        {

            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.Black)
                {
                    MessageBox.Show("Can't use that color");
                }
                else
                {

                    pictureBox1.BackColor = colorDialog1.Color;
                    //Settings.Default.Border = pictureBox1.BackColor;

                    //  panel1.Paint += new System.Windows.Forms.PaintEventHandler(RePaint);
                    lblBorderCode.Text = ToHtml(colorDialog1.Color);
                    //  button2.PerformClick();

                }
            }
        }

        private void btnGlossColor_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.Black)
                {
                    MessageBox.Show("Can't use that color");
                }
                else
                {

                    pictureBox2.BackColor = colorDialog1.Color;
                    //Settings.Default.Border = pictureBox1.BackColor;

                    //  panel1.Paint += new System.Windows.Forms.PaintEventHandler(RePaint);
                    lblGlossColorCode.Text = ToHtml(colorDialog1.Color);
                }
            }
        }

        private void btnC1_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.Black)
                {
                    MessageBox.Show("Can't use that color");
                }
                else
                {

                    pictureBox3.BackColor = colorDialog1.Color;
                    //Settings.Default.Border = pictureBox1.BackColor;

                    //  panel1.Paint += new System.Windows.Forms.PaintEventHandler(RePaint);
                    lblC1code.Text = ToHtml(colorDialog1.Color);
                }
            }
        }

        private void btnC2_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.Black)
                {
                    MessageBox.Show("Can't use that color");
                }
                else
                {

                    pictureBox4.BackColor = colorDialog1.Color;
                    //Settings.Default.Border = pictureBox1.BackColor;

                    //  panel1.Paint += new System.Windows.Forms.PaintEventHandler(RePaint);
                    lblC2Code.Text = ToHtml(colorDialog1.Color);
                }
            }
        }

        private void btnC3_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.Black)
                {
                    MessageBox.Show("Can't use that color");
                }
                else
                {

                    pictureBox5.BackColor = colorDialog1.Color;
                    //Settings.Default.Border = pictureBox1.BackColor;

                    //  panel1.Paint += new System.Windows.Forms.PaintEventHandler(RePaint);
                    lblC3.Text = ToHtml(colorDialog1.Color);
                }
            }
        }

        private void btnC4_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.Black)
                {
                    MessageBox.Show("Can't use that color");
                }
                else
                {

                    pictureBox6.BackColor = colorDialog1.Color;
                    //Settings.Default.Border = pictureBox1.BackColor;

                    //  panel1.Paint += new System.Windows.Forms.PaintEventHandler(RePaint);
                    lblC4code.Text = ToHtml(colorDialog1.Color);
                }
            }
        }

        private void btnC5_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.Black)
                {
                    MessageBox.Show("Can't use that color");
                }
                else
                {

                    pictureBox7.BackColor = colorDialog1.Color;
                    //Settings.Default.Border = pictureBox1.BackColor;

                    //  panel1.Paint += new System.Windows.Forms.PaintEventHandler(RePaint);
                    lblC5code.Text = ToHtml(colorDialog1.Color);
                }
            }
        }

        private void btnBottomColor_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.Black)
                {
                    MessageBox.Show("Can't use that color");
                }
                else
                {

                    pictureBox8.BackColor = colorDialog1.Color;
                    //Settings.Default.Border = pictureBox1.BackColor;

                    //  panel1.Paint += new System.Windows.Forms.PaintEventHandler(RePaint);
                    lblC6code.Text = ToHtml(colorDialog1.Color);
                }
            }
        }

        public static string ToHtml(System.Drawing.Color color)
        {
            if (System.Drawing.Color.Transparent == color)
                return "Transparent";
            return string.Concat("#", (color.ToArgb() & 0x00FFFFFF).ToString("X6"));
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

        private void btnCancelChanges_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Settings.Default.Border;
            lblBorderCode.Text = "---";
            pictureBox2.BackColor = Settings.Default.Tip;
            lblGlossColorCode.Text = "---";
            pictureBox3.BackColor = Settings.Default.C1;
            lblC1code.Text = "---";
            pictureBox4.BackColor = Settings.Default.C2;
            lblC2Code.Text = "---";
            pictureBox5.BackColor = Settings.Default.C3;
            lblC3.Text = "---";
            pictureBox6.BackColor = Settings.Default.C4;
            lblC4code.Text = "---";
            pictureBox7.BackColor = Settings.Default.C5;
            lblC5code.Text = "---";
            pictureBox8.BackColor = Settings.Default.C6;
            lblC6code.Text = "---";

            MessageBox.Show("All settings have been restored the there privious settings.",
                "Process Completed", MessageBoxButtons.OK);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void ActiveColors()
        {
            if (comboBox1.Text == comboBox1.SelectedIndex.ToString("1"))
            {
                //2
                btnGlossColor.Enabled = false;
                pictureBox2.BackColor = Color.Transparent;
                btnGlossColor.Enabled = false;
                lblGlossColorCode.Text = "---";
                //3

                pictureBox3.BackColor = Color.Transparent;
                btnC1.Enabled = false;
                lblC1code.Text = "---";
                //4
                pictureBox4.BackColor = Color.Transparent;
                btnC2.Enabled = false;
                lblC2Code.Text = "---";
                //5
                pictureBox5.BackColor = Color.Transparent;
                btnC3.Enabled = false;
                lblC3.Text = "---";
                //6
                pictureBox6.BackColor = Color.Transparent;
                btnC4.Enabled = false;
                lblC4code.Text = "---";
                //7
                pictureBox7.BackColor = Color.Transparent;
                btnC5.Enabled = false;
                lblC5code.Text = "---";
                //8
                pictureBox8.BackColor = Color.Transparent;
                btnBottomColor.Enabled = false;
                lblC6code.Text = "---";

                MessageBox.Show("One Active Color Set.",
               "DaClock", MessageBoxButtons.OK);
            }
            if (comboBox1.Text == comboBox1.SelectedIndex.ToString("2"))
            {
                btnGlossColor.Enabled = true;
                //3
                pictureBox3.BackColor = Color.Transparent;
                btnC1.Enabled = false;
                lblC1code.Text = "---";
                //4
                pictureBox4.BackColor = Color.Transparent;
                btnC2.Enabled = false;
                lblC2Code.Text = "---";
                //5
                pictureBox5.BackColor = Color.Transparent;
                btnC3.Enabled = false;
                lblC3.Text = "---";
                //6
                pictureBox6.BackColor = Color.Transparent;
                btnC4.Enabled = false;
                lblC4code.Text = "---";
                //7
                pictureBox7.BackColor = Color.Transparent;
                btnC5.Enabled = false;
                lblC5code.Text = "---";
                //8
                pictureBox8.BackColor = Color.Transparent;
                btnBottomColor.Enabled = false;
                lblC6code.Text = "---";
                MessageBox.Show("2 Active Color Set.",
               "DaClock", MessageBoxButtons.OK);
            }
            if (comboBox1.Text == comboBox1.SelectedIndex.ToString("3"))
            {

                pictureBox3.BackColor = Settings.Default.C1;
                btnC1.Enabled = true;
                //4
                pictureBox4.BackColor = Color.Transparent;
                btnC2.Enabled = false;
                lblC2Code.Text = "---";
                //5
                pictureBox5.BackColor = Color.Transparent;
                btnC3.Enabled = false;
                lblC3.Text = "---";
                //6
                pictureBox6.BackColor = Color.Transparent;
                btnC4.Enabled = false;
                lblC4code.Text = "---";
                //7
                pictureBox7.BackColor = Color.Transparent;
                btnC5.Enabled = false;
                lblC5code.Text = "---";
                //8
                pictureBox8.BackColor = Color.Transparent;
                btnBottomColor.Enabled = false;
                lblC6code.Text = "---";
                MessageBox.Show("3 Active Color Set.",
               "DaClock", MessageBoxButtons.OK);
            }
            if (comboBox1.Text == comboBox1.SelectedIndex.ToString("4"))
            {
                pictureBox4.BackColor = Settings.Default.C2;
                //5
                pictureBox5.BackColor = Color.Transparent;
                btnC3.Enabled = false;
                lblC3.Text = "---";
                //6
                pictureBox6.BackColor = Color.Transparent;
                btnC4.Enabled = false;
                lblC4code.Text = "---";
                //7
                pictureBox7.BackColor = Color.Transparent;
                btnC5.Enabled = false;
                lblC5code.Text = "---";
                //8
                pictureBox8.BackColor = Color.Transparent;
                btnBottomColor.Enabled = false;
                lblC6code.Text = "---";
                MessageBox.Show("4 Active Color Set.",
               "DaClock", MessageBoxButtons.OK);
            }
            if (comboBox1.Text == comboBox1.SelectedIndex.ToString("5"))
            {
                pictureBox5.BackColor = Settings.Default.C3;
                //6
                pictureBox6.BackColor = Color.Transparent;
                btnC4.Enabled = false;
                lblC4code.Text = "---";
                //7
                pictureBox7.BackColor = Color.Transparent;
                btnC5.Enabled = false;
                lblC5code.Text = "---";
                //8
                pictureBox8.BackColor = Color.Transparent;
                btnBottomColor.Enabled = false;
                lblC6code.Text = "---";
                MessageBox.Show("5 Active Color Set.",
               "DaClock", MessageBoxButtons.OK);
            }
            if (comboBox1.Text == comboBox1.SelectedIndex.ToString("6"))
            {
                pictureBox6.BackColor = Settings.Default.C4;
                btnC4.Enabled = true;
                //7
                pictureBox7.BackColor = Color.Transparent;
                btnC5.Enabled = false;
                lblC5code.Text = "---";
                //8
                pictureBox8.BackColor = Color.Transparent;
                btnBottomColor.Enabled = false;
                lblC6code.Text = "---";

            }
            if (comboBox1.Text == comboBox1.SelectedIndex.ToString("7"))
            {

                pictureBox7.BackColor = Settings.Default.C5;
                btnC5.Enabled = true;
                //8
                pictureBox8.BackColor = Color.Transparent;
                btnBottomColor.Enabled = false;
                lblC6code.Text = "---";

            }
            if (comboBox1.Text == comboBox1.SelectedIndex.ToString("8"))
            {

                pictureBox1.BackColor = Settings.Default.Border;
                pictureBox2.BackColor = Settings.Default.Tip;
                pictureBox3.BackColor = Settings.Default.C1;
                pictureBox4.BackColor = Settings.Default.C2;
                pictureBox5.BackColor = Settings.Default.C3;
                pictureBox6.BackColor = Settings.Default.C4;
                pictureBox7.BackColor = Settings.Default.C5;
                pictureBox8.BackColor = Settings.Default.C6;

                btnBorderColor.Enabled = true;
                btnGlossColor.Enabled = true;
                btnC1.Enabled = true;
                btnC2.Enabled = true;
                btnC3.Enabled = true;
                btnC4.Enabled = true;
                btnC5.Enabled = true;
                btnBottomColor.Enabled = true;


            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            ActiveColors();
            Settings.Default.activeColors = comboBox1.Text;
            Settings.Default.Save();

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Settings.Default.Border = pictureBox1.BackColor;
            Settings.Default.Tip = pictureBox2.BackColor;
            Settings.Default.C1 = pictureBox3.BackColor;
            Settings.Default.C2 = pictureBox4.BackColor;
            Settings.Default.C3 = pictureBox5.BackColor;
            Settings.Default.C4 = pictureBox6.BackColor;
            Settings.Default.C5 = pictureBox7.BackColor;
            Settings.Default.C6 = pictureBox8.BackColor;
            Settings.Default.borderSize = float.Parse(textBox1.Text,
                 System.Globalization.CultureInfo.InvariantCulture);
            Settings.Default.activeColors = comboBox1.Text;
            Settings.Default.Save();

            Application.Restart();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Save Scheme
            if (textBox2.Text == "Scheme Name Here")
            {
                MessageBox.Show("You must assign a name to your color scheme in order to save it.", "Alert");
            }

            else
            {
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("You must assign a name to your color scheme in order to save it.", "Alert");

                }
                else
                {
                    ListViewItem lvwItem = listView1.Items.Add(textBox2.Text);
                    lvwItem.SubItems.Add(lblBorderCode.Text);
                    lvwItem.SubItems.Add(lblGlossColorCode.Text);
                    lvwItem.SubItems.Add(lblC1code.Text);
                    lvwItem.SubItems.Add(lblC2Code.Text);
                    lvwItem.SubItems.Add(lblC3.Text);
                    lvwItem.SubItems.Add(lblC4code.Text);
                    lvwItem.SubItems.Add(lblC5code.Text);
                    lvwItem.SubItems.Add(lblC6code.Text);
                    lvwItem.SubItems.Add(comboBox1.Text);
                    WriteTextFile();
                    MessageBox.Show("Your custom color scheme has been saved, and is available in the schemes list.", "Grats");
                }
                // WriteTextFile()
            }
        }

        private void WriteTextFile()
        {

            StreamWriter sw = new StreamWriter(@"C:\Philnett\DaClock\cSchemes.txt");
            for (int i = 0; i <= this.listView1.Items.Count - 1; i++)
            {
                string s = this.listView1.Items[i].Text
                    + "," + this.listView1.Items[i].SubItems[1].Text + ","
                    + this.listView1.Items[i].SubItems[2].Text
                    + "," + this.listView1.Items[i].SubItems[3].Text
                    + "," + this.listView1.Items[i].SubItems[4].Text
                    + "," + this.listView1.Items[i].SubItems[5].Text
                    + "," + this.listView1.Items[i].SubItems[6].Text
                    + "," + this.listView1.Items[i].SubItems[7].Text
                    + "," + this.listView1.Items[i].SubItems[8].Text
                    + "," + this.listView1.Items[i].SubItems[9].Text;
                sw.WriteLine(s);
            }
            sw.Close();

        }

        private void OpentextFile(string fname)
        {
            string strFile = null;
            listView1.Items.Clear();

            try
            {
                string[] strParts = null;
                fs = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read);
                bs = new BufferedStream(fs);
                sr = new StreamReader(bs);
                strFile = sr.ReadLine();

                while (!(strFile == null))
                {
                    strParts = Regex.Split(strFile, ",");
                    ListViewItem lv = new ListViewItem();
                    lv.Text = strParts[0];
                    lv.SubItems.Add(strParts[1]);
                    lv.SubItems.Add(strParts[2]);
                    lv.SubItems.Add(strParts[3]);
                    lv.SubItems.Add(strParts[4]);
                    lv.SubItems.Add(strParts[5]);
                    lv.SubItems.Add(strParts[6]);
                    lv.SubItems.Add(strParts[7]);
                    lv.SubItems.Add(strParts[8]);
                    lv.SubItems.Add(strParts[9]);



                    strFile = sr.ReadLine();
                    listView1.Items.Add(lv);
                }

                fs.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            lblCname.Text = " ";
            lblBorderCode.Text = " ";
            lblGlossColorCode.Text = " ";
            lblC1code.Text = " ";
            lblC2Code.Text = " ";
            lblC3.Text = " ";
            lblC4code.Text = " ";
            lblC5code.Text = " ";
            lblC6code.Text = " ";

            for (int i = 0; i <= this.listView1.Items.Count - 1; i++)
            {
                lblCname.Text = "Current Scheme " + listView1.SelectedItems[0].Text;
                lblBorderCode.Text = listView1.SelectedItems[0].SubItems[1].Text;
                lblGlossColorCode.Text = listView1.SelectedItems[0].SubItems[2].Text;
                lblC1code.Text = listView1.SelectedItems[0].SubItems[3].Text;
                lblC2Code.Text = listView1.SelectedItems[0].SubItems[4].Text;
                lblC3.Text = listView1.SelectedItems[0].SubItems[5].Text;
                lblC4code.Text = listView1.SelectedItems[0].SubItems[6].Text;
                lblC5code.Text = listView1.SelectedItems[0].SubItems[7].Text;
                lblC6code.Text = listView1.SelectedItems[0].SubItems[8].Text;
                comboBox1.Text = listView1.SelectedItems[0].SubItems[9].Text;

                pictureBox1.BackColor = ColorTranslator.FromHtml(lblBorderCode.Text);
                pictureBox2.BackColor = ColorTranslator.FromHtml(lblGlossColorCode.Text);
                pictureBox3.BackColor = ColorTranslator.FromHtml(lblC1code.Text);
                pictureBox4.BackColor = ColorTranslator.FromHtml(lblC2Code.Text);
                pictureBox5.BackColor = ColorTranslator.FromHtml(lblC3.Text);
                pictureBox6.BackColor = ColorTranslator.FromHtml(lblC4code.Text);
                pictureBox7.BackColor = ColorTranslator.FromHtml(lblC5code.Text);
                pictureBox8.BackColor = ColorTranslator.FromHtml(lblC6code.Text);



                panel2.Paint += new System.Windows.Forms.PaintEventHandler(RePaint);
                panel2.Invalidate(rect, true);
                //    panel2.Invalidate();
                //  MessageBox.Show(str);
            }

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.borderSize = float.Parse(textBox1.Text,
                 System.Globalization.CultureInfo.InvariantCulture);
            panel2.Invalidate(rect, true);
            panel2.Paint += new PaintEventHandler(RePaint);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
           
        }

        private void removeColorSchemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Delete the items
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                listView1.Items.Remove(item);

            }
            WriteTextFile();
        }

        private void setColorSchemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblCname.Text = " ";
            lblBorderCode.Text = " ";
            lblGlossColorCode.Text = " ";
            lblC1code.Text = " ";
            lblC2Code.Text = " ";
            lblC3.Text = " ";
            lblC4code.Text = " ";
            lblC5code.Text = " ";
            lblC6code.Text = " ";

            for (int i = 0; i <= this.listView1.Items.Count - 1; i++)
            {
                lblCname.Text = "Current Scheme " + listView1.SelectedItems[0].Text;
                lblBorderCode.Text = listView1.SelectedItems[0].SubItems[1].Text;
                lblGlossColorCode.Text = listView1.SelectedItems[0].SubItems[2].Text;
                lblC1code.Text = listView1.SelectedItems[0].SubItems[3].Text;
                lblC2Code.Text = listView1.SelectedItems[0].SubItems[4].Text;
                lblC3.Text = listView1.SelectedItems[0].SubItems[5].Text;
                lblC4code.Text = listView1.SelectedItems[0].SubItems[6].Text;
                lblC5code.Text = listView1.SelectedItems[0].SubItems[7].Text;
                lblC6code.Text = listView1.SelectedItems[0].SubItems[8].Text;
                comboBox1.Text = listView1.SelectedItems[0].SubItems[9].Text;

                pictureBox1.BackColor = ColorTranslator.FromHtml(lblBorderCode.Text);
                pictureBox2.BackColor = ColorTranslator.FromHtml(lblGlossColorCode.Text);
                pictureBox3.BackColor = ColorTranslator.FromHtml(lblC1code.Text);
                pictureBox4.BackColor = ColorTranslator.FromHtml(lblC2Code.Text);
                pictureBox5.BackColor = ColorTranslator.FromHtml(lblC3.Text);
                pictureBox6.BackColor = ColorTranslator.FromHtml(lblC4code.Text);
                pictureBox7.BackColor = ColorTranslator.FromHtml(lblC5code.Text);
                pictureBox8.BackColor = ColorTranslator.FromHtml(lblC6code.Text);



                panel2.Paint += new System.Windows.Forms.PaintEventHandler(RePaint);
                panel2.Invalidate(rect, true);
                //    panel2.Invalidate();
                //  MessageBox.Show(str);
            }
         }
    
    }
}
