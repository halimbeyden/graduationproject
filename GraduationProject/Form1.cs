using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using AForge;
using GraduationProject.Filters;
using System.IO;
using System.Threading;

namespace GraduationProject
{
    public partial class Form1 : Form
    {
        public List<string> photos { get; set; }
        public string planetName { get; set; }
        Thread thread;
        private static int currentProgress = 0;
        public Form1()
        {
            InitializeComponent();
            photos = new List<string>();
        }
        private void openBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder which includes planet photos";
                dialog.ShowNewFolderButton = false;
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string folder = dialog.SelectedPath;

                    foreach (string fileName in Directory.GetFiles(folder, "*.png", SearchOption.TopDirectoryOnly))
                    {
                        photos.Add(Path.GetFullPath(fileName));
                    }
                    planetName = folder.Split('\\').Last();
                    lblPhotoCount.Text = "There are " + photos.Count + " photos for " + planetName;
                    btnTrain.Enabled = true;
                    thread = new Thread(new ThreadStart(trainPlanet));
                    btnTrain.Tag = 1;
                    btnTrain.Text = "Train";
                   
                }
            }
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            if((int)btnTrain.Tag == 1)
            {
                progressBar1.Maximum = photos.Count;
                lblProgress.Text = "0/" + progressBar1.Maximum;
                thread.Start();
                btnTrain.Text = "Stop";
                btnTrain.Tag = 0;
            }
            else {
                thread.Abort();
                btnTrain.Enabled = false;
                progressBar1.Value = 0;
                currentProgress = 0;
                lblPhotoCount.Text = "";
                lblProgress.Text = "0/0";
            }
           
        }
        private void trainPlanet()
        {
            foreach (string cplanet in photos)
            {
                try
                {
                    Bitmap cpyImage = new Bitmap(Image.FromFile(cplanet));
                    Classification cls = new Classification(cpyImage);
                    Rectangle bb = cls.BoundingRect;
                    Features ac = cls.Features;
                    ac.Save(planetName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error for "+cplanet+" : " + ex.Message);
                }
                finally
                {
                    this.Invoke(new Action(() => this.progressBar1.Value = ++currentProgress));
                    this.Invoke(new Action(() => lblProgress.Text = currentProgress + "/" + progressBar1.Maximum));
                }
            }
        }

        //private void processBtn_Click(object sender, EventArgs e)
        //{
        //    //pb2.Image = Filter.SharedInstance.DetectRed(new Bitmap(pb1.Image));
        //    //pb2.Image = Filter.SharedInstance.DetectJupiter(new Bitmap(pb1.Image));
        //    //pb2.Refresh();
        //    Classification cls = new Classification(new Bitmap(pb1.Image));

        //    Bitmap cpyImage = new Bitmap(pb1.Image);
        //    Graphics g = Graphics.FromImage(cpyImage);
        //    Pen redPen = new Pen(Color.Red, 2);
        //    Rectangle bb = cls.BoundingRect;
        //    AvarageColor ac = cls.AvarageColor;
        //    ac.Save("mars");
        //    g.DrawRectangle(redPen, bb);
        //    redPen.Dispose();
        //    g.Dispose();
        //    pb2.Image = cpyImage;
        //    pb2.Refresh();
        //}
    }
}
