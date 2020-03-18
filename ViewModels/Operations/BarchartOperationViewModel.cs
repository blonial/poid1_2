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
                if (n > 256 || n < 1)
                {
                    throw new ArgumentException("Invalid input range!");
                }

                SeriesCollection Series = new SeriesCollection();
                List<string> Labels = new List<string>();

                List<int> red = new List<int>();
                List<int> green = new List<int>();
                List<int> blue = new List<int>();

                Bitmap input = this.WorkspaceViewModel.Input;

                int range = 255 / n;
                int start = 0;

                for (int i = 0; i < n; i++)
                {
                    red.Add(0);
                    green.Add(0);
                    blue.Add(0);
                    if (i != n - 1)
                    {
                        int next = start + range;
                        Labels.Add(start + "-" + next);
                        start = next;
                    }
                    else
                    {
                        Labels.Add(start + "-" + 255);
                    }
                }

                for (int i = 0; i < input.Width; i++)
                {
                    for (int j = 0; j < input.Height; j++)
                    {
                        int red2 = input.GetPixel(i, j).R;
                        int redIndex = input.GetPixel(i, j).R / range;
                        if (redIndex == n)
                        {
                            redIndex--;
                        }
                        red[redIndex]++;

                        int green2 = input.GetPixel(i, j).G;
                        int greenIndex = input.GetPixel(i, j).G / range;
                        if (greenIndex == n)
                        {
                            greenIndex--;
                        }
                        green[greenIndex]++;

                        int blue2 = input.GetPixel(i, j).B;
                        int blueIndex = input.GetPixel(i, j).B / range;
                        if (blueIndex == n)
                        {
                            blueIndex--;
                        }
                        blue[blueIndex]++;
                    }
                }

                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        Series.Add(new ColumnSeries { Title = "R", Values = new ChartValues<int>(red), Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)) });
                        Series.Add(new ColumnSeries { Title = "G", Values = new ChartValues<int>(green), Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0)) });
                        Series.Add(new ColumnSeries { Title = "B", Values = new ChartValues<int>(blue), Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 255)) });
                        Window barchart = new BarchartView(Series, Labels);
                        barchart.Show();
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
