using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tpj_power2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e) // new report
        {
            var newReportForm = new Form2();
            newReportForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ppt_test01DataSet.test_table02' table. You can move, or remove it, as needed.
            this.test_table02TableAdapter.Fill(this.ppt_test01DataSet.test_table02);

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
