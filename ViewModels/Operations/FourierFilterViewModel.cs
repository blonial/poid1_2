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
    public abstract class FourierFilterViewModel : OperationViewModel
    {
        #region Properties

        private int Width;

        private int Height;

        #endregion

        #region Constructors

        public FourierFilterViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void ProcessImage(object sender)
        {
            try
            {
                this.CheckInputs();

                Bitmap input = this.WorkspaceViewModel.Input;
                this.Width = input.Width / 2;
                this.Height = input.Height / 2;

                Complex[,] redFourier = FFT.FFT2(input, PixelSelector.Red);
                Complex[,] greenFourier = FFT.FFT2(input, PixelSelector.Green);
                Complex[,] blueFourier = FFT.FFT2(input, PixelSelector.Blue);

                Complex redConstElement = redFourier[0, 0].Clone();
                Complex greenConstElement = greenFourier[0, 0].Clone();
                Complex blueCosntElement = blueFourier[0, 0].Clone();

                FFT.ReverseQuarters(redFourier);
                FFT.ReverseQuarters(greenFourier);
                FFT.ReverseQuarters(blueFourier);

                int[,] redPhase = FFT.GetPhaseShiftSpectrum(redFourier);
                int[,] greenPhase = FFT.GetPhaseShiftSpectrum(greenFourier);
                int[,] bluePhase = FFT.GetPhaseShiftSpectrum(blueFourier);

                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        ImageView imageView = new ImageView("Phase shift spectrum", this.GetImageFromChannels(redPhase, greenPhase, bluePhase));
                        imageView.Show();
                    });
                });

                int[,] redAmplitude = FFT.GetAmplitudeSpectrum(redFourier);
                int[,] greenAmplitude = FFT.GetAmplitudeSpectrum(greenFourier);
                int[,] blueAmplitude = FFT.GetAmplitudeSpectrum(blueFourier);

                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        ImageView imageView = new ImageView("Amplitude spectrum", this.GetImageFromChannels(redAmplitude, greenAmplitude, blueAmplitude));
                        imageView.Show();
                    });
                });

                this.ProcessChannel(redFourier);
                this.ProcessChannel(greenFourier);
                this.ProcessChannel(blueFourier);

                this.DisplayMask(redFourier);

                FFT.ReverseQuarters(redFourier);
                FFT.ReverseQuarters(greenFourier);
                FFT.ReverseQuarters(blueFourier);

                redFourier[0, 0] = redConstElement;
                greenFourier[0, 0] = greenConstElement;
                blueFourier[0, 0] = blueCosntElement;

                int[,] red = FFT.IFFT2(redFourier);
                int[,] blue = FFT.IFFT2(blueFourier);
                int[,] green = FFT.IFFT2(greenFourier);

                Bitmap output = this.GetImageFromChannels(red, green, blue);

                for (int i = 0; i < input.Width; i++)
                {
                    for (int j = 0; j < input.Height; j++)
                    {
                        output.SetPixel(i, j, Color.FromArgb(red[i, j], green[i, j], blue[i, j]));
                    }
                }

                this.WorkspaceViewModel.Output = output;
            }
            catch (Exception e)
            {
                Notify.Error(e.Message);
            }
        }

        protected abstract void CheckInputs();

        protected abstract void ProcessChannel(Complex[,] channel);

        protected double GetPointDistanceFromCenter(int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - this.Width, 2) + Math.Pow(y2 - this.Height, 2));
        }

        private Bitmap GetImageFromChannels(int[,] red, int[,] green, int[,] blue)
        {
            int width = red.GetLength(0);
            int height = red.GetLength(1);

            Bitmap output = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    output.SetPixel(i, j, Color.FromArgb(red[i, j], green[i, j], blue[i, j]));
                }
            }

            return output;
        }

        private void DisplayMask(Complex[,] channel)
        {
            int width = channel.GetLength(0);
            int height = channel.GetLength(1);

            Bitmap mask = RegionMask.GetEmptyBitmap(width, height, Color.White);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (channel[i, j].Re == 0 && channel[i, j].Im == 0)
                    {
                        mask.SetPixel(i, j, Color.Black);
                    }
                }
            }

            Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    ImageView imageView = new ImageView("Mask", mask);
                    imageView.Show();
                });
            });
        }

        #endregion
    }
}
