namespace Charts
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
            this.button1 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.Button();
            this.chart2 = new System.Windows.Forms.Button();
            this.chart3 = new System.Windows.Forms.Button();
            this.chart4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(306, 66);
            this.button1.TabIndex = 0;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chart1
            // 
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(150, 75);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "Solar Employment Growth by Sector - Line chart";
            this.chart1.UseVisualStyleBackColor = true;
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // chart2
            // 
            this.chart2.Location = new System.Drawing.Point(168, 12);
            this.chart2.Name = "chart2";
            this.chart2.Size = new System.Drawing.Size(150, 75);
            this.chart2.TabIndex = 2;
            this.chart2.Text = "Average fruit consumption during one week - Spline area chart";
            this.chart2.UseVisualStyleBackColor = true;
            this.chart2.Click += new System.EventHandler(this.chart2_Click);
            // 
            // chart3
            // 
            this.chart3.Location = new System.Drawing.Point(12, 93);
            this.chart3.Name = "chart3";
            this.chart3.Size = new System.Drawing.Size(150, 75);
            this.chart3.TabIndex = 3;
            this.chart3.Text = "Browser market shares in January, 2018 - Pie chart";
            this.chart3.UseVisualStyleBackColor = true;
            this.chart3.Click += new System.EventHandler(this.chart3_Click);
            // 
            // chart4
            // 
            this.chart4.Location = new System.Drawing.Point(168, 93);
            this.chart4.Name = "chart4";
            this.chart4.Size = new System.Drawing.Size(150, 75);
            this.chart4.TabIndex = 4;
            this.chart4.Text = "Monthly Average Rainfall - Column chart";
            this.chart4.UseVisualStyleBackColor = true;
            this.chart4.Click += new System.EventHandler(this.chart4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 249);
            this.Controls.Add(this.chart4);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Chart picker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button chart1;
        private System.Windows.Forms.Button chart2;
        private System.Windows.Forms.Button chart3;
        private System.Windows.Forms.Button chart4;
    }
}

