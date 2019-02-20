
/* Name : Aravind Muvva and Abhinav Chamallamudi
 * ZID : Z1835959 and Z1826541
 * Course : CSCI 504
 * Due date : 11-29-2018
 * Purpose : functionality for the second form
 */using System;
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
    public partial class chart2 : Form
    {
        public chart2()
        {
            InitializeComponent();
        }

        private void chart2_Load(object sender, EventArgs e)
        {
            loadChart();
        }

        public void loadChart()
        {
            graph r = Form1.graph2;
            foreach (series s in r.seriesList)
            {
                areaChart.Series.Add(s.name);
                areaChart.Series[areaChart.Series.Count - 1].ChartType = SeriesChartType.SplineArea;
                int y = 0;
                foreach (string ss in s.xPoints)
                {
                    areaChart.Series[areaChart.Series.Count - 1].Points.AddXY(ss, s.yPoints[y]);
                    y++;
                }
            }
            areaChart.Titles.Add(r.title);
            areaChart.ChartAreas["main"].AxisX.Interval = 1;
            areaChart.ChartAreas["main"].AxisX.Title = r.xAxis;
            areaChart.ChartAreas["main"].AxisY.Title = r.yAxis;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
