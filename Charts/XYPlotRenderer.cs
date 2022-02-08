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
        ChartRange rangeX = new ChartRange();
        ChartRange rangeY1 = new ChartRange();
        ChartRange rangeY2 = new ChartRange();

        string labelX = "X Axis";
        string labelY1 = "Y Axis";
        string labelY2 = "";
        string title = "Chart";
        Bitmap bmpLabelX = null;
        Bitmap bmpLabelY1 = null;
        Bitmap bmpLabelY2 = null;
        Bitmap bmpTitle = null;

        [Category("Style")] public string LabelX 
        { 
            get
			{
                return labelX;
			} 
            set
			{
                labelX = TextUnescaper.Unescape(value);
                Util.Cleanup(ref bmpLabelX);
                bmpLabelX = Util.BmpFromBase64(value);
			}
        }
        [Category("Style")] public string LabelY1
		{
			get
			{
                return labelY1;
			}
			set
			{
                labelY1 = TextUnescaper.Unescape(value);
                Util.Cleanup(ref bmpLabelY1);
                bmpLabelY1 = Util.BmpFromBase64(value);
			}
		}
        [Category("Style")] public string LabelY2
		{
			get
			{
                return labelY2;
			}
			set
			{
                labelY2 = TextUnescaper.Unescape(value);
                Util.Cleanup(ref bmpLabelY2);
                bmpLabelY2 = Util.BmpFromBase64(value);
			}
		}
        [Category("Style")] public string Title
		{
			get
			{
                return title;
			}
			set
			{
                title = TextUnescaper.Unescape(value);
                Util.Cleanup(ref bmpTitle);
                bmpTitle = Util.BmpFromBase64(value);
			}
		}

        [Category("Style")] public Color BackgroundColor { get; set; } = Color.White;
        [Category("Style")] public Color ForegroundColor { get; set; } = Color.Black;
        [Category("Data")] public ChartRange RangeX { set { if (value != null) rangeX = value; else rangeX = new ChartRange(); } get { return rangeX; } }
        [Category("Data")] public ChartRange RangeY1 { set { if (value != null) rangeY1 = value; else rangeY1 = new ChartRange(); } get { return rangeY1; } }
        [Category("Data")] public ChartRange RangeY2 { set { if (value != null) rangeY2 = value; else rangeY2 = new ChartRange(); } get { return rangeY2; } }
        [Category("Style")] public int Border { get; set; } = 10;
        [Category("Style")] public Font Font { get; set; } = new Font("Calibri", 12, FontStyle.Regular);
        [Category("Style")] public Font TitleFont { get; set; } = new Font("Calibri", 24, FontStyle.Bold);
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
        [Category("Data")] public float ScaleSubdivisionX { get; set; } = 1;
        [Category("Data")] public float ScaleSubdivisionY1 { get; set; } = 1;
        [Category("Data")] public float ScaleSubdivisionY2 { get; set; } = 1;
        [Category("Background")] public ColorBand HorizontalBackground { get; set; } = new ColorBand();
        [Category("Data")] public XYPointInfo HighlightedPoint { get; set; } = null;
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
            var xrange = new ChartRange(RangeX);
            var yrange1 = new ChartRange(RangeY1);
            var yrange2 = new ChartRange(RangeY2);
            (var xrange2, var yrange1_2, var yrange2_2) = getDataRange();
            if (xrange == null) xrange = xrange2;
            else
            {
                if (double.IsNaN(xrange.Min)) xrange.Min = xrange2.Min;
                if (double.IsNaN(xrange.Max)) xrange.Max = xrange2.Max;
            }
            if (yrange1 == null) yrange1 = yrange1_2;
            else
            {
                if (double.IsNaN(yrange1.Min)) yrange1.Min = yrange1_2.Min;
                if (double.IsNaN(yrange1.Max)) yrange1.Max = yrange1_2.Max;
            }
            if (yrange2 == null) yrange2 = yrange2_2;
            else
            {
                if (double.IsNaN(yrange2.Min)) yrange2.Min = yrange2_2.Min;
                if (double.IsNaN(yrange2.Max)) yrange2.Max = yrange2_2.Max;
            }
            var chartBounds = new Rect(Border, Border, (int)rWidth - Border, (int)rHeight - Border);
            var xaxispos = Math.Min(0, Math.Max(1, -xrange.Min / (xrange.Max - xrange.Min)));
            var yaxispos = Math.Min(0, Math.Max(1, -yrange1.Min / (yrange1.Max - yrange1.Min)));
            var xscalestep = getStepWidth(xrange) / ScaleSubdivisionX;
            var yscalestep1 = getStepWidth(yrange1) / ScaleSubdivisionY1;
            var yscalestep2 = getStepWidth(yrange2) / ScaleSubdivisionY2;
            bool secondPlot = HasSecondPlot();

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = Smoothing ? SmoothingMode.HighQuality : SmoothingMode.None;
                g.TextRenderingHint = Smoothing ? System.Drawing.Text.TextRenderingHint.AntiAliasGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.ScaleTransform(Scaling, Scaling);

                var xtextheight = g.MeasureString(LabelX, Font).Height;
                var ytextheight = g.MeasureString(LabelY1, Font).Height;
                var titleheight = g.MeasureString(Title, TitleFont).Height;
                if (bmpLabelY1 != null)
                    chartBounds.Left += bmpLabelY1.Height;
                else if(LabelY1 != "")
                    chartBounds.Left += (int)ytextheight;
                if (bmpLabelX != null)
                    chartBounds.Bottom -= bmpLabelX.Height;
                else if (LabelX != "")
                    chartBounds.Bottom -= (int)xtextheight;
                if (bmpTitle != null)
                    chartBounds.Top += bmpTitle.Height;
                else if (Title != "")
                    chartBounds.Top += (int)titleheight;

                chartBounds.Bottom -= ScaleNumberSize;
                chartBounds.Left += ScaleNumberSize;
                long legendPos = chartBounds.Right;
                if(secondPlot)
                {
                    chartBounds.Right -= ScaleNumberSize;
                }
                if (LabelY2 != "")
                    chartBounds.Right -= (int)ytextheight;

                g.Clear(BackgroundColor);

                using (Brush textBrush = new SolidBrush(ForegroundColor))
                {
                    if(ShowLegend)
                    {
                        int legendTextWidth = 0;
                        int legendTextHeight = 0;
                        lock(Data)
                        {
                            List<XYPlotData> legendData = new List<XYPlotData>();
                            foreach(var data in Data)
                            {
                                if (!data.LegendVisible) continue;
                                legendData.Add(data);
                                var size = g.MeasureString(data.DataTitle, LegendFont);
                                if (size.Width > legendTextWidth) legendTextWidth = (int)Math.Ceiling(size.Width);
                                if (size.Height > legendTextHeight) legendTextHeight = (int)Math.Ceiling(size.Height);
                            }
                            if (legendData.Count > 0)
                            {
                                int legendWidth = legendTextWidth + LegendSpacing * 3 + legendTextHeight;
                                int legendheight = legendTextHeight * legendData.Count + LegendSpacing * 3;
                                chartBounds.Right -= legendWidth + LegendSpacing;
                                legendPos -= legendWidth + LegendSpacing;
                                using (Pen legendPen = new Pen(ForegroundColor, 1))
                                {
                                    //g.DrawRectangle(legendPen, new Rectangle((int)chartBounds.Right + LegendSpacing, (int)chartBounds.Top, legendWidth, legendheight));
                                    for (int i = 0; i < legendData.Count; i++)
                                    {
                                        int x = (int)legendPos + 2 * LegendSpacing;
                                        int y = (int)chartBounds.Top + LegendSpacing + i * (LegendSpacing + legendTextHeight);
                                        using (Brush dataBrush = new SolidBrush(legendData[i].DataColor ?? Color.Black))
                                            g.FillRectangle(dataBrush, new Rectangle(x, y, legendTextHeight, legendTextHeight));
                                        g.DrawRectangle(legendPen, new Rectangle(x, y, legendTextHeight, legendTextHeight));
                                        using (StringFormat sf = new StringFormat())
                                        {
                                            sf.LineAlignment = StringAlignment.Center;
                                            g.DrawString(legendData[i].DataTitle, LegendFont, textBrush, new Point(x + LegendSpacing + legendTextHeight, y));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        if(bmpLabelX != null)
                            g.DrawImage(bmpLabelX, new PointF(chartBounds.Left + (chartBounds.Width - bmpLabelX.Width) * .5f, rHeight - bmpLabelX.Height));
                        else if (LabelX != "")
                            g.DrawString(LabelX, Font, textBrush, new RectangleF(chartBounds.Left, rHeight - xtextheight, chartBounds.Width, xtextheight), sf);
                        g.RotateTransform(-90);
                        g.TranslateTransform(-rHeight, 0);
                        if(bmpLabelY1 != null)
                            g.DrawImage(bmpLabelY1, new PointF(rHeight - chartBounds.Bottom + (chartBounds.Height - bmpLabelY1.Width) * .5f, 0));
                        else if (LabelY1 != "")
                            g.DrawString(LabelY1, Font, textBrush, new RectangleF(rHeight - chartBounds.Bottom, 0, chartBounds.Height, ytextheight), sf);
                        if(bmpLabelY2 != null)
                            g.DrawImage(bmpLabelY2, new PointF(rHeight - chartBounds.Bottom + (chartBounds.Height - bmpLabelY2.Width) * .5f, chartBounds.Right + bmpLabelY2.Height));
                        else if (LabelY2 != "")
                            g.DrawString(LabelY2, Font, textBrush, new RectangleF(rHeight - chartBounds.Bottom, chartBounds.Right + (secondPlot ? ytextheight : 0), chartBounds.Height, ytextheight), sf);
                        g.ResetTransform();
                        g.ScaleTransform(Scaling, Scaling);
                        if (bmpTitle != null)
                            g.DrawImage(bmpTitle, new PointF((rWidth - bmpTitle.Width) * .5f, 0));
                        else if (Title != "")
                            g.DrawString(Title, TitleFont, textBrush, new RectangleF(0, 0, rWidth, chartBounds.Top), sf);
                    }


                    if (HorizontalBackground != null)
                    {
                        g.Clip = new Region((Rectangle)chartBounds);
                        RenderHelper.DrawHorizontalColorBand(g, HorizontalBackground, (Rectangle)chartBounds, xrange);
                        g.ResetClip();
                    }

                    using (Pen borderPen = new Pen(ForegroundColor, 1))
                    {
                        borderPen.DashStyle = DashStyle.Dash;
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
                                if (yrange1.Min < 0 && yrange1.Max > 0)
                                {
                                    int axisY = (int)(chartBounds.Height - (-yrange1.Min) * chartBounds.Height / yrange1.Range + chartBounds.Top);
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
                                if (Data.Count > 0)
                                {
                                    sf.Alignment = StringAlignment.Center;
                                    sf.LineAlignment = StringAlignment.Near;
                                    gridPen.DashStyle = DashStyle.Dash;
                                    double xstart = Math.Ceiling(xrange.Min / xscalestep) * xscalestep;
                                    double xend = Math.Floor(xrange.Max / xscalestep) * xscalestep;
                                    double ystart1 = Math.Ceiling(yrange1.Min / yscalestep1) * yscalestep1;
                                    double yend1 = Math.Floor(yrange1.Max / yscalestep1) * yscalestep1;
                                    double ystart2 = Math.Ceiling(yrange2.Min / yscalestep2) * yscalestep2;
                                    double yend2 = Math.Floor(yrange2.Max / yscalestep2) * yscalestep2;

                                    int xExp = xscalestep.GetExponent();
                                    int y1Exp = yscalestep1.GetExponent();
                                    int y2Exp = yscalestep2.GetExponent();
                                    //int xExp = Util.MaxAbs(xstart, xend).GetExponent();
                                    //int y1Exp = Util.MaxAbs(ystart1, yend1).GetExponent();
                                    //int y2Exp = Util.MaxAbs(ystart2, yend2).GetExponent();

                                    for (double x = xstart; x <= xend; x += xscalestep)
                                    {
                                        double rx = (x - xrange.Min) * chartBounds.Width / xrange.Range + chartBounds.Left;
                                        if (GridLines == GridLineStyle.FullGrid || (GridLines == GridLineStyle.ZeroOnly && x == 0))
                                            g.DrawLine(gridPen, (float)rx, chartBounds.Top, (float)rx, chartBounds.Bottom);
                                        g.DrawLine(scalePen, (float)rx, chartBounds.Bottom - ScaleLinesLength, (float)rx, chartBounds.Bottom);

                                        Rectangle rect = new Rectangle((int)(rx - 500), (int)chartBounds.Bottom, 1000, ScaleNumberSize);
                                        double lx = Math.Round(x * 100 / Math.Pow(10, xExp)) / 100;
                                        string label = "" + lx;
                                        g.DrawString(label, Font, textBrush, rect, sf);
                                    }
                                    if (xExp != 0)
                                    {
                                        string text = "x10" + Util.GetHighNumber(xExp);
                                        SizeF textS = g.MeasureString(text, Font);
                                        float x = Math.Min(chartBounds.Right, rWidth - textS.Width);
                                        float y = chartBounds.Bottom;
                                        using (SolidBrush b = new SolidBrush(BackgroundColor))
                                            g.FillRectangle(b, new RectangleF(x, y + 1, textS.Width, textS.Height - 1));
                                        g.DrawString(text, Font, textBrush, x, y);
                                    }

                                    using (StringFormat sfYScale = new StringFormat())
                                    {
                                        sfYScale.Alignment = StringAlignment.Far;
                                        for (double y = ystart1; y <= yend1; y += yscalestep1)
                                        {
                                            double ry = chartBounds.Height - (y - yrange1.Min) * chartBounds.Height / yrange1.Range + chartBounds.Top;
                                            if (GridLines == GridLineStyle.FullGrid || (GridLines == GridLineStyle.ZeroOnly && y == 0))
                                                g.DrawLine(gridPen, chartBounds.Left, (float)ry, chartBounds.Right, (float)ry);
                                            g.DrawLine(scalePen, chartBounds.Left, (float)ry, chartBounds.Left + ScaleLinesLength, (float)ry);

                                            Rectangle rect = new Rectangle((int)chartBounds.Left - 100, (int)ry - ScaleNumberSize, 100, ScaleNumberSize);
                                            double ly = Math.Round(y * 100 / Math.Pow(10, y1Exp)) / 100;
                                            string label = "" + ly;
                                            g.DrawString(label, Font, textBrush, rect, sfYScale);
                                        }
                                        if (y1Exp != 0)
                                        {
                                            string text = "x10" + Util.GetHighNumber(y1Exp);
                                            SizeF textS = g.MeasureString(text, Font);
                                            float x = chartBounds.Left;
                                            float y = chartBounds.Top - ScaleNumberSize - LegendSpacing;
                                            using (SolidBrush b = new SolidBrush(BackgroundColor))
                                                g.FillRectangle(b, new RectangleF(x - textS.Width, y + 1, textS.Width, textS.Height - 1));
                                            g.DrawString(text, Font, textBrush, x, y, sfYScale);
                                        }
                                        sfYScale.Alignment = StringAlignment.Near;
                                        if (secondPlot)
                                        {
                                            for (double y = ystart2; y <= yend2; y += yscalestep2)
                                            {
                                                double ry = chartBounds.Height - (y - yrange2.Min) * chartBounds.Height / yrange2.Range + chartBounds.Top;
                                                g.DrawLine(scalePen, chartBounds.Right, (float)ry, chartBounds.Right - ScaleLinesLength, (float)ry);

                                                Rectangle rect = new Rectangle((int)chartBounds.Right, (int)ry - ScaleNumberSize, 100, ScaleNumberSize);
                                                double ly = Math.Round(y * 100 / Math.Pow(10, y2Exp)) / 100;
                                                string label = "" + ly;
                                                g.DrawString(label, Font, textBrush, rect, sfYScale);
                                            }
                                            if (y2Exp != 0)
                                            {
                                                string text = "x10" + Util.GetHighNumber(y2Exp);
                                                SizeF textS = g.MeasureString(text, Font);
                                                float x = Math.Min(chartBounds.Right, rWidth - textS.Width);
                                                float y = chartBounds.Top - ScaleNumberSize - LegendSpacing;
                                                using (SolidBrush b = new SolidBrush(BackgroundColor))
                                                    g.FillRectangle(b, new RectangleF(x, y + 1, textS.Width, textS.Height - 1));
                                                g.DrawString(text, Font, textBrush, x, y, sfYScale);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    using (StringFormat format = new StringFormat())
                                    {
                                        format.Alignment = StringAlignment.Center;
                                        format.LineAlignment = StringAlignment.Center;
                                        g.DrawString("No data available", Font, textBrush, (Rectangle)chartBounds, format);
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
                            using (Pen p = new Pen(data.DataColor ?? Color.Black, data.Width))
                            {
                                if (data.PlotStyle == XYPlotStyle.Dash || data.PlotStyle == XYPlotStyle.Line || data.PlotStyle == XYPlotStyle.LineAndDot)
                                {
                                    if (data.Length <= 1)
                                        continue;
                                    if (data.PlotStyle == XYPlotStyle.Dash)
                                        p.DashStyle = DashStyle.Dash;
                                    List<PointF> pts = null;
                                    for (int i = 0; i <= data.Length; i++)
                                    {
                                        if (i == data.Length || double.IsNaN(data.DataX[i]) || double.IsNaN(data.DataY[i]))
                                        {
                                            if (pts != null)
                                            {
                                                try
                                                {
                                                    if (pts.Count > 1)
                                                        g.DrawLines(p, pts.ToArray());
                                                    else
                                                        g.DrawEllipse(p, new RectangleF(pts[0].X - .5f, pts[0].Y - .5f, 1, 1));
                                                    pts = null;
                                                }
                                                catch (OverflowException) { }
                                            }
                                        }
                                        else
                                        {
                                            double x2 = (data.DataX[i] - xrange.Min) * chartBounds.Width / xrange.Range + chartBounds.Left;
                                            double y2 = transY(data.DataY[i], data.PlotIndex == 0 ? yrange1 : yrange2, chartBounds);
                                            if (pts == null)
                                            {
                                                pts = new List<PointF>();
                                            }
                                            pts.Add(new PointF((float)x2, (float)y2));
                                        }
                                    }
                                }
                                else if(data.PlotStyle == XYPlotStyle.Cross)
                                {
                                    p.Width = data.Width;
                                    float crossSize = data.Width * 2;
                                    for (int i = 0; i < data.Length; i++)
                                    {
                                        try
                                        {
                                            if (double.IsNaN(data.DataX[i]) || double.IsNaN(data.DataY[i]))
                                                continue;
                                            float x1 = transX(data.DataX[i], xrange, chartBounds) - crossSize;
                                            float x2 = transX(data.DataX[i], xrange, chartBounds) + crossSize;
                                            float y1 = transY(data.DataY[i], data.PlotIndex == 0 ? yrange1 : yrange2, chartBounds) - crossSize;
                                            float y2 = transY(data.DataY[i], data.PlotIndex == 0 ? yrange1 : yrange2, chartBounds) + crossSize;
                                            g.DrawLine(p, x1, y1, x2, y2);
                                            g.DrawLine(p, x1, y2, x2, y1);
                                        }
                                        catch (OverflowException) { }
                                    }
                                }
                                else if(data.PlotStyle == XYPlotStyle.Circle)
                                {
                                    p.Width = data.Width;
                                    for (int i = 0; i < data.Length; i++)
                                    {
                                        try
                                        {
                                            if (double.IsNaN(data.DataX[i]) || double.IsNaN(data.DataY[i]))
                                                continue;
                                            float crossSize = data.Width * 2;
                                            float x = transX(data.DataX[i], xrange, chartBounds);
                                            float y = transY(data.DataY[i], data.PlotIndex == 0 ? yrange1 : yrange2, chartBounds);
                                            g.DrawEllipse(p, new RectangleF(x - crossSize, y - crossSize, crossSize * 2, crossSize * 2));
                                        }
                                        catch (OverflowException) { }
                                    }
                                }

                                if(data.PlotStyle == XYPlotStyle.Dot || data.PlotStyle == XYPlotStyle.LineAndDot)
                                {
                                    p.Width = data.Width;
                                    for (int i = 0; i < data.Length; i++)
                                    {
                                        try
                                        {
                                            if (double.IsNaN(data.DataX[i]) || double.IsNaN(data.DataY[i]))
                                                continue;
                                            float x = transX(data.DataX[i], xrange, chartBounds);
                                            float y = transY(data.DataY[i], data.PlotIndex == 0 ? yrange1 : yrange2, chartBounds);
                                            g.DrawEllipse(p, new RectangleF(x - 1, y - 1, 2, 2));
                                        }
                                        catch (OverflowException) { }
                                    }
                                }
                            }
                        }

                        if(HighlightedPoint != null)
						{
                            using(Pen p = new Pen(HighlightedPoint.PlotData.DataColor ?? Color.Black, HighlightedPoint.PlotData.Width))
							{
                                float x = transX(HighlightedPoint.X, xrange, chartBounds);
                                float y = transY(HighlightedPoint.Y, HighlightedPoint.PlotData.PlotIndex == 0 ? yrange1 : yrange2, chartBounds);
                                g.DrawEllipse(p, new RectangleF(x - 4, y - 4, 8, 8));
                            }
						}
                    }
                    g.ResetClip();
                }
            }

            LastRenderInfo = new XYPlotRenderInfo(xrange, yrange1, yrange2, chartBounds * Scaling);
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

        (ChartRange xrange, ChartRange yrange1, ChartRange yrange2) getDataRange()
        {
            ChartRange xrange = new ChartRange(double.MaxValue, double.MinValue);
            ChartRange yrange1 = new ChartRange(double.MaxValue, double.MinValue);
            ChartRange yrange2 = new ChartRange(double.MaxValue, double.MinValue);
            lock (Data)
            {
                foreach(var data in Data)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data.DataX[i] < xrange.Min) xrange.Min = data.DataX[i];
                        if (data.DataX[i] > xrange.Max) xrange.Max = data.DataX[i];
                        if (data.PlotIndex == 0)
                        {
                            if (data.DataY[i] < yrange1.Min) yrange1.Min = data.DataY[i];
                            if (data.DataY[i] > yrange1.Max) yrange1.Max = data.DataY[i];
                        }
                        else if(data.PlotIndex == 1)
                        {
                            if (data.DataY[i] < yrange2.Min) yrange2.Min = data.DataY[i];
                            if (data.DataY[i] > yrange2.Max) yrange2.Max = data.DataY[i];
                        }
                    }
                }
            }
            double overrangeY1 = yrange1.Range * .1;
            yrange1.Min -= overrangeY1;
            yrange1.Max += overrangeY1;
            if (yrange1.Min == yrange1.Max) yrange1.Max += 1;
            double overrangeY2 = yrange2.Range * .1;
            yrange2.Min -= overrangeY2;
            yrange2.Max += overrangeY2;
            if (yrange2.Min == yrange2.Max) yrange2.Max += 1;
            return (xrange, yrange1, yrange2);
        }

        double getStepWidth(ChartRange range)
        {
            if (range == null || !range.Range.IsRegular())
                return double.NaN;
            return Util.GetStepwidth(range.Range);
            //double dimension = Math.Ceiling(Math.Log10(range.Range / 2.1));
            //return Math.Pow(10, dimension - 1);
        }

        public void TranslateRangeByPixel(double shiftX, double shiftY)
        {
            if (LastRenderInfo == null)
                return;
            double rShiftX = shiftX / (double)LastRenderInfo.ChartBounds.Width * LastRenderInfo.RangeX.Range;
            double rShiftY1 = shiftY / (double)LastRenderInfo.ChartBounds.Height * LastRenderInfo.RangeY1.Range;
            double rShiftY2 = shiftY / (double)LastRenderInfo.ChartBounds.Height * LastRenderInfo.RangeY2.Range;
            if (RangeX == null || !RangeX.IsFixed) RangeX = LastRenderInfo.RangeX;
            if (RangeY1 == null || !RangeY1.IsFixed) RangeY1 = LastRenderInfo.RangeY1;
            if (RangeY2 == null || !RangeY2.IsFixed) RangeY2 = LastRenderInfo.RangeY2;
            RangeX.Min -= rShiftX;
            RangeX.Max -= rShiftX;
            RangeY1.Min += rShiftY1;
            RangeY1.Max += rShiftY1;
            RangeY2.Min += rShiftY2;
            RangeY2.Max += rShiftY2;
        }

        public void ClearPlots()
        {
            lock (Data)
            {
                Data.Clear();
                colorIndex = 0;
            }
        }

        float transX(double x, ChartRange xrange, Rect chartBounds)
        {
            return (float)((x - xrange.Min) * chartBounds.Width / xrange.Range + chartBounds.Left);
        }

        float transY(double y, ChartRange yrange, Rect chartBounds)
        {
            return (float)((-y + yrange.Min) * chartBounds.Height / yrange.Range + chartBounds.Bottom);
        }

        /*public XYPlotPointInfo GetPointInfo(double x, double y)
        {
            if (LastRenderInfo == null)
                return null;
            if (!LastRenderInfo.ChartBounds.Contains(x, y))
                return null;
            List<XYPlotData> plots = new List<XYPlotData>();
            double vx = (x - LastRenderInfo.ChartBounds.Left) / LastRenderInfo.ChartBounds.Width * LastRenderInfo.RangeX.Range + LastRenderInfo.RangeX.Min;
            double vy = -((y - LastRenderInfo.ChartBounds.Top) / LastRenderInfo.ChartBounds.Height * LastRenderInfo.RangeY1.Range + LastRenderInfo.RangeY1.Min);
            lock(Data)
            {
                foreach(XYPlotData data in Data)
                {
                    for(int i = 1; i < data.Length; i++)
                    {
                        double x1 = transX(data.DataX[i - 1], LastRenderInfo.RangeX, LastRenderInfo.ChartBounds);
                        double x2 = transX(data.DataX[i], LastRenderInfo.RangeX, LastRenderInfo.ChartBounds);
                        double y1 = transY(data.DataY[i - 1], LastRenderInfo.RangeY1, LastRenderInfo.ChartBounds);
                        double y2 = transY(data.DataY[i], LastRenderInfo.RangeY1, LastRenderInfo.ChartBounds);

                    }
                }
            }

            return new XYPlotPointInfo
            {
                X = vx,
                Y = vy,
                Graphs = plots
            };
        }*/

        public void Zoom(double x, double y)
        {
            if (x <= 0 || y <= 0) throw new ArgumentException("x and y must e greater than zero");
            if (LastRenderInfo == null)
                return;
            double midX = (LastRenderInfo.RangeX.Max + LastRenderInfo.RangeX.Min) / 2;
            double midY = (LastRenderInfo.RangeY1.Max + LastRenderInfo.RangeY1.Min) / 2;
            double minX = LastRenderInfo.RangeX.Min - midX;
            double maxX = LastRenderInfo.RangeX.Max - midX;
            double minY = LastRenderInfo.RangeY1.Min - midY;
            double maxY = LastRenderInfo.RangeY1.Max - midY;

            minX *= x;
            maxX *= x;
            minY *= y;
            maxY *= y;

            RangeX = new ChartRange(minX + midX, maxX + midX);
            RangeY1 = new ChartRange(minY + midY, maxY + midY);
        }

        public bool HasSecondPlot()
        {
            lock (Data)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    if (Data[i].PlotIndex == 1)
                        return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            ClearPlots();
            RangeX = null;
            RangeY1 = null;
            RangeY2 = null;
            HorizontalBackground = null;
            Title = "Chart";
            LabelX = "X Axis";
            LabelY1 = "Y Axis";
            LabelY2 = "";

        }

        public XYPointInfo GetClosestPoint(PointF position)
		{
            XYPlotData closestPlot = null;
            int closestPoint = 0;
            double closestDistance = double.NaN;
            PointF closestScreenPoint = default;

            for(int i = 0; i < Data.Count; i++)
			{
                for(int j = 0; j < Data[i].Length; j++)
				{
                    double x = Data[i].DataX[j];
                    double y = Data[i].DataY[j];
                    if (double.IsNaN(x) || double.IsNaN(y))
                        continue;
                    float sX = transX(x, LastRenderInfo.RangeX, LastRenderInfo.ChartBounds);
                    float sY = transY(y, (Data[i].PlotIndex == 0) ? LastRenderInfo.RangeY1 : LastRenderInfo.RangeY2, LastRenderInfo.ChartBounds);

                    float dx = (sX - position.X);
                    float dy = (sY - position.Y);
                    double dist = Math.Sqrt(dx * dx + dy * dy);

                    if(closestPlot == null || dist < closestDistance)
					{
                        closestPlot = Data[i];
                        closestPoint = j;
                        closestDistance = dist;
                        closestScreenPoint = new PointF(sX, sY);
					}
				}
			}

            if (closestPlot == null)
                return null;
            return new XYPointInfo(closestPlot, closestPoint, closestScreenPoint, (float)closestDistance);
		}
    }
}
