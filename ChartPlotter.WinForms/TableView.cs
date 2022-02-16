using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartPlotter.WinForms
{
    public partial class TableView : Form
    {
        static Icon wndIco = null;

        DataTable table;

        public static void ShowTable(XYPlotData data)
        {
            if(Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                Thread thr = new Thread(() => ShowTable(data));
                thr.SetApartmentState(ApartmentState.STA);
                thr.Start();
                thr.Join();
            }
            else
            {
                Application.EnableVisualStyles();
                TableView tv = new TableView(data);
                Application.Run(tv);
            }
        }

        public TableView(XYPlotData data)
        {
            InitializeComponent();

            if (wndIco == null)
                wndIco = Icon.FromHandle(Properties.Resources.ChartIcon.GetHicon());
            Icon = wndIco;

            table = new DataTable();
            table.Columns.Add("#", typeof(double));
            table.Columns.Add("X", typeof(double));
            table.Columns.Add("Y", typeof(double));
            for(int i = 0; i < data.Length; i++)
            {
                table.Rows.Add(i, data.DataX[i], data.DataY[i]);
            }

            dataGridView1.DataSource = table;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchDialog searchDialog = new SearchDialog();
            if(searchDialog.ShowDialog() == DialogResult.OK)
            {
                listView1.Items.Clear();
                if(searchDialog.ExactValue)
                {
                    for(int i = 0; i < table.Rows.Count; i++)
                    {
                        for(int j = 0; j < table.Columns.Count; j++)
                        {
                            if (table.Rows[i].ItemArray[j] is double)
                            {
                                double val = (double)table.Rows[i].ItemArray[j];
                                if(searchDialog.Value == val)
                                {
                                    ListViewItem itm = new ListViewItem(table.Rows[i].ItemArray[0].ToString());
                                    for(int k = 1; k < table.Columns.Count; k++)
                                    {
                                        itm.SubItems.Add(table.Rows[i].ItemArray[k].ToString());
                                    }
                                    itm.Tag = i;
                                    listView1.Items.Add(itm);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
