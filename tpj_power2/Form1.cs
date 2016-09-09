using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using UserSettingsDemo.Properties;
using Microsoft.Win32.TaskScheduler;

namespace tpj_power2
{
    public partial class Form1 : Form
    {
        private string task_name = "POWAH";

        public Form1()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e) // new report
        {
            var newReportForm = new Form2();
            newReportForm.mainForm = this;
            newReportForm.Show();
        }

        public void refresh_table()
        {
            this.mainTableAdapter.Fill(this.database1DataSet.main);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.main' table. You can move, or remove it, as needed.
            this.mainTableAdapter.Fill(this.database1DataSet.main);
            // TODO: This line of code loads data into the 'ppt_test01DataSet.test_table02' table. You can move, or remove it, as needed.
            //this.test_table02TableAdapter.Fill(this.ppt_test01DataSet.test_table02);

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // DELETE
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                SqlConnection conn = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=Database1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                conn.Open();

                string str = item.Cells[0].Value.ToString();
                string delete_report = "delete from main where report ='" + str + "'";
                string delete_chapter = "delete from chapter where report ='" + str + "'";
                string delete_script = "delete from script where report ='" + str + "'";
                SqlCommand sc1 = new SqlCommand(delete_report, conn);
                SqlCommand sc2 = new SqlCommand(delete_chapter, conn);
                SqlCommand sc3 = new SqlCommand(delete_script, conn);
                object o1 = sc1.ExecuteNonQuery();
                object o2 = sc2.ExecuteNonQuery();
                object o3 = sc3.ExecuteNonQuery();
                refresh_table();
            }
        }


        private static void CheckFirstRun()
        {
            if (Properties.Settings.Default.FirstRun)
            {
                MessageBox.Show(
                    "First run");
                Properties.Settings.Default.FirstRun = false;
                Properties.Settings.Default.Save();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            CheckFirstRun();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // ACTIVATE
        {
            if (label2.Text == "OFF")
            {
                using (TaskService ts = new TaskService())
                {
                    // Create a new task definition and assign properties
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "main task";
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;

                    DailyTrigger dt = (DailyTrigger)td.Triggers.Add(new DailyTrigger(1));
                    dt.Repetition.Duration = TimeSpan.FromHours(4);
                    dt.Repetition.Interval = TimeSpan.FromHours(1);

                    dt.StartBoundary = DateTime.Now + TimeSpan.FromHours(2);

                    // Add an action that will launch Notepad whenever the trigger fires
                    td.Actions.Add(new ExecAction("generate_ppt.exe", null, null));

                    // Register the task in the root folder
                    ts.RootFolder.RegisterTaskDefinition(task_name, td);

                    label2.Text = "ON";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) // STOP
        {
            if (label2.Text == "ON")
            {
                TaskService ts = new TaskService();
                ts.RootFolder.DeleteTask(task_name);
                label2.Text = "OFF";
            }

        }
    }
}
