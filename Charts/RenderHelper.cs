using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    internal class RenderHelper
    {
        internal static void DrawHorizontalColorBand(Graphics g, ColorBand band, RectangleF rect, ChartRange range)
        {
            if (band.Length == 0)
                return;

            double vMin = band.Points.First();
            double vMax = band.Points.Last();

            double scale = rect.Width / range.Range;

            for(int i = 1; i < band.Length; i++)
            {
                double x1 = band.Points[i - 1];
                double x2 = band.Points[i];
                Color c1 = band.Colors[i - 1];
                Color c2 = band.Colors[i];

                if(Math.Abs(x1-x2) > double.Epsilon)
                {
                    x1 -= range.Min;
                    x2 -= range.Min;
                    x1 *= scale;
                    x2 *= scale;
                    x1 += rect.Left;
                    x2 += rect.Left;
                    RectangleF prect = new RectangleF((float)x1, rect.Y, (float)(x2 - x1), rect.Height);
                    using (LinearGradientBrush b = new LinearGradientBrush(new PointF((float)x1, 0), new PointF((float)x2, 0), c1, c2))
                    {
                        g.FillRectangle(b, prect);
                    }
                }
            }

            //using(SolidBrush brush = new SolidBrush(band.Colors[0]))
            //    g.FillRectangle(brush, rect);
        }
    }
}
