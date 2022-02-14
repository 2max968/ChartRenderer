using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    public class CSVWriter
    {
        public static string Write(List<double[]> arrays, string[] titles)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Join(", ", titles));
            if (arrays.Count == 0)
                return sb.ToString();
            int longestSize = arrays[0].Length;
            for (int i = 1; i < arrays.Count; i++)
                if (arrays[i].Length > longestSize)
                    longestSize = arrays[i].Length;

            for(int i = 0; i < longestSize; i++)
            {
                string[] row = new string[arrays.Count];
                for(int j = 0; j < arrays.Count; j++)
                {
                    if(arrays[j].Length > i)
                    {
                        row[j] = arrays[j][i].ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        row[j] = "";
                    }
                }
                sb.AppendLine(string.Join(", ", row));
            }

            return sb.ToString();
        }

        public static string Write(XYPlotData plotData, string xTitle, string yTitle)
        {
            return Write(new List<double[]> { plotData.DataX, plotData.DataY }, new string[] { xTitle, yTitle });
        }

        public static void WriteToFile(string filename, XYPlotData plotData, string xTitle, string yTitle)
        {
            string text = Write(plotData, xTitle, yTitle);
            File.WriteAllText(filename, text);
        }
    }
}
