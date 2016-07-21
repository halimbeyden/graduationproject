using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject
{
    public static class Extentions
    {
        public static double GeoMean<T>(this IEnumerable<T> source, Func<T, double> selector)
        {
            double total = 1;
            foreach (var value in source)
            {
                if (selector(value) < 0)
                total *= selector(value);
            }
            double result = Math.Pow(total, 1.0 / source.Count());
            if (result < 0)
                return 0;
            return result;
        }
        public static double? Variance<TSource>(this IEnumerable<TSource> source,
      Func<TSource, double> selector)
        {
            double sumSquares = 0;
            double avg = source.Average(selector);
            foreach (var item in source)
            {
                var num = selector(item);
                sumSquares += (num - avg * num - avg);
            }
            return sumSquares / (source.Count() - 1);
        }
    }
}
