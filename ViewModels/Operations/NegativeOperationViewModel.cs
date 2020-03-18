using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels.Operations
{
    public class NegativeOperationViewModel : OperationViewModel
    {
        #region Constructors

        public NegativeOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void ProcessImage(object sender)
        {
            Bitmap output = this.WorkspaceViewModel.GetClonedInput();
            for (int i = 0; i < output.Width; i++)
            {
                for (int j = 0; j < output.Height; j++)
                {
                    Color pixel = output.GetPixel(i, j);
                    int r = CalculateNewPixelValue(pixel.R);
                    int g = CalculateNewPixelValue(pixel.G);
                    int b = CalculateNewPixelValue(pixel.B);
                    output.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }
            this.WorkspaceViewModel.Output = output;
        }

        private int CalculateNewPixelValue(int pixelValue)
        {
            if(pixelValue < 255/2)
            {
                return 255 - pixelValue;
            } else
            {
                return 0 + 255 - pixelValue;
            }
        }

        #endregion
    }
}
