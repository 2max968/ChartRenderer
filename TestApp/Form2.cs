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
using ChartPlotter;
using ChartPlotter.WinForms;

namespace TestApp
{
    public partial class Form2 : Form
    {
        XYPlot xyPlot1;

        public Form2()
        {
            InitializeComponent();

            xyPlot1 = new XYPlot();
            xyPlot1.Dock = DockStyle.Fill;
            Controls.Add(xyPlot1);
            xyPlot1.Plotter.AddPlot(ChartPlotter.XYPlotData.CreateData(-2, 0.1, 2, (x) =>
            {
                return Math.Exp(x);
            })).SetTitle("e^x");
            xyPlot1.Plotter.AddPlot(ChartPlotter.XYPlotData.CreateData(-2, 0.1, 2, (x) =>
            {
                return Math.Exp(-x);
            })).SetTitle("e^-x");
            xyPlot1.BringToFront();

            propertyGrid1.SelectedObject = xyPlot1.Plotter;
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            xyPlot1.Redraw();
        }

        private void exponensialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xyPlot1.Plotter.ClearPlots();
            xyPlot1.Plotter.RangeX = xyPlot1.Plotter.RangeY1 = null;
            xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(-2, 0.1, 2, (x) =>
            {
                return Math.Exp(x);
            })).SetTitle("e^x");
            xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(-2, 0.1, 2, (x) =>
            {
                return Math.Exp(-x);
            })).SetTitle("e^-x");
            xyPlot1.Redraw();
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xyPlot1.Plotter.ClearPlots();
            xyPlot1.Plotter.RangeX = xyPlot1.Plotter.RangeY1 = null;
            xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(0, Math.PI / 100, 2 * Math.PI, (t) =>
            {
                return (Math.Cos(t), Math.Sin(t));
            })).HideFromLegend();
            xyPlot1.Redraw();
        }

        private void oscillationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double f = 1;
            double d = .5;
            xyPlot1.Plotter.ClearPlots();
            xyPlot1.Plotter.RangeX = null;
            xyPlot1.Plotter.RangeY1 = new ChartRange(-1, 1);
            xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(0, 0.01, 10, (x) =>
            {
                return Math.Sin(2 * Math.PI * f * x) * Math.Exp(-d * x);
            })).HideFromLegend();
            xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(0, .1, 10, (x) =>
            {
                return Math.Exp(-d * x);
            })).HideFromLegend().SetColor(Color.Gray).SetStyle('-').SetWidth(1);
            xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(0, .1, 10, (x) =>
            {
                return -Math.Exp(-d * x);
            })).HideFromLegend().SetColor(Color.Gray).SetStyle('-').SetWidth(1);
            xyPlot1.Redraw();
        }

        private void dualScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xyPlot1.Plotter.ClearPlots();
            xyPlot1.Plotter.RangeX = null;
            xyPlot1.Plotter.RangeY1 = null;
            xyPlot1.Plotter.RangeY2 = null;
            xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(0, 0.01, 10, (x) =>
            {
                return x;
            }));
            xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(0, 0.01, 10, (x) =>
            {
                return x * 10;
            })).SetPlotIndex(1).MakeLogarithmic();
            xyPlot1.Redraw();
        }

        private void naNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double f = .5;
            xyPlot1.Plotter.ClearPlots();
            xyPlot1.Plotter.RangeX = null;
            xyPlot1.Plotter.RangeY1 = null;
            xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(0, 0.01, 10, (x) =>
            {
                if (Math.Abs(x - (int)x) < .25) return double.NaN;
                return Math.Sin(2 * Math.PI * f * x);
            })).HideFromLegend().SetStyle(XYPlotStyle.Line);
            xyPlot1.Redraw();
        }

        private void bigNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xyPlot1.Plotter.ClearPlots();
            xyPlot1.Plotter.RangeX = null;
            xyPlot1.Plotter.RangeY1 = null;
            xyPlot1.Plotter.RangeY2 = null;
            xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(-5, 0.01, 5, (t) =>
            {
                return (t / 1000000, t * 1000000);
            })).ShowInLegend().SetTitle("1");
            xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(-5, 0.01, 5, (t) =>
            {
                return (t / 1000000, (- t) / 1000000);
            })).ShowInLegend().SetTitle("2").SetPlotIndex(1);
            xyPlot1.Redraw();
        }

        private void manyColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xyPlot1.Plotter.ClearPlots();
            xyPlot1.Plotter.RangeX = null;
            xyPlot1.Plotter.RangeY1 = null;
            xyPlot1.Plotter.RangeY2 = null;
            for (int i = 0; i < PlotColors.Colors.Length; i++)
            {
                xyPlot1.Plotter.AddPlot(XYPlotData.CreateData(-1, 0.01, 1, (x) =>
                 {
                     return Math.Pow(i + 1, x);
                 })).ShowInLegend().SetTitle("" + (i + 1) + "ˣ");
            }
            xyPlot1.Redraw();
        }

        private void emptyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xyPlot1.Plotter.ClearPlots();
            xyPlot1.Plotter.RangeX = null;
            xyPlot1.Plotter.RangeY1 = null;
            xyPlot1.Plotter.RangeY2 = null; 
            xyPlot1.Redraw();
        }

        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "CSV Table|*.csv|All Files|*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fn = dlg.FileName;
                var table = CSVReader.ReadTableFromFile(fn, ";", "de");
                var plot = table.GetXYPlotData(1, 2, 1);
                xyPlot1.Plotter.ClearPlots();
                xyPlot1.Plotter.AddPlot(plot);
                xyPlot1.Plotter.RangeX = null;
                xyPlot1.Plotter.RangeY1 = null;
                xyPlot1.Plotter.RangeY2 = null;
                xyPlot1.Redraw();
            }
        }

        private async void sineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip1.Enabled = false;
            try
            {
                xyPlot1.Plotter.ClearPlots();
                xyPlot1.Plotter.RangeX = null;
                xyPlot1.Plotter.RangeY1 = new ChartRange(-1, 1);
                xyPlot1.Plotter.RangeY2 = null;

                Stopwatch timer = new Stopwatch();
                timer.Start();
                List<double> x = new List<double>();
                List<double> y1 = new List<double>();
                List<double> y2 = new List<double>();
                List<double> y3 = new List<double>();
                while (timer.ElapsedMilliseconds < 3141 * 4)
                {
                    double t = timer.ElapsedMilliseconds * .001;
                    x.Add(t);
                    y1.Add(Math.Sin(t));
                    y2.Add(Math.Cos(t));
                    y3.Add(Math.Sin(t) * Math.Cos(t));

                    xyPlot1.Plotter.ClearPlots();
                    xyPlot1.Plotter.AddPlot(new XYPlotData(x.ToArray(), y1.ToArray()).SetTitle("Sin(t)"));
                    xyPlot1.Plotter.AddPlot(new XYPlotData(x.ToArray(), y2.ToArray()).SetTitle("Cos(t)"));
                    xyPlot1.Plotter.AddPlot(new XYPlotData(x.ToArray(), y3.ToArray()).SetTitle("Sin(t)*Cos(t)"));
                    xyPlot1.Redraw();
                    await Task.Delay(32);
                }
            }
            catch
            {

            }
            menuStrip1.Enabled = true;
        }

        private async void circleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            menuStrip1.Enabled = false;
            try
            {
                xyPlot1.Plotter.ClearPlots();
                xyPlot1.Plotter.RangeX = new ChartRange(-1.1, 1.1);
                xyPlot1.Plotter.RangeY1 = new ChartRange(-1.1, 1.1);
                xyPlot1.Plotter.RangeY2 = null;

                XYPlotData data = new XYPlotData();
                xyPlot1.Plotter.AddPlot(data);

                Stopwatch timer = new Stopwatch();
                timer.Start();
                bool stop = false;
                while (!stop)
                {
                    double t = timer.ElapsedMilliseconds * .001;
                    data.AddPoint(Math.Cos(t), Math.Sin(t));
                    xyPlot1.Redraw();
                    await Task.Delay(32);
                    if (t > Math.PI * 2) stop = true;
                }
            }
            catch
            {

            }
            menuStrip1.Enabled = true;
        }
    }
}
