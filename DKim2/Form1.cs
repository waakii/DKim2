using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.FFMPEG;
using System.Drawing.Imaging;

using Gma.UserActivityMonitor;
using Ini;

namespace DCam2
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            int iOffsetX = Screen.PrimaryScreen.Bounds.Width;
            int iOffsetY = Screen.PrimaryScreen.Bounds.Height;

            btnExit.Location = new System.Drawing.Point(iOffsetX - 100, iOffsetY - 100);
            btnRecord.Location = new System.Drawing.Point(iOffsetX - 100, iOffsetY - 170);
            btnResolution.Location = new System.Drawing.Point(iOffsetX - 100, iOffsetY - 240);
            btnFocus.Location = new System.Drawing.Point(iOffsetX - 100, iOffsetY - 310);
            btnSnapshot.Location = new System.Drawing.Point(iOffsetX - 100, iOffsetY - 380);

            pictureBoxVideo.SetBounds(0, 0, iOffsetX, iOffsetY);

            btnExit.Visible = false;
            btnFocus.Visible = false;
            btnRecord.Visible = false;
            btnSnapshot.Visible = false;
            btnResolution.Visible = false;

        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;

            //List all available video sources. (That can be webcams as well as tv cards, etc)
            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            Logo = new LogoWindow();
            Logo.Show();

            LogoTimer.Interval = 2000;
            LogoTimer.Enabled = true;

            //Check if atleast one video source is available
            if (videosources != null)
            {
                bCameraDetected = false;
                string[] splitedMoniker;

                for (int i = 0; i < videosources.Count; i++)
                {
                    //System.Console.WriteLine(videosources[i].MonikerString);
                    splitedMoniker = videosources[i].MonikerString.Split('#', '&');
                    for (int j = 0; j < splitedMoniker.Length; j++)
                    {
                        if (splitedMoniker[j].CompareTo("vid_1bcf") == 0)
                        {
                            iCurrentCAMNum = i;
                            bCameraDetected = true;

                            System.Console.WriteLine("CAM" + iCurrentCAMNum + " is detected.");
                        }
                    }
                }
                if (bCameraDetected == false) return;

                //For example use first video device. You may check if this is your webcam.
                videoSource = new VideoCaptureDevice(videosources[iCurrentCAMNum].MonikerString);

                try
                {
                    //Check if the video device provides a list of supported resolutions
                    if (videoSource.VideoCapabilities.Length > 0)
                    {
                        //string highestSolution = "0;0";
                        //Search for the highest resolution
                        for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
                        {
                            //if (videoSource.VideoCapabilities[i].FrameSize.Width > Convert.ToInt32(highestSolution.Split(';')[0]))
                            //    highestSolution = videoSource.VideoCapabilities[i].FrameSize.Width.ToString() + ";" + i.ToString();
                            System.Console.WriteLine(i + ": " + videoSource.VideoCapabilities[i].FrameSize);
                        }

                        //Set the highest resolution as active
                        videoSource.VideoResolution = videoSource.VideoCapabilities[6];
                        btnResolution.Text = "720p";
                        //videoSource.VideoCapabilities[Convert.ToInt32(highestSolution.Split(';')[1])].FrameSize;
                    }
                }
                catch { }

                //Create NewFrame event handler
                //(This one triggers every time a new frame/image is captured
                videoSource.NewFrame += new AForge.Video.NewFrameEventHandler(videoSource_NewFrame);

                startTime = DateTime.MinValue;

                //Start recording
                videoSource.Start();

                GCTimer.Enabled = true;

                globalEventProvider1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.globalEventProvider1_MouseMove);
                globalEventProvider1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.globalEventProvider1_KeyPress);

                showButton();

                btnExit.BringToFront();
                btnRecord.BringToFront();
                btnResolution.BringToFront();
                btnFocus.BringToFront();
            }
        }

        private void LogoTimer_Tick(object sender, EventArgs e)
        {
            Logo.Close();
            LogoTimer.Enabled = false;

            if (bCameraDetected == false)
            {
                MessageBox.Show("Camera is not found..");
                Environment.Exit(0);
            }
            else while (videoSource.IsRunning == false) ;

            videoSource.SetCameraProperty(CameraControlProperty.Focus, 0, CameraControlFlags.Auto);
            btnFocus.Text = "AF";

            string LastDirectory;
            string AppDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            IniFile ini = new IniFile(AppDirectory + "\\properties.ini");
            LastDirectory = ini.IniReadValue("Properties", "LastDirectory");
            
            fbd = new FolderBrowserDialog();
            fbd.Description = "Please select directory for video recording.";
            if (LastDirectory != null) fbd.SelectedPath = LastDirectory;
            
            DialogResult result = fbd.ShowDialog();

            ini.IniWriteValue("Properties", "LastDirectory", fbd.SelectedPath);

            if (!System.IO.Directory.Exists(fbd.SelectedPath + "\\DCAM2_" + DateTime.Now.ToString("yyyyMMdd"))) 
                System.IO.Directory.CreateDirectory(fbd.SelectedPath + "\\DCAM2_" + DateTime.Now.ToString("yyyyMMdd"));
            if (!System.IO.Directory.Exists(fbd.SelectedPath + "\\DCAM2_" + DateTime.Now.ToString("yyyyMMdd") + "\\Video"))
                System.IO.Directory.CreateDirectory(fbd.SelectedPath + "\\DCAM2_" + DateTime.Now.ToString("yyyyMMdd") + "\\Video");
            if (!System.IO.Directory.Exists(fbd.SelectedPath + "\\DCAM2_" + DateTime.Now.ToString("yyyyMMdd") + "\\Image"))
                System.IO.Directory.CreateDirectory(fbd.SelectedPath + "\\DCAM2_" + DateTime.Now.ToString("yyyyMMdd") + "\\Image");
        }

        private void btnFocus_Click(object sender, EventArgs e)
        {
            CameraControlFlags fControlFlag;
            int iFocusValue;

            videoSource.GetCameraProperty(CameraControlProperty.Focus, out iFocusValue, out fControlFlag);
            if (fControlFlag == CameraControlFlags.Auto)
            {
                btnFocus.BackColor = Color.Red;
                btnFocus.Text = "MF";
                videoSource.SetCameraProperty(CameraControlProperty.Focus, iFocusValue, CameraControlFlags.Manual);
            }
            else
            {
                btnFocus.BackColor = Color.Black;
                btnFocus.Text = "AF";
                videoSource.SetCameraProperty(CameraControlProperty.Focus, iFocusValue, CameraControlFlags.Auto);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (bRecording)
            {
                bRecording = false;
                startTime = DateTime.MinValue;
                btnRecord.BackColor = Color.Black;

                writer.Close();
                while (writer.IsOpen) ;
            }

            Environment.Exit(0);
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (bRecording)
            {
                bRecording = false;
                startTime = DateTime.MinValue;
                btnRecord.BackColor = Color.Black;

                writer.Close();
                while (writer.IsOpen) ;
            }
            else
            {
                writer = new VideoFileWriter();
                writer.Open(fbd.SelectedPath + "\\DCAM2_" + DateTime.Now.ToString("yyyyMMdd") + "\\Video\\" + DateTime.Now.ToString("yyMMdd-HHmmss") + ".avi", videoSource.VideoResolution.FrameSize.Width, videoSource.VideoResolution.FrameSize.Height, videoSource.VideoResolution.MaximumFrameRate, VideoCodec.MPEG4, 4000000);

                btnRecord.BackColor = Color.Red;
                bRecording = true;
            }
        }

        private void btnResolution_Click(object sender, EventArgs e)
        {
            if (bRecording == false)
            {
                ButtonTimer_Tick(sender, e);
                globalEventProvider1.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.globalEventProvider1_KeyPress);
                globalEventProvider1.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.globalEventProvider1_MouseMove);
            
                videoSource.Stop();
                videoSource.WaitForStop();

                switch(videoSource.VideoResolution.FrameSize.Width)
                {
                    case 640:
                        //change to 720P
                        videoSource.VideoResolution = videoSource.VideoCapabilities[6];
                        btnResolution.Text = "720p";
                        break;

                    case 1280:
                        //change to 1080p
                        videoSource.VideoResolution = videoSource.VideoCapabilities[7];
                        btnResolution.Text = "1080p";
                        break;

                    case 1920:
                        //change to 640p
                        videoSource.VideoResolution = videoSource.VideoCapabilities[2];
                        btnResolution.Text = "640p";
                        break;
                }
                
                videoSource.Start();

                while (videoSource.IsRunning == false) ;
                System.Threading.Thread.Sleep(1000);
            
                globalEventProvider1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.globalEventProvider1_KeyPress);
                globalEventProvider1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.globalEventProvider1_MouseMove);

                showButton();
            }
        }

        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            //Cast the frame as Bitmap object and don't forget to use ".Clone()" otherwise
            //you'll probably get access violation exceptions
            bmpImage = (Bitmap)eventArgs.Frame.Clone();
            if (bRecording == true && writer.IsOpen == true)
            {
                if (startTime == DateTime.MinValue) startTime = DateTime.Now;
                try
                {
                    writer.WriteVideoFrame(bmpImage, DateTime.Now - startTime);
                }
                catch { }
            }
            pictureBoxVideo.BackgroundImage = bmpImage;
        }

        private void globalEventProvider1_MouseMove(object sender, MouseEventArgs e)
        {
            showButton();
        }

        private void globalEventProvider1_KeyPress(object sender, KeyPressEventArgs e)
        {
            showButton();
            switch (e.KeyChar)
            {
                case 's':
                case 'S':
                    btnRecord_Click(sender, e);
                    break;

                case 'f':
                case 'F':
                    btnFocus_Click(sender, e);
                    break;

                case 't':
                case 'T':
                    btnSnapshot_Click(sender, e);
                    break;

                case 'r':
                case 'R':
                    btnResolution_Click(sender, e);
                    break;

                case 'x':
                case 'X':
                    btnExit_Click(sender, e);
                    break;

                default:
                    break;
            }
        }

        private void GCTimer_Tick(object sender, EventArgs e)
        {
            GC.Collect();
            if (videoSource.IsRunning == false)
            {
                GCTimer.Enabled = false;
                MessageBox.Show("The camera is disconnected.");
                btnExit_Click(sender, e);
            }
        }

        private void btnSnapshot_Click(object sender, EventArgs e)
        {
            if (SnapTimer.Enabled == false)
            {
                btnSnapshot.BackColor = Color.Red;
                pictureBoxVideo.BackgroundImage.Save(fbd.SelectedPath + "\\DCAM2_" + DateTime.Now.ToString("yyyyMMdd") + "\\Image\\" + DateTime.Now.ToString("yyMMdd-HHmmss-fff") + ".bmp");
                SnapTimer.Enabled = true;
            }
        }

        private void showButton()
        {
            btnExit.Visible = true;
            btnFocus.Visible = true;
            btnRecord.Visible = true;
            btnSnapshot.Visible = true;
            btnResolution.Visible = true;

            ButtonTimer.Enabled = false;
            ButtonTimer.Interval = 3000;
            ButtonTimer.Enabled = true;
        }

        private void ButtonTimer_Tick(object sender, EventArgs e)
        {
            ButtonTimer.Enabled = false;
            
            btnExit.Visible = false;
            btnFocus.Visible = false;
            btnRecord.Visible = false;
            btnSnapshot.Visible = false;
            btnResolution.Visible = false;
        }

        private void SnapTimer_Tick(object sender, EventArgs e)
        {
            btnSnapshot.BackColor = Color.Black;
            SnapTimer.Enabled = false;
        }


        private bool bRecording = false;
        private int iCurrentCAMNum;
        private VideoCaptureDevice videoSource;
        private bool bCameraDetected;

        private DateTime startTime;
        private VideoFileWriter writer;
        static Bitmap bmpImage = null;

        private Form Logo;
        private TextBox VersionBox;

        private FolderBrowserDialog fbd;

    }
}
