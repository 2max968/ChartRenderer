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
        public long Left;
        public long Top;
        public long Right;
        public long Bottom;

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

        public Rect(long left, long top, long right, long bottom)
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

        public static Rect FromXYWH(long x, long y, long width, long height)
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
    }
}
