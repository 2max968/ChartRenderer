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
            Bitmap bmp = new Bitmap(width, height);
            using (RendererGdi r = new RendererGdi(bmp))
                RenderChart(r, width, height);
            return bmp;
        }

        public void RenderChart(Renderer renderer, int width, int height)
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

            var _font = renderer.CreateFont(Font.FontFamily.Name, Font.Size, Font.Bold, Font.Italic, Font.Underline, Font.Strikeout);
            var _titleFont = renderer.CreateFont(TitleFont.FontFamily.Name, TitleFont.Size, TitleFont.Bold, TitleFont.Italic, TitleFont.Underline, TitleFont.Strikeout);
            var _legendFont = renderer.CreateFont(LegendFont.FontFamily.Name, LegendFont.Size, LegendFont.Bold, LegendFont.Italic, LegendFont.Underline, LegendFont.Strikeout);
            var _bmpTitle = renderer.CreateImage(bmpTitle);
            var _bmpLabelX = renderer.CreateImage(bmpLabelX);
            var _bmpLabelY1 = renderer.CreateImage(bmpLabelY1);
            var _bmpLabelY2 = renderer.CreateImage(bmpLabelY2);

            renderer.Smoothing = Smoothing;
            renderer.ScaleTransform(Scaling, Scaling);

            (_, var xtextheight) = renderer.MeasureString(LabelX, _font);
            (_, var ytextheight) = renderer.MeasureString(LabelY1, _font);
            (_, var titleheight) = renderer.MeasureString(Title, _titleFont);
            if (bmpLabelY1 != null)
                chartBounds.Left += bmpLabelY1.Height;
            else if (LabelY1 != "")
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
            if (secondPlot)
            {
                chartBounds.Right -= ScaleNumberSize;
            }
            if (LabelY2 != "")
                chartBounds.Right -= (int)ytextheight;

            renderer.Clear(BackgroundColor);

            using (CBrush textBrush = renderer.CreateBrush(ForegroundColor))
            {
                if (ShowLegend)
                {
                    int legendTextWidth = 0;
                    int legendTextHeight = 0;
                    lock (Data)
                    {
                        List<XYPlotData> legendData = new List<XYPlotData>();
                        foreach (var data in Data)
                        {
                            if (!data.LegendVisible) continue;
                            legendData.Add(data);
                            var size = renderer.MeasureString(data.DataTitle, _legendFont);
                            if (size.width > legendTextWidth) legendTextWidth = (int)Math.Ceiling(size.width);
                            if (size.height > legendTextHeight) legendTextHeight = (int)Math.Ceiling(size.height);
                        }
                        if (legendData.Count > 0)
                        {
                            int legendWidth = legendTextWidth + LegendSpacing * 3 + legendTextHeight;
                            int legendheight = legendTextHeight * legendData.Count + LegendSpacing * 3;
                            chartBounds.Right -= legendWidth + LegendSpacing;
                            legendPos -= legendWidth + LegendSpacing;
                            using (CPen legendPen = renderer.CreatePen(ForegroundColor, 1))
                            {
                                //g.DrawRectangle(legendPen, new Rectangle((int)chartBounds.Right + LegendSpacing, (int)chartBounds.Top, legendWidth, legendheight));
                                for (int i = 0; i < legendData.Count; i++)
                                {
                                    int x = (int)legendPos + 2 * LegendSpacing;
                                    int y = (int)chartBounds.Top + LegendSpacing + i * (LegendSpacing + legendTextHeight);
                                    using (CBrush dataBrush = renderer.CreateBrush(legendData[i].DataColor ?? Color.Black))
                                        renderer.FillRectangle(dataBrush, Rect.FromXYWH(x, y, legendTextHeight, legendTextHeight));
                                    renderer.DrawRectangle(legendPen, Rect.FromXYWH(x, y, legendTextHeight, legendTextHeight));
                                    // TODO: überprüfen
                                    /*using (StringFormat sf = new StringFormat())
                                    {
                                        sf.LineAlignment = StringAlignment.Center;
                                        g.DrawString(legendData[i].DataTitle, LegendFont, textBrush, new Point(x + LegendSpacing + legendTextHeight, y));
                                    }*/
                                    renderer.DrawString(legendData[i].DataTitle, _legendFont, textBrush, Rect.FromXYWH(x + LegendSpacing + legendTextHeight, y, 300, 300));
                                }
                            }
                        }
                    }
                }

                if (bmpLabelX != null)
                    renderer.DrawImage(_bmpLabelX, chartBounds.Left + (chartBounds.Width - bmpLabelX.Width) * .5f, rHeight - bmpLabelX.Height);
                else if (LabelX != "")
                    renderer.DrawString(LabelX, _font, textBrush, RectF.FromXYHW(chartBounds.Left, rHeight - xtextheight, chartBounds.Width, xtextheight), CTextAlignment.Center, CTextAlignment.Center);
                renderer.RotateTransform(-90);
                renderer.TranslateTransform(-rHeight, 0);
                if (bmpLabelY1 != null)
                    renderer.DrawImage(_bmpLabelY1, rHeight - chartBounds.Bottom + (chartBounds.Height - bmpLabelY1.Width) * .5f, 0);
                else if (LabelY1 != "")
                    renderer.DrawString(LabelY1, _font, textBrush, RectF.FromXYHW(rHeight - chartBounds.Bottom, 0, chartBounds.Height, ytextheight), CTextAlignment.Center, CTextAlignment.Center);
                if (bmpLabelY2 != null)
                    renderer.DrawImage(_bmpLabelY2, rHeight - chartBounds.Bottom + (chartBounds.Height - bmpLabelY2.Width) * .5f, chartBounds.Right + bmpLabelY2.Height);
                else if (LabelY2 != "")
                    renderer.DrawString(LabelY2, _font, textBrush, RectF.FromXYHW(rHeight - chartBounds.Bottom, chartBounds.Right + (secondPlot ? ytextheight : 0), chartBounds.Height, ytextheight), CTextAlignment.Center, CTextAlignment.Center);
                renderer.ResetTransform();
                renderer.ScaleTransform(Scaling, Scaling);
                if (bmpTitle != null)
                    renderer.DrawImage(_bmpTitle, (rWidth - bmpTitle.Width) * .5f, 0);
                else if (Title != "")
                    renderer.DrawString(Title, _titleFont, textBrush, RectF.FromXYHW(0, 0, rWidth, chartBounds.Top), CTextAlignment.Center, CTextAlignment.Center);


                if (HorizontalBackground != null)
                {
                    renderer.Clip(chartBounds);
                    RenderHelper.DrawHorizontalColorBand(renderer, HorizontalBackground, chartBounds, xrange);
                    renderer.ResetClip();
                }

                using (CPen borderPen = renderer.CreatePen(ForegroundColor, 1))
                {
                    borderPen.SetDash(true);
                    renderer.DrawRectangle(borderPen, chartBounds);
                }

                if (ShowAxes)
                {
                    using (CPen axisPen = renderer.CreatePen(ForegroundColor, 2))
                    {
                        axisPen.SetArrow(true);
                        if (xrange.Min < 0 && xrange.Max > 0)
                        {
                            int axisX = (int)(-xrange.Min * chartBounds.Width / xrange.Range + chartBounds.Left);
                            renderer.DrawLine(axisPen, axisX, chartBounds.Bottom, axisX, chartBounds.Top);
                        }
                        if (yrange1.Min < 0 && yrange1.Max > 0)
                        {
                            int axisY = (int)(chartBounds.Height - (-yrange1.Min) * chartBounds.Height / yrange1.Range + chartBounds.Top);
                            renderer.DrawLine(axisPen, chartBounds.Left, axisY, chartBounds.Right, axisY);
                        }
                    }
                }


                using (CPen gridPen = renderer.CreatePen(ForegroundColor, 1))
                {
                    using (CPen scalePen = renderer.CreatePen(ForegroundColor, 1))
                    {
                        using (StringFormat sf = new StringFormat())
                        {
                            if (Data.Count > 0)
                            {
                                sf.Alignment = StringAlignment.Center;
                                sf.LineAlignment = StringAlignment.Near;
                                gridPen.SetDash(true);
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
                                        renderer.DrawLine(gridPen, (float)rx, chartBounds.Top, (float)rx, chartBounds.Bottom);
                                    renderer.DrawLine(scalePen, (float)rx, chartBounds.Bottom - ScaleLinesLength, (float)rx, chartBounds.Bottom);

                                    Rect rect = Rect.FromXYWH((int)(rx - 500), (int)chartBounds.Bottom, 1000, ScaleNumberSize);
                                    double lx = Math.Round(x * 100 / Math.Pow(10, xExp)) / 100;
                                    string label = "" + lx;
                                    renderer.DrawString(label, _font, textBrush, rect, CTextAlignment.Center, CTextAlignment.Center);
                                }
                                if (xExp != 0)
                                {
                                    string text = "x10" + Util.GetHighNumber(xExp);
                                    var textS = renderer.MeasureString(text, _font);
                                    float x = Math.Min(chartBounds.Right, rWidth - textS.width);
                                    float y = chartBounds.Bottom;
                                    using (CBrush b = renderer.CreateBrush(BackgroundColor))
                                        renderer.FillRectangle(b, RectF.FromXYHW(x, y + 1, textS.width, textS.height - 1));
                                    renderer.DrawString(text, _font, textBrush, x, y);
                                }

                                using (StringFormat sfYScale = new StringFormat())
                                {
                                    sfYScale.Alignment = StringAlignment.Far;
                                    for (double y = ystart1; y <= yend1; y += yscalestep1)
                                    {
                                        double ry = chartBounds.Height - (y - yrange1.Min) * chartBounds.Height / yrange1.Range + chartBounds.Top;
                                        if (GridLines == GridLineStyle.FullGrid || (GridLines == GridLineStyle.ZeroOnly && y == 0))
                                            renderer.DrawLine(gridPen, chartBounds.Left, (float)ry, chartBounds.Right, (float)ry);
                                        renderer.DrawLine(scalePen, chartBounds.Left, (float)ry, chartBounds.Left + ScaleLinesLength, (float)ry);

                                        Rect rect = Rect.FromXYWH(chartBounds.Left - 100, (int)ry - ScaleNumberSize, 100, ScaleNumberSize);
                                        double ly = Math.Round(y * 100 / Math.Pow(10, y1Exp)) / 100;
                                        string label = "" + ly;
                                        renderer.DrawString(label, _font, textBrush, rect, CTextAlignment.Far);
                                    }
                                    if (y1Exp != 0)
                                    {
                                        string text = "x10" + Util.GetHighNumber(y1Exp);
                                        var textS = renderer.MeasureString(text, _font);
                                        float x = chartBounds.Left;
                                        float y = chartBounds.Top - ScaleNumberSize - LegendSpacing;
                                        using (CBrush b = renderer.CreateBrush(BackgroundColor))
                                            renderer.FillRectangle(b, RectF.FromXYHW(x - textS.width, y + 1, textS.width, textS.height - 1));
                                        renderer.DrawString(text, _font, textBrush, x, y, CTextAlignment.Far);
                                    }
                                    sfYScale.Alignment = StringAlignment.Near;
                                    if (secondPlot)
                                    {
                                        for (double y = ystart2; y <= yend2; y += yscalestep2)
                                        {
                                            double ry = chartBounds.Height - (y - yrange2.Min) * chartBounds.Height / yrange2.Range + chartBounds.Top;
                                            renderer.DrawLine(scalePen, chartBounds.Right, (float)ry, chartBounds.Right - ScaleLinesLength, (float)ry);

                                            Rect rect = Rect.FromXYWH((int)chartBounds.Right, (int)ry - ScaleNumberSize, 100, ScaleNumberSize);
                                            double ly = Math.Round(y * 100 / Math.Pow(10, y2Exp)) / 100;
                                            string label = "" + ly;
                                            renderer.DrawString(label, _font, textBrush, rect, CTextAlignment.Near);
                                        }
                                        if (y2Exp != 0)
                                        {
                                            string text = "x10" + Util.GetHighNumber(y2Exp);
                                            var textS = renderer.MeasureString(text, _font);
                                            float x = Math.Min(chartBounds.Right, rWidth - textS.width);
                                            float y = chartBounds.Top - ScaleNumberSize - LegendSpacing;
                                            using (CBrush b = renderer.CreateBrush(BackgroundColor))
                                                renderer.FillRectangle(b, RectF.FromXYHW(x, y + 1, textS.width, textS.height - 1));
                                            renderer.DrawString(text, _font, textBrush, x, y, CTextAlignment.Near);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                renderer.DrawString("No data available", _font, textBrush, chartBounds, CTextAlignment.Center, CTextAlignment.Center);
                            }
                        }
                    }
                }

                renderer.Clip(chartBounds);
                lock (Data)
                {
                    foreach (var data in Data)
                    {
                        using (CPen p = renderer.CreatePen(data.DataColor ?? Color.Black, data.Width))
                        {
                            if (data.PlotStyle == XYPlotStyle.Dash || data.PlotStyle == XYPlotStyle.Line || data.PlotStyle == XYPlotStyle.LineAndDot)
                            {
                                if (data.Length <= 1)
                                    continue;
                                if (data.PlotStyle == XYPlotStyle.Dash)
                                    p.SetDash(true);
                                List<(float x, float y)> pts = null;
                                for (int i = 0; i <= data.Length; i++)
                                {
                                    if (i == data.Length || double.IsNaN(data.DataX[i]) || double.IsNaN(data.DataY[i]))
                                    {
                                        if (pts != null)
                                        {
                                            try
                                            {
                                                if (pts.Count > 1)
                                                    renderer.DrawLines(p, pts);
                                                else
                                                    renderer.DrawEllipse(p, RectF.FromXYHW(pts[0].x - .5f, pts[0].y - .5f, 1, 1));
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
                                            pts = new List<(float x, float y)>();
                                        }
                                        pts.Add(((float)x2, (float)y2));
                                    }
                                }
                            }
                            else if (data.PlotStyle == XYPlotStyle.Cross)
                            {
                                p.SetWidth(data.Width);
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
                                        renderer.DrawLine(p, x1, y1, x2, y2);
                                        renderer.DrawLine(p, x1, y2, x2, y1);
                                    }
                                    catch (OverflowException) { }
                                }
                            }
                            else if (data.PlotStyle == XYPlotStyle.Circle)
                            {
                                p.SetWidth(data.Width);
                                for (int i = 0; i < data.Length; i++)
                                {
                                    try
                                    {
                                        if (double.IsNaN(data.DataX[i]) || double.IsNaN(data.DataY[i]))
                                            continue;
                                        float crossSize = data.Width * 2;
                                        float x = transX(data.DataX[i], xrange, chartBounds);
                                        float y = transY(data.DataY[i], data.PlotIndex == 0 ? yrange1 : yrange2, chartBounds);
                                        renderer.DrawEllipse(p, RectF.FromXYHW(x - crossSize, y - crossSize, crossSize * 2, crossSize * 2));
                                    }
                                    catch (OverflowException) { }
                                }
                            }

                            if (data.PlotStyle == XYPlotStyle.Dot || data.PlotStyle == XYPlotStyle.LineAndDot)
                            {
                                p.SetWidth(data.Width);
                                for (int i = 0; i < data.Length; i++)
                                {
                                    try
                                    {
                                        if (double.IsNaN(data.DataX[i]) || double.IsNaN(data.DataY[i]))
                                            continue;
                                        float x = transX(data.DataX[i], xrange, chartBounds);
                                        float y = transY(data.DataY[i], data.PlotIndex == 0 ? yrange1 : yrange2, chartBounds);
                                        renderer.DrawEllipse(p, RectF.FromXYHW(x - 1, y - 1, 2, 2));
                                    }
                                    catch (OverflowException) { }
                                }
                            }
                        }
                    }

                    if (HighlightedPoint != null)
                    {
                        using (CPen p = renderer.CreatePen(HighlightedPoint.PlotData.DataColor ?? Color.Black, HighlightedPoint.PlotData.Width))
                        {
                            float x = transX(HighlightedPoint.X, xrange, chartBounds);
                            float y = transY(HighlightedPoint.Y, HighlightedPoint.PlotData.PlotIndex == 0 ? yrange1 : yrange2, chartBounds);
                            renderer.DrawEllipse(p, RectF.FromXYHW(x - 4, y - 4, 8, 8));
                        }
                    }
                }
                renderer.ResetClip();
            }

            _bmpLabelX?.Dispose();
            _bmpLabelY1?.Dispose();
            _bmpLabelY2?.Dispose();
            _bmpTitle?.Dispose();
            _font?.Dispose();
            _legendFont?.Dispose();
            _titleFont?.Dispose();

            LastRenderInfo = new XYPlotRenderInfo(xrange, yrange1, yrange2, chartBounds * Scaling);
        }

        /*public ChartRenderBounds CalculateBounds(int width, int height)
        {
            using (Graphics dummy = Graphics.FromHwnd(IntPtr.Zero))
            {
                ChartRenderBounds bounds = new ChartRenderBounds();

                if (bmpTitle != null)
                    bounds.TitleBounds = Rect.FromXYWH(0, 0, width, bmpTitle.Height);
                else if (title != null && title != "")
                    bounds.TitleBounds = Rect.FromXYWH(0, 0, width, (long)dummy.MeasureString(Title, TitleFont).Height);
                else
                    bounds.TitleBounds = new Rect(0, 0, width, 0);


            }
        }*/

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
            if (LastRenderInfo == null)
                return null;

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
