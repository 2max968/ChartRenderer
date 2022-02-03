
namespace ChartPlotter.WinForms.GuiElements
{
    partial class XYGraphSetp
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnColor = new System.Windows.Forms.Panel();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.cbLegend = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // pnColor
            // 
            this.pnColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnColor.Location = new System.Drawing.Point(3, 3);
            this.pnColor.Name = "pnColor";
            this.pnColor.Size = new System.Drawing.Size(32, 32);
            this.pnColor.TabIndex = 0;
            this.pnColor.Click += new System.EventHandler(this.pnColor_Click);
            this.pnColor.Paint += new System.Windows.Forms.PaintEventHandler(this.pnColor_Paint);
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(41, 9);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(245, 20);
            this.tbTitle.TabIndex = 0;
            // 
            // cbLegend
            // 
            this.cbLegend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLegend.AutoSize = true;
            this.cbLegend.Location = new System.Drawing.Point(292, 12);
            this.cbLegend.Name = "cbLegend";
            this.cbLegend.Size = new System.Drawing.Size(15, 14);
            this.cbLegend.TabIndex = 1;
            this.cbLegend.UseVisualStyleBackColor = true;
            // 
            // XYGraphSetp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbLegend);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.pnColor);
            this.Name = "XYGraphSetp";
            this.Size = new System.Drawing.Size(310, 38);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnColor;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.CheckBox cbLegend;
    }
}
