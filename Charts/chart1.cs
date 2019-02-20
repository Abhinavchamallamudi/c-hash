/* Name : Aravind Muvva and Abhinav Chamallamudi
 * ZID : Z1835959 and Z1826541
 * Course : CSCI 504
 * Due date : 11-29-2018
 * Purpose : functionality of the first graph
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
    public partial class chart1 : Form
    {
        public chart1()
        {
            InitializeComponent();
        }

        private void lineChart_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Load(object sender, EventArgs e)
        {
            loadChart();
        }

        public void loadChart()
        {
            graph r = Form1.graph1; //This is reference copy to save us from calling it by it's whole name & reference
            foreach (series s in r.seriesList) //Going through every series in our object
            {
                lineChart.Series.Add(s.name); //Add new series to our chart
                lineChart.Series[lineChart.Series.Count - 1].ChartType = SeriesChartType.Line; //Change type of our newly added series
                int y = 0; //This is counting how many times we passed through foreach loop
                foreach (string ss in s.xPoints) //Going through each xPoint 
                {
                    lineChart.Series[lineChart.Series.Count - 1].Points.AddXY(ss, s.yPoints[y]); //Adding new point to our newly added series
                    y++;
                }
            }
            lineChart.Titles.Add(r.title); //Title of a chart
            lineChart.ChartAreas["main"].AxisX.Interval = 1; //We set interval to 1 so we don't miss displaying every piece of data in our file
            lineChart.ChartAreas["main"].AxisX.Title = r.xAxis; //Changing xAxis name (description)
            lineChart.ChartAreas["main"].AxisY.Title = r.yAxis; //Editing name (description) for yAxis
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
