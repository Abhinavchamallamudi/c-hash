using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceCowboy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormClosed +=
               new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // This would be an excellent time to dispose of your temporary resources
            // Or save things you want to remember about this program's execution
        }

        private void NewGame_Button_Click(object sender, EventArgs e)
        {
            (new Form2()).Show(); this.Hide();
        }
    }
}
