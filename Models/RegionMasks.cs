using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models
{
    public class RegionMasks
    {
        #region Properties

        public readonly Bitmap Mask;

        public readonly List<Pixel> Pixels;

        #endregion

        #region Constructors

        private RegionMasks(ImageRegion imageRegion, int width, int height)
        {
            Bitmap mask = GetBlackBitmap(width, height);
            imageRegion.Pixels.ForEach(pixel =>
            {
                mask.SetPixel(pixel.X, pixel.Y, Color.White);
            });
            this.Mask = mask;
            this.Pixels = imageRegion.Pixels;
        }

        #endregion

        #region Methods

        public Bitmap GenerateImageWithMask(Color emptyPixelColor)
        {
            Bitmap image = GetEmptyBitmap(this.Mask.Width, this.Mask.Height, emptyPixelColor);
            this.Pixels.ForEach(pixel =>
            {
                image.SetPixel(pixel.X, pixel.Y, pixel.Color);
            });
            return image;
        }

        #endregion

        #region Static Methods

        public static List<RegionMasks> SplitAndMergeImageRegions(Bitmap image, int splitPixelsRange, int mergePixelsRange)
        {
            List<SplittedImageRegion> splittedRegions = SplittedImageRegion.SplitImageRegions(image, splitPixelsRange);
            List<ImageRegion> imageRegions = ImageRegion.MergeImageRegions(splittedRegions, mergePixelsRange, image.Width, image.Height);
            List<RegionMasks> regionMasks = new List<RegionMasks>();
            imageRegions.ForEach(regionImage => {
                regionMasks.Add(new RegionMasks(regionImage, image.Width, image.Height));
            });
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
