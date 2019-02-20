namespace Charts
{
    partial class chart2
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.areaChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.areaChart)).BeginInit();
            this.SuspendLayout();
            // 
            // areaChart
            // 
            chartArea2.Name = "main";
            this.areaChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.areaChart.Legends.Add(legend2);
            this.areaChart.Location = new System.Drawing.Point(12, 12);
            this.areaChart.Name = "areaChart";
            this.areaChart.Size = new System.Drawing.Size(776, 426);
            this.areaChart.TabIndex = 0;
            this.areaChart.Text = "chart1";
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(12, 444);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(776, 48);
            this.Close.TabIndex = 1;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // chart2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 501);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.areaChart);
            this.Name = "chart2";
            this.Text = "Spline area chart";
            this.Load += new System.EventHandler(this.chart2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.areaChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart areaChart;
        private System.Windows.Forms.Button Close;
    }
}