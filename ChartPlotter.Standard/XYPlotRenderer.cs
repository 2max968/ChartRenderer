using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ChartPlotter
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class XYPlotRenderer
    {
        [Category("Style")] public Color BackgroundColor { get; set; } = Color.White;
        [Category("Style")] public Color ForegroundColor { get; set; } = Color.Black;
        [Category("Style")] public string LabelX { get; set; } = "X Axis";
        [Category("Style")] public string LabelY { get; set; } = "Y Axis";
        [Category("Style")] public string Title { get; set; } = "Chart";
        [Category("Data")] public ChartRange RangeX { get; set; } = null;
        [Category("Data")] public ChartRange RangeY { get; set; } = null;
        [Category("Style")] public int Border { get; set; } = 10;
        [Category("Style")] public Font Font { get; set; } = new Font("Calibri", 12, FontStyle.Regular);
        [Category("Style")] public Font TitleFont { get; set; } = new Font("Book Antiqua", 24, FontStyle.Bold);
        [Category("Style")] public Font LegendFont { get; set; } = new Font("Calibri", 10, FontStyle.Regular);
        [Category("Rendering")] public bool Smoothing { get; set; } = true;
        [Category("Style")] public GridLineStyle GridLines { get; set; } = GridLineStyle.None;
        [Category("Data")] public XYPlotDataCollection Data { get; private set; } = new XYPlotDataCollection();
        [Category("Style")] public float ScaleLinesLength { get; set; } = 4;
        [Category("Style")] public int ScaleNumberSize { get; set; } = 16;
        [Browsable(false)] public XYPlotRenderInfo LastRenderInfo { get; private set; } = null;
        [Category("Style")] public bool ShowLegend { get; set; } = true;
        [Category("Style")] public int LegendSpacing { get; set; } = 4;
        [Category("Style")] public bool ShowAxes { get; set; } = true;
        [Category("Rendering")] public float Scaling { get; set; } = 1;
        int colorIndex = 0;

        /// <summary>
        /// Renders the Chart to a bitmap
        /// </summary>
        /// <param name="width">The width of the bitmap</param>
        /// <param name="height">The height of the bitmap</param>
        /// <returns></returns>
        public Bitmap RenderChart(int width, int height)
        {
            float rWidth = width / Scaling;
            float rHeight = height / Scaling;
            var xrange = RangeX;
            var yrange = RangeY;
            (var xrange2, var yrange2) = getDataRange();
            if (xrange == null) xrange = xrange2;
            if (yrange == null) yrange = yrange2;
            var chartBounds = new Rect(Border, Border, (int)rWidth - Border, (int)rHeight - Border);
            var xaxispos = Math.Min(0, Math.Max(1, -xrange.Min / (xrange.Max - xrange.Min)));
            var yaxispos = Math.Min(0, Math.Max(1, -yrange.Min / (yrange.Max - yrange.Min)));
            var xscalestep = getStepWidth(xrange);
            var yscalestep = getStepWidth(yrange);

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = Smoothing ? SmoothingMode.HighQuality : SmoothingMode.None;
                g.TextRenderingHint = Smoothing ? System.Drawing.Text.TextRenderingHint.AntiAliasGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.ScaleTransform(Scaling, Scaling);

                var xtextheight = g.MeasureString(LabelX, Font).Height;
                var ytextheight = g.MeasureString(LabelY, Font).Height;
                var titleheight = g.MeasureString(Title, TitleFont).Height;
                if(LabelY != "")
                    chartBounds.Left += (int)ytextheight;
                if(LabelX != "")
                    chartBounds.Bottom -= (int)xtextheight;
                if (Title != "")
                    chartBounds.Top += (int)titleheight;

                chartBounds.Bottom -= ScaleNumberSize;
                chartBounds.Left += ScaleNumberSize;

                g.Clear(BackgroundColor);

                using (Brush textBrush = new SolidBrush(ForegroundColor))
                {
                    if(ShowLegend)
                    {
                        int legendTextWidth = 0;
                        int legendTextHeight = 0;
                        lock(Data)
                        {
                            foreach(var data in Data)
                            {
                                var size = g.MeasureString(data.DataTitle, LegendFont);
                                if (size.Width > legendTextWidth) legendTextWidth = (int)Math.Ceiling(size.Width);
                                if (size.Height > legendTextHeight) legendTextHeight = (int)Math.Ceiling(size.Height);
                            }
                            int legendWidth = legendTextWidth + LegendSpacing * 3 + legendTextHeight;
                            int legendheight = legendTextHeight * Data.Count + LegendSpacing * 3;
                            chartBounds.Right -= legendWidth + LegendSpacing;
                            using (Pen legendPen = new Pen(ForegroundColor, 1))
                            {
                                //g.DrawRectangle(legendPen, new Rectangle((int)chartBounds.Right + LegendSpacing, (int)chartBounds.Top, legendWidth, legendheight));
                                for(int i = 0; i < Data.Count; i++)
                                {
                                    int x = (int)chartBounds.Right + 2 * LegendSpacing;
                                    int y = (int)chartBounds.Top + LegendSpacing + i * (LegendSpacing + legendTextHeight);
                                    using(Brush dataBrush = new SolidBrush(Data[i].DataColor ?? Color.Black))
                                        g.FillRectangle(dataBrush, new Rectangle(x, y, legendTextHeight, legendTextHeight));
                                    g.DrawRectangle(legendPen, new Rectangle(x, y, legendTextHeight, legendTextHeight));
                                    using (StringFormat sf = new StringFormat())
                                    {
                                        sf.LineAlignment = StringAlignment.Center;
                                        g.DrawString(Data[i].DataTitle, LegendFont, textBrush, new Point(x + LegendSpacing + legendTextHeight, y));
                                    }
                                }
                            }
                        }
                    }

                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        if (LabelX != "")
                            g.DrawString(LabelX, Font, textBrush, new RectangleF(chartBounds.Left, rHeight - xtextheight, chartBounds.Width, xtextheight), sf);
                        g.RotateTransform(-90);
                        g.TranslateTransform(-rHeight, 0);
                        if (LabelY != "")
                            g.DrawString(LabelY, Font, textBrush, new RectangleF(chartBounds.Top, 0, chartBounds.Height, ytextheight), sf);
                        g.ResetTransform();
                        g.ScaleTransform(Scaling, Scaling);
                        if (Title != "")
                            g.DrawString(Title, TitleFont, textBrush, new RectangleF(0, 0, rWidth, chartBounds.Top), sf);
                    }

                    using (Pen borderPen = new Pen(ForegroundColor, 1))
                    {
                        borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        g.DrawRectangle(borderPen, (Rectangle)chartBounds);
                    }

                    if (ShowAxes)
                    {
                        using (Pen axisPen = new Pen(ForegroundColor, 2))
                        {
                            using (AdjustableArrowCap cap = new AdjustableArrowCap(5, 5, true))
                            {
                                axisPen.CustomEndCap = cap;
                                axisPen.StartCap = LineCap.Round;
                                if (xrange.Min < 0 && xrange.Max > 0)
                                {
                                    int axisX = (int)(-xrange.Min * chartBounds.Width / xrange.Range + chartBounds.Left);
                                    g.DrawLine(axisPen, axisX, chartBounds.Bottom, axisX, chartBounds.Top);
                                }
                                if (yrange.Min < 0 && yrange.Max > 0)
                                {
                                    int axisY = (int)(chartBounds.Height - (-yrange.Min) * chartBounds.Height / yrange.Range + chartBounds.Top);
                                    g.DrawLine(axisPen, chartBounds.Left, axisY, chartBounds.Right, axisY);
                                }
                            }
                        }
                    }

                    using (Pen gridPen = new Pen(ForegroundColor, 1))
                    {
                        using (Pen scalePen = new Pen(ForegroundColor, 1))
                        {
                            using (StringFormat sf = new StringFormat())
                            {
                                sf.Alignment = StringAlignment.Center;
                                sf.LineAlignment = StringAlignment.Near;
                                gridPen.DashStyle = DashStyle.Dash;
                                double xstart = Math.Ceiling(xrange.Min / xscalestep) * xscalestep;
                                double xend = Math.Floor(xrange.Max / xscalestep) * xscalestep;
                                double ystart = Math.Ceiling(yrange.Min / yscalestep) * yscalestep;
                                double yend = Math.Floor(yrange.Max / yscalestep) * yscalestep;
                                for (double x = xstart; x <= xend; x += xscalestep)
                                {
                                    double rx = (x - xrange.Min) * chartBounds.Width / xrange.Range + chartBounds.Left;
                                    if (GridLines == GridLineStyle.FullGrid || (GridLines == GridLineStyle.ZeroOnly && x == 0))
                                        g.DrawLine(gridPen, (float)rx, chartBounds.Top, (float)rx, chartBounds.Bottom);
                                    g.DrawLine(scalePen, (float)rx, chartBounds.Bottom - ScaleLinesLength, (float)rx, chartBounds.Bottom);

                                    Rectangle rect = new Rectangle((int)(rx - 500), (int)chartBounds.Bottom, 1000, ScaleNumberSize);
                                    double lx = Math.Round(x * 100) / 100;
                                    string label = "" + lx;
                                    g.DrawString(label, Font, textBrush, rect, sf);
                                }
                                using (StringFormat sfYScale = new StringFormat())
                                {
                                    sfYScale.Alignment = StringAlignment.Far;
                                    for (double y = ystart; y <= yend; y += yscalestep)
                                    {
                                        double ry = chartBounds.Height - (y - yrange.Min) * chartBounds.Height / yrange.Range + chartBounds.Top;
                                        if (GridLines == GridLineStyle.FullGrid || (GridLines == GridLineStyle.ZeroOnly && y == 0))
                                            g.DrawLine(gridPen, chartBounds.Left, (float)ry, chartBounds.Right, (float)ry);
                                        g.DrawLine(scalePen, chartBounds.Left, (float)ry, chartBounds.Left + ScaleLinesLength, (float)ry);

                                        Rectangle rect = new Rectangle((int)chartBounds.Left - 100, (int)ry - ScaleNumberSize, 100, ScaleNumberSize);
                                        double ly = Math.Round(y * 100) / 100;
                                        string label = "" + ly;
                                        g.DrawString(label, Font, textBrush, rect, sfYScale);
                                    }
                                }
                            }
                        }
                    }

                    g.Clip = new Region((Rectangle)chartBounds);
                    lock (Data)
                    {
                        foreach (var data in Data)
                        {
                            using (Pen p = new Pen(data.DataColor ?? Color.Black, 2))
                            {
                                if (data.PlotStyle == XYPlotStyle.Dash || data.PlotStyle == XYPlotStyle.Line)
                                {
                                    if (data.Length <= 1)
                                        continue;
                                    if (data.PlotStyle == XYPlotStyle.Dash)
                                        p.DashStyle = DashStyle.Dash;
                                    PointF[] pts = new PointF[data.Length];
                                    for (int i = 0; i < data.Length; i++)
                                    {
                                        //double x1 = (data.DataX[i - 1] - xrange.Min) * chartBounds.Width / xrange.Range + chartBounds.Left;
                                        //double y1 = (-data.DataY[i - 1] + yrange.Min) * chartBounds.Height / yrange.Range + chartBounds.Bottom;
                                        double x2 = (data.DataX[i] - xrange.Min) * chartBounds.Width / xrange.Range + chartBounds.Left;
                                        double y2 = (-data.DataY[i] + yrange.Min) * chartBounds.Height / yrange.Range + chartBounds.Bottom;
                                        //g.DrawLine(p, (float)x1, (float)y1, (float)x2, (float)y2);
                                        pts[i] = new PointF((float)x2, (float)y2);
                                    }
                                    g.DrawLines(p, pts);
                                }
                                else if(data.PlotStyle == XYPlotStyle.Cross)
                                {
                                    p.Width = 2;
                                    for (int i = 0; i < data.Length; i++)
                                    {
                                        float crossSize = 4;
                                        float x1 = transX(data.DataX[i], xrange, chartBounds) - crossSize;
                                        float x2 = transX(data.DataX[i], xrange, chartBounds) + crossSize;
                                        float y1 = transY(data.DataY[i], yrange, chartBounds) - crossSize;
                                        float y2 = transY(data.DataY[i], yrange, chartBounds) + crossSize;
                                        g.DrawLine(p, x1, y1, x2, y2);
                                        g.DrawLine(p, x1, y2, x2, y1);
                                    }
                                }
                                else if(data.PlotStyle == XYPlotStyle.Circle)
                                {
                                    p.Width = 2;
                                    for (int i = 0; i < data.Length; i++)
                                    {
                                        float crossSize = 4;
                                        float x = transX(data.DataX[i], xrange, chartBounds);
                                        float y = transY(data.DataY[i], yrange, chartBounds);
                                        g.DrawEllipse(p, new RectangleF(x - crossSize, y - crossSize, crossSize * 2, crossSize * 2));
                                    }
                                }
                            }
                        }
                    }
                    g.ResetClip();
                }
            }

            LastRenderInfo = new XYPlotRenderInfo(xrange, yrange, chartBounds);
            return bmp;
        }

        public XYPlotData AddPlot(double[] x, double[] y)
        {
            return AddPlot(new XYPlotData(x, y));
        }

        public XYPlotData AddPlot(PointF[] points)
        {
            return AddPlot(new XYPlotData(points));
        }

        public XYPlotData AddPlot(XYPlotData data)
        {
            if(data.DataColor == null)
            {
                data.DataColor = PlotColors.Colors[colorIndex % PlotColors.Colors.Length];
                colorIndex++;
            }
            lock (Data)
                Data.Add(data);
            return data;
        }

        (ChartRange xrange, ChartRange yrange) getDataRange()
        {
            ChartRange xrange = new ChartRange(double.MaxValue, double.MinValue);
            ChartRange yrange = new ChartRange(double.MaxValue, double.MinValue);
            lock(Data)
            {
                foreach(var data in Data)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data.DataX[i] < xrange.Min) xrange.Min = data.DataX[i];
                        if (data.DataX[i] > xrange.Max) xrange.Max = data.DataX[i];
                        if (data.DataY[i] < yrange.Min) yrange.Min = data.DataY[i];
                        if (data.DataY[i] > yrange.Max) yrange.Max = data.DataY[i];
                    }
                }
            }
            double overrangeY = yrange.Range * .1;
            yrange.Min -= overrangeY;
            yrange.Max += overrangeY;
            return (xrange, yrange);
        }

        double getStepWidth(ChartRange range)
        {
            double dimension = Math.Ceiling(Math.Log10(range.Range / 2));
            return Math.Pow(10, dimension - 1);
        }

        public void TranslateRangeByPixel(int shiftX, int shiftY)
        {
            if (LastRenderInfo == null)
                return;
            double rShiftX = shiftX / (double)LastRenderInfo.ChartBounds.Width * LastRenderInfo.RangeX.Range;
            double rShiftY = shiftY / (double)LastRenderInfo.ChartBounds.Height * LastRenderInfo.RangeY.Range;
            if (RangeX == null) RangeX = LastRenderInfo.RangeX;
            if (RangeY == null) RangeY = LastRenderInfo.RangeY;
            RangeX.Min -= rShiftX;
            RangeX.Max -= rShiftX;
            RangeY.Min += rShiftY;
            RangeY.Max += rShiftY;
        }

        public void ClearPlots()
        {
            Data.Clear();
            colorIndex = 0;
        }

        float transX(double x, ChartRange xrange, Rect chartBounds)
        {
            return (float)((x - xrange.Min) * chartBounds.Width / xrange.Range + chartBounds.Left);
        }

        float transY(double y, ChartRange yrange, Rect chartBounds)
        {
            return (float)((-y + yrange.Min) * chartBounds.Height / yrange.Range + chartBounds.Bottom);
        }

        public XYPlotPointInfo GetPointInfo(double x, double y)
        {
            if (LastRenderInfo == null)
                return null;
            if (!LastRenderInfo.ChartBounds.Contains(x, y))
                return null;
            List<XYPlotData> plots = new List<XYPlotData>();
            double vx = (x - LastRenderInfo.ChartBounds.Left) / LastRenderInfo.ChartBounds.Width * LastRenderInfo.RangeX.Range + LastRenderInfo.RangeX.Min;
            double vy = -((y - LastRenderInfo.ChartBounds.Top) / LastRenderInfo.ChartBounds.Height * LastRenderInfo.RangeY.Range + LastRenderInfo.RangeY.Min);
            lock(Data)
            {
                foreach(XYPlotData data in Data)
                {
                    for(int i = 1; i < data.Length; i++)
                    {
                        double x1 = transX(data.DataX[i - 1], LastRenderInfo.RangeX, LastRenderInfo.ChartBounds);
                        double x2 = transX(data.DataX[i], LastRenderInfo.RangeX, LastRenderInfo.ChartBounds);
                        double y1 = transY(data.DataY[i - 1], LastRenderInfo.RangeY, LastRenderInfo.ChartBounds);
                        double y2 = transY(data.DataY[i], LastRenderInfo.RangeY, LastRenderInfo.ChartBounds);

                    }
                }
            }

            return new XYPlotPointInfo
            {
                X = vx,
                Y = vy,
                Graphs = plots
            };
        }

        public void Zoom(double x, double y)
        {
            if (LastRenderInfo == null)
                return;
            double midX = (LastRenderInfo.RangeX.Max + LastRenderInfo.RangeX.Min) / 2;
            double midY = (LastRenderInfo.RangeY.Max + LastRenderInfo.RangeY.Min) / 2;
            double minX = LastRenderInfo.RangeX.Min - midX;
            double maxX = LastRenderInfo.RangeX.Max - midX;
            double minY = LastRenderInfo.RangeY.Min - midY;
            double maxY = LastRenderInfo.RangeY.Max - midY;

            minX *= -x;
            maxX *= -x;
            minY *= -y;
            maxY *= -y;

            RangeX = new ChartRange(minX + midX, maxX + midX);
            RangeY = new ChartRange(minY + midY, maxY + midY);
        }
    }
}
