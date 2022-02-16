using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartPlotter.WinForms
{
    public partial class SearchDialog : Form
    {
        public double Value;
        public bool ExactValue;

        public SearchDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(!double.TryParse(tbValue.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double _value))
            {
                if(!double.TryParse(tbValue.Text, out _value))
                {
                    MessageBox.Show(this, "Cant parse value '" + tbValue.Text + "'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            Value = _value;
            ExactValue = rbExact.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
