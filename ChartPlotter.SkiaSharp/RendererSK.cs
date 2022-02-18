using System;
using System.Collections.Generic;
using SkiaSharp;

namespace ChartPlotter.SK
{
    public class RendererSK : Renderer
    {
        SKCanvas canvas;
        bool externalCanvas;

        SKColor _c(CColor color)
        {
            return new SKColor(color.R, color.G, color.B);
        }

        SKRect _r(RectF rect)
        {
            return new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public RendererSK(SKCanvas canvas)
        {
            this.canvas = canvas;
            externalCanvas = true;
        }

        public override void Clear(CColor color)
        {
            canvas.Clear(_c(color));
        }

        public override void Clip(RectF clippingRect)
        {
            canvas.ClipRect(_r(clippingRect)); 
        }

        public override CBrush CreateBrush(CColor color)
        {
            SKPaint paint = new SKPaint();
            paint.Color = _c(color);
            return new BrushSK() { paint = paint };
        }

        public override CBrush CreateBrush(float x1, float y1, float x2, float y2, CColor c1, CColor c2)
        {
            SKPaint paint = new SKPaint();
            paint.Shader = SKShader.CreateLinearGradient(new SKPoint(x1, y1), new SKPoint(x2, y2), new SKColor[] { _c(c1), _c(c2) }, SKShaderTileMode.Repeat);
            return new BrushSK() { paint = paint };
        }

        public override CFont CreateFont(string fontFamily, float size, bool bold = false, bool italic = false, bool underline = false, bool strikeout = false)
        {
            SKFontStyle style;
            if (bold && italic) style = SKFontStyle.BoldItalic;
            else if (bold) style = SKFontStyle.Italic;
            else if (italic) style = SKFontStyle.Italic;
            else style = SKFontStyle.Normal;
            SKTypeface typeface = SKTypeface.FromFamilyName(fontFamily, style);;
            return new FontSK() { typeface = typeface, style = style, size = size };
        }

        public override CBitmap CreateImage(System.Drawing.Bitmap source)
        {
            throw new NotImplementedException();
        }

        public override CPen CreatePen(CColor color, float width)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            if(!externalCanvas)
                canvas?.Dispose();
        }

        public override void DrawEllipse(CPen pen, RectF bounds)
        {
            throw new NotImplementedException();
        }

        public override void DrawImage(CBitmap bmp, float x, float y)
        {
            throw new NotImplementedException();
        }

        public override void DrawLine(CPen pen, float x1, float y1, float x2, float y2)
        {
            throw new NotImplementedException();
        }

        public override void DrawLines(CPen pen, IEnumerable<(float x, float y)> points)
        {
            throw new NotImplementedException();
        }

        public override void DrawRectangle(CPen pen, Rect rect)
        {
            throw new NotImplementedException();
        }

        public override void DrawString(string text, CFont font, CBrush brush, RectF bounds, CTextAlignment horizontalAlignment = CTextAlignment.Near, CTextAlignment verticalAlignment = CTextAlignment.Near)
        {
            
        }

        public override void DrawString(string text, CFont font, CBrush brush, float x, float y, CTextAlignment horizontalAlignment = CTextAlignment.Near, CTextAlignment verticalAlignment = CTextAlignment.Near)
        {
            SKPaint paint = (brush as BrushSK).paint;
            FontSK fontSK = font as FontSK;
            paint.Typeface = fontSK.typeface;
            paint.TextSize = fontSK.size;
            canvas.DrawText(text, x, y, paint);
        }

        public override void FillRectangle(CBrush brush, RectF rect)
        {
            canvas.DrawRect(_r(rect), (brush as BrushSK).paint);
        }

        public override (float width, float height) MeasureString(string text, CFont font)
        {
            using (SKPaint paint = new SKPaint())
            {
                SKRect bounds = new SKRect();
                FontSK fontSK = font as FontSK;
                paint.Typeface = fontSK.typeface;
                paint.TextSize = fontSK.size;
                paint.MeasureText(text, ref bounds);
                return (bounds.Right, bounds.Bottom);
            }
        }

        public override void ResetClip()
        {
            canvas.ClipRect(new SKRect(-10000, -10000, 10000, 10000));
        }

        public override void ResetTransform()
        {
            canvas.ResetMatrix();
        }

        public override void RotateTransform(int angle)
        {
            canvas.RotateDegrees(angle);
        }

        public override void ScaleTransform(float scaleX, float scaleY)
        {
            canvas.Scale(scaleX, scaleY);
        }

        public override void TranslateTransform(float x, float y)
        {
            canvas.Translate(x, y);
        }
    }

    public class BrushSK : CBrush
    {
        public SKPaint paint;

        public override void Dispose()
        {
            paint?.Dispose();
        }
    }

    public class FontSK : CFont
    {
        public SKTypeface typeface;
        public SKFontStyle style;
        public float size;

        public override void Dispose()
        {
            typeface?.Dispose();
            style?.Dispose();
        }
    }
}
