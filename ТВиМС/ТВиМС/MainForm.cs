using CognitioConsulting.Numerics;
using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Instruments.Data;
using static Instruments.Extracter;
using static Instruments.Handler;

namespace ТВиМС
{
    public partial class MainForm : Form
    {
        private static double[] hisqr =
        {
            0,
            3.8415, 5.99146, 7.81473, 9.48773, 11.07050, 12.59159, 14.06714,
            15.50731, 16.91898, 18.30704, 19.67514, 21.02607, 22.36203, 23.68479, 24.99579,
            26.29623, 27.58711, 28.86930, 30.14353, 31.41043, 32.67057, 33.92444, 35.17246,
            36.41503, 37.65248, 38.88514, 40.11327, 41.33714, 42.55697, 43.77297
        };

        public MainForm()
        {
            Extract("D2_v3.csv");

            InitializeComponent(); 
            rowsList.Items.Clear();

            rowsList.Items.Add("ID Штатов");
            rowsList.Items.Add("Количество смертей");
            rowsList.Items.Add("Смертность");

            rowsList.SetItemCheckState(0, CheckState.Checked);
            radioButton_Table.Checked = true;
        }

        private void rowsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked && sender is CheckedListBox list)
                foreach (int index in list.CheckedIndices)
                    if (index != e.Index)
                        list.SetItemChecked(index, false);
        }

        private void button_Analysis_Click(object sender, EventArgs e)
        {
            if (rowsList.CheckedIndices.Count > 0)
            {
                var checkedIndex = rowsList.CheckedIndices[0];
                ClearFields();

                switch (checkedIndex)
                {
                    case 0:
                        ShowDiscreteData(IdDescreteRow);
                        break;
                    case 1:
                        ShowIntervalData(DeathsIntervalRow);
                        break;
                    case 2:
                        ShowIntervalData(DeathRateIntervalRow);
                        break;
                    default:
                        MessageBox.Show("Не выбраны данные для обработки!");
                        break;
                }
            }
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            displayTable.Rows.Clear();
            displayTable.Columns.Clear();

            var table = (DataGridView)tabTable.Controls["dataGridTable"];
            table.Rows.Clear();
            table.Columns.Clear();

            table = (DataGridView)tabPage2.Controls["dataGridtab2"];
            table.Rows.Clear();
            table.Columns.Clear();

            tabs.TabPages.RemoveByKey("tabPage3");
            tabs.TabPages.RemoveByKey("tabPage4");
            tabPage2.Text = "Расчеты№2";

            chart_barChart.Series.Clear();
            chart_barChart.ChartAreas.Clear();
            chart_empFunc.Series.Clear();
            chart_empFunc.ChartAreas.Clear();
        }

        private void radioButton_Table_CheckedChanged_1(object sender, EventArgs e)
        {
            chart_barChart.Hide();
            chart_empFunc.Hide();
            tabs.Hide();
            displayTable.Show();
        }

        private void radioButton_empFunc_CheckedChanged(object sender, EventArgs e)
        {
            chart_barChart.Hide();
            chart_empFunc.Show();
            tabs.Hide();
            displayTable.Hide();
        }

        private void radioButton_barСhart_CheckedChanged(object sender, EventArgs e)
        {
            chart_barChart.Show();
            chart_empFunc.Hide();
            tabs.Hide();
            displayTable.Hide();
        }

        private void radioButton_Calculations_CheckedChanged(object sender, EventArgs e)
        {
            chart_barChart.Hide();
            chart_empFunc.Hide();
            displayTable.Hide();
            tabs.Show();
        }

        private void ShowDiscreteData(byte[] descreteRow)
        {
            float[,] table = GetDiscreteTable(descreteRow);
            ShowDescreteTable(table);
            ShowEmpiricFunctionForDescreteRows(table, rowsList.CheckedItems[0].ToString());
            ShowDiscreteGraph(table, rowsList.CheckedItems[0].ToString());
            ShowDescreteCalculations(descreteRow);
        }

        private void ShowIntervalData(int[] intervalRow)
        {
            var intervalsNumber = GetIntervalsNumber(intervalRow);
            var intervalStep = GetIntervalsStep(intervalRow);
            float[,] table = GetIntervalTable(intervalRow, intervalStep, intervalsNumber);
            ShowIntervalTable(table);
            ShowEmpiricFunctionForIntervalRows(table, intervalStep, rowsList.CheckedItems[0].ToString());
            ShowIntervalGraph(table, rowsList.CheckedItems[0].ToString());
            ShowIntervalCalculations(intervalRow);
        }

        private void ShowIntervalTable(float[,] table)
        {
            displayTable.Columns.Add("Interval", "Интервал");
            displayTable.Columns.Add("Frequency", "Частота");
            displayTable.Columns.Add("RelFrequency", "Относительная частота");
            displayTable.Columns.Add("cumulativeFrequency", "Накопленная частота");
            displayTable.Columns.Add("midleInterval", "Середина интервала");

            for (int i = 0; i < table.GetLength(1); i++)
            {
                displayTable.Rows.Add();
                displayTable.Rows[i].Cells[0].Value = table[0, i] + " - " + table[1, i];
                displayTable.Rows[i].Cells[1].Value = table[2, i];
                displayTable.Rows[i].Cells[2].Value = table[3, i];
                displayTable.Rows[i].Cells[3].Value = table[4, i];
                displayTable.Rows[i].Cells[4].Value = table[5, i];
            }
        }

        private void ShowEmpiricFunctionForIntervalRows(float[,] table, int intervalStep, string name)
        {
            chart_empFunc.Series.Add(name);
            
            chart_empFunc.Series[name].ChartType = SeriesChartType.Line;
            chart_empFunc.Series[name].IsXValueIndexed = true;
            chart_empFunc.Series[name].Color = Color.Crimson;

            chart_empFunc.ChartAreas.Add("area");

            chart_empFunc.ChartAreas[0].AxisX.Interval = 1;
            chart_empFunc.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chart_empFunc.ChartAreas[0].AxisX.LineColor = Color.Gray;
            chart_empFunc.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.AliceBlue;
            chart_empFunc.ChartAreas[0].AxisX.Title = name + ", середины интервалов";

            chart_empFunc.ChartAreas[0].AxisY.LineColor = Color.Gray;
            chart_empFunc.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.AliceBlue;
            chart_empFunc.ChartAreas[0].AxisY.Title = "Относительная частота";
            chart_empFunc.ChartAreas[0].AxisY.Maximum = 1.1;
            chart_empFunc.ChartAreas[0].AxisY.Minimum = -0.1;
            chart_empFunc.ChartAreas[0].AxisY.Interval = 0.1;

            float cumulativeFrequency = 0;
            int pointCounter = 0;

            for (int i = -3; i < table.GetLength(1) + 3; i++)
            {
                if (i < 0)
                {
                    chart_empFunc.Series[name].Points.AddXY(i + 1, cumulativeFrequency);
                    //pointCounter++;
                    chart_empFunc.Series[name].Points[pointCounter++].AxisLabel = (intervalStep * (i + 1)).ToString("F1");
                }
                else
                {
                    if (i >= table.GetLength(1))
                    {
                        cumulativeFrequency = 1;
                        chart_empFunc.Series[name].Points.AddXY(i + 1, cumulativeFrequency);
                        chart_empFunc.Series[name].Points[pointCounter++].AxisLabel = (intervalStep * (i + 1)).ToString("F1");
                    }
                    else
                    {
                        cumulativeFrequency += table[3, i];
                        chart_empFunc.Series[name].Points.AddXY(i + 1, cumulativeFrequency);
                        chart_empFunc.Series[name].Points[pointCounter].Label = cumulativeFrequency.ToString("F2");
                        chart_empFunc.Series[name].Points[pointCounter++].AxisLabel = table[5, i].ToString("F1");
                    }
                }
            }
        }

        private void ShowDescreteTable(float[,] table)
        {
            displayTable.Columns.Add("Value", "Значение");
            displayTable.Columns.Add("State", "Штат");
            displayTable.Columns.Add("Frequency", "Частота");
            displayTable.Columns.Add("RelFrequency", "Относительная частота");
            displayTable.Columns.Add("cumulativeFrequency", "Накопленная частота");

            for (int i = 0; i < table.GetLength(1); i++)
            {
                displayTable.Rows.Add();
                displayTable.Rows[i].Cells[0].Value = table[0, i];
                States.TryGetValue((byte) table[0, i], out string state);
                displayTable.Rows[i].Cells[1].Value = state;
                displayTable.Rows[i].Cells[2].Value = table[1, i];
                displayTable.Rows[i].Cells[3].Value = table[2, i];
                displayTable.Rows[i].Cells[4].Value = table[3, i];
            }
        }

        private void ShowEmpiricFunctionForDescreteRows(float[,] table, string name)
        {
            chart_empFunc.Series.Add(name);
            
            chart_empFunc.Series[name].ChartType = SeriesChartType.StepLine;
            chart_empFunc.Series[name].IsXValueIndexed = true;
            chart_empFunc.Series[name].Color = Color.Crimson;

            chart_empFunc.Series[name].SmartLabelStyle.IsMarkerOverlappingAllowed = false;
            chart_empFunc.Series[name].SmartLabelStyle.IsOverlappedHidden = false;
            chart_empFunc.Series[name].SmartLabelStyle.CalloutLineAnchorCapStyle = LineAnchorCapStyle.Diamond;
            chart_empFunc.Series[name].SmartLabelStyle.MovingDirection = LabelAlignmentStyles.TopLeft | LabelAlignmentStyles.BottomRight;

            chart_empFunc.ChartAreas.Add("area");
            chart_empFunc.ChartAreas[0].AxisX.Interval = 1;
            chart_empFunc.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chart_empFunc.ChartAreas[0].AxisX.LineColor = Color.Gray;
            chart_empFunc.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.AliceBlue;
            chart_empFunc.ChartAreas[0].AxisX.Title = name;

            chart_empFunc.ChartAreas[0].AxisY.LineColor = Color.Gray;
            chart_empFunc.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.AliceBlue;
            chart_empFunc.ChartAreas[0].AxisY.Title = "Относительная частота";
            chart_empFunc.ChartAreas[0].AxisY.Minimum = -0.1;
            chart_empFunc.ChartAreas[0].AxisY.Maximum = 1.1;
            chart_empFunc.ChartAreas[0].AxisY.Interval = 0.1;


            float cumulativeFrequency = 0;
            int pointCounter = 0;

            for (int i = -3; i < table.GetLength(1) + 3; i++)
            {
                if (i < 0)
                {
                    chart_empFunc.Series[name].Points.AddXY(i + 1, cumulativeFrequency);
                    pointCounter++;
                }
                else
                {
                    if (i >= table.GetLength(1))
                    {
                        cumulativeFrequency = 1;
                        chart_empFunc.Series[name].Points.AddXY(i + 1, cumulativeFrequency);
                    }
                    else
                    {
                        cumulativeFrequency += table[2, i];
                        chart_empFunc.Series[name].Points.AddXY(i + 1, cumulativeFrequency);
                        chart_empFunc.Series[name].Points[pointCounter++].Label = cumulativeFrequency.ToString("F2");
                    }
                }
       
            }   
        }

        private void ShowIntervalGraph(float[,] table, string name)
        {
            chart_barChart.Series.Add(name);
            chart_barChart.ChartAreas.Add("area");
            chart_barChart.Series[name].ChartType = SeriesChartType.Column;
            chart_barChart.ChartAreas[0].AxisX.Interval = 1;
            chart_barChart.Series[name].IsXValueIndexed = true;
            chart_barChart.Series[name].Color = Color.Crimson;
            chart_barChart.ChartAreas[0].AxisY.LineColor = Color.Gray;
            chart_barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart_barChart.ChartAreas[0].AxisY.Title = "Частота";
            chart_barChart.ChartAreas[0].AxisX.LineColor = Color.Gray;
            chart_barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart_barChart.ChartAreas[0].AxisX.Title = name + ", интервалы";
            for (int i = 0; i < table.GetLength(1); i++)
            {
                chart_barChart.Series[name].Points.AddXY(i + 1, table[2, i]);
                chart_barChart.Series[name].Points[i].Label = table[2, i].ToString();
                chart_barChart.Series[name].Points[i].AxisLabel = table[0, i] + " - " + table[1, i];
            }
        }

        private void ShowDiscreteGraph(float[,] table, string name)
        {
            chart_barChart.Series.Add(name);
            chart_barChart.ChartAreas.Add("area");
            chart_barChart.Series[name].ChartType = SeriesChartType.Line;
            chart_barChart.ChartAreas[0].AxisX.Interval = 1;
            chart_barChart.Series[name].IsXValueIndexed = true;
            chart_barChart.Series[name].Color = Color.Crimson;           
            chart_barChart.ChartAreas[0].AxisY.LineColor = Color.Gray;
            chart_barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart_barChart.ChartAreas[0].AxisY.Title = "Частота";
            chart_barChart.ChartAreas[0].AxisX.LineColor = Color.Gray;
            chart_barChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.AliceBlue;
            chart_barChart.ChartAreas[0].AxisX.Title = name;

            for (int i = 0; i < table.GetLength(1); i++)
            {
                chart_barChart.Series[name].Points.AddXY(i + 1, table[1, i]);
                chart_barChart.Series[name].Points[i].Label = table[1, i].ToString();
            }
        }

        private void ShowDescreteCalculations(byte[] descreteRow)
        {
            tabPage2.Text = "Пуассона";
            ShowDescreteTable(descreteRow);
            Poisson(descreteRow);
        }

        private void ShowDescreteTable(byte[] discreteRow)
        {
            var table = (DataGridView)tabTable.Controls["dataGridTable"];
            var discreteTable = GetDiscreteTable(discreteRow);
            table.Columns.Add("Characteristic", "Характеристика");
            table.Columns.Add("Value", "Значение");
            
            table.Rows.Add();
            table.Rows[0].Cells[0].Value = "Математичесоке ожидание:";
            table.Rows[0].Cells[1].Value = GetDiscreteRowExpectedValue(discreteRow);

            table.Rows.Add();
            table.Rows[1].Cells[0].Value = "Диспресия:";
            table.Rows[1].Cells[1].Value = GetDiscreteRowDispersion(discreteTable, discreteRow);

            table.Rows.Add();
            table.Rows[2].Cells[0].Value = "Среднеквадратическое отклонение:";
            table.Rows[2].Cells[1].Value = GetDiscreteRowAverageSquareDeviation(discreteTable, discreteRow);

            table.Rows.Add();
            table.Rows[3].Cells[0].Value = "Асимметрия:";
            table.Rows[3].Cells[1].Value = GetDiscreteRowAsymmetry(discreteTable, discreteRow);

            table.Rows.Add();
            table.Rows[4].Cells[0].Value = "Эксцесс:";
            table.Rows[4].Cells[1].Value = GetDiscreteRowExcess(discreteTable, discreteRow);

            table.Rows.Add();
            table.Rows[5].Cells[0].Value = "Коэффицент вариации:";
            table.Rows[5].Cells[1].Value = GetDiscreteRowVariationCoefficient(discreteTable, discreteRow);

            table.Rows.Add();
            table.Rows[6].Cells[0].Value = "Медиана:";
            table.Rows[6].Cells[1].Value = GetDiscreteRowMedian(discreteTable);

            float[] modes = GetDiscreteRowModes(discreteTable);
            table.Rows.Add();
            table.Rows[7].Cells[0].Value = "Мода:";
            for (int i = 0; i < modes.Length; i++)
            {
                table.Rows[7].Cells[1].Value += modes[i] + "; ";
            }
            
        }

        private void Poisson(byte[] discreteRow)
        {
            var descreteTable = GetDiscreteTable(discreteRow);
            float expectedValue = GetDiscreteRowExpectedValue(discreteRow);
            float result = 0f;

            var gridTable = (DataGridView)tabPage2.Controls["dataGridtab2"];
            gridTable.Columns.Add("a", "Штат");
            gridTable.Columns.Add("b", "Частота");
            gridTable.Columns.Add("c", "Теоритическая частота");
            gridTable.Columns.Add("d", "Хи-наблюдаемое");

            foreach (DataGridViewColumn column in gridTable.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            gridTable.Rows.Add(descreteTable.GetLength(1) + 3);

            BigDecimal[] P = new BigDecimal[descreteTable.GetLength(1)];
            decimal[] theoreticalFrequency = new Decimal[descreteTable.GetLength(1)];

            decimal collectionSize = (decimal)descreteTable[3, descreteTable.GetLength(1) - 1];
            for (int i = 0; i < descreteTable.GetLength(1); i++)
            {
                double x = Math.Pow(expectedValue, i) * Math.Exp(-expectedValue);
                P[i] = BigDecimal.Divide(x, Factorial(i));

                theoreticalFrequency[i] = collectionSize * (decimal)P[i];

                gridTable.Rows[i].Cells[0].Value = descreteTable[0, i];
                gridTable.Rows[i].Cells[1].Value = descreteTable[1, i];
                gridTable.Rows[i].Cells[2].Value = (double)theoreticalFrequency[i];
            }

            float Hi = 0F;

            for (int i = 0; i < descreteTable.GetLength(1); i++)
            {
                float currHi = 0;
                decimal frequency = (int)descreteTable[1, i];
                currHi = (float)(Math.Pow((double)(frequency - theoreticalFrequency[i]), 2) / (double)theoreticalFrequency[i]);
                Hi += currHi;
                gridTable.Rows[i].Cells[3].Value = currHi;
            }

            int d = descreteTable.GetLength(1) > 32 ? 30 : descreteTable.GetLength(1) - 1 - 1;
            double J = Math.Abs(Hi - d) / Math.Sqrt(2 * descreteTable.GetLength(1) + 2.4);
            double R = Math.Abs(Hi - d) / Math.Sqrt(2 * d);

            gridTable.Rows[descreteTable.GetLength(1)].Cells[0].Value = "Критерий Ястремского  (надо <= 3):";
            gridTable.Rows[descreteTable.GetLength(1)].Cells[1].Value = J;

            gridTable.Rows[descreteTable.GetLength(1) + 1].Cells[0].Value = "Критерий Романовского (надо <= 3):";
            gridTable.Rows[descreteTable.GetLength(1) + 1].Cells[1].Value = R;

            gridTable.Rows[descreteTable.GetLength(1)].Cells[2].Value = "Критерий Пирсона:";
            gridTable.Rows[descreteTable.GetLength(1)].Cells[3].Value = Hi;
            gridTable.Rows[descreteTable.GetLength(1) + 1].Cells[2].Value = "По таблице критических значений:";
            gridTable.Rows[descreteTable.GetLength(1) + 1].Cells[3].Value = hisqr[d];
            gridTable.Rows[descreteTable.GetLength(1) + 2].Cells[0].Value = "Вывод:";
            gridTable.Rows[descreteTable.GetLength(1) + 2].Cells[1].Value = J < 3 || R < 3 ? "Гипотеза подтверждена" : "Гипотеза не подтверждена";
            gridTable.Rows[descreteTable.GetLength(1) + 2].Cells[3].Value = Hi >= hisqr[d] ? "Гипотеза не подтверждена" : "Гипотеза подтверждена";

            var cell = gridTable.Rows[descreteTable.GetLength(1) + 2].Cells[3];
            cell.Style.Font = new Font(FontFamily.GenericMonospace, 8, FontStyle.Bold);
            cell = gridTable.Rows[descreteTable.GetLength(1) + 2].Cells[1];
            cell.Style.Font = new Font(FontFamily.GenericMonospace, 8, FontStyle.Bold);
        }

        private BigInteger Factorial(int value)
        {
            if (value < 2)
            {
                return 1;
            }

            BigInteger factorial = 1;
            for (int i = 2; i < value + 1; i++)
            {
                factorial *= i;
            }

            return factorial;
        }

        private void ShowIntervalCalculations(int[] intervalRow)
        {
            tabPage2.Text = "Нормальный";
            AddTab("tabPage3", "Показательный");
            AddTab("tabPage4", "Равномерное распределение");

            ShowIntervalTable(intervalRow);
            Normal(intervalRow);
            Showing(intervalRow);
            Distribution(intervalRow);
        }

        private void AddTab(string name, string text)
        {
            tabs.TabPages.Add(name, text);
            var tab = tabs.TabPages[name];
            var gridTable = new DataGridView();
            gridTable.BackgroundColor = Color.White;
            gridTable.Name = name + "table";
            gridTable.Dock = DockStyle.Fill;
            gridTable.RowHeadersVisible = false;
            gridTable.ReadOnly = true;
            gridTable.AllowUserToResizeRows = false;
            gridTable.AllowUserToResizeColumns = false;
            gridTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridTable.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridTable.RowTemplate.Height = 40;
            tab.Controls.Add(gridTable);
        }

        private void ShowIntervalTable(int[] intervalRow)
        {      
            var table = (DataGridView)tabTable.Controls["dataGridTable"];
            var intervalStep = GetIntervalsStep(intervalRow);
            var intervalNumber = GetIntervalsNumber(intervalRow);
            var intervalTable = GetIntervalTable(intervalRow, intervalStep, intervalNumber);

            table.Columns.Add("Characteristic", "Характеристика");
            table.Columns.Add("Value", "Значение");

            table.Rows.Add(17);

            table.Rows[0].Cells[0].Value = "Минимальное значение:";
            table.Rows[0].Cells[1].Value = intervalRow.Min();

            table.Rows[1].Cells[0].Value = "Максимальное значение:";
            table.Rows[1].Cells[1].Value = intervalRow.Max();

            table.Rows[2].Cells[0].Value = "Размах:";
            table.Rows[2].Cells[1].Value = GetIntervalRowVariationalRange(intervalRow);

            table.Rows[3].Cells[0].Value = "Количество интервалов:";
            table.Rows[3].Cells[1].Value = intervalNumber;

            table.Rows[4].Cells[0].Value = "Шаг интервала:";
            table.Rows[4].Cells[1].Value = intervalStep;

            table.Rows[5].Cells[0].Value = "Мода:";
            table.Rows[5].Cells[1].Value = GetIntervalRowModes(intervalTable)[0];

            table.Rows[6].Cells[0].Value = "Медиана:";
            table.Rows[6].Cells[1].Value = GetIntervalRowMedian(intervalTable);

            table.Rows[7].Cells[0].Value = "Математическое ожидание:";
            table.Rows[7].Cells[1].Value = GetIntervalRowExpectedValue(intervalTable, intervalRow);

            float totalDispersion = GetIntervalRowTotalDispersion(intervalTable, intervalRow);
            table.Rows[8].Cells[0].Value = "Общая дисперсия:";
            table.Rows[8].Cells[1].Value = totalDispersion;

            float intergroupDispersion = GetInteravalRowIntergroupDispersion(intervalTable, intervalRow);
            table.Rows[9].Cells[0].Value = "Межгрупповая дисперсия:";
            table.Rows[9].Cells[1].Value = intergroupDispersion;

            float averageGroupDispersion = GetIntervalRowAverageGroupDispersion(intervalTable, intervalRow);
            table.Rows[10].Cells[0].Value = "Средняя из внутригрупповых дисперсий:";
            table.Rows[10].Cells[1].Value = averageGroupDispersion;

            table.Rows[11].Cells[0].Value = "Межгрупповая/общая (%):";
            table.Rows[11].Cells[1].Value = intergroupDispersion / totalDispersion * 100 + "%";

            float sum = intergroupDispersion + averageGroupDispersion;
            table.Rows[12].Cells[0].Value = "Общая дисперсия = межгрупповая + средняя из групповых:";
            table.Rows[12].Cells[1].Value = totalDispersion + " = " + sum + " = " + intergroupDispersion + " + " + averageGroupDispersion;

            table.Rows[13].Cells[0].Value = "Среднеквадратическое отклонение:";
            table.Rows[13].Cells[1].Value = GetIntervalRowAverageSquareDeviation(intervalTable, intervalRow);

            table.Rows[14].Cells[0].Value = "Асимметрия:";
            table.Rows[14].Cells[1].Value = GetIntervalRowAsymmetry(intervalTable, intervalRow);

            table.Rows[15].Cells[0].Value = "Эксцесс:";
            table.Rows[15].Cells[1].Value = GetIntervalRowExcess(intervalTable, intervalRow);

            table.Rows[16].Cells[0].Value = "Коэффицент вариации:";
            table.Rows[16].Cells[1].Value = GetIntervalRowVariationCoefficient(intervalTable, intervalRow);
        }

        private void Normal(int[] intervalRow)
        {
            var gridTable = (DataGridView)tabPage2.Controls["dataGridtab2"];
            gridTable.RowTemplate.Height = 40;
            gridTable.Columns.Add("x", "Середина интервала");
            gridTable.Columns.Add("u", "Ui");
            gridTable.Columns.Add("fi", "φ");
            gridTable.Columns.Add("n0", "Теоретические частоты");
            gridTable.Columns.Add("ni", "Частоты");
            gridTable.Columns.Add("hi", "Наблюдаемое значение критерия");

            foreach (DataGridViewColumn column in gridTable.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            var intervalStep = GetIntervalsStep(intervalRow);
            var intervalNumber = GetIntervalsNumber(intervalRow);
            var intervalTable = GetIntervalTable(intervalRow, intervalStep, intervalNumber);

            float expectedValue = GetIntervalRowExpectedValue(intervalTable, intervalRow);
            float deviation = GetIntervalRowAverageSquareDeviation(intervalTable, intervalRow);
            float collectionsSize = intervalRow.Length;
            float hi = 0F;
            gridTable.Rows.Add(intervalTable.GetLength(1) + 3);

            for (int i = 0; i < intervalTable.GetLength(1); i++)
            {
                float u = (intervalTable[5, i] - expectedValue) / deviation;
                float fu = (float)(1.0 / Math.Sqrt(2 * Math.PI) * Math.Exp(-Math.Pow(u, 2) / 2));
                float n0 = collectionsSize * (intervalTable[1, i] - intervalTable[0, i]) * fu / deviation;
                float currhi = (float)(Math.Pow(intervalTable[2, i] - n0, 2) / n0);
                hi += currhi;

                gridTable.Rows[i].Cells[0].Value = intervalTable[5, i];
                gridTable.Rows[i].Cells[1].Value = u;
                gridTable.Rows[i].Cells[2].Value = fu;
                gridTable.Rows[i].Cells[3].Value = n0;
                gridTable.Rows[i].Cells[4].Value = intervalTable[2, i];
                gridTable.Rows[i].Cells[5].Value = currhi;
            }
            int d = intervalNumber - 2 - 1;
            double J = Math.Abs(hi - d) / Math.Sqrt(2 * intervalNumber + 2.4);
            double R = Math.Abs(hi - d) / Math.Sqrt(2 * d);


            gridTable.Rows[intervalTable.GetLength(1)].Cells[0].Value = "Критерий Ястремского  (надо <= 3):";
            gridTable.Rows[intervalTable.GetLength(1)].Cells[1].Value = J;

            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[0].Value = "Критерий Романовского (надо <= 3):";
            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[1].Value = R;

            gridTable.Rows[intervalTable.GetLength(1)].Cells[4].Value = "Критерий Пирсона:";
            gridTable.Rows[intervalTable.GetLength(1)].Cells[5].Value = hi;
            
            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[4].Value = "По таблице критических значений:";
            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[5].Value = hisqr[d];

            gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[0].Value = "Вывод:";
            gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[1].Value = J < 3 || R < 3 ? "Гипотеза подтверждена" : "Гипотеза не подтверждена";
            gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[5].Value = hi >= hisqr[d] ? "Гипотеза не подтверждена" : "Гипотеза подтверждена";
            var cell = gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[5];
            cell.Style.Font = new Font(FontFamily.GenericMonospace, 8, FontStyle.Bold);
            cell = gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[1];
            cell.Style.Font = new Font(FontFamily.GenericMonospace, 8, FontStyle.Bold);
        }

        private void Showing(int[] intervalRow)
        {
            var gridTable = (DataGridView)tabs.Controls["tabPage3"].Controls["tabPage3table"];
            gridTable.Columns.Add("a", "Правая граница интервала");
            gridTable.Columns.Add("b", "Левая граница интервала");
            gridTable.Columns.Add("c", "Частота");
            gridTable.Columns.Add("d", "Вероятности попадания в интервал");
            gridTable.Columns.Add("f", "Теоретическая частота");

            foreach (DataGridViewColumn column in gridTable.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            var intervalStep = GetIntervalsStep(intervalRow);
            var intervalNumber = GetIntervalsNumber(intervalRow);
            var intervalTable = GetIntervalTable(intervalRow, intervalStep, intervalNumber);

            float expectedValue = GetIntervalRowExpectedValue(intervalTable, intervalRow);
            float reverseExpectedValue = 1 / expectedValue;
            float collectionsSize = intervalRow.Length;

            float hi = 0;
            gridTable.Rows.Add(intervalTable.GetLength(1) + 2);
            for (int i = 0; i < intervalTable.GetLength(1); i++)
            {
                float P = (float) (Math.Exp(-reverseExpectedValue * intervalTable[0, i]) - Math.Exp(-reverseExpectedValue * intervalTable[1, i]));
                float n = collectionsSize * P;

                float currHi = (float) Math.Pow(intervalTable[2, i] - n, 2) / 2;

                hi += currHi;

                gridTable.Rows[i].Cells[0].Value = intervalTable[0, i];
                gridTable.Rows[i].Cells[1].Value = intervalTable[1, i];
                gridTable.Rows[i].Cells[2].Value = intervalTable[2, i];
                gridTable.Rows[i].Cells[3].Value = P;
                gridTable.Rows[i].Cells[4].Value = currHi;
            }

            int d = intervalNumber - 2 - 1;
            double J = Math.Abs(hi - d) / Math.Sqrt(2 * intervalNumber + 2.4);
            double R = Math.Abs(hi - d) / Math.Sqrt(2 * d);

            gridTable.Rows[intervalTable.GetLength(1)].Cells[0].Value = "Критерий Ястремского  (надо <= 3):";
            gridTable.Rows[intervalTable.GetLength(1)].Cells[1].Value = J;

            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[0].Value = "Критерий Романовского (надо <= 3):";
            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[1].Value = R;

            gridTable.Rows[intervalTable.GetLength(1)].Cells[3].Value = "Критерий Пирсона:";
            gridTable.Rows[intervalTable.GetLength(1)].Cells[4].Value = hi;
            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[3].Value = "По таблице критических значений:";
            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[4].Value = hisqr[d];
            gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[0].Value = "Вывод:";
            gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[1].Value = J < 3 || R < 3 ? "Гипотеза подтверждена" : "Гипотеза не подтверждена";
            gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[4].Value = hi >= hisqr[d] ? "Гипотеза не подтверждена" : "Гипотеза подтверждена";
            var cell = gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[4];
            cell.Style.Font = new Font(FontFamily.GenericMonospace, 8, FontStyle.Bold);
            cell = gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[1];
            cell.Style.Font = new Font(FontFamily.GenericMonospace, 8, FontStyle.Bold);
        }

        private void Distribution(int[] intervalRow)
        {
            var gridTable = (DataGridView)tabs.Controls["tabPage4"].Controls["tabPage4table"];
            gridTable.Columns.Add("a", "Правая граница интервала");
            gridTable.Columns.Add("b", "Левая граница интервала");
            gridTable.Columns.Add("c", "Частота");
            gridTable.Columns.Add("d", "Хи-наблюдаемая");

            foreach (DataGridViewColumn column in gridTable.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            var intervalStep = GetIntervalsStep(intervalRow);
            var intervalNumber = GetIntervalsNumber(intervalRow);
            var intervalTable = GetIntervalTable(intervalRow, intervalStep, intervalNumber);

            float expectedValue = GetIntervalRowExpectedValue(intervalTable, intervalRow);
            float deviation = GetIntervalRowAverageSquareDeviation(intervalTable, intervalRow);
            float collectionsSize = intervalRow.Length;

            gridTable.Rows.Add(intervalTable.GetLength(1) + 2);

            float a = (float)(expectedValue - Math.Sqrt(3) * deviation);
            float b = (float)(expectedValue + Math.Sqrt(3) * deviation);
            float f = 1 / (b - a);

            float n1 = collectionsSize * (intervalTable[0, 1] - a) * f;
            float n2 = collectionsSize * (intervalTable[1, 1] - intervalTable[0, 1]) * f;
            float n3 = collectionsSize * (b - intervalTable[1, intervalTable.GetLength(1) - 1]) * f;
            float hi = (float)Math.Pow(intervalTable[2, 1] - n1, 2) / n1;
            float currHi = 0;

            gridTable.Rows[0].Cells[0].Value = intervalTable[0, 0];
            gridTable.Rows[0].Cells[1].Value = intervalTable[1, 0];
            gridTable.Rows[0].Cells[2].Value = intervalTable[2, 0];
            gridTable.Rows[0].Cells[3].Value = hi;
            for (int i = 1; i < intervalTable.GetLength(1) - 1; i++)
            {
                currHi = (float)Math.Pow(intervalTable[2, i] - n2, 2) / n2;
                hi += currHi;

                gridTable.Rows[i].Cells[0].Value = intervalTable[0, i];
                gridTable.Rows[i].Cells[1].Value = intervalTable[1, i];
                gridTable.Rows[i].Cells[2].Value = intervalTable[2, i];
                gridTable.Rows[i].Cells[3].Value = currHi;
            }
            

            currHi = (float)Math.Pow(intervalTable[2, intervalTable.GetLength(1) - 1] - n3, 2) / n3;

            gridTable.Rows[intervalTable.GetLength(1) - 1].Cells[0].Value = intervalTable[0, intervalTable.GetLength(1) - 1];
            gridTable.Rows[intervalTable.GetLength(1) - 1].Cells[1].Value = intervalTable[1, intervalTable.GetLength(1) - 1];
            gridTable.Rows[intervalTable.GetLength(1) - 1].Cells[2].Value = intervalTable[2, intervalTable.GetLength(1) - 1];
            gridTable.Rows[intervalTable.GetLength(1) - 1].Cells[3].Value = currHi;

            hi += currHi;
            int d = intervalNumber - 1 - 1;
            double J = Math.Abs(hi - d) / Math.Sqrt(2 * intervalNumber + 2.4);
            double R = Math.Abs(hi - d) / Math.Sqrt(2 * d);

            gridTable.Rows[intervalTable.GetLength(1)].Cells[0].Value = "Критерий Ястремского  (надо <= 3):";
            gridTable.Rows[intervalTable.GetLength(1)].Cells[1].Value = J;

            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[0].Value = "Критерий Романовского (надо <= 3):";
            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[1].Value = R;

            gridTable.Rows[intervalTable.GetLength(1)].Cells[2].Value = "Критерий Пирсона:";
            gridTable.Rows[intervalTable.GetLength(1)].Cells[3].Value = hi;
            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[2].Value = "По таблице критических значений:";
            gridTable.Rows[intervalTable.GetLength(1) + 1].Cells[3].Value = hisqr[d];
            gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[0].Value = "Вывод:";
            gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[1].Value = J < 3 || R < 3 ? "Гипотеза подтверждена" : "Гипотеза не подтверждена";
            gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[3].Value = hi >= hisqr[d] ? "Гипотеза не подтверждена" : "Гипотеза подтверждена";

            var cell = gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[3];
            cell.Style.Font = new Font(FontFamily.GenericMonospace, 8, FontStyle.Bold);
            cell = gridTable.Rows[intervalTable.GetLength(1) + 2].Cells[1];
            cell.Style.Font = new Font(FontFamily.GenericMonospace, 8, FontStyle.Bold);
        }
    }
}
  

