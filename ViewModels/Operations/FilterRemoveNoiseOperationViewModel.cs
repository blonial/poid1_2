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

        public ObservableCollection<NoiseType> NoiseTypes { get; } = new ObservableCollection<NoiseType>();

        private NoiseType _SelectedNoiseType;
        public NoiseType SelectedNoiseType
        {
            get
            {
                return _SelectedNoiseType;
            }
            set
            {
                _SelectedNoiseType = value;
                NotifyPropertyChanged("SelectedNoiseType");
            }
        }

        #endregion

        #region Enums

        public enum NoiseType
        {
            Salt,
            Pepper,
            SaltAndPepper
        }

        #endregion

        #region Constructors

        public FilterRemoveNoiseOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
            this.InitializeNoiseTypes();
        }

        #endregion

        #region Initializers

        private void InitializeNoiseTypes()
        {
            this.NoiseTypes.Add(NoiseType.Salt);
            this.NoiseTypes.Add(NoiseType.Pepper);
            this.NoiseTypes.Add(NoiseType.SaltAndPepper);

            this.SelectedNoiseType = this.NoiseTypes[0];
        }

        #endregion

        #region Methods

        protected override void ProcessImage(object sender)
        {
            int x = 0, y = 0;
            try
            {
                x = int.Parse(this.X);
                y = int.Parse(this.Y);
                if (x < 1 || y < 1 || x > this.WorkspaceViewModel.Input.Width || y > this.WorkspaceViewModel.Input.Height || x % 2 == 0 || y % 2 == 0)
                {
                    throw new ArgumentException("Invalid input range!");
                }
                Bitmap output = this.WorkspaceViewModel.GetClonedInput();
                switch (this.SelectedNoiseType)
                {
                    case NoiseType.Salt:
                        this.ProcessSaltNoise(x, y, output);
                        break;
                    case NoiseType.Pepper:
                        this.ProcessPepperNoise(x, y, output);
                        break;
                    case NoiseType.SaltAndPepper:
                        this.ProcessSaltAndPepperNoise(x, y, output);
                        break;
                }
            }
            catch (Exception)
            {
                Notify.Error("Invalid input value!\nX must be an odd int between 1 and " + this.WorkspaceViewModel.Input.Width + ".\nY must be an odd int between 1 and " + this.WorkspaceViewModel.Input.Height + ".\nMask can not be 1x1.");
            }
        }

        private void ProcessSaltNoise(int x, int y, Bitmap output)
        {
            this.ProcessNoise(x, y, 255, output);
        }

        private void ProcessPepperNoise(int x, int y, Bitmap output)
        {
            this.ProcessNoise(x, y, 0, output);
        }

        private void ProcessSaltAndPepperNoise(int x, int y, Bitmap output)
        {
            this.ProcessSaltNoise(x, y, output);
            this.ProcessPepperNoise(x, y, output);
        }

        private void ProcessNoise(int x, int y, int noisedPixelValue, Bitmap output)
        {
            Bitmap input = this.WorkspaceViewModel.Input;
            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++)
                {
                    Color pixel = input.GetPixel(i, j);
                    if (this.IsPixelNoised(pixel, noisedPixelValue))
                    {
                        List<Color> pixels = this.GetPixelsIncludedToMask(x, y, i, j, input);
                        output.SetPixel(i, j, Color.FromArgb(
                            this.CalculatePixelValue(pixels.Select(px => (int)px.R)),
                            this.CalculatePixelValue(pixels.Select(px => (int)px.G)),
                            this.CalculatePixelValue(pixels.Select(px => (int)px.B))
                            ));
                    }
                }
            }
            this.WorkspaceViewModel.Output = output;
        }

        private bool IsPixelNoised(Color pixel, int noisedPixelValue)
        {
            return pixel.R == noisedPixelValue && pixel.G == noisedPixelValue && pixel.B == noisedPixelValue;
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
                    if (k == i && l == j)
                    {
                        continue;
                    }
                    pixels.Add(input.GetPixel(k, l));
                }
            }

            return pixels;
        }

        protected abstract int CalculatePixelValue(IEnumerable<int> pixels);

        #endregion
    }
}
