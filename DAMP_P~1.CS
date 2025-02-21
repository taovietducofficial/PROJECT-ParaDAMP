using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace WindowsFormsApp1
{
    public partial class damp_program : Form
    {
        private string fileContent;
        private double[] dataArray;
        public int value1 = 0;
        /// <summary>
        //private List<double> currentArray;
        //private int[] resultArray;
        /// </summary>
        private int scrollPosition;
        //private int scrollStep = 100; // Bước nhảy mới
        public int ScrollPosition
        {
            get { return scrollPosition; }
            set
            {
                scrollPosition = value;
                //DrawChart(scrollPosition);
                //DrawChart_discords(currentArray, resultArray);
            }
        }
        private int visibleDataCount = 100;
        public damp_program()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Khởi tạo control Chart
            Chart chart1 = new Chart();
            chart1.Dock = DockStyle.Fill;

            // Thêm control Chart vào GroupBox
            groupBox4.Controls.Add(chart1);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /*        private void btnLoadData_Click(object sender, EventArgs e)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        dataArray = LoadDataFromFile(filePath); // Gán giá trị cho dataArray khi tải dữ liệu từ tệp tin
                        fileContent = string.Join(Environment.NewLine, dataArray.Select(d => d.ToString()));
                        //DrawChart_subs(8462, 6);
                        // Vẽ biểu đồ
                        DrawChart();

                    }
                }*/

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                dataArray = LoadDataFromFile(filePath);
                fileContent = string.Join(Environment.NewLine, dataArray.Select(d => d.ToString()));

                scrollPosition = 0;
                DrawChart();
                //UpdateScrollBar();
            }
        }

        private double[] LoadDataFromFile(string filePath)
        {
            List<double> dataList = new List<double>();

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (double.TryParse(line, out double number))
                        {
                            dataList.Add(number);
                        }
                    }
                }
            }

            return dataList.ToArray();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            value1 = 0;
            if (dataArray == null)
            {
                MessageBox.Show("Please select a data file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter Min Lenght and Max Lenght.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox6.Text, out int intValue) || !int.TryParse(textBox7.Text, out int intValue1))
            {
                MessageBox.Show("Please only enter integer number for Min Length and Max Length .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!string.IsNullOrEmpty(fileContent))
            {
                if (int.TryParse(textBox6.Text, out int value1) && int.TryParse(textBox7.Text, out int value2))
                {
                    if(textBox5.Text == "")
                    {
                        ExecuteDAMP(dataArray, value1, value2, 1, 1);
                    }
                    else
                    {
                        if(!int.TryParse(textBox5.Text, out int intValue2))
                        {
                            MessageBox.Show("Please only enter integer number for Lookahead.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        int.TryParse(textBox5.Text, out int value3);
                        ExecuteDAMP(dataArray, value1, value2, 1, value3);
                    }
                    
           
                    //*************************************************************************************************************
                    string filePath2 = "D:/Damp_demo/discord_score2.txt";

                    // Mảng để lưu trữ các mảng con
                    List<double[]> arrays2 = new List<double[]>();
                    List<double> currentArray2 = new List<double>();

                    // Mở tệp văn bản
                    using (StreamReader sr2 = new StreamReader(filePath2))
                    {
                        string line2;
                        //List<double> currentArray2 = new List<double>();

                        // Đọc từng dòng trong tệp văn bản
                        while ((line2 = sr2.ReadLine()) != null)
                        {
                            // Chuyển đổi giá trị sang double và thêm vào mảng con
                            double value = double.Parse(line2.Trim().Replace("e", "E"));
                            currentArray2.Add(value);

                            // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                            if (string.IsNullOrWhiteSpace(line2))
                            {
                                arrays2.Add(currentArray2.ToArray());
                                currentArray2.Clear();
                            }
                        }

                        // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                        if (currentArray2.Count > 0)
                        {
                            arrays2.Add(currentArray2.ToArray());
                        }

                    }
                    //*****************************************************************************************************************


                    //*****************************************************************************************************************

                    // Đường dẫn tới tệp văn bản
                    string filePath = "D:/Damp_demo/position2.txt";

                    // Mảng để lưu trữ các mảng con
                    List<double[]> arrays = new List<double[]>();

                    // Mở tệp văn bản
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        List<double> currentArray = new List<double>();

                        // Đọc từng dòng trong tệp văn bản
                        while ((line = sr.ReadLine()) != null)
                        {
                            // Chuyển đổi giá trị sang double và thêm vào mảng con
                            double value = double.Parse(line.Trim().Replace("e", "E"));
                            currentArray.Add(value);

                            // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                arrays.Add(currentArray.ToArray());
                                currentArray.Clear();
                            }
                        }

                        // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                        if (currentArray.Count > 0)
                        {
                            arrays.Add(currentArray.ToArray());
                        }

                    //*****************************************************************************************************************


                    //*****************************************************************************************************************
                    int length = value2 - value1 + 1;
                    int[] resultArray = new int[length];

                    // Thêm các giá trị vào mảng
                    for (int i = 0; i < length; i++)
                    {
                        resultArray[i] = value1 + i;
                    }

                    double[] positions = currentArray.ToArray();
                    double[] discord_score = currentArray2.ToArray();   

                    // Sử dụng HashSet để lưu trữ các giá trị đã xuất hiện trong mảng position
                    HashSet<int> uniquePositions = new HashSet<int>();

                    // Mảng mới sau khi loại bỏ các giá trị trùng lặp
                    List<int> uniqueLengths = new List<int>();

                    List<double> uniqueDiscord_score2 = new List<double>();

                    // Kiểm tra xem cả hai mảng positions và lengths có cùng độ dài không
                    if (positions.Length == resultArray.Length)
                    {
                        for (int i = 0; i < positions.Length; i++)
                        {
                            int position2 = (int)positions[i];
                            int length2 = resultArray[i];
                            double discord_score2 = discord_score[i];

                            // Kiểm tra xem position đã tồn tại trong HashSet chưa
                            if (!uniquePositions.Contains(position2))
                            {
                                // Nếu chưa tồn tại, thêm vào HashSet và thêm length vào mảng uniqueLengths
                                uniquePositions.Add(position2);
                                uniqueLengths.Add(length2);
                                uniqueDiscord_score2.Add(discord_score2);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: positions and lengths arrays have different lengths.");
                        }
                        
                        // Chuyển đổi uniqueLengths thành mảng nếu cần
                        int[] resultLengths = uniqueLengths.ToArray();
                        int[] resultPositions = uniquePositions.ToArray();
                        double[] resultDiscord_score = uniqueDiscord_score2.ToArray();


                        Dictionary<int, Tuple<int, double>> mergedChains = new Dictionary<int, Tuple<int, double>>();

                        for (int i = 0; i < resultPositions.Length; i++)
                        {
                            int startPos1 = resultPositions[i];
                            int endPos1 = startPos1 + resultLengths[i];
                            double discord1 = resultDiscord_score[i];

                            bool merged = false;

                            foreach (var chain in mergedChains.ToList())
                            {
                                int startPos2 = chain.Key;
                                int endPos2 = chain.Value.Item1;
                                double discord2 = chain.Value.Item2;

                                // Tính toán phần giao giữa hai chuỗi
                                int overlapStart = Math.Max(startPos1, startPos2);
                                int overlapEnd = Math.Min(endPos1, endPos2);
                                int overlapLength = Math.Max(0, overlapEnd - overlapStart);

                                double overlapPercentage1 = (double)overlapLength / resultLengths[i];
                                double overlapPercentage2 = (double)overlapLength / (endPos2 - startPos2);

                                // Kiểm tra xem phần giao có lớn hơn 50% độ dài của cả hai chuỗi không
                                if (overlapPercentage1 > 0.5 && overlapPercentage2 > 0.5)
                                {
                                    // So sánh giá trị discord
                                    if (discord1 > discord2)
                                    {
                                        // Thay thế chuỗi hiện tại bằng chuỗi mới
                                        mergedChains.Remove(startPos2);
                                        mergedChains[startPos1] = new Tuple<int, double>(endPos1, discord1);
                                    }
                                    else
                                    {
                                        // Loại bỏ chuỗi hiện tại vì chuỗi cũ có giá trị discord cao hơn hoặc bằng
                                        merged = true;
                                        break;
                                    }
                                }
                            }

                            // Nếu không có sự hợp nhất với bất kỳ chuỗi nào, thêm chuỗi hiện tại vào từ điển
                            if (!merged)
                            {
                                mergedChains[startPos1] = new Tuple<int, double>(endPos1, discord1);
                            }
                        }

                        // Tạo lại các mảng kết quả từ mergedChains
                        List<int> finalPositions = new List<int>();
                        List<int> finalLengths = new List<int>();
                        List<double> finalDiscordScores = new List<double>();

                        foreach (var chain in mergedChains)
                        {
                            finalPositions.Add(chain.Key);
                            finalLengths.Add(chain.Value.Item1 - chain.Key);
                            finalDiscordScores.Add(chain.Value.Item2);
                        }


                        // Chuyển các danh sách thành mảng nếu cần
                        int[] resultPositionsFinal = finalPositions.ToArray();
                        int[] resultLengthsFinal = finalLengths.ToArray();
                        //double[] resultDiscordScoresFinal = finalDiscordScores.ToArray();



                        comboBox1.Items.Clear();

                        for (int i = 0; i < resultPositionsFinal.Length; i++)
                        {
                            string itemText = $"Dài {resultLengthsFinal[i]} vị trí {resultPositionsFinal[i]}";
                            comboBox1.Items.Add(itemText);
                            //DrawChart_subs(resultPositionsFinal[i], resultLengthsFinal[i]);
                        }

                        string filePath1 = "D:/Damp_demo/execution_time.txt"; // Đường dẫn của tệp văn bản
                        try
                        {
                            using (StreamReader sr1 = new StreamReader(filePath1))
                            {
                                string executionTime = sr1.ReadLine(); // Đọc dòng đầu tiên từ tệp
                                textBox1.Text = executionTime; // Hiển thị nội dung trong TextBox
                            }
                        }
                        catch (FileNotFoundException)
                        {
                            textBox1.Text = "File not found"; // Hiển thị thông báo nếu không tìm thấy tệp
                        }
                        catch (Exception ex)
                        {
                            textBox1.Text = "Error: " + ex.Message; // Hiển thị lỗi nếu có lỗi xảy ra
                        }


                        string filePath3 = "D:/Damp_demo/position_final.txt";

                        // Mở tệp văn bản để viết
                        using (StreamWriter writer = new StreamWriter(filePath3))
                        {
                            // Ghi từng phần tử của mảng vào tệp văn bản
                            foreach (int value in resultPositionsFinal)
                            {
                                writer.WriteLine(value);
                            }
                        }

                        string filePath4 = "D:/Damp_demo/length_final.txt";

                        // Mở tệp văn bản để viết
                        using (StreamWriter writer = new StreamWriter(filePath4))
                        {
                            // Ghi từng phần tử của mảng vào tệp văn bản
                            foreach (int value in resultLengthsFinal)
                            {
                                writer.WriteLine(value);
                            }
                        }



                        // Chuyển mảng thành danh sách
                        List<int> resultList = new List<int>(resultPositionsFinal);
                        DrawChart_discords(resultList, resultLengthsFinal);
                    }
                    //*****************************************************************************************************************
                }
            }
        }


        private void ExecuteMERLIN(double[] data, int value1, int value2, int value3)
        {
            MLApp.MLApp matlab = new MLApp.MLApp();
            matlab.Execute(@"cd d:\Merlin_demo");

            object result = null;

            matlab.Feval("Parallel_M", 2, out result, data, value1, value2, value3);
            /*string filePath = "D:/Damp_demo/position2.txt";

            // Mảng để lưu trữ các mảng con
            List<double[]> arrays = new List<double[]>();

            // Mở tệp văn bản
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                List<double> currentArray = new List<double>();

                // Đọc từng dòng trong tệp văn bản
                while ((line = sr.ReadLine()) != null)
                {
                    // Chuyển đổi giá trị sang double và thêm vào mảng con
                    double value = double.Parse(line.Trim().Replace("e", "E"));
                    currentArray.Add(value);

                    // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        arrays.Add(currentArray.ToArray());
                        currentArray.Clear();
                    }
                }

                // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                if (currentArray.Count > 0)
                {
                    arrays.Add(currentArray.ToArray());
                }

                //int j = 0;

                *//*                Button[] buttons = { button4, button5, button6, button7, button8 };

                                for (int i = value1; i <= value2 && i - value1 < buttons.Length; i++)
                                {
                                    buttons[i - value1].Text = $"Dài {i} vị trí {currentArray[j]}";
                                    DrawChart_subs((int)currentArray[j], i);
                                    j++;
                                }
                *//*

                int j = 0;
                comboBox1.Items.Clear();

                for (int i = value1; i <= value2; i++)
                {
                    comboBox1.Items.Add($"Dài {i} vị trí {currentArray[j]}");
                    comboBox1.Items[i - value1] = $"Dài {i} vị trí {currentArray[j]}";
                    DrawChart_subs((int)currentArray[j], i);
                    j++;
                }

                int length = value2 - value1 + 1;
                int[] resultArray = new int[length];

                // Thêm các giá trị vào mảng
                for (int i = 0; i < length; i++)
                {
                    resultArray[i] = value1 + i;
                }

                DrawChart_discords(currentArray, resultArray);*/

/*            string filePath1 = "D:/Merlin_demo/execution_time.txt"; // Đường dẫn của tệp văn bản
            try
            {
                using (StreamReader sr1 = new StreamReader(filePath1))
                {
                    string executionTime = sr1.ReadLine(); // Đọc dòng đầu tiên từ tệp
                    textBox2.Text = executionTime; // Hiển thị nội dung trong TextBox
                }
            }
            catch (FileNotFoundException)
            {
                textBox2.Text = "File not found"; // Hiển thị thông báo nếu không tìm thấy tệp
            }
            catch (Exception ex)
            {
                textBox2.Text = "Error: " + ex.Message; // Hiển thị lỗi nếu có lỗi xảy ra
            }*/
        }


        private void ExecuteDAMP(double[] data, int value1, int value2, int value3, int value4)
        {
            MLApp.MLApp matlab = new MLApp.MLApp();
            matlab.Execute(@"cd d:\Damp_demo");

            object result = null;

            // Chuyển đổi các giá trị thành số nguyên nếu cần thiết
            value1 = Convert.ToInt32(value1);
            value2 = Convert.ToInt32(value2);
            value3 = Convert.ToInt32(value3);
            value4 = Convert.ToInt32(value4);

            matlab.Feval("Parallel", 2, out result, data, value1, value2, value3, value4);
            /*string filePath = "D:/Damp_demo/position2.txt";

            // Mảng để lưu trữ các mảng con
            List<double[]> arrays = new List<double[]>();

            // Mở tệp văn bản
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                List<double> currentArray = new List<double>();

                // Đọc từng dòng trong tệp văn bản
                while ((line = sr.ReadLine()) != null)
                {
                    // Chuyển đổi giá trị sang double và thêm vào mảng con
                    double value = double.Parse(line.Trim().Replace("e", "E"));
                    currentArray.Add(value);

                    // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        arrays.Add(currentArray.ToArray());
                        currentArray.Clear();
                    }
                }

                // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                if (currentArray.Count > 0)
                {
                    arrays.Add(currentArray.ToArray());
                }

                //int j = 0;

                *//*                Button[] buttons = { button4, button5, button6, button7, button8 };

                                for (int i = value1; i <= value2 && i - value1 < buttons.Length; i++)
                                {
                                    buttons[i - value1].Text = $"Dài {i} vị trí {currentArray[j]}";
                                    DrawChart_subs((int)currentArray[j], i);
                                    j++;
                                }
                *//*

                int j = 0;
                comboBox1.Items.Clear();

                for (int i = value1; i <= value2; i++)
                {
                    comboBox1.Items.Add($"Dài {i} vị trí {currentArray[j]}");
                    comboBox1.Items[i - value1] = $"Dài {i} vị trí {currentArray[j]}";
                    DrawChart_subs((int)currentArray[j], i);
                    j++;
                }

                int length = value2 - value1 + 1;
                int[] resultArray = new int[length];

                // Thêm các giá trị vào mảng
                for (int i = 0; i < length; i++)
                {
                    resultArray[i] = value1 + i;
                }

                DrawChart_discords(currentArray, resultArray);*/

/*                string filePath1 = "D:/Damp_demo/execution_time.txt"; // Đường dẫn của tệp văn bản
                try
                {
                    using (StreamReader sr1 = new StreamReader(filePath1))
                    {
                        string executionTime = sr1.ReadLine(); // Đọc dòng đầu tiên từ tệp
                        textBox1.Text = executionTime; // Hiển thị nội dung trong TextBox
                    }
                }
                catch (FileNotFoundException)
                {
                    textBox1.Text = "File not found"; // Hiển thị thông báo nếu không tìm thấy tệp
                }
                catch (Exception ex)
                {
                    textBox1.Text = "Error: " + ex.Message; // Hiển thị lỗi nếu có lỗi xảy ra
                }*/
        }

        /*        private void DrawChart()
                {
                    // Xóa các series cũ trước khi vẽ biểu đồ mới
                    chart1.Series.Clear();

                    // Tạo một series mới
                    Series series = new Series("Data");
                    series.ChartType = SeriesChartType.Line;

                    // Thêm dữ liệu vào series
                    for (int i = 0; i < dataArray.Length; i++)
                    {
                        series.Points.AddXY(i + 1, dataArray[i]);
                    }

                    // Thêm series vào chart
                    chart1.Series.Add(series);

                    // Cập nhật biểu đồ
                    chart1.Update();

                    chart1.ChartAreas[0].BorderWidth = 0;
                    chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
                    chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                    chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
                    chart1.ChartAreas[0].AxisX.LineWidth = 0;
                    chart1.ChartAreas[0].AxisY.LineWidth = 0;
                }*/

        private void DrawChart()
        {
            chart1.Series.Clear();

            Series series = new Series("Data");
            series.ChartType = SeriesChartType.Line;
            series.Legend = null;

            // Thêm dữ liệu vào series, chỉ lấy số lượng dữ liệu hiển thị trên biểu đồ
            //for (int i = ScrollPosition; i < Math.Min(dataArray.Length, ScrollPosition + visibleDataCount); i++)
            for (int i = ScrollPosition; i < dataArray.Length; i++)
            {
                series.Points.AddXY(i + 1, dataArray[i]);
            }

            chart1.Series.Add(series);
            chart1.Update();

            chart1.ChartAreas[0].BorderWidth = 0;
            chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LineWidth = 0;
            chart1.ChartAreas[0].AxisY.LineWidth = 0;
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;

        }

        private void UpdateScrollBar()
        {
            // Thiết lập thuộc tính của scroll bar
            int maxScrollValue = Math.Max(dataArray.Length - visibleDataCount, 0);
            scrollBar.Minimum = 0;
            scrollBar.Maximum = maxScrollValue;
            //scrollBar.LargeChange = visibleDataCount;
            //scrollBar.Value = scrollPosition;
            //scrollBar.LargeChange = scrollStep;
            //scrollBar.Value = ScrollPosition;
        }

        private void DrawChart_subs(int currentArray, int value1)
        {
            // Xóa các series cũ trước khi vẽ biểu đồ mới
            chart2.Series.Clear();

            // Tạo một series mới
            
            
                chart2.Series.Clear();

                //int[] dataArray1 = dataArray;
                Series series = new Series("Data");
                series.ChartType = SeriesChartType.Line;

                // Giới hạn vị trí x để đảm bảo không vượt quá kích thước của mảng
                //int startX = Math.Max(0, currentArray - 1);
                int startX = currentArray;
                int endX = Math.Min(dataArray.Length - 1, currentArray + value1 - 1);

                // Thêm dữ liệu vào series
                for (int j = startX; j < endX; j++)
                {
                    series.Points.AddXY(j + 1, dataArray[j]);
                }

                // Thêm series vào chart
                chart2.Series.Add(series);

                // Cập nhật biểu đồ
                chart2.Update();

                chart2.ChartAreas[0].BorderWidth = 0;
                chart2.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
                chart2.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
                chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart2.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                chart2.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
                chart2.ChartAreas[0].AxisX.LineWidth = 0;
                chart2.ChartAreas[0].AxisY.LineWidth = 0;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            value1 = 0;
            if (dataArray == null)
            {
                MessageBox.Show("Please select a data file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter Min Lenght and Max Lenght.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Please enter NoOfTasks (Recommend '4').", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox6.Text, out int intValue) || !int.TryParse(textBox7.Text, out int intValue1))
            {
                MessageBox.Show("Please only enter integer number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(fileContent))
            {
                if (int.TryParse(textBox6.Text, out int value1) 
                    && int.TryParse(textBox7.Text, out int value2)
                    && int.TryParse(textBox8.Text, out int value3)
                   )
                {

                    if (textBox5.Text == "")
                    {
                        ExecuteDAMP(dataArray, value1, value2, value3, 1);
                    }
                    else
                    {
                        if (!int.TryParse(textBox5.Text, out int intValue2))
                        {
                            MessageBox.Show("Please only enter integer number for Lookahead.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        int.TryParse(textBox5.Text, out int value4);
                        ExecuteDAMP(dataArray, value1, value2, value3, value4);
                    }


                    //*************************************************************************************************************
                    string filePath2 = "D:/Damp_demo/discord_score2.txt";

                    // Mảng để lưu trữ các mảng con
                    List<double[]> arrays2 = new List<double[]>();
                    List<double> currentArray2 = new List<double>();

                    // Mở tệp văn bản
                    using (StreamReader sr2 = new StreamReader(filePath2))
                    {
                        string line2;
                        //List<double> currentArray2 = new List<double>();

                        // Đọc từng dòng trong tệp văn bản
                        while ((line2 = sr2.ReadLine()) != null)
                        {
                            // Chuyển đổi giá trị sang double và thêm vào mảng con
                            double value = double.Parse(line2.Trim().Replace("e", "E"));
                            currentArray2.Add(value);

                            // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                            if (string.IsNullOrWhiteSpace(line2))
                            {
                                arrays2.Add(currentArray2.ToArray());
                                currentArray2.Clear();
                            }
                        }

                        // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                        if (currentArray2.Count > 0)
                        {
                            arrays2.Add(currentArray2.ToArray());
                        }

                    }
                    //*****************************************************************************************************************


                    //*****************************************************************************************************************

                    // Đường dẫn tới tệp văn bản
                    string filePath = "D:/Damp_demo/position2.txt";

                    // Mảng để lưu trữ các mảng con
                    List<double[]> arrays = new List<double[]>();

                    // Mở tệp văn bản
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        List<double> currentArray = new List<double>();

                        // Đọc từng dòng trong tệp văn bản
                        while ((line = sr.ReadLine()) != null)
                        {
                            // Chuyển đổi giá trị sang double và thêm vào mảng con
                            double value = double.Parse(line.Trim().Replace("e", "E"));
                            currentArray.Add(value);

                            // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                arrays.Add(currentArray.ToArray());
                                currentArray.Clear();
                            }
                        }

                        // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                        if (currentArray.Count > 0)
                        {
                            arrays.Add(currentArray.ToArray());
                        }

                        //*****************************************************************************************************************


                        //*****************************************************************************************************************
                        int length = value2 - value1 + 1;
                        int[] resultArray = new int[length];

                        // Thêm các giá trị vào mảng
                        for (int i = 0; i < length; i++)
                        {
                            resultArray[i] = value1 + i;
                        }

                        double[] positions = currentArray.ToArray();
                        double[] discord_score = currentArray2.ToArray();

                        // Sử dụng HashSet để lưu trữ các giá trị đã xuất hiện trong mảng position
                        HashSet<int> uniquePositions = new HashSet<int>();

                        // Mảng mới sau khi loại bỏ các giá trị trùng lặp
                        List<int> uniqueLengths = new List<int>();

                        List<double> uniqueDiscord_score2 = new List<double>();

                        // Kiểm tra xem cả hai mảng positions và lengths có cùng độ dài không
                        if (positions.Length == resultArray.Length)
                        {
                            for (int i = 0; i < positions.Length; i++)
                            {
                                int position2 = (int)positions[i];
                                int length2 = resultArray[i];
                                double discord_score2 = discord_score[i];

                                // Kiểm tra xem position đã tồn tại trong HashSet chưa
                                if (!uniquePositions.Contains(position2))
                                {
                                    // Nếu chưa tồn tại, thêm vào HashSet và thêm length vào mảng uniqueLengths
                                    uniquePositions.Add(position2);
                                    uniqueLengths.Add(length2);
                                    uniqueDiscord_score2.Add(discord_score2);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: positions and lengths arrays have different lengths.");
                        }

                        // Chuyển đổi uniqueLengths thành mảng nếu cần
                        int[] resultLengths = uniqueLengths.ToArray();
                        int[] resultPositions = uniquePositions.ToArray();
                        double[] resultDiscord_score = uniqueDiscord_score2.ToArray();


                        Dictionary<int, Tuple<int, double>> mergedChains = new Dictionary<int, Tuple<int, double>>();

                        for (int i = 0; i < resultPositions.Length; i++)
                        {
                            int startPos1 = resultPositions[i];
                            int endPos1 = startPos1 + resultLengths[i];
                            double discord1 = resultDiscord_score[i];

                            bool merged = false;

                            foreach (var chain in mergedChains.ToList())
                            {
                                int startPos2 = chain.Key;
                                int endPos2 = chain.Value.Item1;
                                double discord2 = chain.Value.Item2;

                                // Tính toán phần giao giữa hai chuỗi
                                int overlapStart = Math.Max(startPos1, startPos2);
                                int overlapEnd = Math.Min(endPos1, endPos2);
                                int overlapLength = Math.Max(0, overlapEnd - overlapStart);

                                double overlapPercentage1 = (double)overlapLength / resultLengths[i];
                                double overlapPercentage2 = (double)overlapLength / (endPos2 - startPos2);

                                // Kiểm tra xem phần giao có lớn hơn 50% độ dài của cả hai chuỗi không
                                if (overlapPercentage1 > 0.5 && overlapPercentage2 > 0.5)
                                {
                                    // So sánh giá trị discord
                                    if (discord1 > discord2)
                                    {
                                        // Thay thế chuỗi hiện tại bằng chuỗi mới
                                        mergedChains.Remove(startPos2);
                                        mergedChains[startPos1] = new Tuple<int, double>(endPos1, discord1);
                                    }
                                    else
                                    {
                                        // Loại bỏ chuỗi hiện tại vì chuỗi cũ có giá trị discord cao hơn hoặc bằng
                                        merged = true;
                                        break;
                                    }
                                }
                            }

                            // Nếu không có sự hợp nhất với bất kỳ chuỗi nào, thêm chuỗi hiện tại vào từ điển
                            if (!merged)
                            {
                                mergedChains[startPos1] = new Tuple<int, double>(endPos1, discord1);
                            }
                        }

                        // Tạo lại các mảng kết quả từ mergedChains
                        List<int> finalPositions = new List<int>();
                        List<int> finalLengths = new List<int>();
                        List<double> finalDiscordScores = new List<double>();

                        foreach (var chain in mergedChains)
                        {
                            finalPositions.Add(chain.Key);
                            finalLengths.Add(chain.Value.Item1 - chain.Key);
                            finalDiscordScores.Add(chain.Value.Item2);
                        }


                        // Chuyển các danh sách thành mảng nếu cần
                        int[] resultPositionsFinal = finalPositions.ToArray();
                        int[] resultLengthsFinal = finalLengths.ToArray();
                        //double[] resultDiscordScoresFinal = finalDiscordScores.ToArray();



                        comboBox1.Items.Clear();

                        for (int i = 0; i < resultPositionsFinal.Length; i++)
                        {
                            string itemText = $"Dài {resultLengthsFinal[i]} vị trí {resultPositionsFinal[i]}";
                            comboBox1.Items.Add(itemText);
                            //DrawChart_subs(resultPositionsFinal[i], resultLengthsFinal[i]);
                        }

                        string filePath1 = "D:/Damp_demo/execution_time.txt"; // Đường dẫn của tệp văn bản
                        try
                        {
                            using (StreamReader sr1 = new StreamReader(filePath1))
                            {
                                string executionTime = sr1.ReadLine(); // Đọc dòng đầu tiên từ tệp
                                textBox3.Text = executionTime; // Hiển thị nội dung trong TextBox
                            }
                        }
                        catch (FileNotFoundException)
                        {
                            textBox3.Text = "File not found"; // Hiển thị thông báo nếu không tìm thấy tệp
                        }
                        catch (Exception ex)
                        {
                            textBox3.Text = "Error: " + ex.Message; // Hiển thị lỗi nếu có lỗi xảy ra
                        }


                        string filePath3 = "D:/Damp_demo/position_final.txt";

                        // Mở tệp văn bản để viết
                        using (StreamWriter writer = new StreamWriter(filePath3))
                        {
                            // Ghi từng phần tử của mảng vào tệp văn bản
                            foreach (int value in resultPositionsFinal)
                            {
                                writer.WriteLine(value);
                            }
                        }

                        string filePath4 = "D:/Damp_demo/length_final.txt";

                        // Mở tệp văn bản để viết
                        using (StreamWriter writer = new StreamWriter(filePath4))
                        {
                            // Ghi từng phần tử của mảng vào tệp văn bản
                            foreach (int value in resultLengthsFinal)
                            {
                                writer.WriteLine(value);
                            }
                        }

                        DrawChart();

                        // Chuyển mảng thành danh sách
                        List<int> resultList = new List<int>(resultPositionsFinal);
                        DrawChart_discords(resultList, resultLengthsFinal);
                    }
                    //*****************************************************************************************************************
                }
            }
        }

/*        private void button4_Click(object sender, EventArgs e)
        {
            string input = button4.Text.ToString();

            MatchCollection matches = Regex.Matches(input, @"\d+");

            int firstNumber = int.Parse(matches[0].Value);

            int secondNumber = int.Parse(matches[1].Value);

            DrawChart_subs(secondNumber, firstNumber);
        }*/

        private void DrawChart_discords(List<int> positions, int[] lengths)
        {
            List<int> positionsInt = positions.ConvertAll(x => (int)x);

            // Khai báo một biến đếm cho tên của các series con
            int subSeriesCount = 1;

            for (int i = 0; i < lengths.Length; i++)
            {

                //Series subSeries = new Series($"SubSeries {subSeriesCount}");
                Series subSeries = new Series();
                subSeries.ChartType = SeriesChartType.Line;

                // Thêm dữ liệu vào series con
                for (int j = positionsInt[i]; j < positionsInt[i] + lengths[i]; j++)
                {
                    subSeries.Points.AddXY(j + 1, dataArray[j]);
                }

                // Thiết lập màu sắc cho dữ liệu của series con
                subSeries.Color = Color.Red;

                // Thêm series con vào chart
                chart1.Series.Add(subSeries);

                // Loại bỏ chú thích cho các series
                chart1.Legends.Clear();

                // Tăng biến đếm cho tên của series con
                subSeriesCount++;
            }

            // Cập nhật biểu đồ
            chart1.Update();
        }

/*        private void button5_Click(object sender, EventArgs e)
        {
            string input = button5.Text.ToString();

            MatchCollection matches = Regex.Matches(input, @"\d+");

            int firstNumber = int.Parse(matches[0].Value);

            int secondNumber = int.Parse(matches[1].Value);

            DrawChart_subs(secondNumber, firstNumber);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string input = button6.Text.ToString();

            MatchCollection matches = Regex.Matches(input, @"\d+");

            int firstNumber = int.Parse(matches[0].Value);

            int secondNumber = int.Parse(matches[1].Value);

            DrawChart_subs(secondNumber, firstNumber);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string input = button7.Text.ToString();

            MatchCollection matches = Regex.Matches(input, @"\d+");

            int firstNumber = int.Parse(matches[0].Value);

            int secondNumber = int.Parse(matches[1].Value);

            DrawChart_subs(secondNumber, firstNumber);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string input = button8.Text.ToString();

            MatchCollection matches = Regex.Matches(input, @"\d+");

            int firstNumber = int.Parse(matches[0].Value);

            int secondNumber = int.Parse(matches[1].Value);

            DrawChart_subs(secondNumber, firstNumber);
        }*/

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void scrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollPosition = scrollBar.Value;
            //DrawChart(ScrollPosition);
            UpdateScrollBar();
            UpdateChartAxisXRange();
        }

        private void UpdateChartAxisXRange()
        {
            int start = ScrollPosition;
            int end = ScrollPosition + visibleDataCount;

            chart1.ChartAreas[0].AxisX.Minimum = start;
            chart1.ChartAreas[0].AxisX.Maximum = end;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (value1 == 0)
            {
                int.TryParse(textBox6.Text, out value1);

                MLApp.MLApp matlab = new MLApp.MLApp();
                matlab.Execute(@"cd d:\Damp_demo");

                string filePath = "D:/Damp_demo/position_final.txt";

                // Mảng để lưu trữ các mảng con
                List<double[]> arrays = new List<double[]>();

                string line;
                List<double> currentArray = new List<double>();

                // Mở tệp văn bản
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Đọc từng dòng trong tệp văn bản
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Chuyển đổi giá trị sang double và thêm vào mảng con
                        double value = double.Parse(line.Trim().Replace("e", "E"));
                        currentArray.Add(value);

                        // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            arrays.Add(currentArray.ToArray());
                            currentArray.Clear();
                        }
                    }

                    // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                    if (currentArray.Count > 0)
                    {
                        arrays.Add(currentArray.ToArray());
                    }
                }


                string filePath2 = "D:/Damp_demo/length_final.txt";

                // Mảng để lưu trữ các mảng con
                List<double[]> arrays2 = new List<double[]>();

                string line2;
                List<double> currentArray2 = new List<double>();

                // Mở tệp văn bản
                using (StreamReader sr2 = new StreamReader(filePath2))
                {
                    // Đọc từng dòng trong tệp văn bản
                    while ((line2 = sr2.ReadLine()) != null)
                    {
                        // Chuyển đổi giá trị sang double và thêm vào mảng con
                        double value2 = double.Parse(line2.Trim().Replace("e", "E"));
                        currentArray2.Add(value2);

                        // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                        if (string.IsNullOrWhiteSpace(line2))
                        {
                            arrays2.Add(currentArray2.ToArray());
                            currentArray2.Clear();
                        }
                    }

                    // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                    if (currentArray2.Count > 0)
                    {
                        arrays2.Add(currentArray2.ToArray());
                    }
                }


                // Lấy chỉ số của mục được chọn trong ComboBox
                int selectedIndex = comboBox1.SelectedIndex;
                // Kiểm tra xem chỉ số có hợp lệ không
                if (selectedIndex >= 0 && selectedIndex < currentArray.Count())
                {
                    int currentLength = (int)currentArray2[selectedIndex];
                    //int value = value1 + selectedIndex; // Giá trị tương ứng với mục được chọn
                    int currentValue = (int)currentArray[selectedIndex];
                    DrawChart_subs(currentValue, currentLength); // Gọi hàm DrawChart_subs với giá trị tương ứng
                }
            }
            else
            {
                int.TryParse(textBox6.Text, out value1);

                MLApp.MLApp matlab = new MLApp.MLApp();
                matlab.Execute(@"cd d:\Merlin_demo");

                string filePath = "D:/Merlin_demo/position_final.txt";

                // Mảng để lưu trữ các mảng con
                List<double[]> arrays = new List<double[]>();

                string line;
                List<double> currentArray = new List<double>();

                // Mở tệp văn bản
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Đọc từng dòng trong tệp văn bản
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Chuyển đổi giá trị sang double và thêm vào mảng con
                        double value = double.Parse(line.Trim().Replace("e", "E"));
                        currentArray.Add(value);

                        // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            arrays.Add(currentArray.ToArray());
                            currentArray.Clear();
                        }
                    }

                    // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                    if (currentArray.Count > 0)
                    {
                        arrays.Add(currentArray.ToArray());
                    }
                }


                string filePath2 = "D:/Merlin_demo/length_final.txt";

                // Mảng để lưu trữ các mảng con
                List<double[]> arrays2 = new List<double[]>();

                string line2;
                List<double> currentArray2 = new List<double>();

                // Mở tệp văn bản
                using (StreamReader sr2 = new StreamReader(filePath2))
                {
                    // Đọc từng dòng trong tệp văn bản
                    while ((line2 = sr2.ReadLine()) != null)
                    {
                        // Chuyển đổi giá trị sang double và thêm vào mảng con
                        double value2 = double.Parse(line2.Trim().Replace("e", "E"));
                        currentArray2.Add(value2);

                        // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                        if (string.IsNullOrWhiteSpace(line2))
                        {
                            arrays2.Add(currentArray2.ToArray());
                            currentArray2.Clear();
                        }
                    }

                    // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                    if (currentArray2.Count > 0)
                    {
                        arrays2.Add(currentArray2.ToArray());
                    }
                }


                // Lấy chỉ số của mục được chọn trong ComboBox
                int selectedIndex = comboBox1.SelectedIndex;
                // Kiểm tra xem chỉ số có hợp lệ không
                if (selectedIndex >= 0 && selectedIndex < currentArray.Count())
                {
                    int currentLength = (int)currentArray2[selectedIndex];
                    //int value = value1 + selectedIndex; // Giá trị tương ứng với mục được chọn
                    int currentValue = (int)currentArray[selectedIndex];
                    DrawChart_subs(currentValue, currentLength); // Gọi hàm DrawChart_subs với giá trị tương ứng
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            value1 = 1;
            if (dataArray == null)
            {
                MessageBox.Show("Please select a data file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter Min Lenght and Max Lenght.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(fileContent))
            {
                if (int.TryParse(textBox6.Text, out int value1) && int.TryParse(textBox7.Text, out int value2))
                {

                    ExecuteMERLIN(dataArray, value1, value2, 1);


                    //*************************************************************************************************************
                    string filePath2 = "D:/Merlin_demo/discord_score2.txt";

                    // Mảng để lưu trữ các mảng con
                    List<double[]> arrays2 = new List<double[]>();
                    List<double> currentArray2 = new List<double>();

                    // Mở tệp văn bản
                    using (StreamReader sr2 = new StreamReader(filePath2))
                    {
                        string line2;
                        //List<double> currentArray2 = new List<double>();

                        // Đọc từng dòng trong tệp văn bản
                        while ((line2 = sr2.ReadLine()) != null)
                        {
                            // Chuyển đổi giá trị sang double và thêm vào mảng con
                            double value = double.Parse(line2.Trim().Replace("e", "E"));
                            currentArray2.Add(value);

                            // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                            if (string.IsNullOrWhiteSpace(line2))
                            {
                                arrays2.Add(currentArray2.ToArray());
                                currentArray2.Clear();
                            }
                        }

                        // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                        if (currentArray2.Count > 0)
                        {
                            arrays2.Add(currentArray2.ToArray());
                        }

                    }
                    //*****************************************************************************************************************


                    //*****************************************************************************************************************

                    // Đường dẫn tới tệp văn bản
                    string filePath = "D:/Merlin_demo/position2.txt";

                    // Mảng để lưu trữ các mảng con
                    List<double[]> arrays = new List<double[]>();

                    // Mở tệp văn bản
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        List<double> currentArray = new List<double>();

                        // Đọc từng dòng trong tệp văn bản
                        while ((line = sr.ReadLine()) != null)
                        {
                            // Chuyển đổi giá trị sang double và thêm vào mảng con
                            double value = double.Parse(line.Trim().Replace("e", "E"));
                            currentArray.Add(value);

                            // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                arrays.Add(currentArray.ToArray());
                                currentArray.Clear();
                            }
                        }

                        // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                        if (currentArray.Count > 0)
                        {
                            arrays.Add(currentArray.ToArray());
                        }

                        //*****************************************************************************************************************


                        //*****************************************************************************************************************
                        int length = value2 - value1 + 1;
                        int[] resultArray = new int[length];

                        // Thêm các giá trị vào mảng
                        for (int i = 0; i < length; i++)
                        {
                            resultArray[i] = value1 + i;
                        }

                        double[] positions = currentArray.ToArray();
                        double[] discord_score = currentArray2.ToArray();

                        // Sử dụng HashSet để lưu trữ các giá trị đã xuất hiện trong mảng position
                        HashSet<int> uniquePositions = new HashSet<int>();

                        // Mảng mới sau khi loại bỏ các giá trị trùng lặp
                        List<int> uniqueLengths = new List<int>();

                        List<double> uniqueDiscord_score2 = new List<double>();

                        // Kiểm tra xem cả hai mảng positions và lengths có cùng độ dài không
                        if (positions.Length == resultArray.Length)
                        {
                            for (int i = 0; i < positions.Length; i++)
                            {
                                int position2 = (int)positions[i];
                                int length2 = resultArray[i];
                                double discord_score2 = discord_score[i];

                                // Kiểm tra xem position đã tồn tại trong HashSet chưa
                                if (!uniquePositions.Contains(position2))
                                {
                                    // Nếu chưa tồn tại, thêm vào HashSet và thêm length vào mảng uniqueLengths
                                    uniquePositions.Add(position2);
                                    uniqueLengths.Add(length2);
                                    uniqueDiscord_score2.Add(discord_score2);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: positions and lengths arrays have different lengths.");
                        }

                        // Chuyển đổi uniqueLengths thành mảng nếu cần
                        int[] resultLengths = uniqueLengths.ToArray();
                        int[] resultPositions = uniquePositions.ToArray();
                        double[] resultDiscord_score = uniqueDiscord_score2.ToArray();


                        Dictionary<int, Tuple<int, double>> mergedChains = new Dictionary<int, Tuple<int, double>>();

                        for (int i = 0; i < resultPositions.Length; i++)
                        {
                            int startPos1 = resultPositions[i];
                            int endPos1 = startPos1 + resultLengths[i];
                            double discord1 = resultDiscord_score[i];

                            bool merged = false;

                            foreach (var chain in mergedChains.ToList())
                            {
                                int startPos2 = chain.Key;
                                int endPos2 = chain.Value.Item1;
                                double discord2 = chain.Value.Item2;

                                // Tính toán phần giao giữa hai chuỗi
                                int overlapStart = Math.Max(startPos1, startPos2);
                                int overlapEnd = Math.Min(endPos1, endPos2);
                                int overlapLength = Math.Max(0, overlapEnd - overlapStart);

                                double overlapPercentage1 = (double)overlapLength / resultLengths[i];
                                double overlapPercentage2 = (double)overlapLength / (endPos2 - startPos2);

                                // Kiểm tra xem phần giao có lớn hơn 50% độ dài của cả hai chuỗi không
                                if (overlapPercentage1 > 0.5 && overlapPercentage2 > 0.5)
                                {
                                    // So sánh giá trị discord
                                    if (discord1 > discord2)
                                    {
                                        // Thay thế chuỗi hiện tại bằng chuỗi mới
                                        mergedChains.Remove(startPos2);
                                        mergedChains[startPos1] = new Tuple<int, double>(endPos1, discord1);
                                    }
                                    else
                                    {
                                        // Loại bỏ chuỗi hiện tại vì chuỗi cũ có giá trị discord cao hơn hoặc bằng
                                        merged = true;
                                        break;
                                    }
                                }
                            }

                            // Nếu không có sự hợp nhất với bất kỳ chuỗi nào, thêm chuỗi hiện tại vào từ điển
                            if (!merged)
                            {
                                mergedChains[startPos1] = new Tuple<int, double>(endPos1, discord1);
                            }
                        }

                        // Tạo lại các mảng kết quả từ mergedChains
                        List<int> finalPositions = new List<int>();
                        List<int> finalLengths = new List<int>();
                        List<double> finalDiscordScores = new List<double>();

                        foreach (var chain in mergedChains)
                        {
                            finalPositions.Add(chain.Key);
                            finalLengths.Add(chain.Value.Item1 - chain.Key);
                            finalDiscordScores.Add(chain.Value.Item2);
                        }


                        // Chuyển các danh sách thành mảng nếu cần
                        int[] resultPositionsFinal = finalPositions.ToArray();
                        int[] resultLengthsFinal = finalLengths.ToArray();
                        //double[] resultDiscordScoresFinal = finalDiscordScores.ToArray();



                        comboBox1.Items.Clear();

                        for (int i = 0; i < resultPositionsFinal.Length; i++)
                        {
                            string itemText = $"Dài {resultLengthsFinal[i]} vị trí {resultPositionsFinal[i]}";
                            comboBox1.Items.Add(itemText);
                            //DrawChart_subs(resultPositionsFinal[i], resultLengthsFinal[i]);
                        }



                        string filePath1 = "D:/Merlin_demo/execution_time.txt"; // Đường dẫn của tệp văn bản
                        try
                        {
                            using (StreamReader sr1 = new StreamReader(filePath1))
                            {
                                string executionTime = sr1.ReadLine(); // Đọc dòng đầu tiên từ tệp
                                textBox2.Text = executionTime; // Hiển thị nội dung trong TextBox
                            }
                        }
                        catch (FileNotFoundException)
                        {
                            textBox2.Text = "File not found"; // Hiển thị thông báo nếu không tìm thấy tệp
                        }
                        catch (Exception ex)
                        {
                            textBox2.Text = "Error: " + ex.Message; // Hiển thị lỗi nếu có lỗi xảy ra
                        }



                        string filePath3 = "D:/Merlin_demo/position_final.txt";

                        // Mở tệp văn bản để viết
                        using (StreamWriter writer = new StreamWriter(filePath3))
                        {
                            // Ghi từng phần tử của mảng vào tệp văn bản
                            foreach (int value in resultPositionsFinal)
                            {
                                writer.WriteLine(value);
                            }
                        }

                        string filePath4 = "D:/Merlin_demo/length_final.txt";

                        // Mở tệp văn bản để viết
                        using (StreamWriter writer = new StreamWriter(filePath4))
                        {
                            // Ghi từng phần tử của mảng vào tệp văn bản
                            foreach (int value in resultLengthsFinal)
                            {
                                writer.WriteLine(value);
                            }
                        }



                        // Chuyển mảng thành danh sách
                        List<int> resultList = new List<int>(resultPositionsFinal);
                        DrawChart_discords(resultList, resultLengthsFinal);
                    }
                    //*****************************************************************************************************************
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            value1 = 1;
            if (dataArray == null)
            {
                MessageBox.Show("Please select a data file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please enter Min Lenght and Max Lenght.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Please enter NoOfTasks (Recommend '4').", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(fileContent))
            {
                if (int.TryParse(textBox6.Text, out int value1)
                    && int.TryParse(textBox7.Text, out int value2)
                    && int.TryParse(textBox8.Text, out int value3)
                   )
                {
                    ExecuteMERLIN(dataArray, value1, value2, value3);

                    //*************************************************************************************************************
                    string filePath2 = "D:/Merlin_demo/discord_score2.txt";

                    // Mảng để lưu trữ các mảng con
                    List<double[]> arrays2 = new List<double[]>();
                    List<double> currentArray2 = new List<double>();

                    // Mở tệp văn bản
                    using (StreamReader sr2 = new StreamReader(filePath2))
                    {
                        string line2;
                        //List<double> currentArray2 = new List<double>();

                        // Đọc từng dòng trong tệp văn bản
                        while ((line2 = sr2.ReadLine()) != null)
                        {
                            // Chuyển đổi giá trị sang double và thêm vào mảng con
                            double value = double.Parse(line2.Trim().Replace("e", "E"));
                            currentArray2.Add(value);

                            // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                            if (string.IsNullOrWhiteSpace(line2))
                            {
                                arrays2.Add(currentArray2.ToArray());
                                currentArray2.Clear();
                            }
                        }

                        // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                        if (currentArray2.Count > 0)
                        {
                            arrays2.Add(currentArray2.ToArray());
                        }

                    }
                    //*****************************************************************************************************************


                    //*****************************************************************************************************************

                    // Đường dẫn tới tệp văn bản
                    string filePath = "D:/Merlin_demo/position2.txt";

                    // Mảng để lưu trữ các mảng con
                    List<double[]> arrays = new List<double[]>();

                    // Mở tệp văn bản
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        List<double> currentArray = new List<double>();

                        // Đọc từng dòng trong tệp văn bản
                        while ((line = sr.ReadLine()) != null)
                        {
                            // Chuyển đổi giá trị sang double và thêm vào mảng con
                            double value = double.Parse(line.Trim().Replace("e", "E"));
                            currentArray.Add(value);

                            // Nếu gặp dòng trống, hoặc kết thúc tệp, thêm mảng con vào danh sách và bắt đầu một mảng mới
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                arrays.Add(currentArray.ToArray());
                                currentArray.Clear();
                            }
                        }

                        // Thêm mảng cuối cùng vào danh sách nếu không có dòng trống ở cuối tệp
                        if (currentArray.Count > 0)
                        {
                            arrays.Add(currentArray.ToArray());
                        }

                        //*****************************************************************************************************************


                        //*****************************************************************************************************************
                        int length = value2 - value1 + 1;
                        int[] resultArray = new int[length];

                        // Thêm các giá trị vào mảng
                        for (int i = 0; i < length; i++)
                        {
                            resultArray[i] = value1 + i;
                        }

                        double[] positions = currentArray.ToArray();
                        double[] discord_score = currentArray2.ToArray();

                        // Sử dụng HashSet để lưu trữ các giá trị đã xuất hiện trong mảng position
                        HashSet<int> uniquePositions = new HashSet<int>();

                        // Mảng mới sau khi loại bỏ các giá trị trùng lặp
                        List<int> uniqueLengths = new List<int>();

                        List<double> uniqueDiscord_score2 = new List<double>();

                        // Kiểm tra xem cả hai mảng positions và lengths có cùng độ dài không
                        if (positions.Length == resultArray.Length)
                        {
                            for (int i = 0; i < positions.Length; i++)
                            {
                                int position2 = (int)positions[i];
                                int length2 = resultArray[i];
                                double discord_score2 = discord_score[i];

                                // Kiểm tra xem position đã tồn tại trong HashSet chưa
                                if (!uniquePositions.Contains(position2))
                                {
                                    // Nếu chưa tồn tại, thêm vào HashSet và thêm length vào mảng uniqueLengths
                                    uniquePositions.Add(position2);
                                    uniqueLengths.Add(length2);
                                    uniqueDiscord_score2.Add(discord_score2);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: positions and lengths arrays have different lengths.");
                        }

                        // Chuyển đổi uniqueLengths thành mảng nếu cần
                        int[] resultLengths = uniqueLengths.ToArray();
                        int[] resultPositions = uniquePositions.ToArray();
                        double[] resultDiscord_score = uniqueDiscord_score2.ToArray();


                        Dictionary<int, Tuple<int, double>> mergedChains = new Dictionary<int, Tuple<int, double>>();

                        for (int i = 0; i < resultPositions.Length; i++)
                        {
                            int startPos1 = resultPositions[i];
                            int endPos1 = startPos1 + resultLengths[i];
                            double discord1 = resultDiscord_score[i];

                            bool merged = false;

                            foreach (var chain in mergedChains.ToList())
                            {
                                int startPos2 = chain.Key;
                                int endPos2 = chain.Value.Item1;
                                double discord2 = chain.Value.Item2;

                                // Tính toán phần giao giữa hai chuỗi
                                int overlapStart = Math.Max(startPos1, startPos2);
                                int overlapEnd = Math.Min(endPos1, endPos2);
                                int overlapLength = Math.Max(0, overlapEnd - overlapStart);

                                double overlapPercentage1 = (double)overlapLength / resultLengths[i];
                                double overlapPercentage2 = (double)overlapLength / (endPos2 - startPos2);

                                // Kiểm tra xem phần giao có lớn hơn 50% độ dài của cả hai chuỗi không
                                if (overlapPercentage1 > 0.5 && overlapPercentage2 > 0.5)
                                {
                                    // So sánh giá trị discord
                                    if (discord1 > discord2)
                                    {
                                        // Thay thế chuỗi hiện tại bằng chuỗi mới
                                        mergedChains.Remove(startPos2);
                                        mergedChains[startPos1] = new Tuple<int, double>(endPos1, discord1);
                                    }
                                    else
                                    {
                                        // Loại bỏ chuỗi hiện tại vì chuỗi cũ có giá trị discord cao hơn hoặc bằng
                                        merged = true;
                                        break;
                                    }
                                }
                            }

                            // Nếu không có sự hợp nhất với bất kỳ chuỗi nào, thêm chuỗi hiện tại vào từ điển
                            if (!merged)
                            {
                                mergedChains[startPos1] = new Tuple<int, double>(endPos1, discord1);
                            }
                        }

                        // Tạo lại các mảng kết quả từ mergedChains
                        List<int> finalPositions = new List<int>();
                        List<int> finalLengths = new List<int>();
                        List<double> finalDiscordScores = new List<double>();

                        foreach (var chain in mergedChains)
                        {
                            finalPositions.Add(chain.Key);
                            finalLengths.Add(chain.Value.Item1 - chain.Key);
                            finalDiscordScores.Add(chain.Value.Item2);
                        }


                        // Chuyển các danh sách thành mảng nếu cần
                        int[] resultPositionsFinal = finalPositions.ToArray();
                        int[] resultLengthsFinal = finalLengths.ToArray();
                        //double[] resultDiscordScoresFinal = finalDiscordScores.ToArray();



                        comboBox1.Items.Clear();

                        for (int i = 0; i < resultPositionsFinal.Length; i++)
                        {
                            string itemText = $"Dài {resultLengthsFinal[i]} vị trí {resultPositionsFinal[i]}";
                            comboBox1.Items.Add(itemText);
                            //DrawChart_subs(resultPositionsFinal[i], resultLengthsFinal[i]);
                        }

                        string filePath1 = "D:/Merlin_demo/execution_time.txt"; // Đường dẫn của tệp văn bản
                        try
                        {
                            using (StreamReader sr1 = new StreamReader(filePath1))
                            {
                                string executionTime = sr1.ReadLine(); // Đọc dòng đầu tiên từ tệp
                                textBox4.Text = executionTime; // Hiển thị nội dung trong TextBox
                            }
                        }
                        catch (FileNotFoundException)
                        {
                            textBox4.Text = "File not found"; // Hiển thị thông báo nếu không tìm thấy tệp
                        }
                        catch (Exception ex)
                        {
                            textBox4.Text = "Error: " + ex.Message; // Hiển thị lỗi nếu có lỗi xảy ra
                        }


                        string filePath3 = "D:/Merlin_demo/position_final.txt";

                        // Mở tệp văn bản để viết
                        using (StreamWriter writer = new StreamWriter(filePath3))
                        {
                            // Ghi từng phần tử của mảng vào tệp văn bản
                            foreach (int value in resultPositionsFinal)
                            {
                                writer.WriteLine(value);
                            }
                        }

                        string filePath4 = "D:/Merlin_demo/length_final.txt";

                        // Mở tệp văn bản để viết
                        using (StreamWriter writer = new StreamWriter(filePath4))
                        {
                            // Ghi từng phần tử của mảng vào tệp văn bản
                            foreach (int value in resultLengthsFinal)
                            {
                                writer.WriteLine(value);
                            }
                        }



                        // Chuyển mảng thành danh sách
                        List<int> resultList = new List<int>(resultPositionsFinal);
                        DrawChart_discords(resultList, resultLengthsFinal);
                    }
                    //*****************************************************************************************************************
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
