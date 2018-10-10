namespace ТВиМС
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.displayTable = new System.Windows.Forms.DataGridView();
            this.button_Analysis = new System.Windows.Forms.Button();
            this.button_Clear = new System.Windows.Forms.Button();
            this.groupBox_show = new System.Windows.Forms.GroupBox();
            this.radioButton_Calculations = new System.Windows.Forms.RadioButton();
            this.radioButton_barСhart = new System.Windows.Forms.RadioButton();
            this.radioButton_empFunc = new System.Windows.Forms.RadioButton();
            this.radioButton_Table = new System.Windows.Forms.RadioButton();
            this.rowsList = new System.Windows.Forms.CheckedListBox();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabTable = new System.Windows.Forms.TabPage();
            this.dataGridTable = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridtab2 = new System.Windows.Forms.DataGridView();
            this.chart_empFunc = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_barChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.displayTable)).BeginInit();
            this.groupBox_show.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTable)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridtab2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_empFunc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_barChart)).BeginInit();
            this.SuspendLayout();
            // 
            // displayTable
            // 
            this.displayTable.AllowUserToAddRows = false;
            this.displayTable.AllowUserToDeleteRows = false;
            this.displayTable.AllowUserToResizeColumns = false;
            this.displayTable.AllowUserToResizeRows = false;
            this.displayTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.displayTable.BackgroundColor = System.Drawing.Color.White;
            this.displayTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.displayTable.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.displayTable.Location = new System.Drawing.Point(12, 12);
            this.displayTable.Name = "displayTable";
            this.displayTable.ReadOnly = true;
            this.displayTable.RowHeadersVisible = false;
            this.displayTable.Size = new System.Drawing.Size(822, 642);
            this.displayTable.TabIndex = 0;
            // 
            // button_Analysis
            // 
            this.button_Analysis.Location = new System.Drawing.Point(839, 549);
            this.button_Analysis.Name = "button_Analysis";
            this.button_Analysis.Size = new System.Drawing.Size(157, 50);
            this.button_Analysis.TabIndex = 2;
            this.button_Analysis.Text = "Анализировать";
            this.button_Analysis.UseVisualStyleBackColor = true;
            this.button_Analysis.Click += new System.EventHandler(this.button_Analysis_Click);
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(839, 604);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(157, 50);
            this.button_Clear.TabIndex = 3;
            this.button_Clear.Text = "Очистить";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // groupBox_show
            // 
            this.groupBox_show.Controls.Add(this.radioButton_Calculations);
            this.groupBox_show.Controls.Add(this.radioButton_barСhart);
            this.groupBox_show.Controls.Add(this.radioButton_empFunc);
            this.groupBox_show.Controls.Add(this.radioButton_Table);
            this.groupBox_show.Location = new System.Drawing.Point(840, 127);
            this.groupBox_show.Name = "groupBox_show";
            this.groupBox_show.Size = new System.Drawing.Size(156, 120);
            this.groupBox_show.TabIndex = 4;
            this.groupBox_show.TabStop = false;
            this.groupBox_show.Text = "Меню:";
            // 
            // radioButton_Calculations
            // 
            this.radioButton_Calculations.AutoSize = true;
            this.radioButton_Calculations.Location = new System.Drawing.Point(6, 91);
            this.radioButton_Calculations.Name = "radioButton_Calculations";
            this.radioButton_Calculations.Size = new System.Drawing.Size(68, 17);
            this.radioButton_Calculations.TabIndex = 3;
            this.radioButton_Calculations.Text = "Расчеты";
            this.radioButton_Calculations.UseVisualStyleBackColor = true;
            this.radioButton_Calculations.CheckedChanged += new System.EventHandler(this.radioButton_Calculations_CheckedChanged);
            // 
            // radioButton_barСhart
            // 
            this.radioButton_barСhart.AutoSize = true;
            this.radioButton_barСhart.Location = new System.Drawing.Point(7, 68);
            this.radioButton_barСhart.Name = "radioButton_barСhart";
            this.radioButton_barСhart.Size = new System.Drawing.Size(83, 17);
            this.radioButton_barСhart.TabIndex = 2;
            this.radioButton_barСhart.Text = "График №2";
            this.radioButton_barСhart.UseVisualStyleBackColor = true;
            this.radioButton_barСhart.CheckedChanged += new System.EventHandler(this.radioButton_barСhart_CheckedChanged);
            // 
            // radioButton_empFunc
            // 
            this.radioButton_empFunc.AutoSize = true;
            this.radioButton_empFunc.Location = new System.Drawing.Point(7, 44);
            this.radioButton_empFunc.Name = "radioButton_empFunc";
            this.radioButton_empFunc.Size = new System.Drawing.Size(83, 17);
            this.radioButton_empFunc.TabIndex = 1;
            this.radioButton_empFunc.Text = "График №1";
            this.radioButton_empFunc.UseVisualStyleBackColor = true;
            this.radioButton_empFunc.CheckedChanged += new System.EventHandler(this.radioButton_empFunc_CheckedChanged);
            // 
            // radioButton_Table
            // 
            this.radioButton_Table.AutoSize = true;
            this.radioButton_Table.Location = new System.Drawing.Point(7, 20);
            this.radioButton_Table.Name = "radioButton_Table";
            this.radioButton_Table.Size = new System.Drawing.Size(68, 17);
            this.radioButton_Table.TabIndex = 0;
            this.radioButton_Table.Text = "Таблица";
            this.radioButton_Table.UseVisualStyleBackColor = true;
            this.radioButton_Table.CheckedChanged += new System.EventHandler(this.radioButton_Table_CheckedChanged_1);
            // 
            // rowsList
            // 
            this.rowsList.FormattingEnabled = true;
            this.rowsList.Items.AddRange(new object[] {
            ""});
            this.rowsList.Location = new System.Drawing.Point(839, 12);
            this.rowsList.Name = "rowsList";
            this.rowsList.Size = new System.Drawing.Size(156, 109);
            this.rowsList.TabIndex = 5;
            this.rowsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.rowsList_ItemCheck);
            // 
            // tabs
            // 
            this.tabs.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabs.Controls.Add(this.tabTable);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Multiline = true;
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(822, 642);
            this.tabs.TabIndex = 8;
            // 
            // tabTable
            // 
            this.tabTable.Controls.Add(this.dataGridTable);
            this.tabTable.Location = new System.Drawing.Point(4, 4);
            this.tabTable.Name = "tabTable";
            this.tabTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabTable.Size = new System.Drawing.Size(795, 634);
            this.tabTable.TabIndex = 0;
            this.tabTable.Text = "Расчеты";
            this.tabTable.UseVisualStyleBackColor = true;
            // 
            // dataGridTable
            // 
            this.dataGridTable.AllowUserToAddRows = false;
            this.dataGridTable.AllowUserToDeleteRows = false;
            this.dataGridTable.AllowUserToResizeColumns = false;
            this.dataGridTable.AllowUserToResizeRows = false;
            this.dataGridTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridTable.BackgroundColor = System.Drawing.Color.White;
            this.dataGridTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTable.ColumnHeadersVisible = false;
            this.dataGridTable.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dataGridTable.Location = new System.Drawing.Point(0, 0);
            this.dataGridTable.Name = "dataGridTable";
            this.dataGridTable.ReadOnly = true;
            this.dataGridTable.RowHeadersVisible = false;
            this.dataGridTable.Size = new System.Drawing.Size(795, 634);
            this.dataGridTable.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridtab2);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(795, 634);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Расчеты№2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridtab2
            // 
            this.dataGridtab2.AllowUserToAddRows = false;
            this.dataGridtab2.AllowUserToDeleteRows = false;
            this.dataGridtab2.AllowUserToResizeRows = false;
            this.dataGridtab2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridtab2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridtab2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridtab2.Location = new System.Drawing.Point(-4, 0);
            this.dataGridtab2.Name = "dataGridtab2";
            this.dataGridtab2.ReadOnly = true;
            this.dataGridtab2.RowHeadersVisible = false;
            this.dataGridtab2.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridtab2.RowTemplate.Height = 30;
            this.dataGridtab2.RowTemplate.ReadOnly = true;
            this.dataGridtab2.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridtab2.Size = new System.Drawing.Size(799, 634);
            this.dataGridtab2.TabIndex = 2;
            // 
            // chart_empFunc
            // 
            this.chart_empFunc.BorderlineColor = System.Drawing.Color.Black;
            this.chart_empFunc.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.chart_empFunc.ChartAreas.Add(chartArea1);
            this.chart_empFunc.Location = new System.Drawing.Point(11, 12);
            this.chart_empFunc.Name = "chart_empFunc";
            this.chart_empFunc.Size = new System.Drawing.Size(823, 642);
            this.chart_empFunc.TabIndex = 9;
            this.chart_empFunc.Text = "chart1";
            // 
            // chart_barChart
            // 
            this.chart_barChart.BorderlineColor = System.Drawing.Color.Black;
            this.chart_barChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.Name = "ChartArea1";
            this.chart_barChart.ChartAreas.Add(chartArea2);
            this.chart_barChart.Location = new System.Drawing.Point(11, 12);
            this.chart_barChart.Name = "chart_barChart";
            this.chart_barChart.Size = new System.Drawing.Size(823, 642);
            this.chart_barChart.TabIndex = 10;
            this.chart_barChart.Text = "chart1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1002, 666);
            this.Controls.Add(this.chart_barChart);
            this.Controls.Add(this.chart_empFunc);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.rowsList);
            this.Controls.Add(this.groupBox_show);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.button_Analysis);
            this.Controls.Add(this.displayTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ТВиМС";
            ((System.ComponentModel.ISupportInitialize)(this.displayTable)).EndInit();
            this.groupBox_show.ResumeLayout(false);
            this.groupBox_show.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTable)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridtab2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_empFunc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_barChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView displayTable;
        private System.Windows.Forms.Button button_Analysis;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.GroupBox groupBox_show;
        private System.Windows.Forms.RadioButton radioButton_Calculations;
        private System.Windows.Forms.RadioButton radioButton_barСhart;
        private System.Windows.Forms.RadioButton radioButton_empFunc;
        private System.Windows.Forms.RadioButton radioButton_Table;
        private System.Windows.Forms.CheckedListBox rowsList;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabTable;
        private System.Windows.Forms.DataGridView dataGridTable;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridtab2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_empFunc;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_barChart;
    }
}

