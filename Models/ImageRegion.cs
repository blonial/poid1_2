using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models
{
    public class ImageRegion
    {
        #region Properties

        public readonly List<Pixel> Pixels = new List<Pixel>();

        public int MinR { get; private set; }

        public int MaxR { get; private set; }

        public int MinG { get; private set; }

        public int MaxG { get; private set; }

        public int MinB { get; private set; }

        public int MaxB { get; private set; }

        #endregion

        #region Constructors

        private ImageRegion(SplittedImageRegion splittedImageRegion)
        {
            this.Pixels.AddRange(splittedImageRegion.GetPixels());
            this.MinR = splittedImageRegion.MinR;
            this.MaxR = splittedImageRegion.MaxR;
            this.MinG = splittedImageRegion.MinG;
            this.MaxG = splittedImageRegion.MaxG;
            this.MinB = splittedImageRegion.MinB;
            this.MaxB = splittedImageRegion.MaxB;
        }

        #endregion

        #region Static methods

        public static List<ImageRegion> MergeImageRegions(List<SplittedImageRegion> splittedImageRegions, int mergePixelsRange, int x, int y)
        {
            ImageRegion[,] map = CreateImageRegionMap(splittedImageRegions, x, y);
            MergeImageRegions(map, mergePixelsRange);
            return GetImageRegions(map);
        }

        private static void MergeImageRegionsIfAreUniform(ref ImageRegion ir1, ref ImageRegion ir2, int mergePixelsRange)
        {
            if (ir1 != ir2 && AreImageRegionsUniform(ir1, ir2, mergePixelsRange))
            {
                MergeImageRegions(ref ir1, ref ir2);
            }
        }

        private static bool AreImageRegionsUniform(ImageRegion ir1, ImageRegion ir2, int mergePixelsRange)
        {
            return ir1.MaxR - ir2.MinR <= mergePixelsRange && ir2.MaxR - ir1.MinR <= mergePixelsRange
                && ir1.MaxG - ir2.MinG <= mergePixelsRange && ir2.MaxG - ir1.MinG <= mergePixelsRange
                && ir1.MaxB - ir2.MinB <= mergePixelsRange && ir2.MaxB - ir1.MinB <= mergePixelsRange;
        }

        private static void MergeImageRegions(ref ImageRegion ir1, ref ImageRegion ir2)
        {
            ir1.Pixels.AddRange(ir2.Pixels);
            ir1.MaxR = ir1.MaxR > ir2.MaxR ? ir1.MaxR : ir2.MaxR;
            ir1.MaxG = ir1.MaxG > ir2.MaxG ? ir1.MaxG : ir2.MaxG;
            ir1.MaxB = ir1.MaxB > ir2.MaxB ? ir1.MaxB : ir2.MaxB;
            ir1.MinR = ir1.MinR < ir2.MinR ? ir1.MinR : ir2.MinR;
            ir1.MinG = ir1.MinG < ir2.MinG ? ir1.MinG : ir2.MinG;
            ir1.MinB = ir1.MinB < ir2.MinB ? ir1.MinB : ir2.MinB;
            ir2 = ir1;
        }

        private static ImageRegion[,] CreateImageRegionMap(List<SplittedImageRegion> splittedImageRegions, int width, int height)
        {
            ImageRegion[,] map = new ImageRegion[width, height];
            for (int i = 0; i < splittedImageRegions.Count; i++)
            {
                int x = splittedImageRegions[i].X;
                int y = splittedImageRegions[i].Y;
                int regionWidth = splittedImageRegions[i].Width + x;
                int regionHeight = splittedImageRegions[i].Height + y;
                ImageRegion region = new ImageRegion(splittedImageRegions[i]);
                for (int m = x; m < regionWidth; m++)
                {
                    for (int n = y; n < regionHeight; n++)
                    {
                        map[m, n] = region;
                    }
                }
            }
            return map;
        }

        private static void MergeImageRegions(ImageRegion[,] map, int mergePixelsRange)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GetNeighboringPoints(i, j, width, height).ForEach(point =>
                       {
                           MergeImageRegionsIfAreUniform(ref map[i, j], ref map[point.X, point.Y], mergePixelsRange);
                       });
                }
            }
        }

        private static List<Point> GetNeighboringPoints(int i, int j, int width, int height)
        {
            List<Point> points = new List<Point>();
            int x = i - 1 < 0 ? 0 : i;
            int y = j - 1 < 0 ? 0 : j;
            int maxX = i + 1 > width - 1 ? width - 1 : i + 1;
            int maxY = j + 1 > height - 1 ? height - 1 : j + 1;
            for (int a = x; a <= maxX; a++)
            {
                for (int b = y; b <= maxY; b++)
                {
                    if (a == i && b == j)
                    {
                        continue;
                    }
                    points.Add(new Point(a, b));
                }
            }

            return points;
        }

        private static List<ImageRegion> GetImageRegions(ImageRegion[,] map)
        {
            List<ImageRegion> imageRegions = new List<ImageRegion>();
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (!imageRegions.Contains(map[i, j]))
                    {
                        imageRegions.Add(map[i, j]);
                    }
                }
            }
            return imageRegions;
        }

        #endregion
    }
}
