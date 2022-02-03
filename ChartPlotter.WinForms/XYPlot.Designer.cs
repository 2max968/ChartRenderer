namespace ChartPlotter.WinForms
{
    partial class XYPlot
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
            this.components = new System.ComponentModel.Container();
            this.pbGraph = new System.Windows.Forms.PictureBox();
            this.ctxChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editGraphsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdImage = new System.Windows.Forms.SaveFileDialog();
            this.pnDataEdit = new System.Windows.Forms.Panel();
            this.flowEdit = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEditCancel = new System.Windows.Forms.Button();
            this.btnEditOk = new System.Windows.Forms.Button();
            this.rangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matchRangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetRangeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editAppearanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnAppearance = new System.Windows.Forms.Panel();
            this.btnApOk = new System.Windows.Forms.Button();
            this.btnApCancel = new System.Windows.Forms.Button();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tbX = new System.Windows.Forms.TextBox();
            this.tbY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnApColor = new System.Windows.Forms.Button();
            this.ctxColors = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.foregroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnApFontTitle = new System.Windows.Forms.Button();
            this.btnApFontLabel = new System.Windows.Forms.Button();
            this.cbLegend = new System.Windows.Forms.CheckBox();
            this.btnApFontLegend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).BeginInit();
            this.ctxChart.SuspendLayout();
            this.pnDataEdit.SuspendLayout();
            this.pnAppearance.SuspendLayout();
            this.ctxColors.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbGraph
            // 
            this.pbGraph.ContextMenuStrip = this.ctxChart;
            this.pbGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbGraph.Location = new System.Drawing.Point(0, 0);
            this.pbGraph.Name = "pbGraph";
            this.pbGraph.Size = new System.Drawing.Size(739, 446);
            this.pbGraph.TabIndex = 0;
            this.pbGraph.TabStop = false;
            this.pbGraph.SizeChanged += new System.EventHandler(this.pbGraph_SizeChanged);
            this.pbGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbGraph_MouseDown);
            this.pbGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbGraph_MouseMove);
            this.pbGraph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbGraph_MouseUp);
            // 
            // ctxChart
            // 
            this.ctxChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rangeToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveAsImageToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.toolStripSeparator2,
            this.editGraphsToolStripMenuItem,
            this.editAppearanceToolStripMenuItem});
            this.ctxChart.Name = "ctxChart";
            this.ctxChart.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxChart.Size = new System.Drawing.Size(161, 126);
            // 
            // saveAsImageToolStripMenuItem
            // 
            this.saveAsImageToolStripMenuItem.Name = "saveAsImageToolStripMenuItem";
            this.saveAsImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsImageToolStripMenuItem.Text = "Save as Image";
            this.saveAsImageToolStripMenuItem.Click += new System.EventHandler(this.saveAsImageToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // editGraphsToolStripMenuItem
            // 
            this.editGraphsToolStripMenuItem.Name = "editGraphsToolStripMenuItem";
            this.editGraphsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editGraphsToolStripMenuItem.Text = "Edit Graphs";
            this.editGraphsToolStripMenuItem.Click += new System.EventHandler(this.editGraphsToolStripMenuItem_Click);
            // 
            // sfdImage
            // 
            this.sfdImage.Filter = "Portable Network Graphic|*.png|Jpeg|*.jpg|Bitmap|*.bmp";
            // 
            // pnDataEdit
            // 
            this.pnDataEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnDataEdit.Controls.Add(this.flowEdit);
            this.pnDataEdit.Controls.Add(this.btnEditCancel);
            this.pnDataEdit.Controls.Add(this.btnEditOk);
            this.pnDataEdit.Location = new System.Drawing.Point(17, 18);
            this.pnDataEdit.Name = "pnDataEdit";
            this.pnDataEdit.Size = new System.Drawing.Size(268, 194);
            this.pnDataEdit.TabIndex = 2;
            this.pnDataEdit.Visible = false;
            // 
            // flowEdit
            // 
            this.flowEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowEdit.AutoScroll = true;
            this.flowEdit.Location = new System.Drawing.Point(3, 3);
            this.flowEdit.Name = "flowEdit";
            this.flowEdit.Size = new System.Drawing.Size(260, 157);
            this.flowEdit.TabIndex = 2;
            // 
            // btnEditCancel
            // 
            this.btnEditCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCancel.Location = new System.Drawing.Point(107, 166);
            this.btnEditCancel.Name = "btnEditCancel";
            this.btnEditCancel.Size = new System.Drawing.Size(75, 23);
            this.btnEditCancel.TabIndex = 1;
            this.btnEditCancel.Text = "Cancel";
            this.btnEditCancel.UseVisualStyleBackColor = true;
            this.btnEditCancel.Click += new System.EventHandler(this.btnEditCancel_Click);
            // 
            // btnEditOk
            // 
            this.btnEditOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditOk.Location = new System.Drawing.Point(188, 166);
            this.btnEditOk.Name = "btnEditOk";
            this.btnEditOk.Size = new System.Drawing.Size(75, 23);
            this.btnEditOk.TabIndex = 0;
            this.btnEditOk.Text = "Ok";
            this.btnEditOk.UseVisualStyleBackColor = true;
            this.btnEditOk.Click += new System.EventHandler(this.btnEditOk_Click);
            // 
            // rangeToolStripMenuItem
            // 
            this.rangeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetRangeToolStripMenuItem1,
            this.matchRangesToolStripMenuItem});
            this.rangeToolStripMenuItem.Name = "rangeToolStripMenuItem";
            this.rangeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rangeToolStripMenuItem.Text = "Range";
            // 
            // matchRangesToolStripMenuItem
            // 
            this.matchRangesToolStripMenuItem.Name = "matchRangesToolStripMenuItem";
            this.matchRangesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.matchRangesToolStripMenuItem.Text = "Match Ranges";
            this.matchRangesToolStripMenuItem.Click += new System.EventHandler(this.matchRangesToolStripMenuItem_Click);
            // 
            // resetRangeToolStripMenuItem1
            // 
            this.resetRangeToolStripMenuItem1.Name = "resetRangeToolStripMenuItem1";
            this.resetRangeToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.resetRangeToolStripMenuItem1.Text = "Reset Range";
            this.resetRangeToolStripMenuItem1.Click += new System.EventHandler(this.resetRangeToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // editAppearanceToolStripMenuItem
            // 
            this.editAppearanceToolStripMenuItem.Name = "editAppearanceToolStripMenuItem";
            this.editAppearanceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editAppearanceToolStripMenuItem.Text = "Edit Appearance";
            this.editAppearanceToolStripMenuItem.Click += new System.EventHandler(this.editAppearanceToolStripMenuItem_Click);
            // 
            // pnAppearance
            // 
            this.pnAppearance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnAppearance.Controls.Add(this.btnApFontLegend);
            this.pnAppearance.Controls.Add(this.cbLegend);
            this.pnAppearance.Controls.Add(this.btnApFontLabel);
            this.pnAppearance.Controls.Add(this.btnApFontTitle);
            this.pnAppearance.Controls.Add(this.btnApColor);
            this.pnAppearance.Controls.Add(this.label3);
            this.pnAppearance.Controls.Add(this.label2);
            this.pnAppearance.Controls.Add(this.label1);
            this.pnAppearance.Controls.Add(this.tbY);
            this.pnAppearance.Controls.Add(this.tbX);
            this.pnAppearance.Controls.Add(this.tbTitle);
            this.pnAppearance.Controls.Add(this.btnApOk);
            this.pnAppearance.Controls.Add(this.btnApCancel);
            this.pnAppearance.Location = new System.Drawing.Point(291, 18);
            this.pnAppearance.Name = "pnAppearance";
            this.pnAppearance.Size = new System.Drawing.Size(377, 194);
            this.pnAppearance.TabIndex = 3;
            this.pnAppearance.Visible = false;
            // 
            // btnApOk
            // 
            this.btnApOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApOk.Location = new System.Drawing.Point(297, 166);
            this.btnApOk.Name = "btnApOk";
            this.btnApOk.Size = new System.Drawing.Size(75, 23);
            this.btnApOk.TabIndex = 4;
            this.btnApOk.Text = "Ok";
            this.btnApOk.UseVisualStyleBackColor = true;
            this.btnApOk.Click += new System.EventHandler(this.btnApOk_Click);
            // 
            // btnApCancel
            // 
            this.btnApCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApCancel.Location = new System.Drawing.Point(216, 166);
            this.btnApCancel.Name = "btnApCancel";
            this.btnApCancel.Size = new System.Drawing.Size(75, 23);
            this.btnApCancel.TabIndex = 5;
            this.btnApCancel.Text = "Cancel";
            this.btnApCancel.UseVisualStyleBackColor = true;
            this.btnApCancel.Click += new System.EventHandler(this.btnApCancel_Click);
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(52, 3);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(100, 20);
            this.tbTitle.TabIndex = 6;
            // 
            // tbX
            // 
            this.tbX.Location = new System.Drawing.Point(52, 29);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(100, 20);
            this.tbX.TabIndex = 7;
            // 
            // tbY
            // 
            this.tbY.Location = new System.Drawing.Point(52, 55);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(100, 20);
            this.tbY.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "X Label";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Y Label";
            // 
            // btnApColor
            // 
            this.btnApColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApColor.Location = new System.Drawing.Point(6, 104);
            this.btnApColor.Name = "btnApColor";
            this.btnApColor.Size = new System.Drawing.Size(75, 23);
            this.btnApColor.TabIndex = 12;
            this.btnApColor.Text = "Colors";
            this.btnApColor.UseVisualStyleBackColor = true;
            this.btnApColor.Click += new System.EventHandler(this.btnApColor_Click);
            // 
            // ctxColors
            // 
            this.ctxColors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.foregroundToolStripMenuItem,
            this.backgroundToolStripMenuItem});
            this.ctxColors.Name = "ctxColors";
            this.ctxColors.Size = new System.Drawing.Size(139, 48);
            // 
            // foregroundToolStripMenuItem
            // 
            this.foregroundToolStripMenuItem.Name = "foregroundToolStripMenuItem";
            this.foregroundToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.foregroundToolStripMenuItem.Text = "Foreground";
            this.foregroundToolStripMenuItem.Click += new System.EventHandler(this.foregroundToolStripMenuItem_Click);
            // 
            // backgroundToolStripMenuItem
            // 
            this.backgroundToolStripMenuItem.Name = "backgroundToolStripMenuItem";
            this.backgroundToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.backgroundToolStripMenuItem.Text = "Background";
            this.backgroundToolStripMenuItem.Click += new System.EventHandler(this.backgroundToolStripMenuItem_Click);
            // 
            // btnApFontTitle
            // 
            this.btnApFontTitle.Location = new System.Drawing.Point(158, 1);
            this.btnApFontTitle.Name = "btnApFontTitle";
            this.btnApFontTitle.Size = new System.Drawing.Size(214, 23);
            this.btnApFontTitle.TabIndex = 13;
            this.btnApFontTitle.Text = "button1";
            this.btnApFontTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnApFontTitle.UseVisualStyleBackColor = true;
            this.btnApFontTitle.Click += new System.EventHandler(this.btnApFontTitle_Click);
            // 
            // btnApFontLabel
            // 
            this.btnApFontLabel.Location = new System.Drawing.Point(158, 27);
            this.btnApFontLabel.Name = "btnApFontLabel";
            this.btnApFontLabel.Size = new System.Drawing.Size(214, 23);
            this.btnApFontLabel.TabIndex = 14;
            this.btnApFontLabel.Text = "button2";
            this.btnApFontLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnApFontLabel.UseVisualStyleBackColor = true;
            this.btnApFontLabel.Click += new System.EventHandler(this.btnApFontTitle_Click);
            // 
            // cbLegend
            // 
            this.cbLegend.AutoSize = true;
            this.cbLegend.Location = new System.Drawing.Point(6, 81);
            this.cbLegend.Name = "cbLegend";
            this.cbLegend.Size = new System.Drawing.Size(92, 17);
            this.cbLegend.TabIndex = 15;
            this.cbLegend.Text = "Show Legend";
            this.cbLegend.UseVisualStyleBackColor = true;
            // 
            // btnApFontLegend
            // 
            this.btnApFontLegend.Location = new System.Drawing.Point(158, 77);
            this.btnApFontLegend.Name = "btnApFontLegend";
            this.btnApFontLegend.Size = new System.Drawing.Size(214, 23);
            this.btnApFontLegend.TabIndex = 16;
            this.btnApFontLegend.Text = "button2";
            this.btnApFontLegend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnApFontLegend.UseVisualStyleBackColor = true;
            this.btnApFontLegend.Click += new System.EventHandler(this.btnApFontTitle_Click);
            // 
            // XYPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnAppearance);
            this.Controls.Add(this.pnDataEdit);
            this.Controls.Add(this.pbGraph);
            this.Name = "XYPlot";
            this.Size = new System.Drawing.Size(739, 446);
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).EndInit();
            this.ctxChart.ResumeLayout(false);
            this.pnDataEdit.ResumeLayout(false);
            this.pnAppearance.ResumeLayout(false);
            this.pnAppearance.PerformLayout();
            this.ctxColors.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbGraph;
        private System.Windows.Forms.ContextMenuStrip ctxChart;
        private System.Windows.Forms.ToolStripMenuItem saveAsImageToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdImage;
        private System.Windows.Forms.ToolStripMenuItem editGraphsToolStripMenuItem;
        private System.Windows.Forms.Panel pnDataEdit;
        private System.Windows.Forms.FlowLayoutPanel flowEdit;
        private System.Windows.Forms.Button btnEditCancel;
        private System.Windows.Forms.Button btnEditOk;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetRangeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem matchRangesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem editAppearanceToolStripMenuItem;
        private System.Windows.Forms.Panel pnAppearance;
        private System.Windows.Forms.Button btnApOk;
        private System.Windows.Forms.Button btnApCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbY;
        private System.Windows.Forms.TextBox tbX;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Button btnApColor;
        private System.Windows.Forms.ContextMenuStrip ctxColors;
        private System.Windows.Forms.ToolStripMenuItem foregroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundToolStripMenuItem;
        private System.Windows.Forms.Button btnApFontLabel;
        private System.Windows.Forms.Button btnApFontTitle;
        private System.Windows.Forms.Button btnApFontLegend;
        private System.Windows.Forms.CheckBox cbLegend;
    }
}
