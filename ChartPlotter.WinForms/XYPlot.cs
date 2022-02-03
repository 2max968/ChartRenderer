using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartPlotter.WinForms
{
    public partial class XYPlot: UserControl
    {
        public XYPlotRenderer Plotter { get; private set; }
        Point? mousePos = null;
        bool running = true;
        bool redraw = false;
        bool stopped;
        List<GuiElements.XYGraphSetp> graphEdits = new List<GuiElements.XYGraphSetp>();
        MouseMoveMode mmm = MouseMoveMode.None;
        Font ftApTitle, ftApLegend, ftAp;

        /*public override Color BackColor 
        {
            get
            {
                if (Plotter == null)
                    return Color.White;
                return Plotter.BackgroundColor;
            }
            set
            {
                if (Plotter == null)
                    return;
                Plotter.BackgroundColor = value;
                Redraw();
            }
        }

        public override Color ForeColor
        {
            get
            {
                if (Plotter == null)
                    return Color.Black;
                return Plotter.ForegroundColor;
            }
            set
            {
                if (Plotter == null)
                    return;
                Plotter.ForegroundColor = value;
                Redraw();
            }
        }*/

        public XYPlot()
        {
            InitializeComponent();

            Plotter = new XYPlotRenderer();
            Redraw();
            renderLoop();
            this.Disposed += XYPlot_Disposed;
            this.MouseWheel += XYPlot_MouseWheel;
        }

        private void XYPlot_MouseWheel(object sender, MouseEventArgs e)
        {
            Plotter.Zoom(Math.Pow(1.1, -e.Delta / 20.0), Math.Pow(1.1, -e.Delta / 20.0));
            Redraw();
        }

        private async void XYPlot_Disposed(object sender, EventArgs e)
        {
            stopped = false;
            running = false;
            while (!stopped) await Task.Delay(100);
        }

        public void Redraw()
        {
            redraw = true;
        }

        private void pbGraph_SizeChanged(object sender, EventArgs e)
        {
            Redraw();
        }

        private void pbGraph_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Plotter.LastRenderInfo != null && Plotter.LastRenderInfo.ChartBounds.Contains(e.X, e.Y))
                {
                    mousePos = new Point(e.X, e.Y);
                    pbGraph.Cursor = Cursors.SizeAll;
                    mmm = MouseMoveMode.Shift;
                }
                else if (Plotter.LastRenderInfo != null && e.X > Plotter.LastRenderInfo.ChartBounds.Left && e.X < Plotter.LastRenderInfo.ChartBounds.Right && e.Y > Plotter.LastRenderInfo.ChartBounds.Bottom)
                {
                    mousePos = new Point(e.X, e.Y);
                    pbGraph.Cursor = Cursors.SizeWE;
                    mmm = MouseMoveMode.ScaleX;
                }
                else if (Plotter.LastRenderInfo != null && e.Y > Plotter.LastRenderInfo.ChartBounds.Top && e.Y < Plotter.LastRenderInfo.ChartBounds.Bottom && e.X < Plotter.LastRenderInfo.ChartBounds.Left)
                {
                    mousePos = new Point(e.X, e.Y);
                    pbGraph.Cursor = Cursors.SizeNS;
                    mmm = MouseMoveMode.ScaleY;
                }
            }
            else
            {
                mousePos = null;
            }
        }

        private void pbGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePos != null && mmm == MouseMoveMode.Shift)
            {
                int dx = e.X - mousePos.Value.X;
                int dy = e.Y - mousePos.Value.Y;
                mousePos = new Point(e.X, e.Y);
                Plotter.TranslateRangeByPixel(dx, dy);
                Redraw();
            }
            else if (mousePos != null && mmm == MouseMoveMode.ScaleX)
            {
                int dx = e.X - mousePos.Value.X;
                mousePos = new Point(e.X, e.Y);
                Plotter.Zoom(Math.Pow(1.1, -dx / 2.0), 1);
                Redraw();
            }
            else if (mousePos != null && mmm == MouseMoveMode.ScaleY)
            {
                int dy = e.Y - mousePos.Value.Y;
                mousePos = new Point(e.X, e.Y);
                Plotter.Zoom(1, Math.Pow(1.1, dy / 2.0));
                Redraw();
            }
            /*if (mousePos != null && e.Button == MouseButtons.Right)
            {
                int dx = e.X - mousePos.Value.X;
                int dy = e.Y - mousePos.Value.Y;
                mousePos = new Point(e.X, e.Y);
                Plotter.Zoom(Math.Pow(1.1, -dx / 2.0), Math.Pow(1.1, dy / 2.0));
                Redraw();
            }*/
        }

        async void renderLoop()
        {
            while(running)
            {
                if (redraw && Plotter != null)
                {
                    int w = pbGraph.Width;
                    int h = pbGraph.Height;
                    redraw = false;
                    if (w <= 0 || h <= 0)
                        continue;
                    Task<Bitmap> renderTask = new Task<Bitmap>(() =>
                    {
                        try
                        {
                            return Plotter.RenderChart(w, h);
                        }
                        catch
                        {
                            return null;
                        }
                    });
                    renderTask.Start();
                    await renderTask;
                    pbGraph.Image?.Dispose();
                    pbGraph.Image = renderTask.Result;
                    await Task.Delay(0);
                }
                else
                {
                    await Task.Delay(30);
                }
            }
            stopped = true;
        }

        private void resetRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void saveAsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sfdImage.ShowDialog() == DialogResult.OK)
            {
                Plotter.Scaling *= 2;
                using (Bitmap bmp = Plotter.RenderChart(pbGraph.Width * 2, pbGraph.Height * 2))
                {
                    Plotter.Scaling /= 2;
                    bmp.Save(sfdImage.FileName);
                }
            }
        }

        private void editGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnDataEdit.Dock = DockStyle.Fill;
            pnDataEdit.Show();

            flowEdit.Controls.Clear();
            graphEdits.Clear();
            for (int i = 0; i < Plotter.Data.Count; i++)
            {
                var item = new GuiElements.XYGraphSetp(Plotter.Data[i]);
                flowEdit.Controls.Add(item);
                graphEdits.Add(item);
            }
        }

        private void btnEditCancel_Click(object sender, EventArgs e)
        {
            pnDataEdit.Hide();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Plotter.Scaling *= 2;
            using (Bitmap bmp = Plotter.RenderChart(pbGraph.Width * 2, pbGraph.Height * 2))
            {
                Plotter.Scaling /= 2;
                Clipboard.SetImage(bmp);
            }
        }

        private void btnEditOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < graphEdits.Count; i++)
            {
                graphEdits[i].Save();
            }
            pnDataEdit.Hide();
            Redraw();
        }

        private void pbGraph_MouseUp(object sender, MouseEventArgs e)
        {
            pbGraph.Cursor = Cursors.Default;
            mmm = MouseMoveMode.None;
        }

        private void resetRangeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Plotter.RangeX = Plotter.RangeY1 = Plotter.RangeY2 = null;
            Redraw();
        }

        private void matchRangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Plotter.LastRenderInfo == null)
                return;
            double x = Plotter.LastRenderInfo.RangeX.Range / Plotter.LastRenderInfo.ChartBounds.Width;
            double y = Plotter.LastRenderInfo.RangeY1.Range / Plotter.LastRenderInfo.ChartBounds.Height;
            if(x < y)
            {
                Plotter.Zoom(y / x, 1);
            }
            else if(y < x)
            {
                Plotter.Zoom(1, x / y);
            }
            Redraw();
        }

        private void editAppearanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbTitle.Text = Plotter.Title;
            tbX.Text = Plotter.LabelX;
            tbY.Text = Plotter.LabelY1;
            btnApColor.BackColor = Plotter.BackgroundColor;
            btnApColor.ForeColor = Plotter.ForegroundColor;
            ftAp = Plotter.Font;
            ftApLegend = Plotter.LegendFont;
            ftApTitle = Plotter.TitleFont;
            apSetFontButtons();
            
            cbLegend.Checked = Plotter.ShowLegend;
            pnAppearance.Dock = DockStyle.Fill;
            pnAppearance.Show();
        }

        void apSetFontButtons()
        {
            btnApFontTitle.Text = $"{ftApTitle.FontFamily.Name}, {ftApTitle.SizeInPoints}pt";
            btnApFontLabel.Text = $"{ftAp.FontFamily.Name}, {ftAp.SizeInPoints}pt";
            btnApFontLegend.Text = $"{ftApLegend.FontFamily.Name}, {ftApLegend.SizeInPoints}pt";
        }

        private void btnApCancel_Click(object sender, EventArgs e)
        {
            pnAppearance.Hide();
        }

        private void btnApOk_Click(object sender, EventArgs e)
        {
            Plotter.Title = tbTitle.Text;
            Plotter.LabelX = tbX.Text;
            Plotter.LabelY1 = tbY.Text;
            Plotter.ForegroundColor = btnApColor.ForeColor;
            Plotter.BackgroundColor = btnApColor.BackColor;
            Plotter.ShowLegend = cbLegend.Checked;
            Plotter.Font = ftAp;
            Plotter.LegendFont = ftApLegend;
            Plotter.TitleFont = ftApTitle;
            Redraw();
            pnAppearance.Hide();
        }

        private void btnApColor_Click(object sender, EventArgs e)
        {
            ctxColors.Show(btnApColor, new Point(0, 0));
        }

        private void btnApFontTitle_Click(object sender, EventArgs e)
        {
            Font ft;
            if (sender == btnApFontLabel) ft = ftAp;
            else if (sender == btnApFontTitle) ft = ftApTitle;
            else if (sender == btnApFontLegend) ft = ftApLegend;
            else return;
            FontDialog fd = new FontDialog();
            fd.Font = ft;
            if(fd.ShowDialog() == DialogResult.OK)
            {
                ft = fd.Font;
                if (sender == btnApFontLabel) ftAp = ft;
                else if (sender == btnApFontTitle) ftApTitle = ft;
                else if (sender == btnApFontLegend) ftApLegend = ft;
                else return;
            }
            apSetFontButtons();
        }

        private void foregroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = btnApColor.ForeColor;
            if(cd.ShowDialog() == DialogResult.OK)
            {
                btnApColor.ForeColor = cd.Color;
            }
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = btnApColor.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                btnApColor.BackColor = cd.Color;
            }
        }
    }
}
