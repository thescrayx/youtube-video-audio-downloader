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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            timer2.Start();
            SavePathTextBox.Text = Application.StartupPath;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public List<Color> colors = new List<Color> {
        Color.DimGray,
        Color.DarkGray
        };

        private int current;
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

            string URL = LinkTextBox.Text;
            string videoID;

            string[] slashes = URL.Split('/');
            string lastSlash = slashes[slashes.Length - 1];

            if (lastSlash.Contains("watch"))
            {
                string[] watchLinkList = lastSlash.Split('=');
                videoID = watchLinkList[watchLinkList.Length - 1];
            }
            else
            {
                videoID = lastSlash;
            }

            var youTube = YouTube.Default;
            var video = youTube.GetVideo(URL);
            pictureBox3.ImageLocation = "https://img.youtube.com/vi/" + videoID + "/0.jpg";

            int ms = Convert.ToInt32(video.ContentLength);
            TimeSpan ts = TimeSpan.FromMilliseconds(ms);

            label5.Text = video.FullName;
            label6.Text = ts.ToString(@"hh\:mm\:ss");
            label8.Text = "...";

            System.IO.File.WriteAllBytes(SavePathTextBox.Text + "\\" + video.FullName, video.GetBytes());

            var inputFile = new MediaToolkit.Model.MediaFile { Filename = SavePathTextBox.Text + "\\" + video.FullName };
            var outPutFile = new MediaToolkit.Model.MediaFile { Filename = $"{SavePathTextBox.Text + "\\" + video.FullName}.mp3" };

            using (var enging = new Engine())
            {
                enging.GetMetadata(inputFile);
                enging.Convert(inputFile, outPutFile);
            }

            System.IO.File.Delete(SavePathTextBox.Text + "\\" + video.FullName);

            progressBar1.Value = 100;
            long length = new System.IO.FileInfo(SavePathTextBox.Text + "\\" + video.FullName + ".mp3").Length;
            label8.Text = BytesToString(length).ToString();

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

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            this.Hide();
            f4.ShowDialog();
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            githubButton.ForeColor = colors[current++]; //change to rainbows other colors
            current %= colors.Count;
        }

        private void githubButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/thescrayx");
        }
    }
}
