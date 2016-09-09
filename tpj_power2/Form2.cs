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

namespace tpj_power2
{
    public partial class Form2 : Form
    {
        //<Control> data_controls;
        int total_chapter_num = 0;
        Button add_chapter_button;
        Button add_source_button;
        public Form1 mainForm { get; set; }
        public Form2()
        {
            InitializeComponent();
            add_chapter_button = new Button();
            add_chapter_button.Text = "Add chapter";
            add_chapter_button.Name = "addchapter_1";
            add_chapter_button.Click += new EventHandler(this.add_chapter_button_Click);
            add_source_button = new Button();
            add_source_button.Name = "addsource_0";
            add_source_button.Text = "Add source";
            add_source_button.Click += new EventHandler(this.add_source_button_Click);

        }


        private void chapter_panel_Click(object sender, EventArgs e)
        {
            add_chapter_button.Parent.Controls.Remove(add_chapter_button);
            add_source_button.Parent.Controls.Remove(add_source_button);
            FlowLayoutPanel pom_sender = (FlowLayoutPanel)sender;
            add_chapter_button.Name = "addchapter_" + (get_control_number(pom_sender) + 1 ).ToString();
            pom_sender.Controls.Add(add_chapter_button);
            pom_sender.Controls.Add(add_source_button);

        }

        private void add_chapter_button_Click(object sender, EventArgs e)
        {
            increment_control_number(add_chapter_button);

            FlowLayoutPanel new_chapter_panel = new FlowLayoutPanel();
             new_chapter_panel.AutoSize = true;
            //new_chapter_panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            new_chapter_panel.FlowDirection = FlowDirection.TopDown;
            new_chapter_panel.Click += new EventHandler(this.chapter_panel_Click);
            new_chapter_panel.Name = "panel_" + (get_control_number(add_chapter_button) - 1).ToString();
            TextBox new_chapter_text = new TextBox();
            new_chapter_text.Name = "chapter_" + (get_control_number(add_chapter_button) - 1).ToString();
            new_chapter_text.Width = 200;
            Label new_chapter_label = new Label();
            new_chapter_label.Name = "chapterlabel_" + (get_control_number(add_chapter_button) - 1).ToString();
            new_chapter_label.Text = "Chapter " + (get_control_number(add_chapter_button) - 1).ToString();

            new_chapter_panel.Controls.Add(new_chapter_label);
            new_chapter_panel.Controls.Add(new_chapter_text);
            new_chapter_panel.Controls.Add(add_chapter_button);
            new_chapter_panel.Controls.Add(add_source_button);
  
            Queue<Control> pom = new Queue<Control>();

           
            if (total_chapter_num == 0)
            {
                flowLayoutPanel1.Controls.Remove(add_chapter_button);
                flowLayoutPanel1.Controls.Remove(add_source_button);
                flowLayoutPanel1.Controls.Add(new_chapter_panel);
                total_chapter_num += 1;

            }
            else
            {
                total_chapter_num += 1;

                foreach (Control control in flowLayoutPanel1.Controls) {

                    if (get_control_number(control) == get_control_number(new_chapter_panel))
                    {
                        pom.Enqueue(new_chapter_panel);
                    }

                    if (get_control_number(control) >= get_control_number(new_chapter_panel))
                    {
                        increment_control_number(control);
                        foreach (Control subcontrol in control.Controls)
                        {
                            increment_control_number(subcontrol);
                        }
                    }
                   
                    pom.Enqueue(control);
                }

                if (total_chapter_num == get_control_number(new_chapter_panel))
                {
                    pom.Enqueue(new_chapter_panel);

                }

                flowLayoutPanel1.Controls.Clear();

                while (!(pom.Count == 0))
                {
                    flowLayoutPanel1.Controls.Add(pom.Dequeue());
                }
            }

           



        }

        private int get_control_number(Control c)
        {
            return Convert.ToInt16(c.Name.Split('_')[1]);
        }

