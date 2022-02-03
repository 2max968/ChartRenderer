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
        public int Length
        {
            get
            {
                return DataX.Length;
            }
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
    }
}
