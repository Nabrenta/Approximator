namespace TestWork
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.MainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.MainChart)).BeginInit();
            this.SuspendLayout();
            // 
            // MainChart
            // 
            chartArea1.AxisX.ScaleView.Zoomable = false;
            chartArea1.AxisX.ScrollBar.Enabled = false;
            chartArea1.AxisX2.ScaleView.Zoomable = false;
            chartArea1.AxisX2.ScrollBar.Enabled = false;
            chartArea1.AxisY.ScaleView.Zoomable = false;
            chartArea1.AxisY.ScrollBar.Enabled = false;
            chartArea1.AxisY2.ScaleView.Zoomable = false;
            chartArea1.AxisY2.ScrollBar.Enabled = false;
            chartArea1.Name = "MainField";
            this.MainChart.ChartAreas.Add(chartArea1);
            this.MainChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainChart.Location = new System.Drawing.Point(0, 0);
            this.MainChart.Name = "MainChart";
            this.MainChart.Size = new System.Drawing.Size(538, 294);
            this.MainChart.TabIndex = 0;
            this.MainChart.Text = "chart1";
            this.MainChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainChart_MouseDown);
            this.MainChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainChart_MouseMove);
            this.MainChart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainChart_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 294);
            this.Controls.Add(this.MainChart);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart MainChart;
    }
}

