namespace tpj_power2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.modify_old_report = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.add_new_report = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ppt_test01DataSet = new tpj_power2.ppt_test01DataSet();
            this.testtable02BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.test_table02TableAdapter = new tpj_power2.ppt_test01DataSetTableAdapters.test_table02TableAdapter();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ppttest01DataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ppt_test01DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testtable02BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppttest01DataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "All reports";
            // 
            // modify_old_report
            // 
            this.modify_old_report.Location = new System.Drawing.Point(100, 358);
            this.modify_old_report.Name = "modify_old_report";
            this.modify_old_report.Size = new System.Drawing.Size(75, 23);
            this.modify_old_report.TabIndex = 2;
            this.modify_old_report.Text = "Modify";
            this.modify_old_report.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(181, 358);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // add_new_report
            // 
            this.add_new_report.Location = new System.Drawing.Point(19, 358);
            this.add_new_report.Name = "add_new_report";
            this.add_new_report.Size = new System.Drawing.Size(75, 23);
            this.add_new_report.TabIndex = 5;
            this.add_new_report.Text = "New";
            this.add_new_report.UseVisualStyleBackColor = true;
            this.add_new_report.Click += new System.EventHandler(this.button5_Click);
            // 
            // listBox1
            // 
            this.listBox1.DataSource = this.testtable02BindingSource;
            this.listBox1.DisplayMember = "colona_1";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 63);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(394, 290);
            this.listBox1.TabIndex = 6;
            // 
            // ppt_test01DataSet
            // 
            this.ppt_test01DataSet.DataSetName = "ppt_test01DataSet";
            this.ppt_test01DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // testtable02BindingSource
            // 
            this.testtable02BindingSource.DataMember = "test_table02";
            this.testtable02BindingSource.DataSource = this.ppt_test01DataSet;
            // 
            // test_table02TableAdapter
            // 
            this.test_table02TableAdapter.ClearBeforeFill = true;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.DataSource = this.ppttest01DataSetBindingSource;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(592, 47);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(300, 300);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // ppttest01DataSetBindingSource
            // 
            this.ppttest01DataSetBindingSource.DataSource = this.ppt_test01DataSet;
            this.ppttest01DataSetBindingSource.Position = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 393);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.add_new_report);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.modify_old_report);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "PowerPoint reporter";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ppt_test01DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testtable02BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppttest01DataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button modify_old_report;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button add_new_report;
        private System.Windows.Forms.ListBox listBox1;
        private ppt_test01DataSet ppt_test01DataSet;
        private System.Windows.Forms.BindingSource testtable02BindingSource;
        private ppt_test01DataSetTableAdapters.test_table02TableAdapter test_table02TableAdapter;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.BindingSource ppttest01DataSetBindingSource;
    }
}

