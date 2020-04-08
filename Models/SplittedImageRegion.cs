using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models
{
    public class SplittedImageRegion
    {
        #region Properties

        public readonly Bitmap Mask;

        public readonly bool IsRegionUniform;

        public readonly int MinR;

        public readonly int MaxR;

        public readonly int MinG;

        public readonly int MaxG;

        public readonly int MinB;

        public readonly int MaxB;

        public readonly int X;

        public readonly int Y;

        public readonly int Width;

        public readonly int Height;

        public readonly int SplitPixelsRange;

        #endregion

        #region Constructors

        private SplittedImageRegion(Bitmap mask, int x, int y, int splitPixelsRange)
        {
            this.Mask = mask;
            this.X = x;
            this.Y = y;
            this.Width = mask.Width;
            this.Height = mask.Height;
            this.SplitPixelsRange = splitPixelsRange;

            List<Color> pixels = GetColors();
            int minR = 255;
            int maxR = 0;
            int minG = 255;
            int maxG = 0;
            int minB = 255;
            int maxB = 0;

            for (int i = 0; i < pixels.Count; i++)
            {
                Color pixel = pixels[i];

                int red = pixel.R;
                int green = pixel.G;
                int blue = pixel.B;

                maxR = red > maxR ? red : maxR;
                maxG = green > maxG ? green : maxG;
                maxB = blue > maxB ? blue : maxB;

                minR = red < minR ? red : minR;
                minG = green < minG ? green : minG;
                minB = blue < minB ? blue : minB;
            }

            this.IsRegionUniform = maxR - minR <= splitPixelsRange && maxG - minG <= splitPixelsRange && maxB - maxB <= splitPixelsRange;

            this.MinR = minR;
            this.MaxR = maxR;
            this.MinG = minG;
            this.MaxG = maxG;
            this.MinB = minB;
            this.MaxB = maxB;
        }

        #endregion

        #region Methods

        private List<Color> GetColors()
        {
            if (Mask != null)
            {
                List<Color> pixels = new List<Color>();
                for (int i = 0; i < this.Mask.Width; i++)
                {
                    for (int j = 0; j < this.Mask.Height; j++)
                    {
                        pixels.Add(this.Mask.GetPixel(i, j));
                    }
                }
                return pixels;
            }
            return null;
        }

        public List<Pixel> GetPixels()
        {
            List<Pixel> pixels = new List<Pixel>();

            for (int i = 0; i < this.Mask.Width; i++)
            {
                for (int j = 0; j < this.Mask.Height; j++)
                {
                    pixels.Add(new Pixel(this.Mask.GetPixel(i, j), this.X + i, this.Y + j));
                }
            }

            return pixels;
        }

        #endregion

        #region Static methods

        public static List<SplittedImageRegion> SplitImageRegions(Bitmap image, int splitPixelsRange)
        {
            return SplitImageRegions(new SplittedImageRegion(image, 0, 0, splitPixelsRange));
        }

        private static List<SplittedImageRegion> SplitImageRegions(SplittedImageRegion splittedImageRegion)
        {
            List<SplittedImageRegion> splittedImageRegions = new List<SplittedImageRegion>();
            if (splittedImageRegion.IsRegionUniform)
            {
                splittedImageRegions.Add(splittedImageRegion);
            }
            else
            {
                Bitmap mask = splittedImageRegion.Mask;
                int height = splittedImageRegion.Height;
                int width = splittedImageRegion.Width;
                int halfHeight = height / 2;
                int halfWidth = width / 2;
                int x = splittedImageRegion.X;
                int y = splittedImageRegion.Y;
                int splitPixelsRange = splittedImageRegion.SplitPixelsRange;
                splittedImageRegions.AddRange(SplitImageRegions(new SplittedImageRegion(GetPartOfBitmap(mask, 0, 0, halfWidth, halfHeight), x, y, splitPixelsRange)));
                splittedImageRegions.AddRange(SplitImageRegions(new SplittedImageRegion(GetPartOfBitmap(mask, halfWidth, 0, halfWidth, halfHeight), x + halfWidth, y, splitPixelsRange)));
                splittedImageRegions.AddRange(SplitImageRegions(new SplittedImageRegion(GetPartOfBitmap(mask, 0, halfHeight, halfWidth, halfHeight), x, y + halfHeight, splitPixelsRange)));
                splittedImageRegions.AddRange(SplitImageRegions(new SplittedImageRegion(GetPartOfBitmap(mask, halfWidth, halfHeight, halfWidth, halfHeight), x + halfWidth, y + halfHeight, splitPixelsRange)));
            }
            return splittedImageRegions;
        }

        private static Bitmap GetPartOfBitmap(Bitmap image, int x, int y, int width, int height)
        {
            return image.Clone(new RectangleF(x, y, width, height), image.PixelFormat);
        }

        #endregion
    }
}
