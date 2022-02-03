using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartPlotter.WinForms.GuiElements
{
    public partial class XYGraphSetp : UserControl
    {
        XYPlotData plotData;

        public XYGraphSetp(XYPlotData plotData)
        {
            InitializeComponent();
            this.plotData = plotData;

            pnColor.BackColor = plotData.DataColor ?? Color.Black;
            tbTitle.Text = plotData.DataTitle;
            cbLegend.Checked = plotData.LegendVisible;
        }

        public void Save()
        {
            plotData.DataColor = pnColor.BackColor;
            plotData.DataTitle = tbTitle.Text;
            plotData.LegendVisible = cbLegend.Checked;
        }

        private void pnColor_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pnColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = pnColor.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                pnColor.BackColor = cd.Color;
            }
            cd.Dispose();
        }
    }
}
