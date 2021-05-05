using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using LiveCharts;
using LiveCharts.Wpf;
using System.Threading;
using System.IO;
using System.Diagnostics;
namespace ArduinoApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    class DataForTask {

        public DataForTask(SerialPort sp, CancellationToken ct) {
            this.sp = sp;
            this.ct = ct;
        }
        public SerialPort sp;
        public CancellationToken ct;
    }
    public partial class MainWindow : Window
    {
        public double[] YFormatter { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ListOfPorts.ItemsSource = SerialPort.GetPortNames();
        }
        private string GetCoinData(object data) {
            CancellationToken ct = ((DataForTask)data).ct;
            SerialPort sp = ((DataForTask)data).sp;
            return sp.ReadLine();
        }
        private void Mems() { }
        private void getData_Click(object sender, RoutedEventArgs e)
        {
            SerialPort serialPort = new SerialPort();
            try
            {
                serialPort = new SerialPort(ListOfPorts.SelectedItem.ToString(), 9600, Parity.None);
                serialPort.Open();
            }

            catch (NullReferenceException NREexc)
            {
                tbtest.Text = "Считывание не удалось, выберите один из доступных портов";
                return;
            }
            catch (Exception exc)
            {

                tbtest.Text = "Считывание не удалось, аппарат не подключён";
                return;
            }
            //tbtest.Text = ListOfPorts.SelectedItem.ToString();//тут выводится инфа об доступных портах в текстбокс
            serialPort.Write(new char[] { 's' },0,1);
            System.Threading.Thread.Sleep(100);
            CancellationTokenSource ver = new CancellationTokenSource();
            Task<string> GetStringFromSerial = new Task<string>(GetCoinData,new DataForTask(serialPort, ver.Token));
            GetStringFromSerial.Start();

            string CurrentCoinData;//serialPort.ReadLine();
            
            Thread.Sleep(1000);
            if (!GetStringFromSerial.IsCompleted)
            {
                tbtest.Text = "Ошибка выполнения: невозможно подключиться к выбранному порту";
                ver.Cancel();
                return;
            }
            else {
                CurrentCoinData = GetStringFromSerial.Result;
            }
            System.Threading.Thread.Sleep(100);
            tbtest.Text = CurrentCoinData;

            string[] splittedCoinAmmountData;
            List<string> RowsToWrite = new List<string>();
            using (FileStream fs = File.Open(@"C:\Users\xbox0\source\repos\ArduinoApp\dataStorage\data1.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    tbtest.Text = CurrentCoinData;
                    string line;
                    splittedCoinAmmountData = CurrentCoinData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    splittedCoinAmmountData = splittedCoinAmmountData.Take(splittedCoinAmmountData.Length - 1).ToArray();
                    for (int a = 0; a < splittedCoinAmmountData.Length; a++)
                    {
                        line = reader.ReadLine();
                        RowsToWrite.Add(line.Replace(line, line + " " + CurrentCoinData.Split(' ')[a]));
                    }
                    line = reader.ReadLine();
                    string[] splittedString = CurrentCoinData.Split(' ');
                    //---
                    RowsToWrite.Add(line.Replace(line, line + " " + (int.Parse(splittedString[0])+ int.Parse(splittedString[1])*2+ int.Parse(splittedString[2]) * 5+int.Parse(splittedString[3]) * 10)));
                    //---
                    //line = reader.ReadLine();
                    //RowsToWrite.Add(line.Replace(line, line + " " + new DateTime().Day+"."+ new DateTime().Month));//---
                    //mb close reader
                }
                fs.Dispose();
                fs.Close();
            }
            File.WriteAllText(@"C:\Users\xbox0\source\repos\ArduinoApp\dataStorage\data1.txt", string.Empty);
            FileStream fsnew = File.Open(@"C:\Users\xbox0\source\repos\ArduinoApp\dataStorage\data1.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            //fsnew.Flush();
            StreamWriter writer = new StreamWriter(fsnew);
            StreamReader readernew = new StreamReader(fsnew);
            foreach (string a in RowsToWrite)
            {
                writer.WriteLine(a);
            }
            //writer.WriteLine(new DateTime().Day+"."+ new DateTime().Month);//---
            writer.Close();
            FileStream fsnewnew = File.Open(@"C:\Users\xbox0\source\repos\ArduinoApp\dataStorage\data1.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            byte colorValue = 50;
            StreamReader readernewnew = new StreamReader(fsnewnew);
            LineSeries ls;
            List<double> parsedData;
            fsnewnew.Position = 0;
            ((Stream)fsnewnew).Position = 0;
            readernewnew.BaseStream.Seek(0, SeekOrigin.Begin);
            readernewnew.DiscardBufferedData();
            int a1 = -1;
            string[] arr = new string[] { "1","2","5","10"};
            SeriesCollection sc = new SeriesCollection();
            List<LineSeries> serieses = new List<LineSeries>();
            while (!readernewnew.EndOfStream) { //---
                a1++;
                ls = new LineSeries();
                if (a1 == 0)
                {
                    ls.Title = "1 руб - ";
                }
                else if (a1 == 1)
                {
                    ls.Title = "2 руб - ";
                }
                else if (a1 == 2)
                {
                    ls.Title = "5 руб - ";
                }
                else if (a1 == 3)
                {
                    ls.Title = "10 руб - ";
                }
                else if(a1 == 4) {
                    ls.Title = "Сумма - ";
                }
                ls.PointGeometry = DefaultGeometries.Circle;
                ls.PointGeometrySize = 15;
                ls.StrokeThickness = 4;
                ls.StrokeDashArray = new DoubleCollection(50);
                ls.LineSmoothness = 0;
                ls.Fill = Brushes.Transparent;
                //ls.Stroke = new SolidColorBrush(Color.FromRgb(colorValue, (byte)(colorValue-25), (byte)(colorValue -50)));
                //ls.PointGeometry = null;
                colorValue += 60;
                parsedData = new List<double>();
                foreach (string a in readernewnew.ReadLine().Substring(1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)) {
                    parsedData.Add(double.Parse(a));
                }
                ls.Values = new ChartValues<double>(parsedData.ToArray());
                //serieses.Add(new LineSeries { Fill = Brushes.White, StrokeThickness = 5, Title = "Сумма - ",Values= });
                serieses.Add(ls);
            }   
            foreach (LineSeries a in serieses) {
                sc.Add(a);
            }
            this.SeriesCollection = sc;
            YFormatter = new double[] { 1.0,2.0,3.0,4.0,5.0,6.0,7.0,8.0,9.0,10.0};
            Labels = new[]{""};
            Graph.Background = new SolidColorBrush(Color.FromRgb(55,55,55));
            
                Graph.Series = sc;
            DataContext = this;
            //Graph.Series.Add(new LineSeries { Values = new ChartValues<double>(reader.ReadLine().Substring(1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Cast<double>()) });
            //writer.Close();
            readernewnew.Close();
            //fs.Close();
            serialPort.Close();
        }

        private void UpdatePorts_Click(object sender, RoutedEventArgs e)
        {

            ListOfPorts.ItemsSource = SerialPort.GetPortNames();
        }
    }
}
