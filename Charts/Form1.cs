/* Name : Aravind Muvva and Abhinav Chamallamudi
 * ZID : Z1835959 and Z1826541
 * Course : CSCI 504
 * Due date : 11-29-2018
 * Purpose : functionality for the intial form
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Charts
{
    public partial class Form1 : Form
    {
        public static graph graph1, graph2, graph3, graph4; //They are static because we need to access them from other forms

        public Form1()
        {
            InitializeComponent();
            graph1 = new graph("chart1"); //Initializing graph objects
            graph2 = new graph("chart2");
            graph3 = new graph("chart3");
            graph4 = new graph("chart4");
            loadFromFileIntoObject(graph1); //Calling function that is reading values from the file and it's storing them in object that we are passing here
            loadFromFileIntoObject(graph2);
            loadFromFileIntoObject(graph3);
            loadFromFileIntoObject(graph4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); //Closing form
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            this.Hide(); //Hiding "portal" form
            chart1 c = new chart1(); //Creating new object from our form1
            c.ShowDialog(); //Displaying form1
            this.Show(); //When we close form1 it's gonna execute this line of code which is line to show back our "portal" form
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            this.Hide();
            chart2 c = new chart2();
            c.ShowDialog();
            this.Show();
        }

        private void chart3_Click(object sender, EventArgs e)
        {
            this.Hide();
            chart3 c = new chart3();
            c.ShowDialog();
            this.Show();
        }

        private void chart4_Click(object sender, EventArgs e)
        {
            this.Hide();
            chart4 c = new chart4();
            c.ShowDialog();
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void loadFromFileIntoObject(graph o) {
            int counter = 0; //Counter for lines
            string line; //Current line text
            int emptyLines = 0; //Passed empty lines
            int currentY = 0; //Current Y value index

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "ChartData", o.path, "data.txt"));
            while ((line = file.ReadLine()) != null)
            {
                if (counter == 0) { //First line of doc is title
                    o.title = line; //Setting title of our chart
                    counter++; //Adding counter for lines read before executing continue
                    continue;
                }
                else if (counter == 1) //Second line is name of x axis (if it does not exist type "." in file)
                {
                    o.xAxis = line; //Editing name (description) for xAxis
                    counter++;
                    continue;
                }
                else if (counter == 2) { //Third line is name of y axis (if it does not exist type "." in file)
                    o.yAxis = line; //Editing name (description) for zAxis
                    counter++;
                    continue;
                }
                if (line == "") { //Checking if line is empty
                    if (emptyLines > 1) { //If we are parsing Y values blocks and it's empty line, reset index counter
                        currentY = 0;
                    }
                    counter++;
                    emptyLines++;
                    continue;
                }
                if (emptyLines == 0) //Parsing series
                {
                    series n = new series();
                    n.name = line;
                    o.seriesList.Add(n);
                }
                else if (emptyLines == 1) //Parsing Y values
                {
                    foreach (series s in o.seriesList)
                    {
                        s.xPoints.Add(line); //Adding our line name to xPoints list
                    }
                }
                else { //Parsing X values
                    o.seriesList[currentY].yPoints.Add(Convert.ToDouble(line)); //Converting value to double before adding it to the yPoints list
                    currentY++;
                }
                counter++;
            }
            file.Close();

        }
    }
}
