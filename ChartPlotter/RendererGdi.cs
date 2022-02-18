using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ChartPlotter
{
    public class RendererGdi : Renderer
    {
        Graphics g;
        bool externalGraphics;

        public RendererGdi(Graphics target)
        {
            g = target;
            externalGraphics = true;
        }

        public RendererGdi(Bitmap target)
        {
            g = Graphics.FromImage(target);
            externalGraphics = false;
        }

        public override CBrush CreateBrush(CColor color)
        {
            return new BrushGdi(new SolidBrush(color));
        }

        public override CBrush CreateBrush(float x1, float y1, float x2, float y2, CColor c1, CColor c2)
        {
            LinearGradientBrush b = new LinearGradientBrush(new PointF(x1, y1), new PointF(x2, y2), c1, c2);
            return new BrushGdi(b);
        }

        public override void Dispose()
        {
            if(!externalGraphics)
                g?.Dispose();
        }

        public override bool Smoothing { 
            get => base.Smoothing; 
            set
            {
                base.Smoothing = value;
                g.SmoothingMode = value ? SmoothingMode.HighQuality : SmoothingMode.None;
                g.TextRenderingHint = value ? System.Drawing.Text.TextRenderingHint.AntiAliasGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            }
        }

        public override void ScaleTransform(float scaleX, float scaleY)
        {
            g.ScaleTransform(scaleX, scaleY);
        }

        public override void Clear(CColor color)
        {
            g.Clear(color);
        }

        public override void DrawRectangle(CPen pen, Rect rect)
        {
            g.DrawRectangle((pen as PenGdi).pen, (Rectangle)rect);
        }

        public override void FillRectangle(CBrush brush, RectF rect)
        {
            g.FillRectangle((brush as BrushGdi).brush, (RectangleF)rect);
        }

        public override CFont CreateFont(string fontFamily, float size, bool bold = false, bool italic = false, bool underline = false, bool strikeout = false)
        {
            return new FontGdi(fontFamily, size, bold, italic, underline, strikeout);
        }

        public override void DrawString(string text, CFont font, CBrush brush, RectF bounds, CTextAlignment horizontalAlignment = CTextAlignment.Near, CTextAlignment verticalAlignment = CTextAlignment.Near)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = FontGdi.GetAligment(horizontalAlignment);
                sf.LineAlignment = FontGdi.GetAligment(verticalAlignment);
                g.DrawString(text, (font as FontGdi).font, (brush as BrushGdi).brush, (RectangleF)bounds, sf);
            }
        }

        public override void DrawString(string text, CFont font, CBrush brush, float x, float y, CTextAlignment horizontalAlignment = CTextAlignment.Near, CTextAlignment verticalAlignment = CTextAlignment.Near)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = FontGdi.GetAligment(horizontalAlignment);
                sf.LineAlignment = FontGdi.GetAligment(verticalAlignment);
                g.DrawString(text, (font as FontGdi).font, (brush as BrushGdi).brush, x, y, sf);
            }
        }

        public override void DrawImage(CBitmap bmp, float x, float y)
        {
            g.DrawImage((bmp as BitmapGdi).bmp, x, y);
        }

        public override void RotateTransform(int angle)
        {
            g.RotateTransform(angle);
        }

        public override void TranslateTransform(float x, float y)
        {
            g.TranslateTransform(x, y);
        }

        public override void ResetTransform()
        {
            g.ResetTransform();
        }

        public override void Clip(RectF clippingRect)
        {
            Region r = new Region((RectangleF)clippingRect);
            g.Clip = r;
        }

        public override void ResetClip()
        {
            g.ResetClip();
        }

        public override (float width, float height) MeasureString(string text, CFont font)
        {
            var size = g.MeasureString(text, (font as FontGdi).font);
            return (size.Width, size.Height);
        }

        public override CPen CreatePen(CColor color, float width)
        {
            return new PenGdi() { pen = new Pen(color, width) };
        }

        public override void DrawLine(CPen pen, float x1, float y1, float x2, float y2)
        {
            g.DrawLine((pen as PenGdi).pen, x1, y1, x2, y2);
        }

        public override void DrawLines(CPen pen, IEnumerable<(float x, float y)> points)
        {
            PointF[] pts = new PointF[points.Count()];
            int i = 0;
            foreach(var point in points)
            {
                if (i >= pts.Length) break;
                pts[i++] = new PointF(point.x, point.y);
            }
            g.DrawLines((pen as PenGdi).pen, pts);
        }

        public override void DrawEllipse(CPen pen, RectF bounds)
        {
            g.DrawEllipse((pen as PenGdi).pen, (RectangleF)bounds);
        }

        public override CBitmap CreateImage(Bitmap source)
        {
            if (source == null)
                return null;
            return new BitmapGdi(source);
        }
    }

    public class BrushGdi : CBrush
    {
        public Brush brush;

        public override void Dispose()
        {
            brush?.Dispose();
        }

        public BrushGdi(Brush b)
        {
            brush = b;
        }
    }

    public class FontGdi : CFont
    {
        public Font font;

        public FontGdi(string family, float size, bool bold, bool italic, bool underline, bool strikeout)
        {
            FontStyle style = FontStyle.Regular;
            if(bold) style |= FontStyle.Bold;
            if(italic) style |= FontStyle.Italic;
            if(underline) style |= FontStyle.Underline;
            if(strikeout) style |= FontStyle.Strikeout;
            font = new Font(family, size, style);
        }

        public override void Dispose()
        {
            font?.Dispose();
        }

        public static StringAlignment GetAligment(CTextAlignment alignment)
        {
            switch (alignment)
            {
                case CTextAlignment.Near:
                    return StringAlignment.Near;
                case CTextAlignment.Center:
                    return StringAlignment.Center;
                case CTextAlignment.Far:
                    return StringAlignment.Far;
            }
            return StringAlignment.Center;
        }
    }

    public class BitmapGdi : CBitmap
    {
        public Bitmap bmp;
        bool externalBitmap;

        public override void Dispose()
        {
            if(!externalBitmap)
                bmp?.Dispose();
        }

        public BitmapGdi(Bitmap source)
        {
            bmp = source;
            externalBitmap = true;
        }

        public override float Height => bmp.Height;
        public override float Width => bmp.Width;
    }

    public class PenGdi : CPen
    {
        public Pen pen;

        public override void Dispose()
        {
            if(pen.EndCap == LineCap.Custom)
                pen?.CustomEndCap?.Dispose();
            if(pen.StartCap == LineCap.Custom)
                pen?.CustomStartCap?.Dispose();
            pen?.Dispose();
        }

        public override void SetDash(bool dash)
        {
            if (dash)
                pen.DashStyle = DashStyle.Dash;
            else
                pen.DashStyle = DashStyle.Solid;
        }

        public override void SetArrow(bool arrow)
        {
            if(arrow)
            {
                AdjustableArrowCap cap = new AdjustableArrowCap(5, 5, true);
                pen.StartCap = LineCap.Round;
                pen.CustomEndCap = cap;
            }
            else
            {
                pen.StartCap = LineCap.Flat;
                pen.CustomEndCap?.Dispose();
                pen.CustomEndCap = null;
                pen.EndCap = LineCap.Flat;
            }
        }

        public override void SetWidth(float width)
        {
            pen.Width = width;
        }
    }
}
