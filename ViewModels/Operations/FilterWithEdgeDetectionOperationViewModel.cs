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
    public class FilterWithEdgeDetectionOperationViewModel : FourierFilterViewModel
    {
        #region Properties

        private string _Diameter;
        public string Diameter
        {
            get
            {
                return _Diameter;
            }
            set
            {
                _Diameter = value;
                NotifyPropertyChanged("Diameter");
            }
        }

        private string _Length;
        public string Length
        {
            get
            {
                return _Length;
            }
            set
            {
                _Length = value;
                NotifyPropertyChanged("Length");
            }
        }

        private string _Angle;
        public string Angle
        {
            get
            {
                return _Angle;
            }
            set
            {
                _Angle = value;
                NotifyPropertyChanged("Angle");
            }
        }

        private int DiameterVal;

        private int LengthVal;

        private int AngleVal;

        #endregion

        #region Constructors

        public FilterWithEdgeDetectionOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void CheckInputs()
        {
            int diameter;
            int length;
            int angle;

            int width = this.WorkspaceViewModel.Input.Width;
            int height = this.WorkspaceViewModel.Input.Height;

            try
            {
                diameter = int.Parse(this.Diameter);
                length = int.Parse(this.Length);
                angle = int.Parse(this.Angle);
            }
            catch (Exception)
            {
                throw this.GetException(width, height);
            }

            if (diameter < 0 || diameter > width || length < 2 || length > height || angle < 0 || angle > 360 || width % 2 == 1 || length % 2 == 1)
            {
                throw this.GetException(width, height);
            }

            this.DiameterVal = diameter;
            this.LengthVal = length;
            this.AngleVal = angle;
        }

        protected override void ProcessChannel(Complex[,] channel)
        {
            double angle = this.CalculateAngleInRadius(this.AngleVal);
            int radius = this.DiameterVal / 2;
            int halfLength = this.LengthVal / 2;

            int width = this.WorkspaceViewModel.Input.Width;
            int height = this.WorkspaceViewModel.Input.Height;
            int halfWidth = width / 2;
            int halfHeight = height / 2;

            // Directional coefficients
            double a1 = this.GetDirectionalCoefficient(0, halfHeight - halfLength, halfWidth, halfHeight);
            double a2 = this.GetDirectionalCoefficient(0, halfHeight + halfLength, halfWidth, halfHeight);

            // Directional coefficients rotated by angle
            a1 = this.CalculateTangesSum(a1, Math.Tan(angle));
            a2 = this.CalculateTangesSum(a2, Math.Tan(angle));

            // Constant terms
            double b1 = this.CalculateConstantTerm(halfWidth, halfHeight, a1);
            double b2 = this.CalculateConstantTerm(halfWidth, halfHeight, a2);

            Bitmap image = new Bitmap(width, height);

            // Straights filter
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double directionalCoefficient = this.GetDirectionalCoefficient(i, j, halfWidth, halfHeight);
                    if(a1 < 0 && a2 > 0)
                    {
                        if ((directionalCoefficient <= a1 && directionalCoefficient >= a2) || (directionalCoefficient >= a1 && directionalCoefficient <= a2))
                        {
                            channel[i, j] = new Complex(0, 0);
                        }
                    } else
                    {
                        if (!((directionalCoefficient <= a1 && directionalCoefficient >= a2) || (directionalCoefficient >= a1 && directionalCoefficient <= a2)))
                        {
                            channel[i, j] = new Complex(0, 0);
                        }
                    }
                }
            }

            // High pass filter
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double distance = this.GetPointDistanceFromCenter(i, j);
                    if (distance < radius)
                    {
                        channel[i, j] = new Complex(0, 0);
                    }
                }
            }
        }

        private ArgumentException GetException(int width, int height)
        {
            return new ArgumentException("Invalid values!\nDiameter must be an even int between 0 and " + width + ".\nLength must be an even int between 2 and " + height + ".\nArc must be an int between 0 and 360.");
        }

        private double CalculateAngleInRadius(int angle)
        {
            return (angle * 2 * Math.PI) / 360;
        }

        private double GetDirectionalCoefficient(int x1, int y1, int x2, int y2)
        {
            return (y1 - y2 * 1.0) / (x1 - x2 * 1.0);
        }

        private double CalculateTangesSum(double alpha, double beta)
        {
            return (alpha + beta) / (1 - (alpha * beta));
        }

        private double CalculateConstantTerm(int x1, int y1, double a)
        {
            return y1 - (a * x1);
        }

        #endregion
    }
}
