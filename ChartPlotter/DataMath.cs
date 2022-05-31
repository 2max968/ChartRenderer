using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    public static class DataMath
    {
        public static double RMS(IEnumerable<double> data)
        {
            int count = 0;
            double squaredSum = 0;
            foreach(double d in data)
            {
                count++;
                squaredSum += d * d;
            }
            if(count == 0)
                return double.NaN;
            return Math.Sqrt(squaredSum / count);
        }

        public static double Mean(IEnumerable<double> data)
        {
            int count = 0;
            double sum = 0;
            foreach(double d in data)
            {
                count++;
                sum += d;
            }
            if (count == 0)
                return double.NaN;
            return sum / count;
        }

        public static double Sum(IEnumerable<double> data)
        {
            double sum = 0;
            foreach (var d in data)
                sum += d;
            return sum;
        }

        public static double Var(IEnumerable<double> data)
        {
            var mean = Mean(data);
            double sum = 0;
            int count = 0;
            foreach(var d in data)
            {
                double v = d - mean;
                sum += v * v;
                count++;
            }
            if (count == 0)
                return double.NaN;
            return sum / count;
        }

        public static double Std(IEnumerable<double> data)
        {
            return Math.Sqrt(Var(data));
        }
    }
}
