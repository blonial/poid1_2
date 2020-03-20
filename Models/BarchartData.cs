using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models
{
    public class BarchartData
    {
        #region Properties

        public List<int> Red { get; }

        public List<int> Green { get; }

        public List<int> Blue { get; }

        #endregion

        #region Constructors

        private BarchartData(List<int> red, List<int> green, List<int> blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        #endregion

        #region Methods

        public static BarchartData GenerateBarcharData(int n, Bitmap input)
        {
            double range = 255.0 / n;

            List<int> red = new List<int>();
            List<int> green = new List<int>();
            List<int> blue = new List<int>();

            for (int i = 0; i < n; i++)
            {
                red.Add(0);
                green.Add(0);
                blue.Add(0);
            }

            for (int i = 0; i < input.Width; i++)
            {

                for (int j = 0; j < input.Height; j++)
                {
                    int red2 = input.GetPixel(i, j).R;
                    int redIndex = Convert.ToInt32(input.GetPixel(i, j).R / range);
                    if (redIndex == n)
                    {
                        redIndex--;
                    }
                    red[redIndex]++;

                    int green2 = input.GetPixel(i, j).G;
                    int greenIndex = Convert.ToInt32(input.GetPixel(i, j).G / range);
                    if (greenIndex == n)
                    {
                        greenIndex--;
                    }
                    green[greenIndex]++;

                    int blue2 = input.GetPixel(i, j).B;
                    int blueIndex = Convert.ToInt32(input.GetPixel(i, j).B / range);
                    if (blueIndex == n)
                    {
                        blueIndex--;
                    }
                    blue[blueIndex]++;
                }
            }

            return new BarchartData(red, green, blue);
        }

        #endregion
    }
}
