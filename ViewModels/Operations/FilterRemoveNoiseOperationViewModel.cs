using poid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels.Operations
{
    public abstract class FilterRemoveNoiseOperationViewModel : OperationViewModel
    {
        #region Properties

        private string _X = "3";
        public string X
        {
            get
            {
                return _X;
            }
            set
            {
                _X = value;
                NotifyPropertyChanged("X");
            }
        }

        private string _Y = "3";
        public string Y
        {
            get
            {
                return _Y;
            }
            set
            {
                _Y = value;
                NotifyPropertyChanged("Y");
            }
        }

        #endregion

        #region Constructors

        public FilterRemoveNoiseOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void ProcessImage(object sender)
        {
            try
            {
                int x = int.Parse(this.X);
                int y = int.Parse(this.Y);
                if (x < 1 || y < 1 || x > this.WorkspaceViewModel.Input.Width || y > this.WorkspaceViewModel.Input.Height || x % 2 == 0 || y % 2 == 0 || (x == 1 && y == 1))
                {
                    throw new ArgumentException("Invalid input range!");
                }

                this.ProcessNoise(x, y);

            }
            catch (Exception)
            {
                Notify.Error("Invalid input value!\nX must be an odd int between 1 and " + this.WorkspaceViewModel.Input.Width + ".\nY must be an odd int between 1 and " + this.WorkspaceViewModel.Input.Height + ".\nMask can not be 1x1.");
            }
        }

        private void ProcessNoise(int x, int y)
        {
            Bitmap output = this.WorkspaceViewModel.GetClonedInput();
            Bitmap input = this.WorkspaceViewModel.Input;
            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++)
                {
                    Color pixel = input.GetPixel(i, j);
                    List<Color> pixels = this.GetPixelsIncludedToMask(x, y, i, j, input);
                    int red = this.CalculatePixelValue(pixels.Select(px => (int)px.R));
                    int green = this.CalculatePixelValue(pixels.Select(px => (int)px.G));
                    int blue = this.CalculatePixelValue(pixels.Select(px => (int)px.B));
                    output.SetPixel(i, j, Color.FromArgb(red, green, blue));
                }
            }
            this.WorkspaceViewModel.Output = output;
        }

        private List<Color> GetPixelsIncludedToMask(int x, int y, int i, int j, Bitmap input)
        {
            List<Color> pixels = new List<Color>();
            int radiusX = (x - 1) / 2;
            int radiusY = (y - 1) / 2;

            int startX = i - radiusX < 0 ? 0 : i - radiusX;
            int stopX = i + radiusX > input.Width - 1 ? input.Width - 1 : i + radiusY;
            int startY = j - radiusY < 0 ? 0 : j - radiusY;
            int stopY = j + radiusY > input.Height - 1 ? input.Height - 1 : j + radiusY;

            for (int k = startX; k <= stopX; k++)
            {
                for (int l = startY; l <= stopY; l++)
                {
                    pixels.Add(input.GetPixel(k, l));
                }
            }

            return pixels;
        }

        protected abstract int CalculatePixelValue(IEnumerable<int> pixels);

        #endregion
    }
}
