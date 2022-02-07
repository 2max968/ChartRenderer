using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    public class XYPlotData
    {
        public delegate double SimpleFunction(double x);
        public delegate (double x, double y) FunctionXY(double t);

        public string DataTitle { get; set; } = "Data";
        public double[] DataX { get; private set; }
        public double[] DataY { get; private set; }
        public Color? DataColor { get; set; } = null;
        public XYPlotStyle PlotStyle { get; set; } = XYPlotStyle.Line;
        public bool LegendVisible { get; set; } = true;
        public float Width { get; set; } = 2;
        public int Length
        {
            get
            {
                return DataX.Length;
            }
            set
            {
                double[] tmpX = new double[value];
                double[] tmpY = new double[value];
                for(int i = DataX.Length; i < value; i++)
                {
                    tmpX[i] = double.NaN;
                    tmpY[i] = double.NaN;
                }
                Array.Copy(DataX, 0, tmpX, 0, Math.Min(value, DataX.Length));
                Array.Copy(DataY, 0, tmpY, 0, Math.Min(value, DataY.Length));
                DataX = tmpX;
                DataY = tmpY;
            }
        }
        public int PlotIndex { get; set; } = 0;
        public bool Logarithmic { get; private set; } = false;

        public XYPlotData()
        {
            DataX = new double[0];
            DataY = new double[0];
        }

        public XYPlotData(double[] x, double[] y)
        {
            if (x.Length != y.Length)
                throw new ArgumentException("x length is not equal to y length");

            DataX = new double[x.Length];
            DataY = new double[x.Length];
            Array.Copy(x, DataX, x.Length);
            Array.Copy(y, DataY, y.Length);
        }

        public XYPlotData(PointF[] points)
        {
            DataX = new double[points.Length];
            DataY = new double[points.Length];
            for(int i = 0; i < points.Length; i++)
            {
                DataX[i] = points[i].X;
                DataY[i] = points[i].Y;
            }
        }

        public XYPlotData SetTitle(string title)
        {
            DataTitle = title;
            return this;
        }

        public XYPlotData SetStyle(char style)
        {
            switch(style)
            {
                case '_':
                    PlotStyle = XYPlotStyle.Line;
                    break;
                case '-':
                    PlotStyle = XYPlotStyle.Dash;
                    break;
                case 'x':
                case 'X':
                    PlotStyle = XYPlotStyle.Cross;
                    break;
                case 'o':
                case 'O':
                    PlotStyle = XYPlotStyle.Circle;
                    break;
            }
            return this;
        }

        public XYPlotData SetStyle(XYPlotStyle style)
        {
            PlotStyle = style;
            return this;
        }

        public XYPlotData HideFromLegend()
        {
            LegendVisible = false;
            return this;
        }

        public XYPlotData ShowInLegend()
        {
            LegendVisible = true;
            return this;
        }

        public XYPlotData SetColor(Color color)
        {
            DataColor = color;
            return this;
        }

        public XYPlotData SetColor(string htmlColor)
        {
            DataColor = ColorTranslator.FromHtml(htmlColor);
            return this;
        }

        public XYPlotData SetWidth(float width)
        {
            Width = width;
            return this;
        }

        public XYPlotData SetPlotIndex(int index)
        {
            PlotIndex = index;
            return this;
        }

        public XYPlotData MakeLogarithmic()
        {
            for(int i = 0; i < DataY.Length; i++)
            {
                DataY[i] = Math.Log10(DataY[i]);
                if (double.IsNegativeInfinity(DataY[i])) DataY[i] = double.NaN;
                if (double.IsPositiveInfinity(DataY[i])) DataY[i] = double.NaN;
            }
            Logarithmic = true;
            return this;
        }

        public static XYPlotData CreateData(double xstart, double xstep, double xend, SimpleFunction function)
        {
            if (xend < xstart) throw new ArgumentException("xend has to be larger than x start");
            if (xstep <= 0) throw new ArgumentException("xstep must be greater than zero");

            int length = (int)Math.Ceiling((xend - xstart) / xstep) + 1;
            double[] x = new double[length];
            double[] y = new double[length];
            for(int i = 0; i < length; i++)
            {
                x[i] = xstart + i * xstep;
                y[i] = function(x[i]);
            }

            return new XYPlotData(x, y);
        }

        public static XYPlotData CreateData(double tstart, double tstep, double tend, FunctionXY function)
        {
            if (tend < tstart) throw new ArgumentException("tend has to be larger than t start");
            if (tstep <= 0) throw new ArgumentException("tstep must be greater than zero");

            int length = (int)Math.Ceiling((tend - tstart) / tstep) + 1;
            double[] x = new double[length];
            double[] y = new double[length];
            for (int i = 0; i < length; i++)
            {
                double t = tstart + i * tstep;
                (x[i], y[i]) = function(t);
            }

            return new XYPlotData(x, y);
        }

        public XYPlotData AddPoint(double x, double y)
        {
            double[] dx = DataX;
            double[] dy = DataY;
            Array.Resize(ref dx, dx.Length + 1);
            Array.Resize(ref dy, dy.Length + 1);
            dx[dx.Length - 1] = x;
            dy[dy.Length - 1] = y;
            DataX = dx;
            DataY = dy;
            return this;
        }

        public XYPlotData AddPoint(PointF point)
        {
            return AddPoint(point.X, point.Y);
        }

        public XYPlotData TranslateX(double distance)
        {
            for(int i = 0; i < DataX.Length; i++)
            {
                DataX[i] += distance;
            }
            return this;
        }

        public static XYPlotData operator&(XYPlotData x, XYPlotData y)
        {
            List<double> dx = new List<double>();
            List<double> dy = new List<double>();
            dx.AddRange(x.DataX);
            dx.AddRange(y.DataX);
            dy.AddRange(x.DataY);
            dy.AddRange(y.DataY);
            XYPlotData result = new XYPlotData(dx.ToArray(), dy.ToArray());
            result.DataColor = x.DataColor;
            result.DataTitle = x.DataTitle;
            result.PlotIndex = x.PlotIndex;
            result.PlotStyle = x.PlotStyle;
            result.Width = x.Width;
            return result;
        }
    }
}
