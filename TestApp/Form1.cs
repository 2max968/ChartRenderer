using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChartPlotter;

namespace TestApp
{
    public partial class Form1 : Form
    {
        XYPlotRenderer renderer;
        bool rendering = false;
        string csvtext = "";

        public Form1()
        {
            InitializeComponent();

            /*double[] plotx = new double[1000];
            double[] ploty = new double[1000];
            for(int i = 0; i < 1000; i++)
            {
                plotx[i] = i / 10.0;
                ploty[i] = Math.Sin(plotx[i]);
            }*/

            renderer = new XYPlotRenderer();
            //renderer.Data.Add(new XYPlotData(plotx, ploty));
            /*renderer.AddChartData(XYPlotData.CreateData(0, 0.01, 2 * Math.PI, (x) =>
              {
                  Console.WriteLine($"{x}: {Math.Sin(x)}");
                  return Math.Sin(x);
              })).SetTitle("Sinus");
            renderer.AddChartData(XYPlotData.CreateData(0, 0.01, 2 * Math.PI, (x) =>
            {
                return Math.Cos(x);
            })).SetTitle("Cosinus");
            renderer.AddChartData(XYPlotData.CreateData(-2, 0.01, 2, (x) =>
              {
                  return x * x;
              })).SetTitle("Parabel");*/
            renderer.AddPlot(XYPlotData.CreateData(0, 0.1, 2 * Math.PI, (t) =>
                {
                    return (Math.Cos(t), Math.Sin(t));
                }));

           pictureBox1.Image = renderer.RenderChart(pictureBox1.Width, pictureBox1.Height);

            propertyGrid1.SelectedObject = renderer;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            render();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            render();
        }

        Point? mousePos = null;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (renderer.LastRenderInfo != null && renderer.LastRenderInfo.ChartBounds.Contains(e.X, e.Y))
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                {
                    mousePos = new Point(e.X, e.Y);
                }
            }
            else
            {
                mousePos = null;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(mousePos != null && e.Button == MouseButtons.Left)
            {
                int dx = e.X - mousePos.Value.X;
                int dy = e.Y - mousePos.Value.Y;
                mousePos = new Point(e.X, e.Y);
                renderer.TranslateRangeByPixel(dx, dy);
                render();
            }
            if(mousePos != null && e.Button == MouseButtons.Right)
            {
                int dx = e.X - mousePos.Value.X;
                int dy = e.Y - mousePos.Value.Y;
                mousePos = new Point(e.X, e.Y);
                renderer.Zoom(Math.Pow(1.1, -dx / 2.0), Math.Pow(1.1, dy / 2.0));
                render();
            }

            var info = renderer.GetPointInfo(e.X, e.Y);
            if(info != null)
            {
                lblPointerInfo.Text = $"X: {info.X.RoundToSignificantDigits(4)}; Y: {info.Y.RoundToSignificantDigits(4)}";
            }
        }

        async void render()
        {
            if (rendering) return;
            rendering = true;
            int pw = pictureBox1.Width;
            int ph = pictureBox1.Height;
            Task<Bitmap> t = new Task<Bitmap>(() =>
            {
                Bitmap bmp = renderer.RenderChart(pw, ph);
                return bmp;
            });
            t.Start();
            Bitmap tmp = await t;
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = tmp;
            rendering = false;
            lblObjectCount.Text = $"GDI Objects: {ObjectCountWatcher.GetGuiResourcesGDICount()}, USER Objects: {ObjectCountWatcher.GetGuiResourcesUserCount()}";
        }

        private void linesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderer.ClearPlots();
            renderer.RangeX = renderer.RangeY1 = null;
            for(int i = 0; i < 10; i++)
            {
                renderer.AddPlot(XYPlotData.CreateData(0, 0.01, 10, (x) =>
                {
                    return x * i;
                })).SetTitle($"y=x*{i}");
            }
            render();
        }

        private void trigonometryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderer.ClearPlots();
            renderer.RangeX = renderer.RangeY1 = null;
            renderer.AddPlot(XYPlotData.CreateData(0, 0.01, 2 * Math.PI, (x) =>
              {
                  return Math.Sin(x);
              })).SetTitle("Sinus");
            renderer.AddPlot(XYPlotData.CreateData(0, 0.01, 2 * Math.PI, (x) =>
            {
                return Math.Cos(x);
            })).SetTitle("Cosinus").SetStyle('-');
            renderer.AddPlot(XYPlotData.CreateData(0, Math.PI / 4, 2 * Math.PI, (x) =>
            {
                return Math.Sin(x);
            })).SetTitle("Sinus Dots").SetStyle('x');
            render();
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderer.ClearPlots();
            renderer.RangeX = renderer.RangeY1 = null;
            renderer.AddPlot(XYPlotData.CreateData(0, 0.02, 2 * Math.PI, (t) =>
            {
                return (Math.Cos(t), Math.Sin(t));
            }));
            render();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sfdImage.ShowDialog() == DialogResult.OK)
            {
                using (Bitmap bmp = renderer.RenderChart(pictureBox1.Width, pictureBox1.Height))
                {
                    bmp.Save(sfdImage.FileName);
                }
            }
        }

        private void dotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderer.ClearPlots();
            renderer.RangeX = renderer.RangeY1 = null;
            renderer.AddPlot(XYPlotData.CreateData(0, 1, 5, (x) =>
            {
                return 1 - x;
            })).SetStyle('x');
            renderer.AddPlot(XYPlotData.CreateData(0, 1, 5, (x) =>
            {
                return x - 1;
            })).SetStyle('o');
            render();
        }

        private void loadFromCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
