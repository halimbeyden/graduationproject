using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject
{
    public class Filter
    {
        private static Filter instance;

        private Filter() { }

        public static Filter SharedInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Filter();
                }
                return instance;
            }
        }
        public Bitmap DetectRed(Bitmap image)
        {
            ColorFiltering cf = new ColorFiltering(new IntRange(150, 255), new IntRange(120, 160), new IntRange(60, 90));
            return cf.Apply(image);
        }
        public Bitmap DetectJupiter(Bitmap image)
        {
            ColorFiltering cf = new ColorFiltering(new IntRange(0,255), new IntRange(76,165), new IntRange(64,154));
            cf.FillOutsideRange = false;
            cf.ApplyInPlace(image);
            BlobCounter bc = new BlobCounter();
            bc.FilterBlobs = true;
            bc.MinHeight = 50;
            bc.MinWidth = 50;
            bc.ProcessImage(image);
            Blob[] blobs = bc.GetObjectsInformation();
            Console.WriteLine(blobs.Count());
            return image;
        }

        public Bitmap DetectCircle(Bitmap image)
        {

            SimpleShapeChecker shapeChecker = new SimpleShapeChecker();
            // locate objects using blob counter
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage(image);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            // create Graphics object to draw on the image and a pen
            Graphics g = Graphics.FromImage(image);
            Pen redPen = new Pen(Color.Red, 2);
            // check each object and draw circle around objects, which
            // are recognized as circles
            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);
                try
                {
                    shapeChecker.CheckShapeType(edgePoints);
                }
                catch (Exception)
                {
                    continue;
                }

                List<IntPoint> corners;
                AForge.Point center;
                float radius;
                if (shapeChecker.IsCircle(edgePoints, out center, out radius))
                {
                    g.DrawEllipse(redPen,
                        (int)(center.X - radius),
                        (int)(center.Y - radius),
                        (int)(radius * 2),
                        (int)(radius * 2));
                }
                else if (shapeChecker.IsQuadrilateral(edgePoints, out corners))
                {
                    g.DrawPolygon(redPen, ToPointsArray(corners));
                }
            }
            redPen.Dispose();
            g.Dispose();
            return image;
        }
        private System.Drawing.Point[] ToPointsArray(List<IntPoint> points)
        {
            return points.Select(p => new System.Drawing.Point(p.X, p.Y)).ToArray();
        }


    }
}
