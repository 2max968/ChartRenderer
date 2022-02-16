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
        static System.Drawing.Icon wndIco = null;

        static Dictionary<int, KeyValuePair<Form, Thread>> windows = new Dictionary<int, KeyValuePair<Form, Thread>>();

        public static Form CreateWindow(XYPlotRenderer plotter, string title = null)
        {
            if (wndIco == null)
                wndIco = System.Drawing.Icon.FromHandle(Properties.Resources.ChartIcon.GetHicon());

            Form frm = new Form();
            XYPlot plot = new XYPlot(plotter);
            plot.Dock = DockStyle.Fill;
            frm.Controls.Add(plot);
            frm.ClientSize = new System.Drawing.Size(600, 400);
            frm.Tag = plot;
            frm.Icon = wndIco;
            if (title == null)
            {
                frm.Text = "Plot - " + plotter.Title;
            }
            else
            {
                frm.Text = title;
            }
            return frm;
        }

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

            Form frm = CreateWindow(plotter, title);
            Application.Run(frm);
        }

        public static int ShowWindowAsync(XYPlotRenderer plotter, string title = null)
        {
            int id;
            for (id = 1; windows.ContainsKey(id); id++) ;
            windows.Add(id, new KeyValuePair<Form, Thread>(null, null));
            Thread thr = new Thread(() =>
            {
                Form frm = CreateWindow(plotter, title);
                windows[id] = new KeyValuePair<Form, Thread>(frm, Thread.CurrentThread);
                Application.Run(frm);

            });
            thr.SetApartmentState(ApartmentState.STA);
            thr.Start();
            return id;
        }

        public static void JoinWindow(int id)
        {
            try
            {
                if(windows.ContainsKey(id) && windows[id].Value.IsAlive)
                    windows[id].Value.Join();
            }
            catch { }
        }

        public static void RedrawWindow(int id)
        {
            try
            {
                if (windows.ContainsKey(id) && windows[id].Value.IsAlive)
                {
                    windows[id].Key.Invoke((MethodInvoker)delegate ()
                    {
                        var plot = windows[id].Key.Tag as XYPlot;
                        plot?.Redraw();
                    });
                }
            }
            catch { }
        }
    }
}
