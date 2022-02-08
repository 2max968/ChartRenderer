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

        private void colorBandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xyPlot1.Plotter.HorizontalBackground == null)
            {
                ColorBand colors = new ColorBand();
                for (int i = 0; i < PlotColors.Colors.Length; i++)
                {
                    colors.AddPoint(i, PlotColors.Colors[i]);
                }

                xyPlot1.Plotter.HorizontalBackground = colors;
            }
            else
            {
                xyPlot1.Plotter.HorizontalBackground = null;
            }
            xyPlot1.Redraw();
        }

        private void iSOPrüfungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] times = new double[] { 0, 1, 6, 7 };
            xyPlot1.Plotter.Reset();
            xyPlot1.Plotter.Title = "";
            xyPlot1.Plotter.LabelX = "Zeit / s";
            xyPlot1.Plotter.LabelY1 = "Spannung / V";
            XYPlotData voltage = new XYPlotData(times, new double[] { 250, 1000, 1000, 259 });
            xyPlot1.Plotter.AddPlot(voltage).HideFromLegend().SetColor("blue");
            xyPlot1.Plotter.GridLines = GridLineStyle.FullGrid;

            xyPlot1.Plotter.HorizontalBackground = new ColorBand(times, new string[] { "#FFA", "#AFA", "#FFA", "black" });
            xyPlot1.Plotter.HorizontalBackground.Interpolate = false;

            xyPlot1.Plotter.RangeY1 = new ChartRange(0, double.NaN);

            xyPlot1.Redraw();
        }

        private void iSOPrüfungRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] times = new double[] { 0, 1, 6, 7 };
            xyPlot1.Plotter.Reset();
            xyPlot1.Plotter.Title = "";
            xyPlot1.Plotter.LabelX = "Zeit / s";
            xyPlot1.Plotter.LabelY1 = "Widerstand / M\\[Omega]";
            XYPlotData resistance = XYPlotData.CreateData(0, .01, 5, (x) =>
              {
                  return 5 - Math.Exp(-x/1);
              }).TranslateX(1);
            xyPlot1.Plotter.AddPlot(resistance).HideFromLegend().SetColor("yellow");
            xyPlot1.Plotter.GridLines = GridLineStyle.FullGrid;

            xyPlot1.Plotter.HorizontalBackground = new ColorBand(times, new string[] { "#FFA", "#AFA", "#FFA", "black" });
            xyPlot1.Plotter.HorizontalBackground.Interpolate = false;

            xyPlot1.Plotter.RangeY1 = new ChartRange(0, 6);
            xyPlot1.Plotter.RangeX = new ChartRange(0, 7);

            xyPlot1.Redraw();
        }

        private void iSOPrüfungIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] times = new double[] { 0, 1, 6, 7 };
            xyPlot1.Plotter.Reset();
            xyPlot1.Plotter.Title = "";
            xyPlot1.Plotter.LabelX = "Zeit / s";
            xyPlot1.Plotter.LabelY1 = "Strom / \\[mu]A";
            XYPlotData current1 = new XYPlotData(new double[] { 0, 1 }, new double[] { 0, 250 });
            XYPlotData current3 = new XYPlotData(new double[] { 6, 7 }, new double[] { 200, 0 });
            XYPlotData current2 = XYPlotData.CreateData(0, .01, 5, (x) =>
            {
                return 200 + 50 * Math.Exp(-x / 1);
            }).TranslateX(1);
            xyPlot1.Plotter.AddPlot(current1 & current2 & current3).HideFromLegend().SetColor("#F41915");
            xyPlot1.Plotter.GridLines = GridLineStyle.FullGrid;

            xyPlot1.Plotter.HorizontalBackground = new ColorBand(times, new string[] { "#FFA", "#AFA", "#FFA", "black" });
            xyPlot1.Plotter.HorizontalBackground.Interpolate = false;

            xyPlot1.Plotter.RangeY1 = new ChartRange(0, 300);
            xyPlot1.Plotter.RangeX = new ChartRange(0, 7);

            xyPlot1.Redraw();
        }

		private void imageLabelsToolStripMenuItem_Click(object sender, EventArgs e)
		{
            string title = "iVBORw0KGgoAAAANSUhEUgAAANsAAAAqCAIAAAC1L7mrAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAJASURBVHhe7ZJRkoMwDEN7/0t3DXgYNtsGkki2k/X7aoDYkqrXO2nn9crcWGSyPWQjeWSyPWQjeWSyPWQjeWSyPWQjebCSXfs/m9ddfOXZyB6ykTyykT1kI3lkI3vIRvKo6RP1Tei1neK4GPO6i68cqW9rJRSdG4/I2urEV87Sx3a+FfaCPrXCfiOK+MonbqT+2pHjgZ7JmC2CE185S5+X862V/NVe7saJr3y1Rh6wt2cjeazZSEEE8DS4u+smvvLF/7OtlTt6BgEfaEZ85Ys3kkQ2kgdF39p1FLKRPLKRPWQjeazQSFl3oGcEOvE7+t1sxFe+SCP1lxVbJS/o0xmIr3b6RrpEXCyVYxN67Tf67hP6BQLsNAYUfZa2XSIeXFpcl2NlYOVVB9hpDCj6LG27RDy49Hr9dhTWoEtcTVD0Wdp2iXhw6fX67SisQZe4mqDos7TtEvHg0uv121FYgy5xNYHXZ+zZJeLBpef1J3OwBl3iagKvz9izS8TjS2XCgZ6/8+Sb52CnMcDrM/bsEjFk6ZMhcHcucTWB12fp2Stf2Tu++smQ8S0F8IFw8PosPTvmO7j6vC4/KqMqr7phzAQyt2HfcEe2F3e/jWIY9A3tlrkNBw+3wl/l8uRAzzvFEQJjJpC5DQcPt0JFubw60UdQSGNR4MVZGg4ebgVH5dnI5APjKXVPyEYmHxhMyfc6lWykD5JSgb54Ruv3BYPXqWR7ovCwJVt5EX2CDGGQjQzE0bY6+ikC7DQU2cj/S8RGvt8/cJIBtU+SP48AAAAASUVORK5CYII=";
            xyPlot1.Plotter.Title = "base64:" + title;
            xyPlot1.Plotter.LabelX = "base64:" + title;
            //xyPlot1.Plotter.LabelY1 = "base64:" + title;
            //xyPlot1.Plotter.LabelY2 = "base64:" + title;
            xyPlot1.Redraw();
        }
	}
}