        private void increment_control_number(Control c)
        {
            if (c.Name.Contains("_"))
            {
                String cname = c.Name.Split('_')[0];
                int cnum = Convert.ToInt16(c.Name.Split('_')[1]) + 1;
                c.Name = cname + "_" + cnum.ToString();
                if (cname == "chapterlabel") c.Text = "Chapter " + cnum.ToString();
                if (cname == "addchapter") c.Text = "Add chapter " + cnum.ToString();
            }
        }

        private void decrement_control_number(Control c)
        {
            String cname = c.Name.Split('_')[0];
            int cnum = Convert.ToInt16(c.Name.Split('_')[1]) - 1;
            c.Name = cname + cnum.ToString();
            if (cname == "chapterlabel") c.Text = "Chapter " + cnum.ToString();
        }

        private void browse_source_button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Source folder";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string sSelectedPath = fbd.SelectedPath;
                ((Control)sender).Parent.GetNextControl(((Control)sender), false).Text = sSelectedPath;
            }

        }

        private void add_source_button_Click(object sender, EventArgs e)
        {
            Control sparent = add_source_button.Parent;
            sparent.Controls.Remove(add_chapter_button);
            sparent.Controls.Remove(add_source_button);
            TextBox pom_textbox = new TextBox();
            pom_textbox.Width = sparent.Width - 20;
            pom_textbox.TextAlign = HorizontalAlignment.Right;
            sparent.Controls.Add(pom_textbox);
            Button pom_button = new Button();
            pom_button.Text = "Browse source";
            pom_button.Click += new EventHandler(this.browse_source_button_Click);
            sparent.Controls.Add(pom_button);
            sparent.Controls.Add(add_chapter_button);
            sparent.Controls.Add(add_source_button);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Add(add_chapter_button);
            flowLayoutPanel1.Controls.Add(add_source_button);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Save to";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string sSelectedPath = fbd.SelectedPath;
                ((Control)sender).Parent.GetNextControl(((Control)sender), false).Text = sSelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "PowerPoint slides (*.pptx)|*.pptx";
            choofdlog.FilterIndex = 1;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                ((Control)sender).Parent.GetNextControl(((Control)sender), false).Text = sFileName;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Enabled) comboBox1.Enabled = false;
            else comboBox1.Enabled = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (monthCalendar1.Enabled) monthCalendar1.Enabled = false;
            else monthCalendar1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e) // ADD BUTTON
        {

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=Database1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            conn.Open();

            string insert_report = "insert into main([report], [active], [presentation_title], [period], [start], [save_to_path], [design_template_path]) values ( '" + textBox1.Text + "' , 1, '" + textBox3.Text + "', " + comboBox1.SelectedItem + ", '" + monthCalendar1.SelectionStart + "','" + textBox2.Text + "','" + textBox4.Text + "' )";
            SqlCommand sc = new SqlCommand(insert_report, conn);
            object o = sc.ExecuteNonQuery();

            int chapter_number = 0;
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                chapter_number += 1;
                int source_num = -1;
                foreach (Control cc in c.Controls)
                {
                    if (cc.Name.Contains("_"))
                    {
                        String cname = cc.Name.Split('_')[0];
                        if (cname == "chapter")
                        {
                            string insert_chapter = "insert into chapter([report], [chapter], [chapter_name]) values ( '" + textBox1.Text + "' ," + chapter_number.ToString() + " , '" + cc.Text + "' )";
                            SqlCommand scc = new SqlCommand(insert_chapter, conn);
                            object o2 = scc.ExecuteNonQuery();
                            source_num = 1;
                        }
                    }
                    {
                        if (cc.GetType().ToString() == "System.Windows.Forms.TextBox")
                        {
                            string insert_source = "insert into [script] values ( '" + textBox1.Text + "' ," + chapter_number.ToString() + " , " + source_num.ToString() + ", '" + cc.Text + "' )";
                            SqlCommand sccc = new SqlCommand(insert_source, conn);
                            object o2 = sccc.ExecuteNonQuery();
                            source_num++;
                        }
                    }
                 }
            }


            conn.Close();
            mainForm.refresh_table();
            this.Close();
        }
    }
}
