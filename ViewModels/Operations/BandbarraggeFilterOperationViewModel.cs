using AForge.Math;
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
    public class BandbarraggeFilterOperationViewModel : FourierFilterViewModel
    {
        #region Properties

        private string _R1;
        public string R1
        {
            get
            {
                return _R1;
            }
            set
            {
                _R1 = value;
                NotifyPropertyChanged("R1");
            }
        }

        private string _R2;
        public string R2
        {
            get
            {
                return _R2;
            }
            set
            {
                _R2 = value;
                NotifyPropertyChanged("R2");
            }
        }

        private int R1Val;

        private int R2Val;

        #endregion

        #region Constructors

        public BandbarraggeFilterOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void CheckInputs()
        {
            int r1;
            int r2;

            int max = this.WorkspaceViewModel.Input.Width > this.WorkspaceViewModel.Input.Height ? this.WorkspaceViewModel.Input.Width / 2 : this.WorkspaceViewModel.Input.Height / 2;
            try
            {
                r1 = int.Parse(this.R1);
                r2 = int.Parse(this.R2);
            }
            catch (Exception)
            {
                throw this.GetException(max);
            }

            if (r1 < 0 || r2 > max || r1 > r2)
            {
                throw this.GetException(max);
            }

            this.R1Val = r1;
            this.R2Val = r2;
        }

        protected override void ProcessChannel(Complex[,] channel)
        {
            int r1 = this.R1Val;
            int r2 = this.R2Val;

            int width = channel.GetLength(0);
            int height = channel.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double distance = this.GetPointDistanceFromCenter(i, j);
                    if (distance >= r1 && distance <= r2)
                    {
                        channel[i, j] = new Complex(0, 0);
                    }
                }
            }
        }

        private ArgumentException GetException(int value)
        {
            return new ArgumentException("Invalid R value!\nR1 and R2 musts be ints between 0 and " + value + ".\nR1 must be lower than or equal R2.");
        }

        #endregion
    }
}
