using poid.Commands;
using poid.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace poid.ViewModels.Operations
{
    public class BrightnessChangeOperationViewModel : OperationViewModel
    {
        #region Properties

        private string _Value;
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                NotifyPropertyChanged("Value");
            }
        }

        #endregion

        #region Constructors

        public BrightnessChangeOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void ProcessImage(object sender)
        {
            try
            {
                int value = int.Parse(this.Value);
                if (value > 255 || value < -255)
                {
                    throw new ArgumentException("Invalid input range!");
                }

                Bitmap output = this.WorkspaceViewModel.GetClonedInput();
                for (int i = 0; i < output.Width; i++)
                {
                    for (int j = 0; j < output.Height; j++)
                    {
                        Color pixel = output.GetPixel(i, j);
                        int r = CalculateNewPixelValue(pixel.R, value);
                        int g = CalculateNewPixelValue(pixel.G, value);
                        int b = CalculateNewPixelValue(pixel.B, value);
                        output.SetPixel(i, j, Color.FromArgb(r, g, b));
                    }
                }
                this.WorkspaceViewModel.Output = output;
            }
            catch (Exception)
            {
                Notify.Error("Invalid input value!\nValue must be a number between -255 and 255.");
            }
        }

        private int CalculateNewPixelValue(int pixelValue, int value)
        {
            int newValue = pixelValue + value;
            if (newValue > 255)
            {
                return 255;
            }
            else if (newValue < 0)
            {
                return 0;
            }
            else
            {
                return newValue;
            }
        }

        #endregion
    }
}
