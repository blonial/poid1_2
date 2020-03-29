using LiveCharts;
using LiveCharts.Wpf;
using poid.Models;
using poid.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace poid.ViewModels.Operations
{
    public class BarchartOperationViewModel : OperationViewModel
    {
        #region Properties

        private string _N;
        public string N
        {
            get
            {
                return _N;
            }
            set
            {
                _N = value;
                NotifyPropertyChanged("N");
            }
        }

        #endregion

        #region Constructors

        public BarchartOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void ProcessImage(object sender)
        {
            try
            {
                int n = int.Parse(this.N);
                if (n > 255 || n < 1)
                {
                    throw new ArgumentException("Invalid input range!");
                }
                List<string> Labels = new List<string>();

                Bitmap input = this.WorkspaceViewModel.Input;

                double range = 255.0 / n;
                double start = 0;

                for (int i = 0; i < n; i++)
                {
                    if (i != n - 1)
                    {
                        double next = start + range;
                        Labels.Add(Convert.ToInt32(start) + "-" + Convert.ToInt32(next));
                        start = next;
                    }
                    else
                    {
                        Labels.Add(Convert.ToInt32(start) + "-" + 255);
                    }
                }

                BarchartData barchartData = BarchartData.GenerateBarcharData(n, input);

                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        SeriesCollection red = new SeriesCollection();
                        red.Add(new LineSeries { Title = "R", Values = new ChartValues<int>(barchartData.Red), Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)), PointGeometry = null });

                        SeriesCollection green = new SeriesCollection();
                        green.Add(new LineSeries { Title = "G", Values = new ChartValues<int>(barchartData.Green), Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0)), PointGeometry = null });

                        SeriesCollection blue = new SeriesCollection();
                        blue.Add(new LineSeries { Title = "B", Values = new ChartValues<int>(barchartData.Blue), Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 255)), PointGeometry = null });

                        Window redWindow = new BarchartView(red, Labels);
                        Window greenWindow = new BarchartView(green, Labels);
                        Window blueWindow = new BarchartView(blue, Labels);

                        redWindow.Show();
                        greenWindow.Show();
                        blueWindow.Show();
                    });
                });
            }
            catch (Exception)
            {
                Notify.Error("Invalid input value!\nValue must be an int between 1 and 256.");
            }
        }

        #endregion
    }
}
