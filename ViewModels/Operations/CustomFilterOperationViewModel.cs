using poid.Models;
using poid.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace poid.ViewModels.Operations
{
    public class CustomFilterOperationViewModel : OperationViewModel
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

        private bool _DifferentRGBMasks = false;
        public bool DifferentRGBMasks
        {
            get
            {
                return _DifferentRGBMasks;
            }
            set
            {
                _DifferentRGBMasks = value;
                NotifyPropertyChanged("DifferentRGBMasks");
            }
        }

        #endregion

        #region Constructors

        public CustomFilterOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
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

                List<List<int>> redMask = this.GetMask(x, y, this.DifferentRGBMasks ? "Red" : "RGB");
                List<List<int>> greenMask;
                List<List<int>> blueMask;

                if (this.DifferentRGBMasks)
                {
                    greenMask = this.GetMask(x, y, "Green");
                    blueMask = this.GetMask(x, y, "Blue");
                }
                else
                {
                    greenMask = redMask;
                    blueMask = redMask;
                }

                if (redMask == null || greenMask == null || blueMask == null)
                {
                    Notify.Error("You have to give all masks!");
                    return;
                }

                Bitmap input = this.WorkspaceViewModel.Input;
                Bitmap output = this.WorkspaceViewModel.GetClonedInput();

                for (int i = 0; i < input.Width; i++)
                {
                    for (int j = 0; j < input.Height; j++)
                    {
                        output.SetPixel(i, j, Color.FromArgb(
                            this.CalculatePixelValue(input, redMask, i, j, PixelSelector.Red, x, y),
                            this.CalculatePixelValue(input, greenMask, i, j, PixelSelector.Green, x, y),
                            this.CalculatePixelValue(input, blueMask, i, j, PixelSelector.Blue, x, y)
                            ));
                    }
                }
                this.WorkspaceViewModel.Output = output;
            }
            catch (Exception)
            {
                Notify.Error("Invalid input value!\nX must be an odd int between 1 and " + this.WorkspaceViewModel.Input.Width + ".\nY must be an odd int between 1 and " + this.WorkspaceViewModel.Input.Height + ".\nMask can not be 1x1.");
            }
        }

        private int CalculatePixelValue(Bitmap input, List<List<int>> mask, int x, int y, Func<Color, int> selector, int maskX, int maskY)
        {
            int newPixelVal = 0;
            int xMaskRange = (maskX - 1) / 2;
            int yMaskRange = (maskY - 1) / 2;
            for (int i = -xMaskRange; i <= xMaskRange; i++)
            {
                for (int j = -yMaskRange; j <= yMaskRange; j++)
                {
                    if (x + i < 0 || y + j < 0 || x + i > input.Width - 1 || y + j > input.Height - 1)
                    {
                        continue;
                    }
                    newPixelVal += selector(input.GetPixel(i + x, y + j)) * mask[i + xMaskRange][j + yMaskRange];
                }
            }
            return newPixelVal > 255 ? 255 : newPixelVal < 0 ? 0 : newPixelVal;
        }

        private List<List<int>> GetMask(int x, int y, string name)
        {
            Task<List<List<int>>> task = new Task<List<List<int>>>(() =>
            {
                return Application.Current.Dispatcher.Invoke(delegate
                {
                    MaskCreatorView window = new MaskCreatorView(x, y, name);
                    window.ShowDialog();
                    return window.MaskCreatorViewModel.GetMask();
                });
            });
            task.Start();
            task.Wait();
            return task.Result;
        }

        #endregion
    }
}
