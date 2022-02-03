using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    public class CSVReader
    {
        public static CSVTable ReadTableFromFile(string filename, string separator = ",;", string culture = "")
        {
            CSVSettings csvSettings = new CSVSettings();
            csvSettings.EntrySeparators = separator;
            //csvSettings.IgnoreQuotationmarks = ignoreQuotationMarks;
            csvSettings.CultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture(culture);
            return ReadTableFromFile(filename, csvSettings);
        }

        public static CSVTable ReadTableFromFile(string filename, CSVSettings readSettings)
        {
            string csv = File.ReadAllText(filename);
            return ReadTable(csv, readSettings);
        }

        public static CSVTable ReadTable(string csv, string separator = ",;", string culture = "")
        {
            CSVSettings csvSettings = new CSVSettings();
            csvSettings.EntrySeparators = separator;
            //csvSettings.IgnoreQuotationmarks = ignoreQuotationMarks;
            csvSettings.CultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture(culture);
            return ReadTable(csv, csvSettings);
        }

        public static CSVTable ReadTable(string csv, CSVSettings readSettings)
        {
            StringReader sr = new StringReader(csv);
            List<double[]> rows = new List<double[]>();
            int maxRowSize = 0;
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var row = readRow(line, readSettings);
                if(row.Length > maxRowSize)
                    maxRowSize = row.Length;
                rows.Add(row);
            }

            double[,] result = new double[maxRowSize, rows.Count];
            for (int i = 0; i < rows.Count; i++)
            {
                for(int j = 0; j < maxRowSize; j++)
                {
                    if (j < rows[i].Length)
                        result[j, i] = rows[i][j];
                    else
                        result[j, i] = double.NaN;
                }
            }

            return new CSVTable(result);
        }

        static double[] readRow(string row, CSVSettings settings)
        {
            string currentWord = "";
            bool inQuotation = false;
            List<double> words = new List<double>();
            for(int i = 0; i < row.Length + 1; i++)
            {
                if(i >= row.Length || (!inQuotation && settings.EntrySeparators.Contains(row[i])))
                {
                    double value;
                    if (!double.TryParse(currentWord, System.Globalization.NumberStyles.Any, settings.CultureInfo, out value))
                        value = double.NaN;
                    words.Add(value);
                    currentWord = "";
                }
                else if(!settings.IgnoreQuotationmarks && row[i] == '"')
                {
                    inQuotation = !inQuotation;
                }
                else
                {
                    currentWord += row[i];
                }
            }
            return words.ToArray();
        }
    }
}
