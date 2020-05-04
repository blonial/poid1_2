using AForge.Math;
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
    public class HighPassFilterOperationViewModel : FourierFilterViewModel
    {
        #region Properties

        private string _R;
        public string R
        {
            get
            {
                return _R;
            }
            set
            {
                _R = value;
                NotifyPropertyChanged("R");
            }
        }

        private int RVal;

        #endregion

        #region Constructors

        public HighPassFilterOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void CheckInputs()
        {
            int value;

            int max = this.WorkspaceViewModel.Input.Width > this.WorkspaceViewModel.Input.Height ? this.WorkspaceViewModel.Input.Width / 2 : this.WorkspaceViewModel.Input.Height / 2;
            try
            {
                value = int.Parse(this.R);
            }
            catch (Exception)
            {
                throw this.GetException(max);
            }

            if (value < 0 || value > max)
            {
                throw this.GetException(max);
            }

            this.RVal = value;
        }

        protected override void ProcessChannel(Complex[,] channel)
        {
            int r = this.RVal;

            int width = channel.GetLength(0);
            int height = channel.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double distance = this.GetPointDistanceFromCenter(i, j);
                    if (distance < r)
                    {
                        channel[i, j] = new Complex(0, 0);
                    }
                }
            }
        }

        private ArgumentException GetException(int value)
        {
            return new ArgumentException("Invalid R value!\nR must be an int between 0 and " + value + ".");
        }

        #endregion
    }
}
