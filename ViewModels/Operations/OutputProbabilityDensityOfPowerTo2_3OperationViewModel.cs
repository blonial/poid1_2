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
        #region Methods

        private string _GMin;
        public string GMin
        {
            get
            {
                return _GMin;
            }
            set
            {
                _GMin = value;
                NotifyPropertyChanged("GMin");
            }
        }

        private string _GMax;
        public string GMax
        {
            get
            {
                return _GMax;
            }
            set
            {
                _GMax = value;
                NotifyPropertyChanged("GMax");
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
                int gMin = int.Parse(this.GMin);
                int gMax = int.Parse(this.GMax);

                if (gMin < 0 || gMin > 255 || gMax < 0 || gMax > 255 || gMin > gMax)
                {
                    throw new ArgumentException("Invalid input range!");
                }

                Bitmap input = this.WorkspaceViewModel.Input;
                Bitmap output = this.WorkspaceViewModel.GetClonedInput();

                BarchartData barchartData = BarchartData.GenerateBarcharData(256, input);

                for (int i = 0; i < output.Width; i++)
                {
                    for (int j = 0; j < output.Height; j++)
                    {
                        Color pixel = input.GetPixel(i, j);
                        output.SetPixel(i, j, Color.FromArgb(
                            this.CalculateNewPixelValue(gMin, gMax, i, j, pixel.R, barchartData.Red),
                            this.CalculateNewPixelValue(gMin, gMax, i, j, pixel.G, barchartData.Green),
                            this.CalculateNewPixelValue(gMin, gMax, i, j, pixel.B, barchartData.Blue)
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

        private int CalculateNewPixelValue(int gMin, int gMax, int x, int y, int pixelValue, List<int> barcharChannel)
        {
            double gMin1_3 = Math.Pow(Convert.ToDouble(gMin), 2.0 / 3.0);
            double gMax1_3 = Math.Pow(Convert.ToDouble(gMax), 2.0 / 3.0);
            double n = Convert.ToDouble(x * y);
            double difference = gMax1_3 - gMin1_3;
            double histogramSum = this.CalculateHistogramSum(pixelValue, barcharChannel);
            double division = histogramSum / n;
            double value = Math.Pow(gMin1_3 + (difference * division), 3.0);
            int newValue = 0;
            try
            {
                newValue = Convert.ToInt32(value);
            }
            catch (Exception e)
            {

            }

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
