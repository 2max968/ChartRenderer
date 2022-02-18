using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    public abstract class Renderer : IDisposable
    {
        public virtual bool Smoothing { get; set; } = true;

        public abstract void Dispose();
        public abstract CBrush CreateBrush(CColor color);
        public abstract CBrush CreateBrush(float x1, float y1, float x2, float y2, CColor c1, CColor c2);
        public abstract void ScaleTransform(float scaleX, float scaleY);
        public abstract CFont CreateFont(string fontFamily, float size, bool bold = false, bool italic = false, bool underline = false, bool strikeout = false);
        public abstract (float width, float height) MeasureString(string text, CFont font);
        public abstract void Clear(CColor color);
        public abstract void FillRectangle(CBrush brush, RectF rect);
        public abstract void DrawRectangle(CPen pen, Rect rect);
        public abstract void DrawString(string text, CFont font, CBrush brush, RectF bounds, CTextAlignment horizontalAlignment = CTextAlignment.Near, CTextAlignment verticalAlignment = CTextAlignment.Near);
        public abstract void DrawString(string text, CFont font, CBrush brush, float x, float y, CTextAlignment horizontalAlignment = CTextAlignment.Near, CTextAlignment verticalAlignment = CTextAlignment.Near);
        public abstract void DrawImage(CBitmap bmp, float x, float y);
        public abstract void RotateTransform(int angle);
        public abstract void TranslateTransform(float x, float y);
        public abstract void ResetTransform();
        public abstract void Clip(RectF clippingRect);
        public abstract void ResetClip();
        public abstract CPen CreatePen(CColor color, float width);
        public abstract void DrawLine(CPen pen, float x1, float y1, float x2, float y2);
        public abstract void DrawLines(CPen pen, IEnumerable<(float x, float y)> points);
        public abstract void DrawEllipse(CPen pen, RectF bounds);
        public abstract CBitmap CreateImage(System.Drawing.Bitmap source);
    }

    public abstract class CBrush : IDisposable
    {
        public abstract void Dispose();
    }

    public abstract class CPen : IDisposable
    {
        public abstract void Dispose();
        public abstract void SetDash(bool dash);
        public abstract void SetArrow(bool arrow);
        public abstract void SetWidth(float width);
    }

    public abstract class CFont : IDisposable
    {
        public abstract void Dispose();
    }

    public enum CTextAlignment
    {
        Near, Center, Far
    }

    public class CColor
    {
        public byte R;
        public byte G;
        public byte B;

        public CColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static implicit operator System.Drawing.Color(CColor c)
        {
            return System.Drawing.Color.FromArgb(c.R, c.G, c.B);
        }

        public static implicit operator CColor(System.Drawing.Color c)
        {
            return new CColor(c.R, c.G, c.B);
        }
    }

    public abstract class CBitmap : IDisposable
    {
        public abstract void Dispose();
        public abstract float Width { get; }
        public abstract float Height { get; }
    }
}
