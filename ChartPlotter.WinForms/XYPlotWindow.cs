using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartPlotter.WinForms
{
    public class XYPlotWindow
    {
        public static void ShowWindow(XYPlotRenderer plotter, string title = null)
        {
            if(Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                Thread thr = new Thread(()=> ShowWindow(plotter, title));
                thr.SetApartmentState(ApartmentState.STA);
                thr.Start();
                thr.Join();
                return;
            }

            Form frm = new Form();
            XYPlot plot = new XYPlot(plotter);
            plot.Dock = DockStyle.Fill;
            frm.Controls.Add(plot);
            frm.ClientSize = new System.Drawing.Size(600, 400);
            if(title == null)
            {
                frm.Text = "Plot - " + plotter.Title;
            }
            else
            {
                frm.Text = title;
            }
            Application.Run(frm);
        }
    }
}
