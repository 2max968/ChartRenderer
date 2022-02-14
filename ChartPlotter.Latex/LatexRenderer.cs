using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMath;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;

namespace ChartPlotter.Latex
{
    public class LatexRenderer
    {
        public static Bitmap RenderMath(string text)
        {
            try
            {
                var parser = new TexFormulaParser();
                var formula = parser.Parse(text);
                var renderer = formula.GetRenderer(TexStyle.Display, 20.0, "Arial");
                var bitmapSource = renderer.RenderToBitmap(0, 0);

                Bitmap bmp = new Bitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight);
                var bData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                bitmapSource.CopyPixels(new Int32Rect(0, 0, bmp.Width, bmp.Height), bData.Scan0, bData.Stride * bData.Height, bData.Stride);
                bmp.UnlockBits(bData);
                return bmp;
            }
            catch(Exception ex)
            {
                Font ft = new Font("Courier New", 12);
                using (Graphics probe = Graphics.FromHwnd(IntPtr.Zero))
                {
                    var size = probe.MeasureString(ex.Message, ft);
                    Bitmap bmp = new Bitmap((int)size.Width + 1, (int)size.Height + 1);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.Transparent);
                        g.DrawString(ex.Message, ft, Brushes.Black, 0, 0);
                    }
                    return bmp;
                }
            }
        }
    }
}
