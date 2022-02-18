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
                    Brush b;
                    if (band.Interpolate)
                    {
                        b = new LinearGradientBrush(new PointF((float)x1, 0), new PointF((float)x2, 0), c1, c2);
                    }
                    else
                    {
                        b = new SolidBrush(c1);
                    }
                    g.FillRectangle(b, prect);
                    b.Dispose();
                }
            }

            if(band.Extend)
            {
                double left = band.Points.First();
                double right = band.Points.Last();
                left -= range.Min;
                right -= range.Min;
                left *= scale;
                right *= scale;
                left += rect.Left;
                right += rect.Left;
                if (left > rect.Left)
                {
                    RectangleF prect = new RectangleF((float)rect.Left, rect.Top, (float)(left - rect.Left), rect.Height);
                    using (Brush b = new SolidBrush(band.Colors.First()))
                    {
                        g.FillRectangle(b, prect);
                    }
                }
                if(right < rect.Right)
                {
                    RectangleF prect = new RectangleF((float)right, rect.Top, (float)(rect.Right - right), rect.Height);
                    using (Brush b = new SolidBrush(band.Colors.Last()))
                    {
                        g.FillRectangle(b, prect);
                    }
                }
            }
        }

        internal static void DrawHorizontalColorBand(Renderer g, ColorBand band, RectF rect, ChartRange range)
        {
            if (band.Length == 0)
                return;

            double vMin = band.Points.First();
            double vMax = band.Points.Last();

            double scale = rect.Width / range.Range;

            for (int i = 1; i < band.Length; i++)
            {
                double x1 = band.Points[i - 1];
                double x2 = band.Points[i];
                Color c1 = band.Colors[i - 1];
                Color c2 = band.Colors[i];

                if (Math.Abs(x1 - x2) > double.Epsilon)
                {
                    x1 -= range.Min;
                    x2 -= range.Min;
                    x1 *= scale;
                    x2 *= scale;
                    x1 += rect.Left;
                    x2 += rect.Left;
                    RectF prect = RectF.FromXYHW((float)x1, rect.Top, (float)(x2 - x1), rect.Height);
                    CBrush b;
                    if (band.Interpolate)
                    {
                        b = g.CreateBrush((float)x1, 0, (float)x2, 0, c1, c2);
                    }
                    else
                    {
                        b = g.CreateBrush(c1);
                    }
                    g.FillRectangle(b, prect);
                    b.Dispose();
                }
            }

            if (band.Extend)
            {
                double left = band.Points.First();
                double right = band.Points.Last();
                left -= range.Min;
                right -= range.Min;
                left *= scale;
                right *= scale;
                left += rect.Left;
                right += rect.Left;
                if (left > rect.Left)
                {
                    RectF prect = RectF.FromXYHW((float)rect.Left, rect.Top, (float)(left - rect.Left), rect.Height);
                    using (CBrush b = g.CreateBrush(band.Colors.First()))
                    {
                        g.FillRectangle(b, prect);
                    }
                }
                if (right < rect.Right)
                {
                    RectF prect = RectF.FromXYHW((float)right, rect.Top, (float)(rect.Right - right), rect.Height);
                    using (CBrush b = g.CreateBrush(band.Colors.Last()))
                    {
                        g.FillRectangle(b, prect);
                    }
                }
            }
        }
    }
}
