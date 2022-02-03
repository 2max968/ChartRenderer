using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static int GetExponent(this double value)
        {
            if (value == 0)
                return 0;
            value = Math.Abs(value) * 1.01;
            if (value < 10000 && value >= 1)
                return 0;

            return (int)Math.Ceiling(Math.Log10(value)) - 1;
        }

        public static char GetHighChar(char chr)
        {
            switch(chr)
            {
                case '0': return (char)0x2070;
                case '1': return (char)0xB9;
                case '2': return (char)0xB2;
                case '3': return (char)0xB3;
                case '4': return (char)0x2074;
                case '5': return (char)0x2075;
                case '6': return (char)0x2076;
                case '7': return (char)0x2077;
                case '8': return (char)0x2078;
                case '9': return (char)0x2079;
                case '-': return (char)0x207B;
                case '(': return (char)0x207D;
                case ')': return (char)0x207E;
                case 'i': return (char)0x2071;
            }
            return '?';
        }

        public static string GetHighNumber(int num)
        {
            string str = num.ToString();
            string str2 = "";
            for (int i = 0; i < str.Length; i++)
                str2 += GetHighChar(str[i]);
            return str2;
        }

        public static double MaxAbs(double a, params double[] b)
        {
            double max = Math.Abs(a);
            for(int i = 0; i < b.Length; i++)
            {
                double v = Math.Abs(b[i]);
                if (v > max)
                    max = v;
            }
            return max;
        }

        public static double[] CreateNAN(int length)
        {
            double[] dbl = new double[length];
            for (int i = 0; i < dbl.Length; i++)
                dbl[i] = double.NaN;
            return dbl;
        }

        public static double GetStepwidth(double range)
        {
            range = Math.Abs(range);
            if(range == 0) return 0;
            double fact = 1;
            while (range >= 10)
            {
                range /= 10;
                fact /= 10;
            }
            while (range < 1)
            {
                range *= 10;
                fact *= 10;
            }

            if (range > 8) return 1/ fact;
            if (range > 5) return 1/ fact;
            if (range > 2) return .5/ fact;
            if (range > 1) return .25/ fact;
            return .1/ fact;
        }

        public static bool IsRegular(this double val)
        {
            return !double.IsNaN(val) && !double.IsInfinity(val) && !double.IsPositiveInfinity(val) && !double.IsNegativeInfinity(val);
        }
    }
}
