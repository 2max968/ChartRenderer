
namespace TestApp
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trigonometryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblObjectCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.sfdImage = new System.Windows.Forms.SaveFileDialog();
            this.lblPointerInfo = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(726, 407);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGrid1.Location = new System.Drawing.Point(726, 24);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(245, 407);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.plotsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(971, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadFromCSVToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadFromCSVToolStripMenuItem
            // 
            this.loadFromCSVToolStripMenuItem.Name = "loadFromCSVToolStripMenuItem";
            this.loadFromCSVToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.loadFromCSVToolStripMenuItem.Text = "Load From CSV";
            this.loadFromCSVToolStripMenuItem.Click += new System.EventHandler(this.loadFromCSVToolStripMenuItem_Click);
            // 
            // plotsToolStripMenuItem
            // 
            this.plotsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linesToolStripMenuItem,
            this.trigonometryToolStripMenuItem,
            this.circleToolStripMenuItem,
            this.dotsToolStripMenuItem});
            this.plotsToolStripMenuItem.Name = "plotsToolStripMenuItem";
            this.plotsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.plotsToolStripMenuItem.Text = "Plots";
            // 
            // linesToolStripMenuItem
            // 
            this.linesToolStripMenuItem.Name = "linesToolStripMenuItem";
            this.linesToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.linesToolStripMenuItem.Text = "Lines";
            this.linesToolStripMenuItem.Click += new System.EventHandler(this.linesToolStripMenuItem_Click);
            // 
            // trigonometryToolStripMenuItem
            // 
            this.trigonometryToolStripMenuItem.Name = "trigonometryToolStripMenuItem";
            this.trigonometryToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.trigonometryToolStripMenuItem.Text = "Trigonometry";
            this.trigonometryToolStripMenuItem.Click += new System.EventHandler(this.trigonometryToolStripMenuItem_Click);
            // 
            // circleToolStripMenuItem
            // 
            this.circleToolStripMenuItem.Name = "circleToolStripMenuItem";
            this.circleToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.circleToolStripMenuItem.Text = "Circle";
            this.circleToolStripMenuItem.Click += new System.EventHandler(this.circleToolStripMenuItem_Click);
            // 
            // dotsToolStripMenuItem
            // 
            this.dotsToolStripMenuItem.Name = "dotsToolStripMenuItem";
            this.dotsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.dotsToolStripMenuItem.Text = "Dots";
            this.dotsToolStripMenuItem.Click += new System.EventHandler(this.dotsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblObjectCount,
            this.lblPointerInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 431);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(971, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblObjectCount
            // 
            this.lblObjectCount.Name = "lblObjectCount";
            this.lblObjectCount.Size = new System.Drawing.Size(118, 17);
            this.lblObjectCount.Text = "toolStripStatusLabel1";
            // 
            // sfdImage
            // 
            this.sfdImage.Filter = "PNG|*png|Jpeg|*.jpg|Bitmap|*.bmp";
            // 
            // lblPointerInfo
            // 
            this.lblPointerInfo.Name = "lblPointerInfo";
            this.lblPointerInfo.Size = new System.Drawing.Size(118, 17);
            this.lblPointerInfo.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 453);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plotsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trigonometryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circleToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblObjectCount;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdImage;
        private System.Windows.Forms.ToolStripMenuItem dotsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFromCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblPointerInfo;
    }
}

