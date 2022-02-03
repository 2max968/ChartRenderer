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
        public double[,] Data { get; private set; }
        public int Rows
        {
            get { return Data.GetLength(1); }
        }
        public int Columns
        { get { return Data.GetLength(0); } }

        public CSVTable(double[,] data)
        {
            Data = data;
        }

        public XYPlotData GetXYPlotData(int columnX, int columnY, int firstRow = 0)
        {
            double[] x = new double[Rows - firstRow];
            double[] y = new double[Rows - firstRow];
            if (columnX < Columns)
                x = GetColumn(columnX, firstRow);
            else
                x = Util.CreateNAN(Rows - firstRow);
            if(columnY < Columns)
                y = GetColumn(columnY, firstRow);
            else
                y = Util.CreateNAN(Rows - firstRow);
            return new XYPlotData(x, y);
        }

        public double[] GetColumn(int index, int firstRow = 0)
        {
            if (index >= Columns)
                throw new IndexOutOfRangeException();
            double[] data = new double[Rows - firstRow];
            Buffer.BlockCopy(Data, Rows * index * sizeof(double) + firstRow * sizeof(double), data, 0, Rows * sizeof(double) - firstRow * sizeof(double));
            return data;
        }
    }
}
