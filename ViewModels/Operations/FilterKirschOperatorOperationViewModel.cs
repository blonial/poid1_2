using poid.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels.Operations
{
    public class FilterKirschOperatorOperationViewModel : OperationViewModel
    {
        #region Constructors

        public FilterKirschOperatorOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void ProcessImage(object sender)
        {
            Bitmap input = this.WorkspaceViewModel.Input;
            Bitmap output = this.WorkspaceViewModel.GetClonedInput();
            List<List<int>> red = this.GetChannelValues(input, PixelSelector.Red);
            List<List<int>> green = this.GetChannelValues(input, PixelSelector.Green);
            List<List<int>> blue = this.GetChannelValues(input, PixelSelector.Blue);
            for (int i = 0; i < output.Width; i++)
            {
                for (int j = 0; j < output.Height; j++)
                {
                    Color pixel = output.GetPixel(i, j);
                    int r = CalculateNewPixelValue(i, j, red);
                    int g = CalculateNewPixelValue(i, j, green);
                    int b = CalculateNewPixelValue(i, j, blue);
                    output.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }
            this.WorkspaceViewModel.Output = output;
        }
        private int CalculateNewPixelValue(int x, int y, List<List<int>> channel)
        {
            int max = 0;
            for (int i = 0; i < 8; i++)
            {
                int newValue = Math.Abs((5 * CalculateK1(i, x, y, channel)) - (3 * CalculateK2(i, x, y, channel)));
                max = Math.Max(max, newValue);
            }
            max = max > 255 ? 255 : max;
            return Math.Max(1, max);
        }

        private List<List<int>> GetChannelValues(Bitmap input, Func<Color, int> selector)
        {
            List<List<int>> channel = new List<List<int>>();
            for (int i = 0; i < input.Width; i++)
            {
                channel.Add(new List<int>());
                for (int j = 0; j < input.Height; j++)
                {
                    channel[i].Add(selector(input.GetPixel(i, j)));
                }
            }
            return channel;
        }

        private int CalculateK1(int i, int x, int y, List<List<int>> channel)
        {
            return CalculateA(x, y, i, channel) + CalculateA(x, y, i + 1, channel) + CalculateA(x, y, i + 2, channel);
        }

        private int CalculateK2(int i, int x, int y, List<List<int>> channel)
        {
            return CalculateA(x, y, i + 3, channel) + CalculateA(x, y, i + 4, channel) + CalculateA(x, y, i + 5, channel) + CalculateA(x, y, i + 6, channel) + CalculateA(x, y, i + 7, channel);
        }

        private int CalculateA(int x, int y, int a, List<List<int>> channel)
        {
            a = a % 8;
            try
            {
                switch (a)
                {
                    case 0:
                        return channel[x - 1][y - 1];
                    case 1:
                        return channel[x][y - 1];
                    case 2:
                        return channel[x + 1][y - 1];
                    case 3:
                        return channel[x + 1][y];
                    case 4:
                        return channel[x + 1][y + 1];
                    case 5:
                        return channel[x][y + 1];
                    case 6:
                        return channel[x - 1][y + 1];
                    case 7:
                        return channel[x - 1][y];

                }
            }
            catch (Exception)
            {
                return 0;
            }

            throw new ArgumentException("Invalid A value");
        }

        #endregion
    }
}
