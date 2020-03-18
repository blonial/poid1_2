using poid.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels.Operations
{
    public class ContrastChangeOperationViewModel : OperationViewModel
    {
        #region Properties

        private string _A;
        public string A
        {
            get
            {
                return _A;
            }
            set
            {
                _A = value;
                NotifyPropertyChanged("A");
            }
        }

        #endregion

        #region Constructors

        public ContrastChangeOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void ProcessImage(object sender)
        {
            try
            {
                double a = double.Parse(this.A);
                if (a > 127 || a < 0)
                {
                    throw new ArgumentException("Invalid input range!");
                }

                Bitmap output = this.WorkspaceViewModel.GetClonedInput();
                for (int i = 0; i < output.Width; i++)
                {
                    for (int j = 0; j < output.Height; j++)
                    {
                        Color pixel = output.GetPixel(i, j);
                        int r = CalculateNewPixelValue(pixel.R, a);
                        int g = CalculateNewPixelValue(pixel.G, a);
                        int b = CalculateNewPixelValue(pixel.B, a);
                        output.SetPixel(i, j, Color.FromArgb(r, g, b));
                    }
                }
                this.WorkspaceViewModel.Output = output;
            }
            catch (Exception)
            {
                Notify.Error("Invalid input value!\nValue must be an float between 0 and 127.");
            }
        }

        private int CalculateNewPixelValue(int pixelValue, double a)
        {
            int newValue = Convert.ToInt32((a * (pixelValue - (255 / 2))) + (255 / 2));
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
