using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models
{
    public class RegionMask
    {
        #region Properties

        public readonly List<Pixel> Pixels;

        public readonly int Width;

        public readonly int Height;

        public int MaskNumber { get; }

        #endregion

        #region Constructors

        private RegionMask(ImageRegion imageRegion, int width, int height, int maskNumber)
        {
            this.Pixels = imageRegion.Pixels;
            this.Width = width;
            this.Height = height;
            this.MaskNumber = maskNumber;
        }

        #endregion

        #region Methods

        public Bitmap GenerateImageWithMask(Color emptyPixelColor)
        {
            Bitmap image = GetEmptyBitmap(this.Width, this.Height, emptyPixelColor);
            this.Pixels.ForEach(pixel =>
            {
                image.SetPixel(pixel.X, pixel.Y, pixel.Color);
            });
            return image;
        }

        public Bitmap GenerateMask()
        {
            Bitmap mask = GetBlackBitmap(this.Width, this.Height);
            this.Pixels.ForEach(pixel =>
            {
                mask.SetPixel(pixel.X, pixel.Y, Color.White);
            });
            return mask;
        }

        public override string ToString()
        {
            return "Mask " + this.MaskNumber;
        }

        #endregion

        #region Static Methods

        public static List<RegionMask> SplitAndMergeImageRegions(Bitmap image, int splitPixelsRange, int mergePixelsRange)
        {
            List<SplittedImageRegion> splittedRegions = SplittedImageRegion.SplitImageRegions(image, splitPixelsRange);
            List<ImageRegion> imageRegions = ImageRegion.MergeImageRegions(splittedRegions, mergePixelsRange, image.Width, image.Height);
            List<RegionMask> regionMasks = new List<RegionMask>();
            for(int i=0; i<imageRegions.Count; i++)
            {
                regionMasks.Add(new RegionMask(imageRegions[i], image.Width, image.Height, i + 1));
            }
            return regionMasks;
        }

        private static Bitmap GetBlackBitmap(int width, int height)
        {
            return GetEmptyBitmap(width, height, Color.Black);
        }

        private static Bitmap GetEmptyBitmap(int width, int height, Color emptyPixelColor)
        {
            Bitmap bitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bitmap.SetPixel(i, j, emptyPixelColor);
                }
            }
            return bitmap;
        }

        #endregion
    }
}
