
namespace TestApp
{
    partial class Form2
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exponensialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oscillationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dualScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.naNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bigNumbersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manyColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dynamicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGrid1.Location = new System.Drawing.Point(534, 24);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(266, 426);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            this.propertyGrid1.Click += new System.EventHandler(this.propertyGrid1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plotsToolStripMenuItem,
            this.cSVToolStripMenuItem,
            this.dynamicToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plotsToolStripMenuItem
            // 
            this.plotsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exponensialToolStripMenuItem,
            this.circleToolStripMenuItem,
            this.oscillationToolStripMenuItem,
            this.dualScaleToolStripMenuItem,
            this.naNToolStripMenuItem,
            this.bigNumbersToolStripMenuItem,
            this.manyColorsToolStripMenuItem,
            this.emptyToolStripMenuItem});
            this.plotsToolStripMenuItem.Name = "plotsToolStripMenuItem";
            this.plotsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.plotsToolStripMenuItem.Text = "Plots";
            // 
            // exponensialToolStripMenuItem
            // 
            this.exponensialToolStripMenuItem.Name = "exponensialToolStripMenuItem";
            this.exponensialToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exponensialToolStripMenuItem.Text = "Exponential";
            this.exponensialToolStripMenuItem.Click += new System.EventHandler(this.exponensialToolStripMenuItem_Click);
            // 
            // circleToolStripMenuItem
            // 
            this.circleToolStripMenuItem.Name = "circleToolStripMenuItem";
            this.circleToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.circleToolStripMenuItem.Text = "Circle";
            this.circleToolStripMenuItem.Click += new System.EventHandler(this.circleToolStripMenuItem_Click);
            // 
            // oscillationToolStripMenuItem
            // 
            this.oscillationToolStripMenuItem.Name = "oscillationToolStripMenuItem";
            this.oscillationToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.oscillationToolStripMenuItem.Text = "Oscillation";
            this.oscillationToolStripMenuItem.Click += new System.EventHandler(this.oscillationToolStripMenuItem_Click);
            // 
            // dualScaleToolStripMenuItem
            // 
            this.dualScaleToolStripMenuItem.Name = "dualScaleToolStripMenuItem";
            this.dualScaleToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.dualScaleToolStripMenuItem.Text = "Dual Scale";
            this.dualScaleToolStripMenuItem.Click += new System.EventHandler(this.dualScaleToolStripMenuItem_Click);
            // 
            // naNToolStripMenuItem
            // 
            this.naNToolStripMenuItem.Name = "naNToolStripMenuItem";
            this.naNToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.naNToolStripMenuItem.Text = "NaN";
            this.naNToolStripMenuItem.Click += new System.EventHandler(this.naNToolStripMenuItem_Click);
            // 
            // bigNumbersToolStripMenuItem
            // 
            this.bigNumbersToolStripMenuItem.Name = "bigNumbersToolStripMenuItem";
            this.bigNumbersToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.bigNumbersToolStripMenuItem.Text = "Big Numbers";
            this.bigNumbersToolStripMenuItem.Click += new System.EventHandler(this.bigNumbersToolStripMenuItem_Click);
            // 
            // manyColorsToolStripMenuItem
            // 
            this.manyColorsToolStripMenuItem.Name = "manyColorsToolStripMenuItem";
            this.manyColorsToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.manyColorsToolStripMenuItem.Text = "Many Colors";
            this.manyColorsToolStripMenuItem.Click += new System.EventHandler(this.manyColorsToolStripMenuItem_Click);
            // 
            // emptyToolStripMenuItem
            // 
            this.emptyToolStripMenuItem.Name = "emptyToolStripMenuItem";
            this.emptyToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.emptyToolStripMenuItem.Text = "Empty";
            this.emptyToolStripMenuItem.Click += new System.EventHandler(this.emptyToolStripMenuItem_Click);
            // 
            // cSVToolStripMenuItem
            // 
            this.cSVToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readToolStripMenuItem});
            this.cSVToolStripMenuItem.Name = "cSVToolStripMenuItem";
            this.cSVToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.cSVToolStripMenuItem.Text = "CSV";
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.readToolStripMenuItem.Text = "Read";
            this.readToolStripMenuItem.Click += new System.EventHandler(this.readToolStripMenuItem_Click);
            // 
            // dynamicToolStripMenuItem
            // 
            this.dynamicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sineToolStripMenuItem,
            this.circleToolStripMenuItem1});
            this.dynamicToolStripMenuItem.Name = "dynamicToolStripMenuItem";
            this.dynamicToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.dynamicToolStripMenuItem.Text = "Dynamic";
            // 
            // sineToolStripMenuItem
            // 
            this.sineToolStripMenuItem.Name = "sineToolStripMenuItem";
            this.sineToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sineToolStripMenuItem.Text = "Sine";
            this.sineToolStripMenuItem.Click += new System.EventHandler(this.sineToolStripMenuItem_Click);
            // 
            // circleToolStripMenuItem1
            // 
            this.circleToolStripMenuItem1.Name = "circleToolStripMenuItem1";
            this.circleToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.circleToolStripMenuItem1.Text = "Circle";
            this.circleToolStripMenuItem1.Click += new System.EventHandler(this.circleToolStripMenuItem1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.Text = "Form2";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plotsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exponensialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oscillationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dualScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem naNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bigNumbersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manyColorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dynamicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circleToolStripMenuItem1;
    }
}