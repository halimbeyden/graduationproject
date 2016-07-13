using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Filters
{
    public class Features
    {
        /// <summary>
        /// Red
        /// </summary>
        public float AvarageR { get; set; }
        /// <summary>
        /// Green
        /// </summary>
        public float AvarageG { get; set; }
        /// <summary>
        /// Blue
        /// </summary>
        public float AvarageB { get; set; }
        /// <summary>
        /// Hue
        /// </summary>
        public float AvarageH { get; set; }
        /// <summary>
        /// Saturation
        /// </summary>
        public float AvarageS { get; set; }
        /// <summary>
        /// Brightness or Lightness
        /// </summary>
        public float AvarageL { get; set; }
        public int Size { get; set; }

        public void Save(string name)
        {
            File.AppendAllText("./training.txt",AvarageR+";"+AvarageG+";"+AvarageB+";"+AvarageH+";"+AvarageS+";"+AvarageL+";"+Size+";"+name+Environment.NewLine);
        }
    }
}
