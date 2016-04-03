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

namespace GraduationProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pb1.SizeMode = PictureBoxSizeMode.StretchImage;
            pb2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pb1.ImageLocation = ofd.FileName;
                pb1.Refresh();
            }
        }

        private void processBtn_Click(object sender, EventArgs e)
        {
            //pb2.Image = Filter.SharedInstance.DetectRed(new Bitmap(pb1.Image));
            pb2.Image = Filter.SharedInstance.DetectCircle(new Bitmap(pb1.Image));
            pb2.Refresh();
        }
    }
}
