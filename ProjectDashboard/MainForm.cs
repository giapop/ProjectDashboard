using ProjectDashboard.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDashboard
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        { 
            InitializeComponent();
            textBox1.Text = "Aceasta este o aplicatie ce are in vedere gestiunea studentilor!";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new SecondForm();
            form.Show();
        }

      

       
    }
}
