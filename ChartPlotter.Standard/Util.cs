using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    public static class Util
    {
        public static double RoundToSignificantDigits(this double value, int digits)
        {
            if (value == 0)
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(value))) + 1);
            return scale * Math.Round(value / scale, digits);
        }
    }
}
