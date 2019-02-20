/* Name : Aravind Muvva and Abhinav Chamallamudi
 * ZID : Z1835959 and Z1826541
 * Course : CSCI 504
 * Due date : 11-29-2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts
{
    public class graph
    {
        public string path;
        public string xAxis;
        public string yAxis;
        public string title;

        public List<series> seriesList = new List<series>();

        public graph(string _path) {
            path = _path;
        }
    }


    public class series{
        public string name;
        public List<string> xPoints = new List<string>();
        public List<double> yPoints = new List<double>();
    }
}
