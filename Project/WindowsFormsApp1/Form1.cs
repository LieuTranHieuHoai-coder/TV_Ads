using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public List<string> videos = new List<string>();
        public List<string> images = new List<string>();
        public int count = 0;
        public int countImg = 0;
        public Form1()
        {
            InitializeComponent();
            
            hide_taskBar();
            
            set_Width_Height();
            createListVideos();
            axWindowsMediaPlayer1.Dock = DockStyle.None;
            axWindowsMediaPlayer1.enableContextMenu = false;
            axWindowsMediaPlayer1.enableContextMenu = false;
            
            //axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer2.Dock = DockStyle.None;
            axWindowsMediaPlayer2.enableContextMenu = false;
            //axWindowsMediaPlayer2.stretchToFit = true;
            axWindowsMediaPlayer3.Dock = DockStyle.None;
            axWindowsMediaPlayer3.enableContextMenu = false;
            //axWindowsMediaPlayer3.stretchToFit = true;


        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    DialogResult dialogResult = MessageBox.Show("Do you want to close program ?", "Exit", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Application.Exit();
                        Close();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void hide_taskBar()
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }
        public void set_Width_Height()
        {
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            int w = resolution.Width;
            int h = resolution.Height;
            this.Size = new Size(w, h);
            axWindowsMediaPlayer1.Width = w ;
            axWindowsMediaPlayer1.Location = new Point(0);
            axWindowsMediaPlayer1.Height = (h / 3) - (h * 5 / 100);
            axWindowsMediaPlayer2.Width = w ;
            axWindowsMediaPlayer2.Location = new Point(0, (h / 3));
            axWindowsMediaPlayer2.Height = (h / 3) - (h * 5 / 100);
            axWindowsMediaPlayer3.Width = w;
            axWindowsMediaPlayer3.Location = new Point(0, ((h / 3))*2);
            axWindowsMediaPlayer3.Height = (h / 3) - (h * 5 / 100);

            pictureBox1.Width = w;
            pictureBox1.Height = h;
            pictureBox1.Location = new Point(0,0-(h*3/100));


            //pictureBox1.BringToFront();
            
            button1.BackColor = Color.Red; 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, Color.Transparent); ;
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, Color.Transparent); ;
            button1.Width = 50;
            button1.Height = 25;
            button1.Location = new Point(w - 40, 0);
            button1.Text = "X";
            button1.BringToFront();
        }
        private void createListVideos()
        {
            List<string> f = new List<string>();
            List<string> f1 = new List<string>();
            //List<string> f1Videos = new List<string>();
            //string path = @"D:\Files\TVconfig.txt";
            //StreamReader streamReader = new StreamReader(path);
            //string data = streamReader.ReadToEnd();

            string basePath = Environment.CurrentDirectory;
            string relativePath = "TVconfig.txt";
            
            string path = Path.GetFullPath(relativePath);

            StreamReader streamReader = new StreamReader(path);
            string splitResult = streamReader.ReadToEnd();
            string[] arr = splitResult.Split(',', '"', '\n', '\r');
            for (int i = 0; i < arr.Length; i++)
            {
                if (!arr[i].Equals(""))
                {
                    f.Add(arr[i].ToString());

                    string[] arrf1 = arr[i].Split('-', '"', '\n', '\r');
                    for (int j = 0; j < arrf1.Length; j++)
                    {
                        if (!arrf1[j].Equals(""))
                        {
                            f1.Add(arrf1[j].ToString());
                            //videos.Add(f1[j]);
                            //images.Add(image[1]);
                        }

                    }
                }

            }

            for (int i = 0; i < f1.Count; i++)
            {
                if (i % 2 == 0)
                {
                    videos.Add(f1[i]);
                }
                else
                {
                    images.Add(f1[i]);
                }
            }

            Console.WriteLine(images[2]);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.ImageLocation = images[2];
            //pictureBox1.Image = Image.FromFile(images[0]);

            //pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            //videos.Add("D:/Files/video/1.mp4");
            //videos.Add("D:/Files/video/2.mp4");
            //videos.Add("D:/Files/video/3.mp4");

        }
        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            axWindowsMediaPlayer3.Visible = false;
            axWindowsMediaPlayer2.Visible = false;
            //axWindowsMediaPlayer2.SendToBack();

            axWindowsMediaPlayer1.URL = videos[videos.Count - (videos.Count - count)];
            axWindowsMediaPlayer1.Ctlcontrols.play();

            //load image
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.ImageLocation = images[0];
        }
        private void axWindowsMediaPlayer2_Enter(object sender, EventArgs e)
        {

        }
        private void axWindowsMediaPlayer3_Enter(object sender, EventArgs e)
        {

        }
        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // Check Full Full Screen => Set False
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.fullScreen = false;
                if(axWindowsMediaPlayer1.fullScreen == true)
                {
                    axWindowsMediaPlayer1.fullScreen = false;
                }
            }
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                count++;
                countImg++;
                if (count + 1 > videos.Count)
                {
                    axWindowsMediaPlayer1.Visible = false;
                    axWindowsMediaPlayer2.Visible = true;
                    axWindowsMediaPlayer3.Visible = false;
                    count = 0;
                    axWindowsMediaPlayer2.URL = videos[videos.Count - (videos.Count - count)];
                    axWindowsMediaPlayer2.Ctlcontrols.play();

                }
                else
                {
                    axWindowsMediaPlayer1.Visible = false;
                    axWindowsMediaPlayer2.Visible = true;
                    axWindowsMediaPlayer3.Visible = false;
                    axWindowsMediaPlayer2.URL = videos[videos.Count - (videos.Count - count)];

                    axWindowsMediaPlayer2.Ctlcontrols.play();

                }
                //load image
                if (countImg + 1 > images.Count)
                {

                    countImg = 0;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.ImageLocation = images[images.Count - (images.Count - countImg)];
                }
                else
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.ImageLocation = images[images.Count - (images.Count - countImg)];
                }
            }
        }
        private void axWindowsMediaPlayer2_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // Check Full Full Screen => Set False
            if (axWindowsMediaPlayer2.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer2.fullScreen = false;
                if (axWindowsMediaPlayer2.fullScreen == true)
                {
                    axWindowsMediaPlayer2.fullScreen = false;
                }
            }
            if (axWindowsMediaPlayer2.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                count++;
                countImg++;
                if (count + 1 > videos.Count)
                {
                    axWindowsMediaPlayer1.Visible = false;
                    axWindowsMediaPlayer2.Visible = false;
                    axWindowsMediaPlayer3.Visible = true;
                    count = 0;
                    axWindowsMediaPlayer3.URL = videos[videos.Count - (videos.Count - count)];
                    axWindowsMediaPlayer3.Ctlcontrols.play();
                }
                else
                {
                    axWindowsMediaPlayer1.Visible = false;
                    axWindowsMediaPlayer2.Visible = false;
                    axWindowsMediaPlayer3.Visible = true;
                    axWindowsMediaPlayer3.URL = videos[videos.Count - (videos.Count - count)];

                    axWindowsMediaPlayer3.Ctlcontrols.play();

                }
                //load image
                if (countImg + 1 > images.Count)
                {

                    countImg = 0;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.ImageLocation = images[images.Count - (images.Count - countImg)];
                }
                else
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.ImageLocation = images[images.Count - (images.Count - countImg)];
                }

            }
        }
        private void axWindowsMediaPlayer3_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // Check Full Full Screen => Set False
            if (axWindowsMediaPlayer3.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer3.fullScreen = false;
                if (axWindowsMediaPlayer3.fullScreen == true)
                {
                    axWindowsMediaPlayer3.fullScreen = false;
                }
            }
            if (axWindowsMediaPlayer3.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                count++;
                countImg++;
                if (count + 1 > videos.Count)
                {
                    axWindowsMediaPlayer1.Visible = true;
                    axWindowsMediaPlayer2.Visible = false;
                    axWindowsMediaPlayer3.Visible = false;
                    count = 0;
                    axWindowsMediaPlayer1.URL = videos[videos.Count - (videos.Count - count)];
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
                else
                {
                    axWindowsMediaPlayer1.Visible = true;
                    axWindowsMediaPlayer2.Visible = false;
                    axWindowsMediaPlayer3.Visible = false;
                    axWindowsMediaPlayer1.URL = videos[videos.Count - (videos.Count - count)];

                    axWindowsMediaPlayer1.Ctlcontrols.play();

                }
                //load image
                if (countImg + 1 > images.Count)
                {

                    countImg = 0;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.ImageLocation = images[images.Count - (images.Count - countImg)];
                }
                else
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.ImageLocation = images[images.Count - (images.Count - countImg)];
                }

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to close program ?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
                Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void axWindowsMediaPlayer1_DoubleClickEvent(object sender, AxWMPLib._WMPOCXEvents_DoubleClickEvent e)
        {
            
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
