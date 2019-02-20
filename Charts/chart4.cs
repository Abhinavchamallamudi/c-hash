/* Name : Aravind Muvva and Abhinav Chamallamudi
 * ZID : Z1835959 and Z1826541
 * Course : CSCI 504
 * Due date : 11-29-2018
 * Purpose :functionality for fourth chart
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
    public partial class chart4 : Form
    {
        public chart4()
        {
            InitializeComponent();
        }

        private void chart4_Load(object sender, EventArgs e)
        {
            loadChart();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        public void loadChart() {
            graph r = Form1.graph4;
            foreach (series s in r.seriesList) {
                chart1.Series.Add(s.name);
                int y = 0;
                foreach (string ss in s.xPoints) {
                    chart1.Series[chart1.Series.Count - 1].Points.AddXY(ss, s.yPoints[y]);
                    y++;
                }
            }
            chart1.Titles.Add(r.title);
            chart1.ChartAreas["main"].AxisX.Interval = 1;
            chart1.ChartAreas["main"].AxisX.Title = r.xAxis;
            chart1.ChartAreas["main"].AxisY.Title = r.yAxis;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
