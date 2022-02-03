using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class CSVInput : Form
    {
        public string Content { get; set; } = "";

        public CSVInput()
        {
            InitializeComponent();

            textBox1.Text = Content;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Content = textBox1.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
