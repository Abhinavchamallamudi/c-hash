/* Name : Aravind Muvva and Abhinav Chamallamudi
 * ZID : Z1835959 and Z1826541
 * Course : CSCI 504
 * Due date : 11-29-2018
 * Purpose : functionality for the third form
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Charts
{
    public partial class chart3 : Form
    {
        public chart3()
        {
            InitializeComponent();
        }

        private void chart3_Load(object sender, EventArgs e)
        {
            loadChart();
        }

        public void loadChart()
        {
            graph r = Form1.graph3;
            foreach (series s in r.seriesList)
            {
                pieChart.Series.Add(s.name);
                pieChart.Series[pieChart.Series.Count - 1].ChartType = SeriesChartType.Pie;
                pieChart.Series[pieChart.Series.Count - 1].Label = "#PERCENT";
                pieChart.Series[pieChart.Series.Count - 1].LegendText = "#VALX";
                int y = 0;
                foreach (string ss in s.xPoints)
                {
                    pieChart.Series[pieChart.Series.Count - 1].Points.AddXY(ss, s.yPoints[y]);
                    y++;
                }
            }
            pieChart.Titles.Add(r.title);
            pieChart.ChartAreas["main"].AxisX.Interval = 1;
            pieChart.ChartAreas["main"].AxisX.Title = r.xAxis;
            pieChart.ChartAreas["main"].AxisY.Title = r.yAxis;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
