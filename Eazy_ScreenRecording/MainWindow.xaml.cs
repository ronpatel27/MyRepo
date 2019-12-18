using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;
using Microsoft.Expression.Encoder.ScreenCapture;
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder;

namespace Eazy_ScreenRecording
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string workDir = @"C:/Easy_ScreenRecordings/";
        ScreenCaptureJob jobsc = new ScreenCaptureJob();
        public MainWindow()
        {
            InitializeComponent();
            outDir.Text = ".. All Recordings: " + workDir;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!jobsc.Status.ToString().Equals("Running"))
            {
                System.IO.Directory.CreateDirectory(workDir);
                jobsc = new ScreenCaptureJob();
                jobsc.ScreenCaptureVideoProfile.FrameRate = 10;
                int width = ((int)System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width) / 4;
                int height = ((int)System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height) / 4;
                width = width * 4;
                height = height * 4;
                System.Drawing.Rectangle capRect = new System.Drawing.Rectangle(4, 0, width, height);  //iMacros billpc slides only
                jobsc.CaptureRectangle = capRect;

                var index = 0;
                var fileCreated = false;
                do
                {
                    if (!File.Exists(workDir + "CurrentScreenCapture" + index + ".mp4"))
                    {
                        jobsc.OutputScreenCaptureFileName = workDir + "CurrentScreenCapture" + index + ".mp4";
                        fileCreated = true;
                    }
                    index++;
                } while (!fileCreated);
                jobsc.Start();
            }
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                jobsc.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex: " + ex.ToString());
            }
        }
    }
}
