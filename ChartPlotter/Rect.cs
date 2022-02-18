using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChartPlotter
{
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public long Width
        {
            get
            {
                return Right - Left;
            }
        }
        public long Height
        {
            get
            {
                return Bottom - Top;
            }
        }

        public Rect(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public bool Contains(double x, double y)
        {
            return x >= Left && x <= Right && y <= Bottom && y >= Top;
        }

        public static Rect FromXYWH(int x, int y, int width, int height)
        {
            return new Rect(x, y, x + width, y + height);
        }

        public static explicit operator Rectangle(Rect rect)
        {
            return new Rectangle((int)(rect.Left), (int)(rect.Top), (int)(rect.Right - rect.Left), (int)(rect.Bottom - rect.Top));
        }

        public static explicit operator Rect(Rectangle rect)
        {
            return Rect.FromXYWH(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public override string ToString()
        {
            return $"{{Left={Left}; Top={Top}; Right={Right}; Bottom={Bottom}}}";
        }

        public static Rect operator*(Rect r, double d)
        {
            return new Rect
                (
                (int)(r.Left * d),
                (int)(r.Top * d),
                (int)(r.Right * d),
                (int)(r.Bottom * d)
                );
        }

        public static Rect operator *(Rect r, long l)
        {
            return new Rect
                (
                (int)(r.Left * l),
                (int)(r.Top * l),
                (int)(r.Right * l),
                (int)(r.Bottom * l)
                );
        }
    }

    public struct RectF
    {
        public float Left;
        public float Top;
        public float Right;
        public float Bottom;

        public float Width { get=> Right - Left; }
        public float Height { get=> Bottom - Top; }

        public static implicit operator RectF(Rect rect)
        {
            return new RectF()
            {
                Left = rect.Left,
                Top = rect.Top,
                Right = rect.Right,
                Bottom = rect.Bottom,
            };
        }

        public static explicit operator RectangleF(RectF rect)
        {
            return new RectangleF((rect.Left), (rect.Top), (rect.Right - rect.Left), (rect.Bottom - rect.Top));
        }

        public static RectF FromXYHW(float x, float y, float w, float h)
        {
            return new RectF()
            {
                Left = x,
                Top = y,
                Right = x+w,
                Bottom = y+h
            };
        }
    }
}
