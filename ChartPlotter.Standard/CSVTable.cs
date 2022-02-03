using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    public class CSVTable
    {
        public int Columns { get; private set; }
        public List<CSVObject[]> Rows { get; private set; } = new List<CSVObject[]>();

        private CSVTable(int columns)
        {
            Columns = columns;
        }

        public List<double[]> GetNumericColumns(bool ignoreNonNumerics = false)
        {
            List<double[]> columns = new List<double[]>();
            List<int> numericRows = new List<int>();
            int rows = 0;
            for (int i = 0; i < Rows.Count; i++)
            {
                bool isNumeric = true;
                int wrongIndex = 0;
                for (int j = 0; j < Columns; j++)
                {
                    if (Rows[i][j].IsReal())
                    {
                        isNumeric = false;
                        wrongIndex = j;
                    }
                }
                if(isNumeric)
                {
                    if (rows != 0 && !ignoreNonNumerics)
                        throw new Exception("Non numeric value " + Rows[i][wrongIndex].GetText() + " in row " + i);
                    numericRows.Add(i);
                }
            }
            for (int i = 0; i < Columns; i++)
                columns.Add(new double[numericRows.Count]);
            for(int i = 0; i < numericRows.Count; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    columns[j][i] = Rows[numericRows[i]][j].GetReal();
                }
            }
            return columns;
        }

        public static CSVTable LoadFromFile(string path, CSVSettings settings)
        {
            string text = File.ReadAllText(path);
            return LoadFromText(text, settings);
        }

        public static CSVTable LoadFromText(string text, CSVSettings settings)
        {
            string[] lines = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int columns = 0;
            List<List<CSVObject>> rows = new List<List<CSVObject>>();
            for(int i = 0; i < lines.Length; i++)
            {
                var entries = ParseLine(lines[i], settings);
                if (entries.Count > columns) columns = entries.Count;
                rows.Add(entries);
            }
            CSVTable table = new CSVTable(columns);
            for(int i = 0; i < rows.Count; i++)
            {
                CSVObject[] objects = new CSVObject[columns];
                for(int j = 0; j < objects.Length; j++)
                {
                    if (j < rows[i].Count)
                        objects[j] = rows[i][j];
                    else
                        objects[j] = new CSVObject(null);
                }
                table.Rows.Add(objects);
            }
            return table;
        }

        public static List<CSVObject> ParseLine(string line, CSVSettings settings)
        {
            List<CSVObject> entries = new List<CSVObject>();
            bool quote = false;
            string text = "";
            for(int i = 0; i < line.Length; i++)
            {
                char chr = line[i];
                if ((chr == '"' || chr == '\'') && !settings.IgnoreQuotes)
                    quote = !quote;
                else if(!quote)
                {
                    if(settings.EntrySeparators.Contains(chr))
                    {
                        entries.Add(ParseText(text, settings));
                        text = "";
                    }
                }
                else
                {
                    text += quote;
                }
            }
            entries.Add(ParseText(text, settings));
            return entries;
        }

        public static CSVObject ParseText(string text, CSVSettings settings)
        {
            if (int.TryParse(text, System.Globalization.NumberStyles.Any, settings.CultureInfo, out int i))
                return new CSVObject(i);
            if (double.TryParse(text, System.Globalization.NumberStyles.Any, settings.CultureInfo, out double d))
                return new CSVObject(d);
            return new CSVObject(text);
        }
    }
}
