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
        private AvarageColor _avarageColor;
        private bool _avarageColorIsNotSet;

        public AvarageColor AvarageColor
        {
            get { return getAvarages(); }
        }

        public int WindowsSize { get; set; }
        /// <summary>
        /// R + G + B
        /// </summary>
        private Rectangle _boundingRect;
        private bool _boundingRectIsNotSet;

        public Rectangle BoundingRect
        {
            get { return getBoundingBox(); }
        }
        private int MinRGB { get; set; }

        public Classification(Bitmap bm)
        {
            WindowsSize = 20;//px
            MinRGB = 75;//R+G+B
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
        private float[,] getRGBAvarage()
        {
            int height = Image.Height / WindowsSize;
            if (Image.Height % WindowsSize != 0)
                height++;
            int width = Image.Width / WindowsSize;
            if (Image.Width % WindowsSize != 0)
                width++;

            float[,] result = new float[width, height];

            int r, g, b;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    r = 0;g = 0;b = 0;
                    int ws = 0;
                    for (int k = WindowsSize*i; k < WindowsSize*(i+1) && k < Image.Width; k++)
                    {
                        for (int l = WindowsSize*j; l < WindowsSize*(j+1) && l < Image.Height; l++, ws++)
                        {
                            Color cPixel = Image.GetPixel(k, l);
                            r += cPixel.R;
                            g += cPixel.G;
                            b += cPixel.B;
                        }
                    }
                    result[i, j] = (float)r / (ws) + (float)g / (ws) + (float)b / (ws);
                }
            }
            return result; 
        }
        private Rectangle getBoundingBox()
        {
            if (_boundingRectIsNotSet)
                return _boundingRect;
            _boundingRectIsNotSet = true;
            List<Point> points = new List<Point>();

            float [,]matrix = getRGBAvarage();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if(matrix[i,j] > MinRGB)
                        points.Add(new Point(Decode(i,Image.Width),Decode(j,Image.Height)));
                }
            }
            _boundingRect = BoundingBox(points);
            return _boundingRect;
        }
        private AvarageColor getAvarages()
        {
            if (_avarageColorIsNotSet)
                return _avarageColor;
            _avarageColorIsNotSet = true;
            if (_boundingRectIsNotSet)
                _boundingRect = getBoundingBox();
            float resultR = 0, resultG = 0, resultB = 0, resultH = 0, resultS = 0, resultL = 0;
            for (int i = BoundingRect.Left; i < BoundingRect.Right; i++)
            {
                for (int j = BoundingRect.Top; j < BoundingRect.Bottom; j++)
                {
                    resultR += Image.GetPixel(i,j).R;
                    resultG += Image.GetPixel(i,j).G;
                    resultB += Image.GetPixel(i,j).B;
                    resultH += Image.GetPixel(i,j).GetHue();
                    resultS += Image.GetPixel(i,j).GetSaturation();
                    resultL += Image.GetPixel(i,j).GetBrightness();
                }
            }
            _avarageColor = new AvarageColor();
            _avarageColor.R = resultR / (BoundingRect.Width * BoundingRect.Height);
            _avarageColor.G = resultG / (BoundingRect.Width * BoundingRect.Height);
            _avarageColor.B = resultB / (BoundingRect.Width * BoundingRect.Height);
            _avarageColor.H = resultH / (BoundingRect.Width * BoundingRect.Height);
            _avarageColor.S = resultS / (BoundingRect.Width * BoundingRect.Height);
            _avarageColor.L = resultL / (BoundingRect.Width * BoundingRect.Height);

            return _avarageColor;
        }
        private Rectangle BoundingBox(IEnumerable<Point> points)
        {
            var x_query = from Point p in points select p.X;
            int xmin = x_query.Min();
            int xmax = x_query.Max();

            var y_query = from Point p in points select p.Y;
            int ymin = y_query.Min();
            int ymax = y_query.Max();

            int sizeX = xmax - xmin + WindowsSize;
            if (xmin + sizeX > Image.Width)
                sizeX = Image.Width - xmin;

            int sizeY = ymax - ymin + WindowsSize;
            if (ymin + sizeY > Image.Height)
                sizeY = Image.Height - ymin;

            return new Rectangle(xmin, ymin, sizeX, sizeY);
        }
        private int Decode(int index,int max)
        {
            return (index * WindowsSize) > max ? max : index * WindowsSize;
        }
        private Rectangle findBoundingBox()
        {
            List<Point> points = new List<Point>();
            

            for (int i = 0; i < Image.Width; i++)
            {
                for (int j = 0; j < Image.Height; j++)
                {
                    Color mPixel = Image.GetPixel(i, j);
                    if (mPixel.GetBrightness() > 0.45)
                        points.Add(new Point(i, j));
                }
            }
            return BoundingBox(points);
        }
    }
}
