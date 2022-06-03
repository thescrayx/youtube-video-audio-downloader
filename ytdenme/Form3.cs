using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using VideoLibrary;
using MediaToolkit;

namespace ytdenme
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            SavePathTextBox.Text = Application.StartupPath;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;

            string URLS = LinkTextBox.Text;
            string[] ListURLS = URLS.Split('\n');

            label3.Visible = true;

            int i = 0;

            while (i < ListURLS.Length)
            {
                progressBar1.Value = 0;

                string URL = ListURLS[i];

                var youTube = YouTube.Default;
                var video = youTube.GetVideo(URL);

                System.IO.File.WriteAllBytes(SavePathTextBox.Text + "\\" + video.FullName, video.GetBytes());

                downloadLogTextBox.Text = video.FullName + " file downloaded";

                if(i == ListURLS.Length - 1)
                {
                    downloadLogTextBox.Text = "All videos have been downloaded.";
                }

                progressBar1.Value = 100;

                i++;
            }

            label3.Visible = false;

            progressBar1.Value = 100;

            label9.Visible = true;
            timer1.Start();
        }

        private string ByteConverter(long b)
        {
            string final;
            //to kb
            float c = (float)b;
            c /= 1024;
            final = c.ToString("0.00") + " KB";

            if (c >= (float)1)
            {
                //to mb
                c /= 1024;
                final = c.ToString("0.00") + " MB";
            }

            else if (c >= (float)1)
            {
                //to gb
                c /= 1024;
                final = c.ToString("0.00") + " MB";
            }
            return final;
        }
		
		private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label9.Visible = false;
            timer1.Stop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", SavePathTextBox.Text);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select Video Save Path." })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    SavePathTextBox.Text = fbd.SelectedPath;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", SavePathTextBox.Text);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.ShowDialog();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            this.Hide();
            f4.ShowDialog();
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }
    }
}
