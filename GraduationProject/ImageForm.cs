using GraduationProject.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraduationProject
{
    public partial class ImageForm : Form
    {
        public ImageForm()
        {
            InitializeComponent();
        }

        private void btnImageOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    Pen RedPen = new Pen(Color.Red, 2);

                    Bitmap cpyImage = new Bitmap(Image.FromFile(dialog.FileName));
                    Classification cls = new Classification(cpyImage);
                    Rectangle bb = cls.BoundingRect;
                    using (var graphics = Graphics.FromImage(cpyImage))
                    {
                        graphics.DrawRectangle(RedPen, bb);
                    }
                    imgPlanet.Image = cpyImage;
                }
            }

        }
    }
}
