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
            int width = data.GetLength(0);
            int height = data.GetLength(1);

            FourierTransform.FFT2(data, FourierTransform.Direction.Backward);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (((x + y) & 0x1) != 0)
                    {
                        data[y, x].Re *= -1;
                        data[y, x].Im *= -1;
                    }
                }
            }

            int[,] result = new int[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int value = Math.Abs(Convert.ToInt32(data[i, j].Re));
                    result[i, j] = value > 255 ? 255 : value;
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
                        Complex c1 = data[i, j];
                        Complex c2 = data[i + halfX, j + halfY];
                        data[i, j] = c2;
                        data[i + halfX, j + halfY] = c1;
                    }
                    else
                    {
                        Complex c1 = data[i, j];
                        Complex c2 = data[i + halfX, j - halfY];
                        data[i, j] = c2;
                        data[i + halfX, j - halfY] = c1;
                    }
                }
            }
        }

        public static int[,] GetPhaseShiftSpectrum(Complex[,] data)
        {
            int x = data.GetLength(0);
            int y = data.GetLength(1);

            int[,] spectrum = new int[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    double div = data[i, j].Im / data[i, j].Re;
                    double arcSin = Math.Atan(div);
                    spectrum[i, j] = Convert.ToInt32(NormalizeValue(arcSin, -Math.PI / 2, Math.PI / 2) * 255);
                }
            }

            return spectrum;
        }

        #endregion

        #region Helpers

        private static double NormalizeValue(double x, double min, double max)
        {
            return (x - min) / (max - min);
        }

        #endregion
    }
}
