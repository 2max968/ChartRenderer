using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChartPlotter.WinForms;
using ChartPlotter;

namespace TestApp
{
    public partial class Scwingkreis : Form
    {
        XYPlot plot;   

        public Scwingkreis()
        {
            InitializeComponent();

            plot = new XYPlot();
            plot.Dock = DockStyle.Fill;
            Controls.Add(plot);
            plot.BringToFront();
            plot.Plotter.RangeY1 = new ChartRange(-1, 1);
            setData();
        }

        private void tbF_Scroll(object sender, EventArgs e)
        {
            setData();
        }

        private void tbD_Scroll(object sender, EventArgs e)
        {
            setData();
        }

        void setData()
        {
            float f = tbF.Value / 10f;
            float d = tbD.Value / 10f;
            plot.Plotter.ClearPlots();
            plot.Plotter.AddPlot(XYPlotData.CreateData(0, 0.05, 10, (x) =>
            {
                return Math.Exp(-d * x);
            })).SetStyle('-').HideFromLegend().SetWidth(1).SetColor(Color.Gray);
            plot.Plotter.AddPlot(XYPlotData.CreateData(0, 0.05, 10, (x) =>
            {
                return -Math.Exp(-d * x);
            })).SetStyle('-').HideFromLegend().SetWidth(1).SetColor(Color.Gray);
            plot.Plotter.AddPlot(XYPlotData.CreateData(0, 0.002, 10, (x) =>
              {
                  return Math.Sin(2 * Math.PI * f * x) * Math.Exp(-d * x);
              }));
            plot.Plotter.Title = $"f={f}, d={d}";
            plot.Redraw();
        }
    }
}
