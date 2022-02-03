using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace TestApp.D2D
{
    public partial class Form1 : Form
    {
        Factory factory;
        WindowRenderTarget rt;
        Stopwatch stp = new Stopwatch();
        long lastFrame = 0;

        public Form1()
        {
            InitializeComponent();

            factory = new Factory(FactoryType.MultiThreaded);
            rt = new WindowRenderTarget(factory, new RenderTargetProperties()
            {
               Type = RenderTargetType.Default,
               PixelFormat = new PixelFormat(SharpDX.DXGI.Format.R8G8B8A8_UNorm, AlphaMode.Unknown)
            }, new HwndRenderTargetProperties()
            {
                Hwnd = this.Handle,
                PixelSize = new Size2(this.ClientSize.Width, this.ClientSize.Height),
                PresentOptions = PresentOptions.RetainContents
            });

            rt.AntialiasMode = AntialiasMode.PerPrimitive;

            this.Disposed += Form1_Disposed;
            this.Resize += Form1_Resize;
            stp.Start();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            rt.Resize(new Size2(ClientSize.Width, ClientSize.Height));
        }

        private void Form1_Disposed(object sender, EventArgs e)
        {
            rt.Dispose();
            factory.Dispose();
        }

        private void tmGraph_Tick(object sender, EventArgs e)
        {
            float[] x = new float[rt.PixelSize.Width / 1];
            float[] y = new float[x.Length];
            for(int i = 0; i < x.Length; i++)
            {
                y[i] = (float)(Math.Sin(2 * Math.PI / y.Length * i) * Math.Sin(stp.ElapsedMilliseconds / 1000f) + 1) * rt.PixelSize.Height * .5f;
                x[i] = (float)i * rt.PixelSize.Width / x.Length;
            }

            rt.BeginDraw();
            rt.Clear(new RawColor4(1, 1, 1, 1));

            using (var brush = new SolidColorBrush(rt, new RawColor4(0, 1, 1, 1)))
            {
                for (int i = 1; i < x.Length; i++)
                {
                    rt.DrawLine(new RawVector2(x[i - 1], y[i - 1]), new RawVector2(x[i], y[i]), brush);
                }
            }

            rt.EndDraw();

            long ct = stp.ElapsedMilliseconds;
            long frameTime = ct - lastFrame;
            lastFrame = ct;
            this.Text = $"Resolution: {rt.PixelSize}; Number of Lines: {x.Length - 1}; FPS: {1000/ frameTime}";
        }
    }
}
