using AForge.Math;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models
{
    public static class FFT
    {
        #region Methods

        public static Complex[,] FFT2(Bitmap bitmap, Func<Color, int> selector)
        {
            Complex[,] data = new Complex[bitmap.Width, bitmap.Height];

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    data[i, j] = new Complex(selector(bitmap.GetPixel(i, j)), 0);
                }
            }

            FourierTransform.FFT2(data, FourierTransform.Direction.Forward);

            return data;
        }

        public static int[,] IFFT2(Complex[,] data)
        {
            int x = data.GetLength(0);
            int y = data.GetLength(1);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    SwapComplexReIm(data[i, j]);
                }
            }

            FourierTransform.FFT2(data, FourierTransform.Direction.Forward);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    SwapComplexReIm(data[i, j]);
                    data[i, j].Re /= x;
                }
            }

            int[,] result = new int[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    result[i, j] = Convert.ToInt32(data[i, j].Re);
                }
            }
            return result;
        }

        public static void ReverseQuarters(Complex[,] data)
        {
            int X = data.GetLength(0);
            int Y = data.GetLength(1);

            int halfX = X / 2;
            int halfY = Y / 2;

            for (int i = 0; i < halfX; i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (j < halfY)
                    {
                        SwapComplex(data[i, j], data[i + halfX, j + halfY]);
                    }
                    else
                    {
                        SwapComplex(data[i, j], data[i + halfX, j - halfY]);
                    }

                }
            }
        }

        public static int[,] GetAmplitudeSpectrum(Complex[,] data)
        {
            int x = data.GetLength(0);
            int y = data.GetLength(1);

            int[,] spectrum = new int[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    spectrum[i, j] = NormalizeValue(Math.Abs(data[i, j].Re));
                }
            }

            return spectrum;
        }

        public static int[,] GetPhaseShiftSpectrum(Complex[,] data)
        {
            int x = data.GetLength(0);
            int y = data.GetLength(1);

            int[,] spectrum = new int[x, y];

            double shift = GetPhaseShift(data);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    spectrum[i, j] = NormalizeValue(data[i, j].Im + shift);
                }
            }

            return spectrum;
        }

        #endregion

        #region Helpers

        private static void SwapComplex(Complex c1, Complex c2)
        {
            Complex temp = c1;
            c1 = c2;
            c2 = temp;
        }

        private static void SwapComplexReIm(Complex c)
        {
            double temp = c.Re;
            c.Re = c.Im;
            c.Im = temp;
        }

        private static int NormalizeValue(double x)
        {
            return Convert.ToInt32(Math.Pow(Math.E, x) / (1 - Math.Pow(Math.E, x)) * 255);
        }

        private static double GetPhaseShift(Complex[,] data)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (data[i, j].Im < 0)
                    {
                        return Math.PI;
                    }
                }
            }
            return 0;
        }

        #endregion
    }
}
