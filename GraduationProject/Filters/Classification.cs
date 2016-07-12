using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Filters
{
    public class Classification
    {
        public Bitmap Image { get; set; }

        public Color[,] Matrix { get; set; }
        public int WindowsSize { get; set; }

        public Classification(Bitmap bm)
        {
            WindowsSize = 30;//px
            Image = bm;
            Matrix = new Color[Image.Width,Image.Height];
            for (int i = 0; i < Image.Width; i++)
            {
                for (int j = 0; j < Image.Height; j++)
                {
                    Matrix[i, j] = Image.GetPixel(i, j);
                }
            }
        }
        public float[,] getRGBAvarage()
        {
            int height = Image.Height / WindowsSize;
            if (Image.Height % WindowsSize != 0)
                height++;
            int width = Image.Width / WindowsSize;
            if (Image.Width % WindowsSize != 0)
                width++;

            float[,] result = new float[width, height];
            float[,] resultR = new float[width, height];
            float[,] resultG = new float[width, height];
            float[,] resultB = new float[width, height];
            float[,] resultBrightness = new float[width, height];

            int r, g, b;
            float hue,saturation,brightness;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    r = 0;g = 0;b = 0;
                    hue = 0; saturation=0; brightness = 0;
                    int ws = 0;
                    for (int k = WindowsSize*i; k < WindowsSize*(i+1) && k < Image.Width; k++)
                    {
                        for (int l = WindowsSize*j; l < WindowsSize*(j+1) && l < Image.Height; l++, ws++)
                        {
                            Color cPixel = Image.GetPixel(k, l);
                            r += cPixel.R;
                            g += cPixel.G;
                            b += cPixel.B;
                            brightness += cPixel.GetBrightness();
                            hue += cPixel.GetHue();
                            saturation += cPixel.GetSaturation();
                        }

                    }
                    resultR[i, j] = (float)r / (ws);
                    resultG[i, j] = (float)g / (ws);
                    resultB[i, j] = (float)b / (ws);
                    result[i, j] = resultR[i, j] + resultG[i, j] + resultB[i, j];
                    resultBrightness[i, j] = brightness / (ws);
                }
            }
            
            return resultR; 
        }
        public Rectangle getBoundingBox()
        {
            List<Point> points = new List<Point>();

            float [,]matrix = getRGBAvarage();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if(matrix[i,j] > 255)
                        points.Add(new Point(i,j));
                }
            }
            return BoundingBox(points);
        }
        private Rectangle BoundingBox(IEnumerable<Point> points)
        {
            var x_query = from Point p in points select p.X;
            int xmin = x_query.Min();
            int xmax = x_query.Max();

            var y_query = from Point p in points select p.Y;
            int ymin = y_query.Min();
            int ymax = y_query.Max();

            return new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
        }
    }
}
