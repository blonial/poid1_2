using poid.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels.Operations
{
    public class OutputProbabilityDensityOfPowerTo2_3OperationViewModel : OperationViewModel
    {
        #region Properties

        private string _RGMin;
        public string RGMin
        {
            get
            {
                return _RGMin;
            }
            set
            {
                _RGMin = value;
                NotifyPropertyChanged("RGMin");
            }
        }

        private string _RGMax;
        public string RGMax
        {
            get
            {
                return _RGMax;
            }
            set
            {
                _RGMax = value;
                NotifyPropertyChanged("RGMax");
            }
        }

        private string _GGMin;
        public string GGMin
        {
            get
            {
                return _GGMin;
            }
            set
            {
                _GGMin = value;
                NotifyPropertyChanged("GGMin");
            }
        }

        private string _GGMax;
        public string GGMax
        {
            get
            {
                return _GGMax;
            }
            set
            {
                _GGMax = value;
                NotifyPropertyChanged("GGMax");
            }
        }

        private string _BGMin;
        public string BGMin
        {
            get
            {
                return _BGMin;
            }
            set
            {
                _BGMin = value;
                NotifyPropertyChanged("BGMin");
            }
        }

        private string _BGMax;
        public string BGMax
        {
            get
            {
                return _BGMax;
            }
            set
            {
                _BGMax = value;
                NotifyPropertyChanged("BGMax");
            }
        }

        #endregion

        #region Constructors

        public OutputProbabilityDensityOfPowerTo2_3OperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void ProcessImage(object sender)
        {
            try
            {
                int rGMin = int.Parse(this.RGMin);
                int rGMax = int.Parse(this.RGMax);
                int gGMin = int.Parse(this.GGMin);
                int gGMax = int.Parse(this.GGMax);
                int bGMin = int.Parse(this.BGMin);
                int bGMax = int.Parse(this.BGMax);

                if (rGMin < 0 || rGMin > 255 || rGMax < 0 || rGMax > 255 || rGMin > rGMax || gGMin < 0 || gGMin > 255 || gGMax < 0 || gGMax > 255 || gGMin > gGMax || bGMin < 0 || bGMin > 255 || bGMax < 0 || bGMax > 255 || bGMin > bGMax)
                {
                    throw new ArgumentException("Invalid input range!");
                }

                Bitmap input = this.WorkspaceViewModel.Input;
                Bitmap output = this.WorkspaceViewModel.GetClonedInput();

                BarchartData barchartData = BarchartData.GenerateBarcharData(256, input);
                int n = input.Width * input.Height;

                for (int i = 0; i < output.Width; i++)
                {
                    for (int j = 0; j < output.Height; j++)
                    {
                        Color pixel = input.GetPixel(i, j);
                        output.SetPixel(i, j, Color.FromArgb(
                            this.CalculateNewPixelValue(rGMin, rGMax, i, j, pixel.R, barchartData.Red, n),
                            this.CalculateNewPixelValue(gGMin, gGMax, i, j, pixel.G, barchartData.Green, n),
                            this.CalculateNewPixelValue(bGMin, bGMax, i, j, pixel.B, barchartData.Blue, n)
                            ));
                    }
                }
                this.WorkspaceViewModel.Output = output;
            }
            catch (Exception)
            {
                Notify.Error("Invalid input value!\nGmin must be an int between 0 and 255.\nGmax must be an int between 0 and 255.\nGmin must be lower than Gmax.");
            }
        }

        private int CalculateNewPixelValue(int gMin, int gMax, int x, int y, int pixelValue, List<int> barcharChannel, int n)
        {
            double gMin1_3 = Math.Pow(Convert.ToDouble(gMin), 1.0 / 3.0);
            double gMax1_3 = Math.Pow(Convert.ToDouble(gMax), 1.0 / 3.0);
            double difference = gMax1_3 - gMin1_3;
            double histogramSum = this.CalculateHistogramSum(pixelValue, barcharChannel);
            double division = histogramSum / n;
            double value = Math.Pow(gMin1_3 + (difference * division), 3.0);
            int newValue = Convert.ToInt32(value);

            return newValue > 255 ? 255 : newValue < 0 ? 0 : newValue;
        }

        private double CalculateHistogramSum(int pixelValue, List<int> barcharChannel)
        {
            double sum = 0;
            for (int i = 0; i <= pixelValue; i++)
            {
                sum += barcharChannel[i];
            }
            return sum;
        }

        #endregion
    }
}
