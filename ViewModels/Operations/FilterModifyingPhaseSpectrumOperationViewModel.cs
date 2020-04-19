using AForge.Math;
using poid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels.Operations
{
    public class FilterModifyingPhaseSpectrumOperationViewModel : FourierFilterViewModel
    {
        #region Properties

        private string _K;
        public string K
        {
            get
            {
                return _K;
            }
            set
            {
                _K = value;
                NotifyPropertyChanged("K");
            }
        }

        private string _L;
        public string L
        {
            get
            {
                return _L;
            }
            set
            {
                _L = value;
                NotifyPropertyChanged("L");
            }
        }

        private int KVal;

        private int LVal;

        #endregion

        #region Constructors

        public FilterModifyingPhaseSpectrumOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void CheckInputs()
        {
            int k;
            int l;

            try
            {
                k = int.Parse(this.K);
                l = int.Parse(this.L);
            }
            catch (Exception)
            {
                throw this.GetException();
            }

            this.KVal = k;
            this.LVal = l;
        }

        protected override void ProcessChannel(Complex[,] channel)
        {
            int k = this.KVal;
            int l = this.LVal;

            int width = channel.GetLength(0);
            int height = channel.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Complex mult = Complex.Exp(new Complex(0,
                            (-i * k * 2 * Math.PI / height) +
                            (-j * l * 2 * Math.PI / width) +
                            (Math.PI * (k + l))));
                    channel[i, j] = Complex.Multiply(channel[i, j], mult);
                }
            }
        }

        private ArgumentException GetException()
        {
            return new ArgumentException("Invalid R value!\nK and L musts be an ints.");
        }

        #endregion
    }
}
