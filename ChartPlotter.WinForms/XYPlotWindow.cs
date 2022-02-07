using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartPlotter.WinForms
{
    public class XYPlotWindow
    {
        public static void ShowWindow(XYPlotRenderer plotter)
        {
            Form frm = new Form();
            XYPlot plot = new XYPlot(plotter);
            plot.Dock = DockStyle.Fill;
            frm.Controls.Add(plot);
            frm.ClientSize = new System.Drawing.Size(600, 400);
            Application.Run(frm);
        }
    }
}
