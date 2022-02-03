
namespace TestApp
{
    partial class Scwingkreis
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
            this.tbF = new System.Windows.Forms.TrackBar();
            this.tbD = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tbF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbD)).BeginInit();
            this.SuspendLayout();
            // 
            // tbF
            // 
            this.tbF.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbF.Location = new System.Drawing.Point(0, 0);
            this.tbF.Maximum = 100;
            this.tbF.Minimum = 1;
            this.tbF.Name = "tbF";
            this.tbF.Size = new System.Drawing.Size(800, 45);
            this.tbF.TabIndex = 0;
            this.tbF.Value = 1;
            this.tbF.Scroll += new System.EventHandler(this.tbF_Scroll);
            // 
            // tbD
            // 
            this.tbD.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbD.Location = new System.Drawing.Point(0, 45);
            this.tbD.Maximum = 100;
            this.tbD.Name = "tbD";
            this.tbD.Size = new System.Drawing.Size(800, 45);
            this.tbD.TabIndex = 1;
            this.tbD.Scroll += new System.EventHandler(this.tbD_Scroll);
            // 
            // Scwingkreis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbD);
            this.Controls.Add(this.tbF);
            this.Name = "Scwingkreis";
            this.Text = "Scwingkreis";
            ((System.ComponentModel.ISupportInitialize)(this.tbF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbF;
        private System.Windows.Forms.TrackBar tbD;
    }
}