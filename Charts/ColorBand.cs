using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ColorBand
    {
        public double[] Points { get;private set;}
        public Color[] Colors { get; private set; }

        public int Length
        {
            get
            {
                return Colors.Length;
            }
            set
            {
                double[] tmpX = new double[value];
                Color[] tmpY = new Color[value];
                for (int i = Points.Length; i < value; i++)
                {
                    tmpX[i] = double.NaN;
                    tmpY[i] = Color.Transparent;
                }
                Array.Copy(Points, 0, tmpX, 0, Math.Min(value, Points.Length));
                Array.Copy(Colors, 0, tmpY, 0, Math.Min(value, Colors.Length));
                Points = tmpX;
                Colors = tmpY;
            }
        }

        public ColorBand()
        {
            Points = new double[0];
            Colors = new Color[0];
        }

        public ColorBand(double[] points, Color[] colors)
        {
            if(points.Length != colors.Length)
                throw new ArgumentException("points length is not equal to colors length");
            Points = points;
            Colors = colors;
            Array.Sort(Points, Colors);
        }

        public ColorBand AddPoint(double t, Color c)
        {
            double[] dx = Points;
            Color[] dy = Colors;
            Array.Resize(ref dx, dx.Length + 1);
            Array.Resize(ref dy, dy.Length + 1);
            dx[dx.Length - 1] = t;
            dy[dy.Length - 1] = c;

            Array.Sort(dx, dy);

            Points = dx;
            Colors = dy;
            return this;
        }
    }
}
