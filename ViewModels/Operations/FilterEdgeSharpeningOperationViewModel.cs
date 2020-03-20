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
    public class FilterEdgeSharpeningOperationViewModel : OperationViewModel
    {
        #region Properties

        public enum EdgeSharpeningMask
        {
            EDGE_SHARPENING_MASK_1,
            EDGE_SHARPENING_MASK_2,
            EDGE_SHARPENING_MASK_3
        }

        public static ObservableCollection<EdgeSharpeningMask> EdgeSharpeningMasks { get; } = new ObservableCollection<EdgeSharpeningMask>() { EdgeSharpeningMask.EDGE_SHARPENING_MASK_1, EdgeSharpeningMask.EDGE_SHARPENING_MASK_2, EdgeSharpeningMask.EDGE_SHARPENING_MASK_3 };

        private EdgeSharpeningMask _SelectedEdgeSharpeningMask;
        public EdgeSharpeningMask SelectedEdgeSharpeningMask
        {
            get
            {
                return _SelectedEdgeSharpeningMask;
            }
            set
            {
                _SelectedEdgeSharpeningMask = value;
                NotifyPropertyChanged("SelectedEdgeSharpeningMask");
            }
        }

        private static List<List<int>> EdgeSharpeningMask1 { get; } = new List<List<int>>()
        {
            new List<int>() { 0, -1, 0 },
            new List<int>() { -1, 5, -1 },
            new List<int>() { 0, -1, 0 }
        };

        private static List<List<int>> EdgeSharpeningMask2 { get; } = new List<List<int>>()
        {
            new List<int>() { -1, -1, -1 },
            new List<int>() { -1, 9, -1 },
            new List<int>() { -1, -1, -1 }
        };

        private static List<List<int>> EdgeSharpeningMask3 { get; } = new List<List<int>>()
        {
            new List<int>() { 1, -2, 1 },
            new List<int>() { -2, 5, -2 },
            new List<int>() { 1, -2, 1 }
        };

        #endregion

        #region Constructors

        public FilterEdgeSharpeningOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
            this.InitializeEdgeSharpeningMasks();
        }

        #endregion

        #region Initializers

        private void InitializeEdgeSharpeningMasks()
        {
            this.SelectedEdgeSharpeningMask = EdgeSharpeningMasks[0];
        }

        #endregion

        #region Methods

        protected override void ProcessImage(object sender)
        {
            List<List<int>> mask = EdgeSharpeningMask1;
            switch (this.SelectedEdgeSharpeningMask)
            {
                case EdgeSharpeningMask.EDGE_SHARPENING_MASK_1:
                    mask = EdgeSharpeningMask1;
                    break;
                case EdgeSharpeningMask.EDGE_SHARPENING_MASK_2:
                    mask = EdgeSharpeningMask2;
                    break;
                case EdgeSharpeningMask.EDGE_SHARPENING_MASK_3:
                    mask = EdgeSharpeningMask3;
                    break;
            }
            Bitmap input = this.WorkspaceViewModel.Input;
            Bitmap output = this.WorkspaceViewModel.GetClonedInput();
            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++)
                {
                    output.SetPixel(i, j, Color.FromArgb(
                        this.CalculatePixelValue(input, mask, i, j, PixelSelector.Red),
                        this.CalculatePixelValue(input, mask, i, j, PixelSelector.Green),
                        this.CalculatePixelValue(input, mask, i, j, PixelSelector.Blue)
                        ));
                }
            }
            this.WorkspaceViewModel.Output = output;
        }

        private int CalculatePixelValue(Bitmap input, List<List<int>> mask, int x, int y, Func<Color, int> selector)
        {
            int newPixelVal = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i < 0 || y + j < 0 || x + i > input.Width - 1 || y + j > input.Height - 1)
                    {
                        continue;
                    }
                    newPixelVal += selector(input.GetPixel(i + x, y + j)) * mask[i + 1][j + 1];
                }
            }
            return newPixelVal > 255 ? 255 : newPixelVal < 0 ? 0 : newPixelVal;
        }

        #endregion
    }
}
